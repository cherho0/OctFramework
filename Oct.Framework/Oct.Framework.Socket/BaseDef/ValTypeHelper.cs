using System;
using System.Collections.Generic;

namespace Oct.Framework.Socket.BaseDef
{
    public static class VarTypeHelper
    {
        private static readonly List<Type> _ids = new List<Type>
                                                      {
                                                          typeof (NotSupportedException),
                                                          typeof (bool),
                                                          typeof (byte),
                                                          typeof (short),
                                                          typeof (int),
                                                          typeof (long),
                                                          typeof (float),
                                                          typeof (double),
                                                          typeof (string),
                                                          typeof (ObjId)
                                                      };

        public static Type GetClsType(VarType type)
        {
            return _ids[(int) type];
        }

        public static bool IsMatch(VarType x, VarType y)
        {
            switch (x)
            {
                case VarType.Bool:
                    return (y == VarType.Bool);

                case VarType.Int8:
                    return (y == VarType.Int8);

                case VarType.Int16:
                    return (y == VarType.Int16);

                case VarType.Int32:
                    return (y == VarType.Int32);

                case VarType.Int64:
                    return (y == VarType.Int64);

                case VarType.Float:
                    return (y == VarType.Float);

                case VarType.Double:
                    return (y == VarType.Double);

                case VarType.Str:
                    return (y == VarType.Str);

                case VarType.Obj:
                    return (y == VarType.Obj);
            }
            return false;
        }
    }
}