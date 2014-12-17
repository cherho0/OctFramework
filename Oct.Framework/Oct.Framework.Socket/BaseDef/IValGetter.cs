namespace Oct.Framework.Socket.BaseDef
{
    public interface IValGetter
    {
        bool IsZero { get; }
        VarType Type { get; }
        bool Bool();
        double Double();
        float Float();
        short Int16();
        int Int32();
        long Int64();
        byte Int8();
        ObjId Obj();
        string Str();
    }
}