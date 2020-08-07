using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq.Expressions;
using System.Net.Http.Headers;
using System.Text;

namespace KOAR_Lib.Format {
    public class Lua:FormatBase {
        public static readonly ReadOnlyCollection<String> LuaOpcodeNames = new ReadOnlyCollection<string>(new[] {
            "getfield", "test", "call_i", "call_c", "eq", "eq_bk", "getglobal", "move", "self", "return", "gettable_s", "gettable_n",
            "gettable", "loadbool", "tforloop", "setfield", "settable_s", "settable_s_bk", "settable_n", "settable_n_bk", "settable",
            "settable_bk", "tailcall_i", "tailcall_c", "tailcall_m", "loadk", "loadnil", "setglobal", "jmp", "call_m", "call", "tailcall",
            "getupval", "setupval", "add", "add_bk", "sub", "sub_bk", "mul", "mul_bk", "div", "div_bk", "mod", "mod_bk", "pow", "pow_bk",
            "newtable", "unm", "not", "len", "lt", "lt_bk", "le", "le_bk", "concat", "testset", "forprep", "forloop", "setlist", "close",
            "closure", "vararg", "tailcall_i_r1", "call_i_r1", "setupval_r1", "test_r1", "not_r1", "getfield_r1", "setfield_r1", "newstruct",
            "data", "setslotn", "setsloti", "setslot", "setslots", "setslotmt", "checktype", "checktypes", "getslot", "getslotmt", "selfslot",
            "selfslotmt", "getfield_mm", "checktype_d", "getslot_d", "getglobal_mem", "opcode_max",
        });
        public enum LuaOpcodes {
            op_getfield, op_test, op_call_i, op_call_c, op_eq, op_eq_bk, op_getglobal, op_move, op_self, op_return, op_gettable_s, op_gettable_n,
            op_gettable, op_loadbool, op_tforloop, op_setfield, op_settable_s, op_settable_s_bk, op_settable_n, op_settable_n_bk, op_settable,
            op_settable_bk, op_tailcall_i, op_tailcall_c, op_tailcall_m, op_loadk, op_loadnil, op_setglobal, op_jmp, op_call_m, op_call, op_tailcall,
            op_getupval, op_setupval, op_add, op_add_bk, op_sub, op_sub_bk, op_mul, op_mul_bk, op_div, op_div_bk, op_mod, op_mod_bk, op_pow, op_pow_bk,
            op_newtable, op_unm, op_not, op_len, op_lt, op_lt_bk, op_le, op_le_bk, op_concat, op_testset, op_forprep, op_forloop, op_setlist, op_close,
            op_closure, op_vararg, op_tailcall_i_r1, op_call_i_r1, op_setupval_r1, op_test_r1, op_not_r1, op_getfield_r1, op_setfield_r1, op_newstruct,
            op_data, op_setslotn, op_setsloti, op_setslot, op_setslots, op_setslotmt, op_checktype, op_checktypes, op_getslot, op_getslotmt, op_selfslot,
            op_selfslotmt, op_getfield_mm, op_checktype_d, op_getslot_d, op_getglobal_mem, op_opcode_max,
        }
        public String Module { get; set; }
        public List<String> PrehashedFunctions { get; set; }
        public LuaFunction Root { get; set; }
        
        LuaHeader _header;
        byte[] _footer;

        public override void Load(Stream stream) {
            KOARBinaryReader br = new KOARBinaryReader(stream);
            
            // Metadata

            Module = br.ReadString();
            PrehashedFunctions = new List<string>();

            var count = br.ReadInt();
            for(int i = 0; i < count; i++) {
                PrehashedFunctions.Add(br.ReadString());
            }

            br.ReadInt(); // Size of Lua chunk

            // Lua
            // header

            _header = new LuaHeader();
            _header.Singature = br.ReadUInt();
            _header.Version = (byte)br.ReadInt(1);
            _header.Format = (byte)br.ReadInt(1);
            _header.Endian = (LuaHeader.ChunkEndian)br.ReadInt(1);
            _header.SizeOfInt = (byte)br.ReadInt(1);
            _header.SizeOfSize_T = (byte)br.ReadInt(1);
            _header.SizeOfInstruction = (byte)br.ReadInt(1);
            _header.SizeOfLuaNumber = (byte)br.ReadInt(1);
            _header.IntegralFlag = (byte)br.ReadInt(1);
            _header.Unknown1 = (byte)br.ReadInt(1);
            _header.Unknown2 = (byte)br.ReadInt(1);

            if(_header.Endian == LuaHeader.ChunkEndian.big) br.SetLittleEndian(false);

            // value types

            br.Read(224);

            // main function

            Root = LuaFunction.ReadFunction(br);

            // KoreVM footer

            _footer = br.Read(8);

            br.Close();
        }

        public override void Save(Stream stream) {
            throw new NotImplementedException();
        }
    }

    public class LuaFunction {
        public String Source { get; set; }
        public String Name { get; set; }
        public Tuple<int, int> LinesDefined { get; set; }
        public int Upvalues_Count { get; set; }
        public int Arguments { get; set; }
        public byte Vararg { get; set; }
        public int StackSize{ get; set; }
        public List<LuaInstruction> Instructions { get; set; } = new List<LuaInstruction>();
        public List<LuaConstant> Constants { get; set; } = new List<LuaConstant>();
        public List<LuaFunction> Functions { get; set; } = new List<LuaFunction>();

        // Debug lists
        public List<int> Debug_SourceLines = new List<int>();
        public List<Tuple<String, int, int>> Debug_Locals = new List<Tuple<string, int, int>>();
        public List<String> Debug_UpvalueNames = new List<string>();

        public static LuaFunction ReadFunction(KOARBinaryReader br) {
            LuaFunction func = new LuaFunction();

            // header

            func.Source = br.ReadString();
            func.Name = br.ReadString();
            func.LinesDefined = new Tuple<int, int>(br.ReadInt(), br.ReadInt());
            func.Upvalues_Count = br.ReadInt();
            func.Arguments = br.ReadInt();
            func.Vararg = (byte)br.ReadInt(1);
            func.StackSize = br.ReadInt();

            // instructions

            int count = br.ReadInt();
            for(int i = 0; i < count; i++) {
                func.Instructions.Add(new LuaInstruction(br.ReadUInt()));
            }

            // constants

            count = br.ReadInt();
            for(int i = 0; i < count; i++) {
                func.Constants.Add(LuaConstant.ReadConstant(br));
            }

            // functions

            count = br.ReadInt();
            for(int i = 0; i < count; i++) {
                func.Functions.Add(LuaFunction.ReadFunction(br));
            }

            // Debug lists
            // source lines

            count = br.ReadInt();
            for(int i = 0; i < count; i++) {
                func.Debug_SourceLines.Add(br.ReadInt());
            }

            // locals

            count = br.ReadInt();
            for(int i = 0; i < count; i++) {
                String name = br.ReadString();
                int line_start = br.ReadInt();
                int line_end = br.ReadInt();
                func.Debug_Locals.Add(new Tuple<string, int, int>(name, line_start, line_end));
            }

            // upvalues

            count = br.ReadInt();
            for(int i = 0; i < count; i++) {
                func.Debug_UpvalueNames.Add(br.ReadString());
            }

            return func;
        }
    }

    public class LuaInstruction {
        // constants
        #region constants
        const int SIZE_C = 9;                   // max value: 511      max value (RK): 255
        const int SIZE_B = 8;                   // max value: 255      max value (RK): 127
        const int SIZE_Bx = SIZE_C + SIZE_B;    // max value: 131071   max value (RK): 65535
        const int SIZE_A = 8;
        const int SIZE_OP = 7;
        const int POS_A = 0;
        const int POS_C = POS_A + SIZE_A;
        const int POS_B = POS_C + SIZE_C;
        const int POS_Bx = POS_C;
        const int POS_OP = POS_B + SIZE_B;
        const uint MASK_A = ~(UInt32.MaxValue << SIZE_A) << POS_A;
        const uint MASK_B = ~(UInt32.MaxValue << SIZE_B) << POS_B;
        const uint MASK_Bx = ~(UInt32.MaxValue << SIZE_Bx) << POS_Bx;
        const uint MASK_C = ~(UInt32.MaxValue << SIZE_C) << POS_C;
        const uint MASK_OP = ~(UInt32.MaxValue << SIZE_OP) << POS_OP;
        const int MAXARG_A = (1 << SIZE_A) - 1;
        const int MAXARG_B = (1 << SIZE_B) - 1;
        const int MAXARG_C = (1 << SIZE_C) - 1;
        const int MAXARG_Bx = (1 << SIZE_Bx) - 1;
        const int MAXARG_sBx = MAXARG_Bx >> 1;
        //const int LFIELDS_PER_FLUSH = 50; // ?
        #endregion
        enum Opmode { ABC, ABx, AsBx }

        UInt32 _code;
        public LuaInstruction(UInt32 code) {
            _code = code;
        }

        public int OP { get { return (int)(_code & MASK_OP) >> POS_OP; } }
        public int A { get { return (int)(_code & MASK_A) >> POS_A; } }
        public int B { get { return (int)(_code & MASK_B) >> POS_B; } }
        public int C { get { return (int)(_code & MASK_C) >> POS_C; } }
        public int Bx { get { return (int)(_code & MASK_Bx) >> POS_Bx; } }
        public int sBx { get { return Bx - MAXARG_sBx;  } }
        public override string ToString() {
            return String.Format("{0} {1} {2} {3}", Lua.LuaOpcodeNames[OP], A, B, C);
        }
    }
    
    public abstract class LuaConstant {
        public static LuaConstant ReadConstant(KOARBinaryReader br) {
            int type = br.ReadInt(1);
            switch(type) {
                case 0:
                    return new LuaConstantNil();
                case 1:
                    return new LuaConstantBool(br.ReadInt(1) == 1);
                case 3:
                    return new LuaConstantNumber(br.ReadInt());
                case 4:
                    String s = br.ReadString();
                    return new LuaConstantString(s.Substring(0, s.Length - 1));
                case 11:
                    return new LuaConstantUI64(br.ReadUInt64());
                default:
                    throw new NotSupportedException(String.Format("constant type {0} is not supported", type));
            }
        }
        public abstract void Write(KOARBinaryWriter bw);
    }

    public class LuaConstantNil:LuaConstant {
        public override void Write(KOARBinaryWriter bw) {
            bw.WriteByte(0);
        }
        public override string ToString() {
            return "nil";
        }
    }
    public class LuaConstantBool:LuaConstant {
        public bool Value { get; set; }
        public LuaConstantBool(bool value) {
            Value = value;
        }
        public override void Write(KOARBinaryWriter bw) {
            bw.WriteByte(1);
            bw.WriteInt(Value ? 1: 0, 1);
        }
        public override string ToString() {
            return Value ? "true" : "false";
        }
    }
    public class LuaConstantNumber:LuaConstant {
        public int Value { get; set; }
        public LuaConstantNumber(int value) {
            Value = value;
        }
        public override void Write(KOARBinaryWriter bw) {
            bw.WriteByte(3);
            bw.WriteInt(Value);
        }
        public override string ToString() {
            return Value.ToString();
        }
    }
    public class LuaConstantString:LuaConstant {
        public String Value { get; set; }
        public LuaConstantString(String value) {
            Value = value;
        }
        public override void Write(KOARBinaryWriter bw) {
            bw.WriteByte(4);
            bw.WriteString(Value + "\0");
        }
        public override string ToString() {
            return Value;
        }
    }

    public class LuaConstantUI64:LuaConstant {
        public UInt64 Value { get; set; }
        public LuaConstantUI64(UInt64 value) {
            Value = value;
        }
        public override void Write(KOARBinaryWriter bw) {
            bw.WriteByte(11);
            bw.WriteUInt64(Value);
        }
        public override string ToString() {
            return String.Format("0x{0}HL", Value.ToString("X"));
        }
    }

    class LuaHeader {
        public enum ChunkEndian { big, little }
        public UInt32 Singature { get; set; } // == 0x61754C1B or "\x1BLua"
        public byte Version { get; set; } // Lua version, == 0x51 aka Lua 5.1 for KoreVM
        public byte Format { get; set; } // Chunk format, == 8 for KoAR chunks
        public ChunkEndian Endian { get; set; } // 0 = big endian, 1 = little endian; 0 for KoAR
        public byte SizeOfInt { get; set; }
        public byte SizeOfSize_T { get; set; }
        public byte SizeOfInstruction { get; set; }
        public byte SizeOfLuaNumber{ get; set; } // all size values == 4 for KoAR chunks
        public byte IntegralFlag { get; set; } // 0 - lua numbers are floats, 1 - lua numbers are integers; 1 for KoAR
        public byte Unknown1 { get; set; } // == 1 for KoAR
        public byte Unknown2 { get; set; } // == 0 for KoAR
    }
}
