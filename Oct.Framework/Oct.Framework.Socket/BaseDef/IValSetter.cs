namespace Oct.Framework.Socket.BaseDef
{
    public interface IValSetter
    {
        void SetBool(bool val);
        void SetDouble(double val);
        void SetFloat(float val);
        void SetInt16(short val);
        void SetInt32(int val);
        void SetInt64(long val);
        void SetInt8(byte val);
        void SetObj(ObjId val);
        void SetStr(string val);
        void Zero();
    }
}