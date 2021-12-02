using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace ProjectS.TileMap
{
	// 헥스 메쉬는 메쉬 필터와 메쉬 렌더러 컴포넌트를 요구합니다.
	[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
	public class HexMesh : MonoBehaviour
	{
		private Mesh hexMesh;
		private List<Vector3> vertices;
		private List<int> triangles;
		private MeshCollider meshCollider;
		private List<Color> colors;

		void Awake()
		{
			GetComponent<MeshFilter>().mesh = hexMesh = new Mesh();
			meshCollider = gameObject.AddComponent<MeshCollider>();
			hexMesh.name = "Hex Mesh";
			vertices = new List<Vector3>();
			colors = new List<Color>();

			triangles = new List<int>();
		}
		/// <summary>
		/// 셀들을 삼각측량합니다.
		/// </summary>
		/// <param name="cells">셀들 목록</param>
		public void Triangulate(HexCell[] cells)
		{
			hexMesh.Clear();
			vertices.Clear();
			colors.Clear();
			triangles.Clear();
			for (int i = 0; i < cells.Length; i++)
			{
				Triangulate(cells[i]);
			}
			hexMesh.vertices = vertices.ToArray();
			hexMesh.colors = colors.ToArray();
			hexMesh.triangles = triangles.ToArray();
			hexMesh.RecalculateNormals();
			meshCollider.sharedMesh = hexMesh;
		}

		/// <summary>
		/// 셀 하나를 삼각측량합니다.
		/// </summary>
		/// <param name="cell">삼각측량할 셀</param>
		void Triangulate(HexCell cell)
		{
			Vector3 center = cell.transform.localPosition;
			for (int i = 0; i < 6; i++)
			{
				AddTriangle(
					center,
					center + HexMetrics.corners[i],
					center + HexMetrics.corners[i + 1]
				);
				AddTriangleColor(cell.color);
			}
		}
		/// <summary>
		/// 삼각형에 색을 추가합니다.
		/// </summary>
		/// <param name="color"></param>
		void AddTriangleColor(Color color)
		{
			colors.Add(color);
			colors.Add(color);
			colors.Add(color);
		}
		/// <summary>
		/// 메쉬에 삼각형을 추가합니다.
		/// </summary>
		/// <param name="v1"></param>
		/// <param name="v2"></param>
		/// <param name="v3"></param>
		void AddTriangle(Vector3 v1, Vector3 v2, Vector3 v3)
		{
			int vertexIndex = vertices.Count;
			vertices.Add(v1);
			vertices.Add(v2);
			vertices.Add(v3);
			triangles.Add(vertexIndex);
			triangles.Add(vertexIndex + 1);
			triangles.Add(vertexIndex + 2);
		}
	}

}
