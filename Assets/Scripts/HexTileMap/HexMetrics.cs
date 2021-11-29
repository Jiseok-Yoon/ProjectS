using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace ProjectS.TileMap
{
	public static class HexMetrics
	{
		public const float outerRadius = 10f;
		public const float innerRadius = outerRadius * 0.866025404f;

		// À°°¢¼¿ÀÇ ²ÀÁşÁ¡ ÁÂÇ¥
		public static Vector3[] corners = {
		new Vector3(0f, 0f, outerRadius),
		new Vector3(innerRadius, 0f, 0.5f * outerRadius),
		new Vector3(innerRadius, 0f, -0.5f * outerRadius),
		new Vector3(0f, 0f, -outerRadius),
		new Vector3(-innerRadius, 0f, -0.5f * outerRadius),
		new Vector3(-innerRadius, 0f, 0.5f * outerRadius),
		new Vector3(0f, 0f, outerRadius)
		};
	}
}
