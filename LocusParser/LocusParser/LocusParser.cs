using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocusParser
{
    class LocusFrame
    {
        private uint _UnixTime;
        private byte _FixFlag;
        private float _Latitude;
        private float _Longitude;
        private ushort _Height;
        private byte _Checksum;


        public uint UnixTime
        {
            get { return _UnixTime; }
            set { _UnixTime = value; }
        }

        public byte FixFlag
        {
            get { return _FixFlag; }
            set { _FixFlag = value; }
        }

        public float Latitude
        {
            get { return _Latitude; }
            set { _Latitude = value; }
        }

        public float Longitude
        {
            get { return _Longitude; }
            set { _Longitude = value; }
        }
        public ushort Height
        {
            get { return _Height; }
            set { _Height = value; }
        }

        public byte Checksum
        {
            get { return _Checksum; }
            set { _Checksum = value; }
        }
    }
}
