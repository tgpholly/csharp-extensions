using System;
using System.Text;
using System.IO;

namespace HollyExtensions.BinaryTools
{
    public class hBinaryReader : BinaryReader
    {
        public hBinaryReader(Stream s)
            : base(s)
        {
        }

        public float ReadPackedFloat()
        {
            float packedFloat = ReadByte() / 255f;

            return (float)Math.Round(packedFloat * 100f) / 100f;
        }

        public string ReadShortString()
        {
            byte stringLength = ReadByte();

            return Encoding.ASCII.GetString(ReadBytes(stringLength));
        }

        public override string ReadString()
        {
            short stringLength = ReadInt16();

            return Encoding.ASCII.GetString(ReadBytes(stringLength));
        }
    }
}
