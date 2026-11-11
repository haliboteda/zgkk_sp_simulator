using System;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace zgkk_sp_simulator
{
    // Which test bench is simulated; it decides which relays the model follows
    public enum Bench { Breath, SafetyOnline, SafetyOffline }

    public enum Drive { Hold, Supply, Vent }

    // Every value the user can tune; persisted in settings.json
    [DataContract]
    public class ValveParams
    {
        [DataMember, Category("1 安全阀"), DisplayName("整定压力 MPa")] public double SetMPa { get; set; } = 1.0;
        [DataMember, Category("1 安全阀"), DisplayName("回座压力 MPa")] public double ReseatMPa { get; set; } = 0.9;
        [DataMember, Category("1 安全阀"), DisplayName("升压速率 MPa/s")] public double RiseMPaPerS { get; set; } = 0.05;
        [DataMember, Category("1 安全阀"), DisplayName("泄压速率 MPa/s")] public double VentMPaPerS { get; set; } = 0.2;
        [DataMember, Category("1 安全阀"), DisplayName("保压泄漏速率 MPa/s")] public double LeakMPaPerS { get; set; } = 0;

        [DataMember, Category("2 呼吸阀"), DisplayName("正压开启 Pa")] public double PosOpenPa { get; set; } = 1500;
        [DataMember, Category("2 呼吸阀"), DisplayName("正压回座 Pa")] public double PosReseatPa { get; set; } = 1200;
        [DataMember, Category("2 呼吸阀"), DisplayName("负压开启 Pa")] public double NegOpenPa { get; set; } = -500;
        [DataMember, Category("2 呼吸阀"), DisplayName("负压回座 Pa")] public double NegReseatPa { get; set; } = -350;
        [DataMember, Category("2 呼吸阀"), DisplayName("升压速率 Pa/s")] public double RisePaPerS { get; set; } = 100;
        [DataMember, Category("2 呼吸阀"), DisplayName("泄压速率 Pa/s")] public double VentPaPerS { get; set; } = 400;
        [DataMember, Category("2 呼吸阀"), DisplayName("保压泄漏速率 Pa/s")] public double LeakPaPerS { get; set; } = 0;

        [DataMember, Category("3 开启时"), DisplayName("开启高度 mm")] public double LiftMm { get; set; } = 4;
        [DataMember, Category("3 开启时"), DisplayName("声音底噪 dB")] public double SoundBaseDb { get; set; } = 45;
        [DataMember, Category("3 开启时"), DisplayName("声音增量 dB")] public double SoundDeltaDb { get; set; } = 40;
        [DataMember, Category("3 开启时"), DisplayName("基础温度 ℃")] public double TempBaseC { get; set; } = 25;
        [DataMember, Category("3 开启时"), DisplayName("温度变化量 ℃（负数=降温）")] public double TempDeltaC { get; set; } = 35;

        [DataMember, Category("4 离线台液压"), DisplayName("液压升压速率 MPa/s")] public double HydRiseMPaPerS { get; set; } = 2;
        [DataMember, Category("4 离线台液压"), DisplayName("最高液压压力 MPa")] public double HydMaxMPa { get; set; } = 16;
        [DataMember, Category("4 离线台液压"), DisplayName("人体安全压力 MPa")] public double SafeMPa { get; set; } = 0.01;

        // 1-based DY numbers, same defaults as zgkk Sensor_relay.Config
        [DataMember, Category("5 继电器分工"), DisplayName("校验给压 DY")] public int SupplyDy { get; set; } = 1;
        [DataMember, Category("5 继电器分工"), DisplayName("校验泄压 DY")] public int VentDy { get; set; } = 2;
        [DataMember, Category("5 继电器分工"), DisplayName("液压输出（夹紧）DY")] public int ClampDy { get; set; } = 3;
        [DataMember, Category("5 继电器分工"), DisplayName("液压回油 DY")] public int ReturnDy { get; set; } = 4;

        public static bool On(int relayBits, int dy) { return dy >= 1 && dy <= 8 && (relayBits & (1 << (dy - 1))) != 0; }
    }

    // Virtual valve: one state that every channel reads from, stepped on a timer
    public class ValveModel
    {
        public ValveParams P = new ValveParams();
        public Bench Bench = Bench.SafetyOnline;
        public bool BreathNegative;   // breath valve: supply drives pressure below zero

        // Test pressure: MPa on safety benches, Pa on the breath bench
        public double Pressure { get; private set; }
        public double HydraulicMPa { get; private set; }
        public bool Open { get; private set; }

        public event Action<string> Event;

        public void Reset()
        {
            Pressure = 0;
            Open = false;
        }

        public double Lift => Open ? P.LiftMm : 0;
        public double Sound => P.SoundBaseDb + (Open ? P.SoundDeltaDb : 0);
        public double Temp => P.TempBaseC + (Open ? P.TempDeltaC : 0);

        public Drive DriveFromRelays(int bits)
        {
            if (ValveParams.On(bits, P.VentDy)) return Drive.Vent;
            if (ValveParams.On(bits, P.SupplyDy)) return Drive.Supply;
            return Drive.Hold;
        }

        public void Step(double dt, Drive drive, int relayBits)
        {
            if (Bench == Bench.SafetyOffline) StepHydraulic(dt, relayBits);
            if (Bench == Bench.Breath) StepBreath(dt, drive);
            else StepSafety(dt, drive);
        }

        // Clamp output raises the hydraulic pressure up to its maximum, return drops it to 0, both off holds it
        void StepHydraulic(double dt, int bits)
        {
            if (ValveParams.On(bits, P.ReturnDy)) HydraulicMPa = 0;
            else if (ValveParams.On(bits, P.ClampDy)) HydraulicMPa = Math.Min(P.HydMaxMPa, HydraulicMPa + P.HydRiseMPaPerS * dt);
        }

        void StepSafety(double dt, Drive drive)
        {
            double p = Pressure;
            if (drive == Drive.Supply) p += P.RiseMPaPerS * dt;
            else if (drive == Drive.Vent) p -= P.VentMPaPerS * dt;
            else p -= P.LeakMPaPerS * dt;
            if (p < 0) p = 0;

            // Pops at the set pressure and falls back to the reseat pressure; reseats once below it
            if (!Open && p >= P.SetMPa) { Open = true; Raise($"阀门起跳 @ {P.SetMPa:0.###} MPa"); p = P.ReseatMPa; }
            else if (Open && p > P.ReseatMPa) p = P.ReseatMPa;
            else if (Open && p < P.ReseatMPa) { Open = false; Raise($"阀门回座 @ {p:0.###} MPa"); }
            Pressure = p;
        }

        // Same rules as the safety valve, mirrored below zero for the negative-pressure test
        void StepBreath(double dt, Drive drive)
        {
            double sign = BreathNegative ? -1 : 1;
            double open = BreathNegative ? -P.NegOpenPa : P.PosOpenPa;
            double reseat = BreathNegative ? -P.NegReseatPa : P.PosReseatPa;
            double m = Pressure * sign;   // magnitude in the active direction
            if (drive == Drive.Supply) m += P.RisePaPerS * dt;
            else if (drive == Drive.Vent) m -= P.VentPaPerS * dt;
            else m -= P.LeakPaPerS * dt;
            if (m < 0) m = 0;

            if (!Open && m >= open) { Open = true; Raise($"阀门开启 @ {open * sign:0} Pa"); m = reseat; }
            else if (Open && m > reseat) m = reseat;
            else if (Open && m < reseat) { Open = false; Raise($"阀门回座 @ {m * sign:0} Pa"); }
            Pressure = m * sign;
        }

        void Raise(string msg) { Event?.Invoke(msg); }

        // A return write acts at once, so a pulse shorter than one step is not missed
        public void ApplyRelayWrite(int bits)
        {
            if (Bench == Bench.SafetyOffline && ValveParams.On(bits, P.ReturnDy)) HydraulicMPa = 0;
        }

        // Offline-bench safety rules from zgkk docs/ai/pages/ZGKK系统流程图.md 3.3.2; returns a warning or null
        public string CheckRelayWrite(int oldBits, int newBits)
        {
            if (Bench != Bench.SafetyOffline) return null;
            bool ret = ValveParams.On(newBits, P.ReturnDy), clamp = ValveParams.On(newBits, P.ClampDy);
            if (ret && clamp) return "液压输出和液压回油同时打开";
            if (ret && !ValveParams.On(oldBits, P.ReturnDy) && Pressure > P.SafeMPa)
                return $"校验压力 {Pressure:0.###} MPa 高于人体安全压力 {P.SafeMPa} MPa 时打开了液压回油（阀门带压放开）";
            return null;
        }
    }
}
