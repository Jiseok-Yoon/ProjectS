using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static ProjectS.Define.HexTileMap;

namespace ProjectS.TileMap
{
	// �� �޽��� �޽� ���Ϳ� �޽� ������ ������Ʈ�� �䱸�մϴ�.
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
		/// ������ �ﰢ�����մϴ�.
		/// </summary>
		/// <param name="cells">���� ���</param>
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
		/// �� �ϳ��� �ﰢ�����մϴ�.
		/// </summary>
		/// <param name="cell"></param>
		void Triangulate (HexCell cell)
        {
			for (HexDirection d = HexDirection.NE; d <= HexDirection.NW; d++)
			{
				Triangulate(d, cell);
			}
		}
		/// <summary>
		/// ���� 6������ �ﰢ���� �����մϴ�.
		/// </summary>
		/// <param name="direction"></param>
		/// <param name="cell"></param>
		void Triangulate(HexDirection direction, HexCell cell)
		{
			Vector3 center = cell.transform.localPosition;

			AddTriangle(center,
				center + HexMetrics.GetFirstCorner(direction),
				center + HexMetrics.GetSecondCorner(direction));
			HexCell neighbor = cell.GetNeighbor(direction) ?? cell;
			AddTriangleColor(cell.color, neighbor.color, neighbor.color);
		}
		/// <summary>
		/// �ﰢ���� ���� �߰��մϴ�. ������ ���� ���� ���� �˴ϴ�.
		/// </summary>
		/// <param name="c1"></param>
		/// <param name="c2"></param>
		/// <param name="c3"></param>
		void AddTriangleColor(Color c1, Color c2, Color c3)
		{
			colors.Add(c1);
			colors.Add(c2);
			colors.Add(c3);
		}
		/// <summary>
		/// �޽��� �ﰢ���� �߰��մϴ�.
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
