using Ceras;
using RTCV.CorruptCore;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using RTCV.NetCore;
using RTCV.Common.CustomExtensions;

namespace MiniEngines.Engines.BitManip
{
    public enum ManipMethod
    {
        ShiftLeft,
        ShiftRight,
        XOR,
        AND,
        OR
    }

    public struct BitManipEntry
    {
        public ManipMethod Method;
        public ulong Rhs;
    }

    public enum ManipMode
    {
        Random,
        Lists,
    }

    [Serializable]
    [Ceras.MemberConfig(TargetMember.All)]
    public class BitManipEngine : ICorruptionEngine
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

        public BitManipEngine()
        {

        }

        public BitManipEngine(Form form)
        {
            control = form;
        }

        public BlastUnit ShiftRight(MemoryInterface mi, long address, ulong value, int amount, int precision)
        {
            if (amount < 0 || amount > (precision * 8))
            {
                return null;
            }
            ulong buvalue = value >> amount;
            byte[] bytes = BitConverter.GetBytes(buvalue);
            Array.Resize(ref bytes, precision);
            //Array.Reverse(bytes);
            return new BlastUnit(bytes, mi.Name, address, precision, mi.BigEndian, 0, 1, $"{value:X} >> {amount}");
        }

        public BlastUnit ShiftLeft(MemoryInterface mi, long address, ulong value, int amount, int precision)
        {
            if (amount < 0 || amount > (precision * 8))
            {
                return null;
            }
            ulong buvalue = value << amount;
            byte[] bytes = BitConverter.GetBytes(buvalue);
            Array.Resize(ref bytes, precision);
            //Array.Reverse(bytes);
            return new BlastUnit(bytes, mi.Name, address, precision, mi.BigEndian, 0, 1, $"{value:X} << {amount}");
        }

        public BlastUnit XOR(MemoryInterface mi, long address, ulong value, ulong rhs, int precision)
        {
            ulong buvalue = value ^ rhs;
            byte[] bytes = BitConverter.GetBytes(buvalue);
            Array.Resize(ref bytes, precision);
            //Array.Reverse(bytes);
            return new BlastUnit(bytes, mi.Name, address, precision, mi.BigEndian, 0, 1, $"{value:X} ^ {rhs:X}");
        }

        public BlastUnit AND(MemoryInterface mi, long address, ulong value, ulong rhs, int precision)
        {
            ulong buvalue = value & rhs;
            byte[] bytes = BitConverter.GetBytes(buvalue);
            Array.Resize(ref bytes, precision);
            //Array.Reverse(bytes);
            return new BlastUnit(bytes, mi.Name, address, precision, mi.BigEndian, 0, 1, $"{value:X} & {rhs:X}");
        }

        public BlastUnit OR(MemoryInterface mi, long address, ulong value, ulong rhs, int precision)
        {
            ulong buvalue = value | rhs;
            byte[] bytes = BitConverter.GetBytes(buvalue);
            Array.Resize(ref bytes, precision);
            //Array.Reverse(bytes);
            return new BlastUnit(bytes, mi.Name, address, precision, mi.BigEndian, 0, 1, $"{value:X} | {rhs:X}");
        }


        public static List<BitManipEntry> Entries
        {
            get { return EngineSpec.Get<List<BitManipEntry>>(nameof(BitManipEngine) + "_" + nameof(Entries)); }
            set { EngineSpec.Set(nameof(BitManipEngine) + "_" + nameof(Entries), value); }
        }


        public static ManipMode EngineMode
        {
            get { return EngineSpec.Get<ManipMode>(nameof(BitManipEngine) + "_" + nameof(EngineMode)); }
            set { EngineSpec.Set(nameof(BitManipEngine) + "_" + nameof(EngineMode), value); }
        }
        public static ulong BytesToUlong(byte[] byteRef)
        {
            var bytes = (byte[])byteRef.Clone();
            //Fun switch of fun, but is faster for most paths than resizing to 8 bytes
            switch (bytes.Length)
            {
                case 1:
                    return (ulong)bytes[0];
                case 2:
                    return (ulong)BitConverter.ToUInt16(bytes, 0);
                case 3:
                    byte[] new3byte = new byte[8];
                    Array.Copy(byteRef, 0, new3byte, 5, 3);
                    //bytes.FlipBytes();
                    //Array.Resize(ref bytes, 4);
                    //bytes.FlipBytes();
                    return BitConverter.ToUInt64(new3byte, 0);
                case 4:
                    return (ulong)BitConverter.ToUInt32(bytes, 0);
                case 5:
                case 6:
                case 7:
                    byte[] new7byte = new byte[8];
                    Array.Copy(byteRef, 0, new7byte, 1, 7);
                    //bytes.FlipBytes();
                    //Array.Resize(ref bytes, 8);
                    //bytes.FlipBytes();
                    return BitConverter.ToUInt64(new7byte, 0);
                case 8:
                    return BitConverter.ToUInt64(bytes, 0);
                default:
                    throw new Exception("Invalid byte count in BitLogicListFilter. Limiter must be less than 64 bits (8 bytes)");
            }
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
                    long address = rand.NextLong(0, mi.Size - RtcCore.CurrentPrecision);
                    long safeAddress = address - (address % RtcCore.CurrentPrecision) + RtcCore.Alignment;
                    if (safeAddress > mi.Size - RtcCore.CurrentPrecision && mi.Size > RtcCore.CurrentPrecision)
                    {
                        safeAddress = mi.Size - (2 * RtcCore.CurrentPrecision) + RtcCore.Alignment; //If we're out of range, hit the last aligned address
                    }
                    if (EngineMode == ManipMode.Lists)
                    {
                        //var entry = Entries[rand.Next(Entries.Count)];
                        //switch (entry.Method)
                        //{
                        //    case ManipMethod.ShiftLeft:
                        //        blastUnits.Add(ShiftLeft(mi, ))
                        //}
                        MessageBox.Show("Lists for this engine are not yet implemented.");
                    }
                    else
                    {
                        ManipMethod method = (ManipMethod)rand.Next(5);
                        switch (method)
                        {
                            case ManipMethod.ShiftLeft:
                                {
                                    int shiftAmount = rand.Next(0, RtcCore.CurrentPrecision * 8);
                                    byte[] origBytes = mi.PeekBytes(safeAddress, safeAddress + RtcCore.CurrentPrecision, !mi.BigEndian);
                                    ulong toUlong = BytesToUlong(origBytes);
                                    blastUnits.Add(ShiftLeft(mi, safeAddress, toUlong, shiftAmount, RtcCore.CurrentPrecision));
                                    break;
                                }
                            case ManipMethod.ShiftRight:
                                {
                                    int shiftAmount = rand.Next(0, RtcCore.CurrentPrecision * 8);
                                    byte[] origBytes = mi.PeekBytes(safeAddress, safeAddress + RtcCore.CurrentPrecision, !mi.BigEndian);
                                    ulong toUlong = BytesToUlong(origBytes);
                                    blastUnits.Add(ShiftRight(mi, safeAddress, toUlong, shiftAmount, RtcCore.CurrentPrecision));
                                    break;
                                }
                            case ManipMethod.XOR:
                                {
                                    ulong rhs = 0;
                                    switch (RtcCore.CurrentPrecision)
                                    {
                                        case 1:
                                            rhs = (ulong)(byte)rand.Next(0x100);
                                            break;
                                        case 2:
                                            rhs = (ulong)(ushort)rand.Next(0x10000);
                                            break;
                                        case 4:
                                            rhs = (ulong)(uint)rand.NextLong(0, 0x100000000);
                                            break;
                                        case 8:
                                            rhs = rand.NextULong();
                                            break;
                                        default:
                                            rhs = 0;
                                            break;
                                    }
                                    byte[] origBytes = mi.PeekBytes(safeAddress, safeAddress + RtcCore.CurrentPrecision, !mi.BigEndian);
                                    ulong toUlong = BytesToUlong(origBytes);
                                    blastUnits.Add(XOR(mi, safeAddress, toUlong, rhs, RtcCore.CurrentPrecision));
                                    break;
                                }
                            case ManipMethod.AND:
                                {
                                    ulong rhs = 0;
                                    switch (RtcCore.CurrentPrecision)
                                    {
                                        case 1:
                                            rhs = (ulong)(byte)rand.Next(0x100);
                                            break;
                                        case 2:
                                            rhs = (ulong)(ushort)rand.Next(0x10000);
                                            break;
                                        case 4:
                                            rhs = (ulong)(uint)rand.NextLong(0, 0x100000000);
                                            break;
                                        case 8:
                                            rhs = rand.NextULong();
                                            break;
                                        default:
                                            rhs = 0;
                                            break;
                                    }
                                    byte[] origBytes = mi.PeekBytes(safeAddress, safeAddress + RtcCore.CurrentPrecision, !mi.BigEndian);
                                    ulong toUlong = BytesToUlong(origBytes);
                                    blastUnits.Add(AND(mi, safeAddress, toUlong, rhs, RtcCore.CurrentPrecision));
                                    break;
                                }
                            case ManipMethod.OR:
                                {
                                    ulong rhs = 0;
                                    switch (RtcCore.CurrentPrecision)
                                    {
                                        case 1:
                                            rhs = (ulong)(byte)rand.Next(0x100);
                                            break;
                                        case 2:
                                            rhs = (ulong)(ushort)rand.Next(0x10000);
                                            break;
                                        case 4:
                                            rhs = (ulong)(uint)rand.NextLong(0, 0x100000000);
                                            break;
                                        case 8:
                                            rhs = rand.NextULong();
                                            break;
                                        default:
                                            rhs = 0;
                                            break;
                                    }
                                    byte[] origBytes = mi.PeekBytes(safeAddress, safeAddress + RtcCore.CurrentPrecision, !mi.BigEndian);
                                    ulong toUlong = BytesToUlong(origBytes);
                                    blastUnits.Add(OR(mi, safeAddress, toUlong, rhs, RtcCore.CurrentPrecision));
                                    break;
                                }
                            default:
                                break;
                        }
                    }
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
            
        }

        public void OnSelect()
        {
            
        }

        string ICorruptionEngine.ToString() => "Bit Manipulation Engine";
    }
}
