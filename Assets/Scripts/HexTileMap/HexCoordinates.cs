using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace ProjectS.TileMap
{
	[Serializable]
	public struct HexCoordinates
	{
		[SerializeField]
		private int x, z;
		public int X { get => x; private set => x = value; }

		public int Z { get => z; private set => z = value; }

		public int Y
		{
			get
			{
				return -X - Z;
			}
		}
		public HexCoordinates(int x, int z)
		{
			this.x = x;
			this.z = z;
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

		/// <summary>
		/// 한 줄에 XYZ 좌표를 나타냅니다.
		/// </summary>
		/// <returns></returns>
		public override string ToString()
		{
			return $"({X}, {Y}, {Z})";
		}
		/// <summary>
		/// 여러줄로 XYZ 좌표를 나타냅니다.
		/// </summary>
		/// <returns></returns>
		public string ToStringOnSeparateLines()
		{
			return $"{X}\n{Y}\n{Z}";
		}
		/// <summary>
		/// 그리드의 로컬 위치에서 셀의 좌표를 반환합니다.
		/// </summary>
		/// <param name="position"></param>
		/// <returns></returns>
		public static HexCoordinates FromPosition(Vector3 position)
		{
			float x = position.x / (HexMetrics.innerRadius * 2f);
			float y = -x;

			float offset = position.z / (HexMetrics.outerRadius * 3f);
			x -= offset;
			y -= offset;

			int iX = Mathf.RoundToInt(x);
			int iY = Mathf.RoundToInt(y);
			int iZ = Mathf.RoundToInt(-x - y);

			return new HexCoordinates(iX, iZ);
		}
	}
}
