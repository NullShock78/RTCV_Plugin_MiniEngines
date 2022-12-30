using Ceras;
using RTCV.CorruptCore;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using RTCV.NetCore;
using RTCV.Common.CustomExtensions;

namespace MiniEngines.Engines.LoopingVector
{
    

    [Serializable]
    [Ceras.MemberConfig(TargetMember.All)]
    public class LoopingVectorEngine : ICorruptionEngine
    {
        public bool SupportsCustomPrecision => true;

        public bool SupportsAutoCorrupt => true;

        public bool SupportsGeneralParameters => true;

        public bool SupportsMemoryDomains => true;

        [Exclude]
        static Random rand = new Random();

        [Exclude]
        Form ICorruptionEngine.Control { get { return control; } }

        [Exclude]
        Form control { get; set; } = null;

        public static int NumExtraUnits
        {
            get { return EngineSpec.Get<int>(nameof(NumExtraUnits)); }
            set { EngineSpec.Set(nameof(NumExtraUnits), value); }
        }

        public static int IntervalBetweenUnitToggle
        {
            get { return EngineSpec.Get<int>(nameof(IntervalBetweenUnitToggle)); }
            set { EngineSpec.Set(nameof(IntervalBetweenUnitToggle), value); }
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
                    var bu = GenerateUnitGroup(domain, rand.Next((int)mi.Size - 1), RtcCore.Alignment);
                    if (bu.Count > 0)
                        blastUnits.AddRange(bu);
                }
                return blastUnits.Count > 0 ? new BlastLayer(blastUnits) : null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<BlastUnit> GenerateUnitGroup(string domain, long address, int alignment)
        {
            List<BlastUnit> unitGroup = new List<BlastUnit>();
            if (domain == null)
            {
                return null;
            }

            int precision;

            //Behavior: Will use the selected limiter list's precision by default
            //And if the precision was unlocked, use what was set in the precision box

            if (Filtering.Hash2LimiterDico.TryGetValue(LimiterListHash, out IListFilter list))
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
            //Enforce the safeaddress at generation
            var matchBytes = Filtering.LimiterPeekAndGetBytes(safeAddress, safeAddress + precision, LimiterListHash, mi);
            if (matchBytes != null)
            {
                BlastUnit orig = new BlastUnit(matchBytes, domain, safeAddress, precision, mi.BigEndian, 0, loopTiming: IntervalBetweenUnitToggle * (NumExtraUnits + 1));
                orig.Loop = true;
                unitGroup.Add(orig);
                for (int i = 0; i < NumExtraUnits; i++)
                {
                    BlastUnit bu = new BlastUnit(Filtering.GetRandomConstant(ValueListHash, precision, matchBytes), domain, safeAddress, precision, mi.BigEndian, (i + 1) * IntervalBetweenUnitToggle, loopTiming: IntervalBetweenUnitToggle * (NumExtraUnits + 1));
                    bu.Loop = true;
                    unitGroup.Add(bu);
                }

            }
            return unitGroup;
        }

        public LoopingVectorEngine()
        {

        }

        public LoopingVectorEngine(Form form)
        {
            control = form;
        }

        public void OnDeselect()
        {

        }

        public void OnSelect()
        {

        }

        string ICorruptionEngine.ToString() => "Looping Vector Engine";
    }
}
