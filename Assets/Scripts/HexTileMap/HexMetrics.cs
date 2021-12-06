using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static ProjectS.Define.HexTileMap;

namespace ProjectS.TileMap
{
	public static class HexMetrics
	{
		public const float outerRadius = 10f;
		public const float innerRadius = outerRadius * 0.866025404f;

		// À°°¢¼¿ÀÇ ²ÀÁþÁ¡ ÁÂÇ¥
		static Vector3[] corners = {
		new Vector3(0f, 0f, outerRadius),
		new Vector3(innerRadius, 0f, 0.5f * outerRadius),
		new Vector3(innerRadius, 0f, -0.5f * outerRadius),
		new Vector3(0f, 0f, -outerRadius),
		new Vector3(-innerRadius, 0f, -0.5f * outerRadius),
		new Vector3(-innerRadius, 0f, 0.5f * outerRadius),
		new Vector3(0f, 0f, outerRadius)
		};
		/// <summary>
		/// ÇØ´ç ¹æÇâÀÇ Ã¹ ²ÀÁþÁ¡À» ¹ÝÈ¯ÇÕ´Ï´Ù.
		/// </summary>
		/// <param name="direction"></param>
		/// <returns></returns>
		public static Vector3 GetFirstCorner(HexDirection direction)
		{
			return corners[(int)direction];
		}
		/// <summary>
		/// ÇØ´ç ¹æÇâÀÇ µÎ¹øÂ° ²ÀÁþÁ¡À» ¹ÝÈ¯ÇÕ´Ï´Ù.
		/// </summary>
		/// <param name="direction"></param>
		/// <returns></returns>
		public static Vector3 GetSecondCorner(HexDirection direction)
		{
			return corners[(int)direction + 1];
		}
	}
}
