using System;
using ProtoBuf;

namespace Oct.Framework.Socket.BaseDef
{
    [ProtoContract]
    public struct ObjId : IComparable<ObjId>, IEquatable<ObjId>
    {
        #region Fields

        public static readonly ObjId Empty;
        private int _cls;
        private int _id;
        private int _owner;

        #endregion

        #region Properties

        [ProtoMember(1, IsRequired = false, Name = @"RowId")]
        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        [ProtoMember(2, IsRequired = false, Name = @"Cls", DataFormat = DataFormat.TwosComplement)]
        public int Cls
        {
            get { return _cls; }
            set { _cls = value; }
        }


        [ProtoMember(3, IsRequired = false, Name = @"Owner", DataFormat = DataFormat.TwosComplement)]
        public int Owner
        {
            get { return _owner; }
            set { _owner = value; }
        }

        #endregion

        #region Constructor

        static ObjId()
        {
            Empty = new ObjId(0, 0, 0);
        }

        public ObjId(int cls, int owner, int id)
        {
            _owner = default(int);
            _id = default(int);
            _cls = default(int);
            Cls = cls;
            Owner = owner;
            Id = id;
        }

        //public ObjId()
        //{
        //    _owner = default(int);
        //    _id = default(int);
        //    _cls = default(int);
        //}

        #endregion

        #region IComparable<ObjId> Members

        public int CompareTo(ObjId other)
        {
            return (GetHashCode() - other.GetHashCode());
        }

        #endregion

        #region IEquatable<ObjId> Members

        public bool Equals(ObjId other)
        {
            return (((other.Id == Id) && (other.Owner == Owner)) && (other.Cls == Cls));
        }

        #endregion

        #region Methods

        public bool IsTypeOf(int cls)
        {
            return Cls.Equals(cls);
        }

        public static bool IsSameLocal(ObjId x, ObjId y)
        {
            return x.Owner.Equals(y.Owner);
        }

        public string ToBag()
        {
            return string.Join(",", new object[] {Cls, Owner, Id});
        }

        public static ObjId FromBag(string s)
        {
            string[] s2 = s.Split(new[] { ',' });
            return new ObjId(Convert.ToInt32(s2[0]), Convert.ToInt32(s2[1]), Convert.ToInt32(s2[2]));
        }

        public static bool operator ==(ObjId x, ObjId y)
        {
            return x.Equals(y);
        }

        public static bool operator !=(ObjId x, ObjId y)
        {
            return !(x == y);
        }

        public override bool Equals(object obj)
        {
            return (((obj != null) && (obj.GetType() == base.GetType())) && Equals((ObjId) obj));
        }

        public override string ToString()
        {
            return string.Format("Cls:{0},Owner:{1},RowId:{2}", Cls, Owner, Id);
        }

        public override int GetHashCode()
        {
            return Id;
        }

        #endregion
    }
}