using ProtoBuf;

namespace Oct.Framework.Socket.BaseDef
{
    [ProtoContract(Name = @"VarType")]
    public enum VarType
    {

        [ProtoEnum(Name = @"None", Value = 0)]
        None = 0,

        [ProtoEnum(Name = @"Bool", Value = 1)]
        Bool = 1,

        [ProtoEnum(Name = @"Int8", Value = 2)]
        Int8 = 2,

        [ProtoEnum(Name = @"Int16", Value = 3)]
        Int16 = 3,

        [ProtoEnum(Name = @"Int32", Value = 4)]
        Int32 = 4,

        [ProtoEnum(Name = @"Int64", Value = 5)]
        Int64 = 5,

        [ProtoEnum(Name = @"Float", Value = 6)]
        Float = 6,

        [ProtoEnum(Name = @"Double", Value = 7)]
        Double = 7,

        [ProtoEnum(Name = @"Str", Value = 8)]
        Str = 8,

        [ProtoEnum(Name = @"Void", Value = 9)]
        Void = 9,

        [ProtoEnum(Name = @"Obj", Value = 10)]
        Obj = 10
    }
}