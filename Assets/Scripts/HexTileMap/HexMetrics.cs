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

		// 육각셀의 꼭짓점 좌표
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
		/// 해당 방향의 첫 꼭짓점을 반환합니다.
		/// </summary>
		/// <param name="direction"></param>
		/// <returns></returns>
		public static Vector3 GetFirstCorner(HexDirection direction)
		{
			return corners[(int)direction];
		}
		/// <summary>
		/// 해당 방향의 두번째 꼭짓점을 반환합니다.
		/// </summary>
		/// <param name="direction"></param>
		/// <returns></returns>
		public static Vector3 GetSecondCorner(HexDirection direction)
		{
			return corners[(int)direction + 1];
		}
	}
}
