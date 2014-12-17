using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using NyMQ.Core.Socket;
using Oct.Framework.Socket.BaseDef;
using Oct.Framework.Socket.Socket;
using ProtoBuf;

namespace Oct.Framework.Socket.Protocal
{
    [Serializable]
    [ProtoContract]
    public class VarList : IDisposable
    {
        #region Fields
        [ProtoMember(1, Name = @"_bools", DataFormat = DataFormat.Default)]
        private readonly List<bool> _bools = new List<bool>();
        [ProtoMember(2, Name = @"_doubles", DataFormat = DataFormat.TwosComplement)]
        private readonly List<double> _doubles = new List<double>();
        [ProtoMember(3, Name = @"_floats", DataFormat = DataFormat.FixedSize)]
        private readonly List<float> _floats = new List<float>();
        [ProtoMember(5, Name = @"_int16s", DataFormat = DataFormat.TwosComplement)]
        private readonly List<int> _int16s = new List<int>();
        [ProtoMember(6, Name = @"_int64s", DataFormat = DataFormat.TwosComplement)]
        private readonly List<long> _int64s = new List<long>();
        [ProtoMember(7, Name = @"_int8s", DataFormat = DataFormat.TwosComplement)]
        private readonly List<int> _int8s = new List<int>();
        [ProtoMember(8, Name = @"_ints", DataFormat = DataFormat.TwosComplement)]
        private readonly List<int> _ints = new List<int>();
        [ProtoMember(13, Name = @"_objs", DataFormat = DataFormat.Default)]
        private readonly List<ObjId> _objs = new List<ObjId>();
        [ProtoMember(9, Name = @"_strs", DataFormat = DataFormat.Default)]
        private readonly List<string> _strs = new List<string>();
        [ProtoMember(10, Name = @"_types", DataFormat = DataFormat.TwosComplement)]
        private readonly List<VarType> _types = new List<VarType>();
        [ProtoMember(11, Name = @"_voids", DataFormat = DataFormat.Default)]
        private readonly List<byte[]> _voids = new List<byte[]>();

        [ProtoMember(4, Name = @"_indexs", DataFormat = DataFormat.TwosComplement)]
        private readonly List<int> _indexs = new List<int>();

        [ProtoMember(12, IsRequired = true, Name = @"Count", DataFormat = DataFormat.TwosComplement)]
        private int _count;
        #endregion

        #region Properties
        public bool Empty
        {
            get { return (_count == 0); }
        }

        public int Count
        {
            get { return _count; }
            set { _count = value; }
        }

        public int TypesCount
        {
            get { return _types.Count; }
        }

        public bool IsContainsBytes { get; private set; }
        #endregion

        #region Constructor
        public VarList()
        {

        }
        #endregion

        #region Public Methods
        public VarList AddBool(bool val)
        {
            VarType item = VarType.Bool;

            int count = _bools.Count;
            _types.Add(item);
            _indexs.Add(count);
            _bools.Add(val);
            Count++;
            return this;
        }

        public VarList AddFloat(float val)
        {
            VarType item = VarType.Float;
            int count = _floats.Count;
            _types.Add(item);
            _indexs.Add(count);
            _floats.Add(val);
            Count++;
            return this;
        }

        public VarList AddInt8(int val)
        {
            VarType item = VarType.Int8;
            int count = _ints.Count;
            _types.Add(item);
            _indexs.Add(count);
            _int8s.Add(val);
            Count++;
            return this;
        }

        public VarList AddInt16(int val)
        {
            VarType item = VarType.Int16;
            int count = _ints.Count;
            _types.Add(item);
            _indexs.Add(count);
            _int16s.Add(val);
            Count++;
            return this;
        }

        public VarList AddInt32(int val)
        {
            VarType item = VarType.Int32;
            int count = _ints.Count;
            _types.Add(item);
            _indexs.Add(count);
            _ints.Add(val);
            Count++;
            return this;
        }

        public VarList AddInt64(long val)
        {
            VarType item = VarType.Int64;
            int count = _ints.Count;
            _types.Add(item);
            _indexs.Add(count);
            _int64s.Add(val);
            Count++;
            return this;
        }

        public VarList AddDouble(double val)
        {
            VarType item = VarType.Double;
            int count = _ints.Count;
            _types.Add(item);
            _indexs.Add(count);
            _doubles.Add(val);
            Count++;
            return this;
        }

        public VarList AddObj(ObjId val)
        {
            VarType item = VarType.Obj;
            int count = _objs.Count;
            _types.Add(item);
            _indexs.Add(count);
            _objs.Add(val);
            Count++;
            return this;
        }

        public VarList AddStr(string val)
        {
            VarType item = VarType.Str;
            int count = _strs.Count;
            _types.Add(item);
            _indexs.Add(count);
            _strs.Add(string.IsNullOrEmpty(val) ? string.Empty : val);
            Count++;
            return this;
        }

        public bool IsContainsByte { get; private set; }

        public VarList AddVoid(byte[] val)
        {
            return AddVoid(val, 0, val.Length);
        }

        public VarList AddVoid(byte[] val, int start, int len)
        {
            VarType item = VarType.Void;
            int count = _voids.Count;
            var dst = new byte[len];
            System.Buffer.BlockCopy(val, start, dst, 0, len);
            _types.Add(item);
            _indexs.Add(count);
            _voids.Add(dst);
            Count++;
            IsContainsByte = true;
            return this;
        }

        public VarList Append(VarList other, int start, int count)
        {
            int num = start + count;
            for (int i = start; i < num; i++)
            {
                switch (other.Type(i))
                {
                    case VarType.Bool:
                        AddBool(other.Bool(i));
                        break;
                    case VarType.Int8:
                        AddInt8(other.Int8(i));
                        break;
                    case VarType.Int16:
                        AddInt16(other.Int16(i));
                        break;
                    case VarType.Int32:
                        AddInt32(other.Int32(i));
                        break;
                    case VarType.Int64:
                        AddInt64(other.Int32(i));
                        break;
                    case VarType.Float:
                        AddFloat(other.Float(i));
                        break;
                    case VarType.Double:
                        AddDouble(other.Double(i));
                        break;
                    case VarType.Str:
                        AddStr(other.Str(i));
                        break;
                    case VarType.Obj:
                        AddObj(other.Obj(i));
                        break;
                    case VarType.Void:
                        AddVoid(other.Void(i));
                        break;
                    default:
                        Debug.Assert(false, "Append");
                        break;
                }
            }

            //Count += count;
            return this;
        }

        //public void AddVal(IVal val)
        //{
        //    switch (val.Type)
        //    {
        //        case VarType.Bool:
        //            AddBool(val.Bool());
        //            break;
        //        case VarType.Int8:
        //            AddInt8(val.Int8());
        //            break;
        //        case VarType.Int16:
        //            AddInt16(val.Int16());
        //            break;
        //        case VarType.Int32:
        //            AddInt32(val.Int32());
        //            break;
        //        case VarType.Int64:
        //            AddInt64(val.Int64());
        //            break;
        //        case VarType.Float:
        //            AddFloat(val.Float());
        //            break;
        //        case VarType.Double:
        //            AddDouble(val.Double());
        //            break;
        //        case VarType.Str:
        //            AddStr(val.Str());
        //            break;
        //        case VarType.Obj:
        //            AddObj(val.Obj());
        //            break;
        //    }
        //}

        public void Clear()
        {
            Count = 0;
            _bools.Clear();
            _ints.Clear();
            _int16s.Clear();
            _int8s.Clear();
            _int64s.Clear();
            _floats.Clear();
            _objs.Clear();
            _voids.Clear();
            _types.Clear();
            _indexs.Clear();
            _doubles.Clear();
            _strs.Clear();
        }

        public VarList Concat(VarList other)
        {
            return Append(other, 0, other.Count);
        }

        public bool Bool(int index)
        {
            Debug.Assert(Type(index) == VarType.Bool, "type == VAR_TYPE.VARTYPE_BOOL");
            return _bools[_indexs[index]];
        }

        public float Float(int index)
        {
            Debug.Assert(Type(index) == VarType.Float, "type == VAR_TYPE.FLOAT");
            return _floats[_indexs[index]];
        }

        public int Int8(int index)
        {
            Debug.Assert(Type(index) == VarType.Int8, "type == VAR_TYPE.INT8");
            return _int8s[_indexs[index]];
        }

        public int Int16(int index)
        {
            Debug.Assert(Type(index) == VarType.Int16, "type == VAR_TYPE.Int16");
            return _int16s[_indexs[index]];
        }

        public int Int32(int index)
        {
            Debug.Assert(Type(index) == VarType.Int32, "type == VAR_TYPE.INT");
            return _ints[_indexs[index]];
        }

        public long Int64(int index)
        {
            Debug.Assert(Type(index) == VarType.Int64, "type == VAR_TYPE.INT64");
            return _int64s[_indexs[index]];
        }

        public double Double(int index)
        {
            Debug.Assert(Type(index) == VarType.Double, "type == VAR_TYPE.Double");
            return _doubles[_indexs[index]];
        }

        public ObjId Obj(int index)
        {
            Debug.Assert(Type(index) == VarType.Obj, "type == VAR_TYPE.OBJECT");
            return _objs[_indexs[index]];
        }

        public string Str(int index)
        {
            Debug.Assert(Type(index) == VarType.Str, "type == VAR_TYPE.VARTYPE_STRING");
            return _strs[_indexs[index]];
        }

        public byte[] Void(int index)
        {
            Debug.Assert(Type(index) == VarType.Void, "type == VAR_TYPE.VARTYPE_VOID");
            byte[] src = _voids[_indexs[index]];
            int length = src.Length;
            var dst = new byte[length];
            System.Buffer.BlockCopy(src, 0, dst, 0, length);
            return dst;
        }

        public VarType Type(int index)
        {
            Debug.Assert((index >= 0) && (index < Count), "index >= 0 && index < Count");
            return _types[index];
        }

        public override string ToString()
        {
            var builder = new StringBuilder(128);
            builder.Append(string.Format("count:{0}", Count));
            for (int i = 0; i < Count; i++)
            {
                switch (Type(i))
                {
                    case VarType.Bool:
                        builder.Append(",");
                        builder.Append(string.Format("bool:{0}", Bool(i)));
                        break;
                    case VarType.Int8:
                        builder.Append(",");
                        builder.Append(string.Format("int:{0}", Int8(i)));
                        break;
                    case VarType.Int16:
                        builder.Append(",");
                        builder.Append(string.Format("int:{0}", Int16(i)));
                        break;
                    case VarType.Int32:
                        builder.Append(",");
                        builder.Append(string.Format("int:{0}", Int32(i)));
                        break;
                    case VarType.Int64:
                        builder.Append(",");
                        builder.Append(string.Format("int:{0}", Int64(i)));
                        break;
                    case VarType.Float:
                        builder.Append(",");
                        builder.Append(string.Format("float:{0}", Float(i)));
                        break;
                    case VarType.Str:
                        builder.Append(",");
                        builder.Append(string.Format("str:{0}", Str(i)));
                        break;
                    case VarType.Obj:
                        builder.Append(",");
                        builder.Append(string.Format("obj:{0}", Obj(i)));
                        break;
                    case VarType.Void:
                        builder.Append(",");
                        builder.Append(string.Format("obj:{0}", Void(i)));
                        break;
                    default:
                        Debug.Assert(false, "ToString");
                        break;
                }
            }
            return builder.ToString();
        }

        public byte[] GetBytes()
        {
            using (var ms = new MemoryStream())
            {
                //包头定义
                ms.WriteByte((byte)SocketHelper.H1);
                ms.WriteByte((byte)SocketHelper.H2);
                ms.WriteByte((byte)SocketHelper.H3);
                ms.WriteByte((byte)SocketHelper.H4);
                //ph.GetHeadBytes(ms);
                ms.Position = 8;

                //消息序列化
                Serializer.Serialize(ms, this);
                int count = Convert.ToInt32(ms.Position - 8);

                //设置包体长度
                ms.Position = 4;
                ms.Write(BitConverter.GetBytes(count), 0, 4);

                return ms.ToArray();
            }
        }

        public void Dispose()
        {
            Count = 0;
            _bools.Clear();
            _ints.Clear();
            _int16s.Clear();
            _int8s.Clear();
            _int64s.Clear();
            _floats.Clear();
            _objs.Clear();
            _voids.Clear();
            _types.Clear();
            _indexs.Clear();
            _doubles.Clear();
            _strs.Clear();
        }

        #endregion
    }
}
