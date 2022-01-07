using System;
using System.IO;
using System.Text;

namespace HollyExtensions.BinaryTools
{
    public class hBinaryWriter : BinaryWriter
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="T:Hacknet.Multi.hBinaryWriter"/> class. Pre-allocation, highly suggest using this.
        /// Initializes a new instance of the <see cref="T:HollyExtensions.BinaryTools.hBinaryWriter"/> class. Pre-allocation, highly suggest using this.
        /// </summary>
        /// <param name="size">Size.</param>
        public hBinaryWriter(int size)
            : base(new MemoryStream(size))
        {
        }

        // TODO: Confirm this works. I don't use C# often enough and my current project has no need for this.
        /// <summary>
        /// Initializes a new instance of the <see cref="T:Hacknet.Multi.hBinaryWriter"/> class. Dynamic allocations enabled, pass size to pre-allocate
        /// Initializes a new instance of the <see cref="T:HollyExtensions.BinaryTools.hBinaryWriter"/> class. Dynamic allocations enabled, pass size to pre-allocate
        /// </summary>
        public hBinaryWriter()
            : base(new MemoryStream())
        {
        }

        /// <summary>
        /// Converts this BinaryWriter to a byte array
        /// </summary>
        /// <returns>byte[]</returns>
        public byte[] ToArray()
        {
            return ((MemoryStream)BaseStream).ToArray();
        }

        /// <summary>
        /// Explicit byte writing
        /// </summary>
        /// <returns>hBinaryWriter</returns>
        /// <param name="value">byte</param>
        public hBinaryWriter WriteByte(byte value)
        {
            Write(value);
            return this;
        }

        /// <summary>
        /// Explicit short writing
        /// </summary>
        /// <returns>hBinaryWriter</returns>
        /// <param name="value">short</param>
        public hBinaryWriter WriteShort(short value)
        {
            Write(value);
            return this;
        }

        /// <summary>
        /// Explicit int writing
        /// </summary>
        /// <returns>hBinaryWriter</returns>
        /// <param name="value">int</param>
        public hBinaryWriter WriteInt(int value)
        {
            Write(value);
            return this;
        }

        /// <summary>
        /// Explicit long writing
        /// </summary>
        /// <returns>hBinaryWriter</returns>
        /// <param name="value">long</param>
        public hBinaryWriter WriteLong(long value)
        {
            Write(value);
            return this;
        }

        /// <summary>
        /// Writes a packed float. A very packed format that allows for decimals between 0 and 1 to be crushed into a single unsigned byte. Has roughly 1-2 decimal(s) of precision.
        /// </summary>
        /// <returns>The packed float.</returns>
        /// <param name="value">Value.</param>
        public hBinaryWriter WritePackedFloat(float value)
        {
            if (value < 0 || value > 1) throw new Exception("WritePackedFloat only supports values between 0 and 1");
            byte packedFloat = (byte)(value * 255f);
            WriteByte(packedFloat);
            return this;
        }

        /// <summary>
        /// Explicit float writing
        /// </summary>
        /// <returns>hBinaryWriter</returns>
        /// <param name="value">float</param>
        public hBinaryWriter WriteFloat(float value)
        {
            Write(value);
            return this;
        }

        /// <summary>
        /// Explicit double writing
        /// </summary>
        /// <returns>hBinaryWriter</returns>
        /// <param name="value">double</param>
        public hBinaryWriter WriteDouble(double value)
        {
            Write(value);
            return this;
        }

        // NOTE: This only saves 1 byte lmao
        /// <summary>
        /// Custom string format (ASCII) but can only have 255 chars
        /// Custom network string format (ASCII) but can only have 255 chars. This only saves 1 byte over regular WriteString.
        /// </summary>
        /// <returns>hBinaryWriter</returns>
        /// <param name="value">string</param>
        /// <param name="value">String to write, must be less than 256 chars</param>
        public hBinaryWriter WriteShortString(string value)
        {
            if (value.Length > 255) throw new Exception("String is too long. Can only write a string of max size 255 chars");
            WriteByte((byte)value.Length);
            Write(Encoding.ASCII.GetBytes(value));
            return this;
        }

        /// <summary>
        /// Custom string format (ASCII)
        /// </summary>
        /// <returns>hBinaryWriter</returns>
        /// <param name="value">string</param>
        /// <param name="value">String to write, must be less than 65536 chars</param>
        public hBinaryWriter WriteString(string value)
        {
            if (value.Length > 65280) throw new Exception("String is too long. Can only write a string of max size 65280 chars");
            if (value.Length > 65536) throw new Exception("String is too long. Can only write a string of max size 65536 chars");
            WriteShort((short)value.Length);
            Write(Encoding.ASCII.GetBytes(value));
            return this;
        }
    }
}
