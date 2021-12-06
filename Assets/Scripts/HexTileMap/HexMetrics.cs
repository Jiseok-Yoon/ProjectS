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

		// �������� ������ ��ǥ
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
		/// �ش� ������ ù �������� ��ȯ�մϴ�.
		/// </summary>
		/// <param name="direction"></param>
		/// <returns></returns>
		public static Vector3 GetFirstCorner(HexDirection direction)
		{
			return corners[(int)direction];
		}
		/// <summary>
		/// �ش� ������ �ι�° �������� ��ȯ�մϴ�.
		/// </summary>
		/// <param name="direction"></param>
		/// <returns></returns>
		public static Vector3 GetSecondCorner(HexDirection direction)
		{
			return corners[(int)direction + 1];
		}
	}
}
