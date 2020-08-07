using System;
using System.Collections.Generic;
using System.Text;

namespace KOAR_Lib
{
    public static class Utils {
        public static Dictionary<UInt32, String> _hash_table;

        public static uint SH(string val, bool lower = true) {
            if(lower) val = val.ToLowerInvariant();

            ReadOnlySpan<byte> data = new byte[]
            {
                0x7F,0x00,0x00,0x00,0x2B,0x03,0x00,0x00,0x3D,0x06,0x00,0x00,0x53,0x08,0x00,0x00,0xBD,0x0A,0x00,0x00,0x97,0x12,0x00,0x00,0x97,
                0x15,0x00,0x00,0x41,0x17,0x00,0x00,0xB5,0x1F,0x00,0x00,0x43,0x25,0x00,0x00,0x21,0x28,0x00,0x00,0x01,0x2A,0x00,0x00,0x97,0x2B,
                0x00,0x00,0x0D,0x30,0x00,0x00,0xA1,0x33,0x00,0x00,0x7F,0x37,0x00,0x00,0x35,0x3C,0x00,0x00,0x11,0x45,0x00,0x00,0xE5,0x48,0x00,
                0x00,0x45,0x4A,0x00,0x00,0x61,0x52,0x00,0x00,0x23,0x56,0x00,0x00,0x17,0x62,0x00,0x00,0xC9,0x64,0x00,0x00,0x41,0x6B,0x00,0x00,
                0x99,0x6D,0x00,0x00,0x8D,0x73,0x00,0x00,0x59,0x78,0x00,0x00,0x63,0x7F,0x00,0x00,0xA5,0x86,0x00,0x00,0xE3,0x8C,0x00,0x00,0x87,
                0x92,0x00,0x00,0x43,0x97,0x00,0x00,0x9D,0x9C,0x00,0x00,0xFF,0xA3,0x00,0x00,0x39,0xA9,0x00,0x00,0x1B,0xB0,0x00,0x00,0x47,0xB9,
                0x00,0x00,0x03,0xC2,0x00,0x00,0x4F,0xC6,0x00,0x00,0xCD,0xD0,0x00,0x00,0xAD,0xD8,0x00,0x00,0x69,0xDF,0x00,0x00,0xE9,0xE7,0x00,
                0x00,0x23,0xF2,0x00,0x00,0x2F,0xFE,0x00,0x00,0xCD,0x1E,0x01,0x00,0x19,0x30,0x01,0x00,0xFF,0x48,0x01,0x00,0xB1,0x5B,0x01,0x00
            };

            var hashtable = System.Runtime.InteropServices.MemoryMarshal.Cast<byte, int>(data);
            int hash = 0;
            for(int i = val.Length - 1; i > -1; i--) {
                var c = val[i];
                hash += (i + 1) * hashtable[c % 50] + (c * hashtable[i % 50]);
            }
            return (UInt32)hash;
        }

        public static String RestoreHashedString(UInt32 hash) {
            if(_hash_table == null) {
                _hash_table = new Dictionary<uint, string>();
                KOARBinaryReader br = new KOARBinaryReader("data\\hash_table");
                var count = br.ReadInt();
                for(int i = 0; i < count; i++) {
                    var h = br.ReadUInt();
                    var s = br.ReadString();
                    _hash_table[h] = s;
                }
                br.Close();
            }

            if(_hash_table.ContainsKey(hash)) return _hash_table[hash];

            return null;
        }

        public static String BundleTypeName(UInt32 type) {
            switch(type) {
                case 0x00: return "texture";
                case 0x01: return "luascript";
                case 0x02: return "material";
                case 0x03: return "globalobjectref";
                case 0x04: return "motionset";
                case 0x05: return "animation";
                case 0x06: return "uixml";
                case 0x07: return "quest";
                case 0x08: return "dialoguecameraset";
                case 0x09: return "fxe"; // init file?
                case 0x0A: return "QuestMgrData"; // init file?
                case 0x0B: return "player_ability";
                case 0x0C: return "buff";
                case 0x0D: return "simtype";
                case 0x0E: return "simspace";
                case 0x0F: return "cluster";
                case 0x10: return "fab";
                case 0x11: return "encounter";
                case 0x12: return "platform_shader";
                case 0x13: return "shader";
                case 0x14: return "pathset";
                case 0x15: return "collisionset";
                case 0x16: return "monstergroup";
                case 0x17: return "map"; // init file?
                case 0x18: return "task";
                case 0x19: return "talent";
                case 0x1A: return "loottable";
                case 0x1B: return "interaction";
                case 0x1C: return "physicsaction";
                case 0x1D: return "behavior";
                case 0x1E: return "cameracontroller";
                case 0x1F: return "fxpattern";
                case 0x20: return "motiontree";
                case 0x21: return "inputdeviceaction";
                case 0x22: return "inputdevicesequence";
                case 0x23: return "weapon_effect_table";
                case 0x24: return "collision_effect_table";
                // GAP for fileid and filehash only used in the converters?
                case 0x28: return "inputdevicecontrol";
                case 0x29: return "inputdevicemode";
                case 0x2A: return "collision_profile";
                case 0x2B: return "faction";
                case 0x2C: return "behaviortree";
                case 0x2D: return "artsetammo";
                case 0x2E: return "itemset";
                case 0x2F: return "ragdoll";
                case 0x30: return "hateset";
                case 0x31: return "fxpath"; // init file?
                case 0x32: return "effectstate";
                case 0x33: return "region";
                case 0x34: return "sky";
                case 0x35: return "terrain"; // TerrainObject?
                case 0x36: return "terrainpaletteset";
                case 0x37: return "rumble_sequence";
                case 0x38: return "vignette";
                case 0x39: return "decal";
                case 0x3A: return "dynamic_character_weapon_fx_table";
                case 0x3B: return "fxactor";
                case 0x3C: return "lore";
                case 0x3D: return "cinematic_scene";
                case 0x3E: return "cinematic";
                case 0x3F: return "uibundle"; // unused
                case 0x40: return "FabVisualData";
                case 0x41: return "active_summoning_encounter"; // ASE
                case 0x42: return "WeaponSpecificMotionSetBucket"; // unused
                case 0x43: return "achievement";
                case 0x44: return "LocalizationTable";
                case 0x45: return "FabVisualDataVariantBundle";
                case 0x46: return "SimTypeVisualDataVariantBundle";
                case 0x47: return "PlayerCustomization";
                case 0x48: return "BundleReference";
                case 0x49: return "MotionSetVisualDataVariantBundle";
                case 0x4A: return "FXAnimsetLookupFile";
                case 0x4B: return "ConditionalExpression";
                case 0x4C: return "BehaviorOverride";
                case 0x4D: return "voicetype";
                // added types
                case 0x100: return "bundle";
                default: return type.ToString("X");
            }
        }
        public static String BundleMgrName(UInt32 type) {
            switch(type) {
                case 0x01: return "lua";
                case 0x03: return "gor";
                case 0x0B: return "player_ability";
                case 0x12:
                case 0x13: return "shader";
                case 0x16: return "monstergroup";
                case 0x18: return "task"; // taskmgr?
                case 0x19: return "talent";
                case 0x1A: return "loottable";
                case 0x1B: return "interaction";
                case 0x0C: return "buffmgr";
                case 0x0D: return "simtype_mgr";
                case 0x1E: return "camera_controller";
                case 0x21:
                case 0x22:
                case 0x28:
                case 0x29: return "input_device_mode";
                case 0x2A: return "collision_profile";
                case 0x2B: return "faction";
                case 0x32: return "effectstate_mgr";
                case 0x38: return "vignette";
                case 0x43: return "achievement";
                case 0x44: return "start_up_localization_tables";
                case 0x4B: return "start_up_conditional_expression_tables";
                case 0x4C: return "start_up_behavior_override_tables";
                default: return type.ToString("X");
            }
            // player_customisation - textures?

        }
    }
}
