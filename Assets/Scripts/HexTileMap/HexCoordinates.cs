using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectS.TileMap
{
	[Serializable]
	public struct HexCoordinates
	{
		public int X { get; private set; }

		public int Z { get; private set; }

		public int Y
		{
			get
			{
				return -X - Z;
			}
		}
		public HexCoordinates(int x, int z)
		{
			X = x;
			Z = z;
		}
		/// <summary>
		/// 오프셋 좌표에서 육각 좌표로 변환합니다.
		/// </summary>
		/// <param name="x"></param>
		/// <param name="z"></param>
		/// <returns></returns>
		public static HexCoordinates FromOffsetCoordinates(int x, int z)
		{
			return new HexCoordinates(x - z / 2, z);
		}

		public override string ToString()
		{
			return $"({X}, {Y}, {Z})";
		}

		public string ToStringOnSeparateLines()
		{
			return $"({X}\n{Y}\n{Z})";
		}
	}
}
