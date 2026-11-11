using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace zgkk_sp_simulator
{
    public enum Quantity { PressureMPa, PressurePa, Hydraulic, Lift, Sound, Temp }

    // How one model quantity maps to the 2-byte reading; which opcode answers it is the row's 来源 choice
    [DataContract]
    public class Channel
    {
        [DataMember] public Quantity Q;
        [DataMember] public double From, To;          // physical range
        [DataMember] public int RawMin = 0, RawMax = 32767;
        // zgkk reads an inverted sensor as RawMax + RawMin - raw (Detail_Sensor 翻转)
        [DataMember] public bool Invert;

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

        public string Format => Q == Quantity.PressureMPa ? "0.000" : Q == Quantity.PressurePa ? "0" : Q == Quantity.Hydraulic ? "0.00" : "0.0";

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
            return Invert ? RawMax + RawMin - raw : raw;
        }

        public static List<Channel> Defaults()
        {
            return new List<Channel>
            {
                new Channel { Q = Quantity.PressureMPa, From = 0, To = 10 },
                new Channel { Q = Quantity.PressurePa, From = -10000, To = 10000 },
                new Channel { Q = Quantity.Hydraulic, From = 0, To = 40 },
                new Channel { Q = Quantity.Lift, From = 0, To = 10 },
                new Channel { Q = Quantity.Sound, From = 30, To = 130 },
                new Channel { Q = Quantity.Temp, From = -50, To = 150 },
            };
        }
    }
}
