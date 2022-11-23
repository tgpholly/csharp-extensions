using System;
using System.Text;
using System.IO;
using System.Runtime.CompilerServices; 

namespace HollyExtensions.BinaryTools
{
	// This class is an extension of BinaryReader that adds some custom readers for custom writers added in hBinaryWriter
	public class hBinaryReader : BinaryReader
	{
		public hBinaryReader(Stream stream)
			: base(stream)
		{
		}

		/// <summary>
		/// Reads a packed float. A very packed format that allows for decimals between 0 and 1 to be crushed into a single unsigned byte. Has roughly 1-2 decimal(s) of precision.
		/// </summary>
		/// <returns>The unpacked float.</returns>
		public float ReadPackedFloat()
		{
			float packedFloat = ReadByte() / 255f;

			return (float)Math.Round(packedFloat * 100f) / 100f;
		}

		/// <summary>
		/// Reads a float from the BinaryReader
		/// </summary>
		/// <note>This is the same as BinaryReader.ReadSingle but this is easier to remember</note>
		/// <returns>The read float</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public float ReadFloat()
		{
			return ReadSingle();
		}

		/// <summary>
		/// Custom network string format (ASCII) but can only have 255 chars
		/// </summary>
		/// <returns>The decoded short string.</returns>
		public string ReadShortString()
		{
			byte stringLength = ReadByte();

			return Encoding.ASCII.GetString(ReadBytes(stringLength));
		}

		/// <summary>
		/// Custom network string format (ASCII)
		/// </summary>
		/// <returns>The decoded string.</returns>
		public override string ReadString()
		{
			short stringLength = ReadInt16();

			return Encoding.ASCII.GetString(ReadBytes(stringLength));
		}
	}
}
