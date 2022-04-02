using Ceras;
using RTCV.CorruptCore;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using RTCV.NetCore;

namespace MiniEngines.Engines
{
    public enum EngineMethod
    {
        FloatBleed,
        Vec2Corrupt,
        Vec3Corrupt,
        Vec4Corrupt,
    }
    [Serializable]
    [Ceras.MemberConfig(TargetMember.All)]
    public class DynaVectorEngine : ICorruptionEngine
    {
        public static int BleedForwards
        {
            get { return EngineSpec.Get<int>(nameof(BleedForwards)); }
            set { EngineSpec.Set(nameof(BleedForwards), value); }
        }

        public static int BleedBackwards
        {
            get { return EngineSpec.Get<int>(nameof(BleedBackwards)); }
            set { EngineSpec.Set(nameof(BleedBackwards), value); }
        }

        public static string LimiterListHash
        {
            get { return EngineSpec.Get<string>(nameof(LimiterListHash)); }
            set { EngineSpec.Set(nameof(LimiterListHash), value); }
        }

        public static string ValueListHash
        {
            get { return EngineSpec.Get<string>(nameof(ValueListHash)); }
            set { EngineSpec.Set(nameof(ValueListHash), value); }
        }

        public static bool IncludeNeighboringZeros
        {
            get { return EngineSpec.Get<bool>(nameof(IncludeNeighboringZeros)); }
            set { EngineSpec.Set(nameof(IncludeNeighboringZeros), value); }
        }

        [Exclude]
        static Random rand = new Random();

        bool ICorruptionEngine.SupportsCustomPrecision => false;
        bool ICorruptionEngine.SupportsAutoCorrupt => true;
        bool ICorruptionEngine.SupportsGeneralParameters => true;
        bool ICorruptionEngine.SupportsMemoryDomains => true;


        [Exclude]
        Form ICorruptionEngine.Control { get { return control; } }

        [Exclude]
        Form control { get; set; } = null;
        public DynaVectorEngine()
        {
        }
        public DynaVectorEngine(Form form)
        {
            control = form;
        }

        public static EngineMethod Method { get; set; }

        public static BlastUnit BleedFloat(MemoryInterface mi, long address, int precision, string domain)
        {
            if ((address - (BleedBackwards * precision) < 0) || (address + (BleedForwards * precision) >= mi.Size))
            {
                return null;
            }
            byte[][] prev = GatherFloats(mi, address, precision, BleedBackwards, false);
            byte[][] next = GatherFloats(mi, address, precision, BleedForwards, true);
            bool usenext = rand.Next(2) == 0;
            if (next.Length == 0 && usenext) usenext = false;
            if (prev.Length == 0 && !usenext) usenext = true;
            if (usenext)
            {
                if (next.Length > 0)
                {
                    byte[] val = (next[rand.Next(next.Length)]);
                    return new BlastUnit(val, domain, address, precision, mi.BigEndian);
                }
            }
            else
            {
                if (prev.Length > 0)
                {
                    byte[] val = (prev[rand.Next(prev.Length)]);
                    return new BlastUnit(val, domain, address, precision, mi.BigEndian);
                }
            }
            return null;
        }

        private static byte[][] GatherFloats(MemoryInterface mi, long address, int precision, int bleedAmt, bool forwards)
        {
            List<byte[]> result = new List<byte[]>();
            for (int i = 0; i < bleedAmt; i++)
            {
                address += forwards ? precision : -precision;
                var bytes = Filtering.LimiterPeekAndGetBytes(address, address + precision, DynaVectorEngine.LimiterListHash, mi);
                if (bytes == null)
                    continue;
                result.Add(bytes);
            }
            return result.ToArray();
        }

        private static BlastUnit VecCorrupt(MemoryInterface mi, long address, int precision, int vecLength, string domain)
        {
            if (address < 0 || address + (vecLength * precision) >= mi.Size)
            {
                return null;
            }
            List<(byte[], long)> matchBytes = new List<(byte[], long)>();
            address = address - (address % (precision * vecLength)) + RtcCore.Alignment;
            for (int i = 0; i < vecLength; i++)
            {
                var bytes = Filtering.LimiterPeekAndGetBytes(address + (precision * i), address + (precision * i) + precision, DynaVectorEngine.LimiterListHash, mi);
                if (bytes != null)
                    matchBytes.Add((bytes, address + (precision * i)));
            }
            if (matchBytes.Count == vecLength)
            {
                return new BlastUnit(Filtering.GetRandomConstant(DynaVectorEngine.ValueListHash, precision, matchBytes[rand.Next(vecLength)].Item1), domain, matchBytes[rand.Next(vecLength)].Item2, precision,
                    mi.BigEndian, 0, 1, null, true, false, true);
            }
            return null;
        }

        public static BlastUnit GenerateUnit(string domain, long address, int alignment)
        {
            if (domain == null)
            {
                return null;
            }

            int precision;

            //Behavior: Will use the selected limiter list's precision by default
            //And if the precision was unlocked, use what was set in the precision box

            if (Filtering.Hash2LimiterDico.TryGetValue(DynaVectorEngine.LimiterListHash, out IListFilter list))
            {
                precision = list.GetPrecision();
            }
            else
            {
                return null;
            }

            long safeAddress = address - (address % precision) + alignment; //32-bit trunk

            MemoryInterface mi = MemoryDomains.GetInterface(domain);
            if (mi == null)
            {
                return null;
            }

            if (safeAddress >= mi.Size - precision)
            {
                safeAddress = mi.Size - (2 * precision) + alignment; //If we're out of range, hit the last aligned address
            }

            switch (Method)
            {
                default:
                    break;
                case EngineMethod.Vec2Corrupt:
                    return VecCorrupt(mi, safeAddress, precision, 2, domain);
                case EngineMethod.Vec3Corrupt:
                    return VecCorrupt(mi, safeAddress, precision, 3, domain);
                case EngineMethod.Vec4Corrupt:
                    return VecCorrupt(mi, safeAddress, precision, 4, domain);
            }
            //Enforce the safeaddress at generation
            var matchBytes = Filtering.LimiterPeekAndGetBytes(safeAddress, safeAddress + precision, DynaVectorEngine.LimiterListHash, mi);
            if (matchBytes != null)
            {
                switch (Method)
                {
                    default:
                        break;
                    case EngineMethod.FloatBleed:
                        return BleedFloat(mi, safeAddress, precision, domain);
                }
            }

            return null;
        }
        public BlastLayer GetBlastLayer(long intensity)
        {
            try
            {
                var domains = (string[])AllSpec.UISpec[UISPEC.SELECTEDDOMAINS];
                if (domains == null || domains.Length == 0)
                {
                    MessageBox.Show("Can't corrupt with no domains selected.");
                    return null;
                }

                List<BlastUnit> blastUnits = new List<BlastUnit>();
                for (int i = 0; i < intensity; i++)
                {
                    var domain = domains[rand.Next(domains.Length)];
                    MemoryInterface mi = MemoryDomains.GetInterface(domain);
                    var bu = GenerateUnit(domain, rand.Next((int)mi.Size - 1), RtcCore.Alignment);
                    if (bu != null)
                        blastUnits.Add(bu);
                }
                return blastUnits.Count > 0 ? new BlastLayer(blastUnits) : null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void OnDeselect()
        {
            //throw new System.NotImplementedException();
        }

        public void OnSelect()
        {
            //throw new System.NotImplementedException();
        }
        string ICorruptionEngine.ToString() => "Dynamic Vector Engine";
    }

}