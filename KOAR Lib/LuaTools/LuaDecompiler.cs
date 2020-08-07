using System;
using System.Collections.Generic;
using System.Text;
using KOAR_Lib.Format;

namespace KOAR_Lib.LuaTools {
    public static class LuaDecompiler {
        private static bool IsConditional(uint opcode) {
            switch((Lua.LuaOpcodes)opcode) {
                case Lua.LuaOpcodes.op_eq:
                case Lua.LuaOpcodes.op_eq_bk:
                case Lua.LuaOpcodes.op_le:
                case Lua.LuaOpcodes.op_le_bk:
                case Lua.LuaOpcodes.op_lt:
                case Lua.LuaOpcodes.op_lt_bk:
                case Lua.LuaOpcodes.op_test:
                case Lua.LuaOpcodes.op_test_r1:
                case Lua.LuaOpcodes.op_testset:
                    return true;
                default:
                    return false;
            }
        }
        public static String Decompile(LuaFunction func) {
            LuaRegistry reg = new LuaRegistry();
            #region scope_old
            /*List<LuaScope> scopes = new List<LuaScope>();

            for(int i = 0; i < root.Instructions.Count; i++) {
                var inst = root.Instructions[i];
                switch((Lua.LuaOpcodes)inst.OP) {
                    case Lua.LuaOpcodes.op_jmp:
                        if(inst.sBx < 0) { // jmp-
                            var prev_inst = root.Instructions[i - 1];
                            LuaScope.ScopeType type = LuaScope.ScopeType.scope_while;
                            if((Lua.LuaOpcodes)prev_inst.OP == Lua.LuaOpcodes.op_tforloop) type = LuaScope.ScopeType.scope_tforloop;
                            else if(IsConditional(prev_inst.OP)) type = LuaScope.ScopeType.scope_repeat;
                            scopes.Add(new LuaScope(i + inst.sBx + 1, i + 1, type));
                        } else { // jmp +
                            // break? if?
                        }
                                
                        break;
                    case Lua.LuaOpcodes.op_forloop:
                        scopes.Add(new LuaScope(i + inst.sBx, i + 1, LuaScope.ScopeType.scope_forloop));
                        break;
                }
            }*/
            #endregion scope_old

            for(int i = 0; i < func.Arguments; i++) {
                reg[i] = new LuaRegArgument("arg" + i.ToString());
            }

            for(int i = 0; i < func.Instructions.Count; i++) {
                var inst = func.Instructions[i];
                switch((Lua.LuaOpcodes)inst.OP) {
                    case Lua.LuaOpcodes.op_data:
                        continue;
                    case Lua.LuaOpcodes.op_getglobal:
                    case Lua.LuaOpcodes.op_getglobal_mem:
                        reg[inst.A] = new LuaRegGlobal(((LuaConstantString)func.Constants[inst.Bx]).Value);
                        break;
                    case Lua.LuaOpcodes.op_loadk:
                        reg[inst.A] = new LuaRegConstant(func.Constants[inst.Bx]);
                        break;
                }
            }

            return "";
        }
    }
    class LuaBlock {
        public List<LuaStatement> Statements { get; set; } = new List<LuaStatement>();
    }
    abstract class LuaStatement {}

    // old
    public class LuaScope {
        public enum ScopeType { scope_explicit, scope_tforloop, scope_forloop, scope_repeat, scope_while, scope_if, scope_else, scope_else_if, }
        public ScopeType Type { get; set; }
        public int Start { get; set; }
        public int End { get; set; }
        public LuaScope(int start, int end, ScopeType scopeType = ScopeType.scope_explicit) {
            Start = start;
            End = end;
            Type = scopeType;
        }
    }
    public class LuaRegistry {
        Dictionary<int, LuaRegisterValue> _reg = new Dictionary<int, LuaRegisterValue>();
        int _top = 0;
        public LuaRegisterValue this[int key] {
            get {
                if(_reg.ContainsKey(key)) return _reg[key];
                Set(key, new LuaRegConstant(new LuaConstantNil()));
                return _reg[key];
            }

            set => Set(key, value);
        }
        private void Set(int index, LuaRegisterValue value) {
            _reg[index] = value;
            if(index > _top) _top = index;
        }
    }

    public abstract class LuaRegisterValue {}

    public class LuaRegConstant:LuaRegisterValue {
        public LuaConstant Value { get; private set; }
        public LuaRegConstant(LuaConstant value) {
            Value = value;
        }
        public override string ToString() {
            return Value.ToString();
        }
    }
    public abstract class LuaRegVariable:LuaRegisterValue {
        public String Name { get; private set; }
        public LuaRegVariable(String name) {
            Name = name;
        }
        public override string ToString() {
            return Name;
        }
    }
    public class LuaRegGlobal:LuaRegVariable {
        public LuaRegGlobal(String name):base(name) {}
    }
    public class LuaRegLocal:LuaRegVariable {
        public LuaRegLocal(String name) : base(name) { }
    }
    public class LuaRegArgument:LuaRegVariable {
        public LuaRegArgument(String name) : base(name) { }
    }
}
