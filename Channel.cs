using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace zgkk_sp_simulator
{
    public enum Quantity { PressureMPa, PressurePa, Hydraulic, Lift, Sound, Temp }

    // One model channel: which opcode answers it and how the physical value maps to the 2-byte reading
    [DataContract]
    public class Channel
    {
        [DataMember] public Quantity Q;
        [DataMember] public bool Enabled;
        [DataMember] public int Opcode;
        [DataMember] public double From, To;          // physical range
        [DataMember] public int RawMin = 0, RawMax = 32767;
        // zgkk reads an inverted sensor as RawMax + RawMin - raw (Detail_Sensor 翻转)
        [DataMember] public bool Invert;
        public int Offset;                            // raw counts added by the old +/- buttons; not persisted

        public string Name
        {
            get
            {
                switch (Q)
                {
                    case Quantity.PressureMPa: return "校验压力 MPa";
                    case Quantity.PressurePa: return "校验压力 Pa";
                    case Quantity.Hydraulic: return "液压压力 MPa";
                    case Quantity.Lift: return "位移 mm";
                    case Quantity.Sound: return "声音 dB";
                    default: return "温度 ℃";
                }
            }
        }

        // The pressure channel is always on; the hydraulic one exists only on the offline bench
        public bool Optional => Q == Quantity.Lift || Q == Quantity.Sound || Q == Quantity.Temp;

        public bool ShownOn(Bench b)
        {
            switch (Q)
            {
                case Quantity.PressureMPa: return b != Bench.Breath;
                case Quantity.PressurePa: return b == Bench.Breath;
                case Quantity.Hydraulic: return b == Bench.SafetyOffline;
                default: return true;
            }
        }

        public bool ActiveOn(Bench b) { return ShownOn(b) && (!Optional || Enabled); }

        public double Value(ValveModel m)
        {
            switch (Q)
            {
                case Quantity.PressureMPa:
                case Quantity.PressurePa: return m.Pressure;
                case Quantity.Hydraulic: return m.HydraulicMPa;
                case Quantity.Lift: return m.Lift;
                case Quantity.Sound: return m.Sound;
                default: return m.Temp;
            }
        }

        // Inverse of zgkk's linear conversion, see zgkk docs/数采板通信协议.md「读数换算成物理量」
        public int ToRaw(double v)
        {
            double f = To == From ? 0 : (v - From) / (To - From);
            f = Math.Max(0, Math.Min(1, f));
            int raw = (int)Math.Round(RawMin + f * (RawMax - RawMin));
            if (Invert) raw = RawMax + RawMin - raw;
            return Math.Max(0, Math.Min(0xFFFF, raw + Offset));
        }

        // Defaults follow zgkk Utilitys.BSend_* where they exist; sound/temp/hydraulic opcodes are set per bench
        public static List<Channel> Defaults()
        {
            return new List<Channel>
            {
                new Channel { Q = Quantity.PressureMPa, Opcode = 0x02, From = 0, To = 10 },
                new Channel { Q = Quantity.PressurePa, Opcode = 0x02, From = -10000, To = 10000 },
                new Channel { Q = Quantity.Hydraulic, Opcode = 0x07, From = 0, To = 40 },
                new Channel { Q = Quantity.Lift, Enabled = true, Opcode = 0x04, From = 0, To = 10 },
                new Channel { Q = Quantity.Sound, Opcode = 0x05, From = 30, To = 130 },
                new Channel { Q = Quantity.Temp, Opcode = 0x06, From = -50, To = 150 },
            };
        }
    }
}
