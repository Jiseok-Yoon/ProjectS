using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace ProjectS.TileMap
{
	// �� �޽��� �޽� ���Ϳ� �޽� ������ ������Ʈ�� �䱸�մϴ�.
	[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
	public class HexMesh : MonoBehaviour
	{
		Mesh hexMesh;
		List<Vector3> vertices;
		List<int> triangles;

		void Awake()
		{
			GetComponent<MeshFilter>().mesh = hexMesh = new Mesh();
			hexMesh.name = "Hex Mesh";
			vertices = new List<Vector3>();
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
			triangles.Clear();
			for (int i = 0; i < cells.Length; i++)
			{
				Triangulate(cells[i]);
			}
			hexMesh.vertices = vertices.ToArray();
			hexMesh.triangles = triangles.ToArray();
			hexMesh.RecalculateNormals();
		}

		/// <summary>
		/// �� �ϳ��� �ﰢ�����մϴ�.
		/// </summary>
		/// <param name="cell">�ﰢ������ ��</param>
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
			}
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
