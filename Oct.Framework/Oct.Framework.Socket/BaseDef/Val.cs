using System;
using System.Diagnostics;

namespace Oct.Framework.Socket.BaseDef
{
    public class Val : IVal, IEquatable<Val>
    {
        private VarType _type;
        private Var _val;

        public Val()
        {
        }

        public Val(VarType type)
            : this(type, Var.Create(type))
        {
        }

        private Val(VarType type, Var val)
        {
            _type = type;
            _val = val;
        }

        public Val(VarType type, object val)
            : this(type, Var.Create(type, val))
        {
        }

        public Var Valu
        {
            get { return _val; }
            set { _val = value; }
        }

        #region IEquatable<Val> Members

        public bool Equals(Val other)
        {
            return (((other != null) && (other._type == _type)) && (other._val == _val));
        }

        #endregion

        #region IVal Members

        public VarType Type
        {
            get { return _type; }
            set { _type = value; }
        }

        public bool Bool()
        {
            Trace.Assert(_type == VarType.Bool, "");
            return _val.Bool();
        }

        public double Double()
        {
            Trace.Assert(_type == VarType.Double, "");
            return _val.Double();
        }

        public float Float()
        {
            Trace.Assert(_type == VarType.Float, "");
            return _val.Float();
        }

        public short Int16()
        {
            Trace.Assert(_type == VarType.Int16, "");
            return _val.Int16();
        }

        public int Int32()
        {
            Trace.Assert(_type == VarType.Int32, "");
            return _val.Int32();
        }

        public long Int64()
        {
            Trace.Assert(_type == VarType.Int64, "");
            return _val.Int64();
        }

        public byte Int8()
        {
            Trace.Assert(_type == VarType.Int8, "");
            return _val.Int8();
        }

        public ObjId Obj()
        {
            Trace.Assert(_type == VarType.Obj, "");
            return _val.Obj();
        }

        public void SetBool(bool val)
        {
            Trace.Assert(_type == VarType.Bool, "");

            if (!val.Equals(_val.Bool()))
            {
                _val.SetBool(val);
            }
        }

        public void SetDouble(double val)
        {
            Trace.Assert(_type == VarType.Double, "");

            if (!val.Equals(_val.Double()))
            {
                _val.SetDouble(val);
            }
        }

        public void SetFloat(float val)
        {
            Trace.Assert(_type == VarType.Float, "");
            if (!val.Equals(_val.Float()))
            {
                _val.SetFloat(val);
            }
        }

        public void SetInt16(short val)
        {
            Trace.Assert(_type == VarType.Int16, "");
            if (!val.Equals(_val.Int16()))
            {
                _val.SetInt16(val);
            }
        }

        public void SetInt32(int val)
        {
            Trace.Assert(_type == VarType.Int32, "");
            if (!val.Equals(_val.Int32()))
            {
                _val.SetInt32(val);
            }
        }

        public void SetInt64(long val)
        {
            Trace.Assert(_type == VarType.Int64, "");
            if (!val.Equals(_val.Int64()))
            {
                _val.SetInt64(val);
            }
        }

        public void SetInt8(byte val)
        {
            Trace.Assert(_type == VarType.Int8, "");
            if (!val.Equals(_val.Int8()))
            {
                _val.SetInt8(val);
            }
        }

        public void SetObj(ObjId val)
        {
            Trace.Assert(_type == VarType.Obj, "");
            if (!val.Equals(_val.Obj()))
            {
                _val.SetObj(val);
            }
        }

        public void SetStr(string val)
        {
            Trace.Assert(_type == VarType.Str, "");
            if (!val.Equals(_val.Str()))
            {
                _val.SetStr(val);
            }
        }

        public string Str()
        {
            Trace.Assert(_type == VarType.Str, "");
            return _val.Str();
        }

        public void Zero()
        {
            _val.Zero();
        }

        public bool IsZero
        {
            get { return _val.IsZero; }
        }

        #endregion

        public Val Clone()
        {
            return new Val(_type, _val.Clone());
        }

        public override bool Equals(object obj)
        {
            return (((obj != null) && (obj.GetType() == base.GetType())) && Equals((Val) obj));
        }

        public override int GetHashCode()
        {
            return ToString().GetHashCode();
        }

        //public static bool operator ==(Val x, Val y)
        //{
        //    if (x == null || y == null)
        //    {
        //        return false;
        //    }

        //    if (ReferenceEquals(x, y))
        //    {
        //        return true;
        //    }

        //    return x.Valu == y.Valu;
        //}

        //public static bool operator !=(Val x, Val y)
        //{
        //    return !(x == y);
        //}

        public override string ToString()
        {
            string s = "";
            if (_type == VarType.Str)
            {
                s = string.Format("\"{0}\"", _val);
            }
            else
            {
                s = _val.ToString();
            }
            return s;
        }
    }

    #region Nested type: Var
    public abstract class Var
    {
        public abstract bool IsZero { get; }

        public virtual bool Bool()
        {
            throw new NotImplementedException();
        }

        public abstract Var Clone();

        public static Var Create(VarType type)
        {
            switch (type)
            {
                case VarType.Bool:
                    return new VarBool();
                case VarType.Int8:
                    return new VarInt8();

                case VarType.Int16:
                    return new VarInt16();

                case VarType.Int32:
                    return new VarInt32();

                case VarType.Int64:
                    return new VarInt64();

                case VarType.Float:
                    return new VarFloat();
                case VarType.Double:
                    return new VarDouble();
                case VarType.Str:
                    return new VarStr();
                case VarType.Obj:
                    return new VarObj();
            }
            throw new ArgumentOutOfRangeException("type");
        }

        public static Var Create(VarType type, object val)
        {
            switch (type)
            {
                case VarType.Bool:
                    return new VarBool((bool) val);

                case VarType.Int8:
                    return new VarInt8((byte) val);

                case VarType.Int16:
                    return new VarInt16((short) val);

                case VarType.Int32:
                    return new VarInt32((int) val);

                case VarType.Int64:
                    return new VarInt64((long) val);

                case VarType.Float:
                    return new VarFloat((float) val);

                case VarType.Double:
                    return new VarDouble((double) val);

                case VarType.Str:
                    return new VarStr((string) val);

                case VarType.Obj:
                    return new VarObj((ObjId) val);
            }
            throw new ArgumentOutOfRangeException("type");
        }

        public virtual double Double()
        {
            throw new NotImplementedException();
        }

        public virtual float Float()
        {
            throw new NotImplementedException();
        }

        public virtual short Int16()
        {
            throw new NotImplementedException();
        }

        public virtual int Int32()
        {
            throw new NotImplementedException();
        }

        public virtual long Int64()
        {
            throw new NotImplementedException();
        }

        public virtual byte Int8()
        {
            throw new NotImplementedException();
        }

        public virtual ObjId Obj()
        {
            throw new NotImplementedException();
        }

        public virtual void SetBool(bool val)
        {
            throw new NotImplementedException();
        }

        public virtual void SetDouble(double val)
        {
            throw new NotImplementedException();
        }

        public virtual void SetFloat(float val)
        {
            throw new NotImplementedException();
        }

        public virtual void SetInt16(short val)
        {
            throw new NotImplementedException();
        }

        public virtual void SetInt32(int val)
        {
            throw new NotImplementedException();
        }

        public virtual void SetInt64(long val)
        {
            throw new NotImplementedException();
        }

        public virtual void SetInt8(byte val)
        {
            throw new NotImplementedException();
        }

        public virtual void SetObj(ObjId val)
        {
            throw new NotImplementedException();
        }

        public virtual void SetStr(string val)
        {
            throw new NotImplementedException();
        }

        public virtual string Str()
        {
            throw new NotImplementedException();
        }

        public abstract void Zero();
    }
    #endregion

    #region Nested type: VarBool

    public class VarBool : Var, IEquatable<VarBool>
    {
        private const bool _Zero = false;

        public bool _val;

        public VarBool()
        {
            Zero();
        }

        public VarBool(bool val)
        {
            SetBool(val);
        }

        public override bool IsZero
        {
            get { return _val.Equals(false); }
        }

        #region IEquatable<VarBool> Members

        public bool Equals(VarBool other)
        {
            return ((other != null) && (other._val == _val));
        }

        #endregion

        public override bool Bool()
        {
            return _val;
        }

        public override Var Clone()
        {
            return new VarBool(_val);
        }

        public override bool Equals(object obj)
        {
            return (((obj != null) && (obj.GetType() == base.GetType())) && Equals((VarBool) obj));
        }

        public override int GetHashCode()
        {
            return _val.GetHashCode();
        }

        public static bool operator ==(VarBool x, VarBool y)
        {
            return (ReferenceEquals(x, y) || (((x != null) && (y != null)) && x.Equals(y)));
        }

        public static bool operator !=(VarBool x, VarBool y)
        {
            return !(x == y);
        }

        public override void SetBool(bool val)
        {
            _val = val;
        }

        public override string ToString()
        {
            return _val.ToString();
        }

        public override void Zero()
        {
            SetBool(false);
        }
    }

    #endregion

    #region Nested type: VarDouble

    public class VarDouble : Var, IEquatable<VarDouble>
    {
        private const double _Zero = 0.0;
        public double _val;

        public VarDouble()
        {
            Zero();
        }

        public VarDouble(double val)
        {
            SetDouble(val);
        }

        public override bool IsZero
        {
            get { return _val.Equals(0.0); }
        }

        #region IEquatable<VarDouble> Members

        public bool Equals(VarDouble other)
        {
            return ((other != null) && (other._val == _val));
        }

        #endregion

        public override Var Clone()
        {
            return new VarDouble(_val);
        }

        public override double Double()
        {
            return _val;
        }

        public override bool Equals(object obj)
        {
            return (((obj != null) && (obj.GetType() == base.GetType())) && Equals((VarDouble) obj));
        }

        public override int GetHashCode()
        {
            return _val.GetHashCode();
        }

        public static bool operator ==(VarDouble x, VarDouble y)
        {
            return (ReferenceEquals(x, y) || (((x != null) && (y != null)) && x.Equals(y)));
        }

        public static bool operator !=(VarDouble x, VarDouble y)
        {
            return !(x == y);
        }

        public override void SetDouble(double val)
        {
            _val = val;
        }

        public override string ToString()
        {
            return _val.ToString();
        }

        public override void Zero()
        {
            SetDouble(0.0);
        }
    }

    #endregion

    #region Nested type: VarFloat

    public class VarFloat : Var, IEquatable<VarFloat>
    {
        private const float _Zero = 0f;
        public float _val;

        public VarFloat()
        {
            Zero();
        }

        public VarFloat(float val)
        {
            SetFloat(val);
        }

        public override bool IsZero
        {
            get { return _val.Equals(0f); }
        }

        #region IEquatable<VarFloat> Members

        public bool Equals(VarFloat other)
        {
            return ((other != null) && (other._val == _val));
        }

        #endregion

        public override Var Clone()
        {
            return new VarFloat(_val);
        }

        public override bool Equals(object obj)
        {
            return (((obj != null) && (obj.GetType() == base.GetType())) && Equals((VarFloat) obj));
        }

        public override float Float()
        {
            return _val;
        }

        public override int GetHashCode()
        {
            return _val.GetHashCode();
        }

        public static bool operator ==(VarFloat x, VarFloat y)
        {
            return (ReferenceEquals(x, y) || (((x != null) && (y != null)) && x.Equals(y)));
        }

        public static bool operator !=(VarFloat x, VarFloat y)
        {
            return !(x == y);
        }

        public override void SetFloat(float val)
        {
            _val = val;
        }

        public override string ToString()
        {
            return _val.ToString();
        }

        public override void Zero()
        {
            SetFloat(0f);
        }
    }

    #endregion

    #region Nested type: VarInt16

    public class VarInt16 : Var, IEquatable<VarInt16>
    {
        private const short _Zero = 0;
        public short _val;

        public VarInt16()
        {
            Zero();
        }

        public VarInt16(short val)
        {
            SetInt16(val);
        }

        public override bool IsZero
        {
            get { return _val.Equals(0); }
        }

        #region IEquatable<VarInt16> Members

        public bool Equals(VarInt16 other)
        {
            return ((other != null) && (other._val == _val));
        }

        #endregion

        public override Var Clone()
        {
            return new VarInt16(_val);
        }

        public override bool Equals(object obj)
        {
            return (((obj != null) && (obj.GetType() == base.GetType())) && Equals((VarInt16) obj));
        }

        public override int GetHashCode()
        {
            return _val.GetHashCode();
        }

        public override short Int16()
        {
            return _val;
        }

        public static bool operator ==(VarInt16 x, VarInt16 y)
        {
            return (ReferenceEquals(x, y) || (((x != null) && (y != null)) && x.Equals(y)));
        }

        public static bool operator !=(VarInt16 x, VarInt16 y)
        {
            return !(x == y);
        }

        public override void SetInt16(short val)
        {
            _val = val;
        }

        public override string ToString()
        {
            return _val.ToString();
        }

        public override void Zero()
        {
            SetInt16(0);
        }
    }

    #endregion

    #region Nested type: VarInt32

    public class VarInt32 : Var, IEquatable<VarInt32>
    {
        private const int _Zero = 0;
        public int _val;

        public VarInt32()
        {
            Zero();
        }

        public VarInt32(int val)
        {
            SetInt32(val);
        }

        public override bool IsZero
        {
            get { return _val.Equals(0); }
        }

        #region IEquatable<VarInt32> Members

        public bool Equals(VarInt32 other)
        {
            return ((other != null) && (other._val == _val));
        }

        #endregion

        public override Var Clone()
        {
            return new VarInt32(_val);
        }

        public override bool Equals(object obj)
        {
            return (((obj != null) && (obj.GetType() == base.GetType())) && Equals((VarInt32) obj));
        }

        public override int GetHashCode()
        {
            return _val.GetHashCode();
        }

        public override int Int32()
        {
            return _val;
        }

        public static bool operator ==(VarInt32 x, VarInt32 y)
        {
            return (ReferenceEquals(x, y) || (((x != null) && (y != null)) && x.Equals(y)));
        }

        public static bool operator !=(VarInt32 x, VarInt32 y)
        {
            return !(x == y);
        }

        public override void SetInt32(int val)
        {
            _val = val;
        }

        public override string ToString()
        {
            return _val.ToString();
        }

        public override void Zero()
        {
            SetInt32(0);
        }
    }

    #endregion

    #region Nested type: VarInt64

    public class VarInt64 : Var, IEquatable<VarInt64>
    {
        private const long _Zero = 0L;
        public long _val;

        public VarInt64()
        {
            Zero();
        }

        public VarInt64(long val)
        {
            SetInt64(val);
        }

        public override bool IsZero
        {
            get { return _val.Equals(0); }
        }

        #region IEquatable<VarInt64> Members

        public bool Equals(VarInt64 other)
        {
            return ((other != null) && (other._val == _val));
        }

        #endregion

        public override Var Clone()
        {
            return new VarInt64(_val);
        }

        public override bool Equals(object obj)
        {
            return (((obj != null) && (obj.GetType() == base.GetType())) && Equals((VarInt64) obj));
        }

        public override int GetHashCode()
        {
            return _val.GetHashCode();
        }

        public override long Int64()
        {
            return _val;
        }

        public static bool operator ==(VarInt64 x, VarInt64 y)
        {
            return (ReferenceEquals(x, y) || (((x != null) && (y != null)) && x.Equals(y)));
        }

        public static bool operator !=(VarInt64 x, VarInt64 y)
        {
            return !(x == y);
        }

        public override void SetInt64(long val)
        {
            _val = val;
        }

        public override string ToString()
        {
            return _val.ToString();
        }

        public override void Zero()
        {
            SetInt64(0);
        }
    }

    #endregion

    #region Nested type: VarInt8

    public class VarInt8 : Var, IEquatable<VarInt8>
    {
        private const byte _Zero = 0;
        public byte _val;

        public VarInt8()
        {
            Zero();
        }

        public VarInt8(byte val)
        {
            SetInt8(val);
        }

        public override bool IsZero
        {
            get { return _val.Equals(0); }
        }

        #region IEquatable<VarInt8> Members

        public bool Equals(VarInt8 other)
        {
            return ((other != null) && (other._val == _val));
        }

        #endregion

        public override Var Clone()
        {
            return new VarInt8(_val);
        }

        public override bool Equals(object obj)
        {
            return (((obj != null) && (obj.GetType() == base.GetType())) && Equals((VarInt8) obj));
        }

        public override int GetHashCode()
        {
            return _val.GetHashCode();
        }

        public override byte Int8()
        {
            return _val;
        }

        public static bool operator ==(VarInt8 x, VarInt8 y)
        {
            return (ReferenceEquals(x, y) || (((x != null) && (y != null)) && x.Equals(y)));
        }

        public static bool operator !=(VarInt8 x, VarInt8 y)
        {
            return !(x == y);
        }

        public override void SetInt8(byte val)
        {
            _val = val;
        }

        public override string ToString()
        {
            return _val.ToString();
        }

        public override void Zero()
        {
            SetInt8(0);
        }
    }

    #endregion

    #region Nested type: VarObj

    public class VarObj : Var, IEquatable<VarObj>
    {
        private static ObjId _Zero = ObjId.Empty;
        public ObjId _val;

        public VarObj()
        {
            Zero();
        }

        public VarObj(ObjId val)
        {
            SetObj(val);
        }

        public override bool IsZero
        {
            get { return _val != null && _val.Equals(_Zero); }
        }

        #region IEquatable<VarObj> Members

        public bool Equals(VarObj other)
        {
            return ((other != null) && (other._val == _val));
        }

        #endregion

        public override Var Clone()
        {
            return new VarObj(_val);
        }

        public override bool Equals(object obj)
        {
            return (((obj != null) && (obj.GetType() == base.GetType())) && Equals((VarObj) obj));
        }

        public override int GetHashCode()
        {
            return _val.GetHashCode();
        }

        public override ObjId Obj()
        {
            return _val;
        }

        public static bool operator ==(VarObj x, VarObj y)
        {
            return (ReferenceEquals(x, y) || (((x != null) && (y != null)) && x.Equals(y)));
        }

        public static bool operator !=(VarObj x, VarObj y)
        {
            return !(x == y);
        }

        public override void SetObj(ObjId val)
        {
            _val = val;
        }

        public override string ToString()
        {
            return _val.ToString();
        }

        public override void Zero()
        {
            if (_Zero.Cls != 0)
            {
                _Zero.Id = 0;
                _Zero.Cls = 0;
                _Zero.Owner = 0;
            }

            SetObj(_Zero);
        }
    }

    #endregion

    #region Nested type: VarStr

    public class VarStr : Var, IEquatable<VarStr>
    {
        private static readonly string _Zero = string.Empty;
        public string _val;

        public VarStr()
        {
            Zero();
        }

        public VarStr(string val)
        {
            SetStr(val);
        }

        public override bool IsZero
        {
            get { return _val.Equals(_Zero); }
        }

        #region IEquatable<VarStr> Members

        public bool Equals(VarStr other)
        {
            return ((other != null) && (other._val == _val));
        }

        #endregion

        public override Var Clone()
        {
            return new VarStr(_val);
        }

        public override bool Equals(object obj)
        {
            return (((obj != null) && (obj.GetType() == base.GetType())) && Equals((VarStr) obj));
        }

        public override int GetHashCode()
        {
            return _val.GetHashCode();
        }

        public static bool operator ==(VarStr x, VarStr y)
        {
            return (ReferenceEquals(x, y) || (((x != null) && (y != null)) && x.Equals(y)));
        }

        public static bool operator !=(VarStr x, VarStr y)
        {
            return !(x == y);
        }

        public override void SetStr(string val)
        {
            _val = val;
        }

        public override string Str()
        {
            return _val;
        }

        public override string ToString()
        {
            return _val;
        }

        public override void Zero()
        {
            SetStr(_Zero);
        }
    }

    #endregion
}