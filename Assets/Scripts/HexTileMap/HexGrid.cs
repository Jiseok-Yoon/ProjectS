using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static ProjectS.Define.HexTileMap;

namespace ProjectS.TileMap
{
	public class HexGrid : MonoBehaviour
	{
		public int width = 6;
		public int height = 6;
		public HexCell cellPrefab;
		private HexCell[] cells;
		public Text cellLabelPrefab;
		private HexMesh hexMesh;
		private Canvas gridCanvas;
		public Color defaultColor = Color.white;
		public Color touchedColor = Color.magenta;

		void Awake()
		{
			// �׸��� ĵ������ ��ǥ �ؽ�Ʈ�� ��� ĵ������ �����ɴϴ�.
			gridCanvas = GetComponentInChildren<Canvas>();
			// HexMesh�� �����ɴϴ�.
			hexMesh = GetComponentInChildren<HexMesh>();

			cells = new HexCell[height * width];

			// HexCell�� �ν��Ͻ�ȭ �մϴ�.
			for (int z = 0, i = 0; z < height; z++)
			{
				for (int x = 0; x < width; x++)
				{
					CreateCell(x, z, i++);
				}
			}
		}
		void Start()
		{
			// �޽��� �ﰢ�����մϴ�.
			hexMesh.Triangulate(cells);
		}

		/// <summary>
		/// �� ��ġ�� ���� ��ǥ��� ��ȯ�� ��ǥ�� �����ɴϴ�.
		/// </summary>
		/// <param name="position"></param>
		public void ColorCell (Vector3 position, Color color)
		{
			position = transform.InverseTransformPoint(position);
            HexCoordinates coordinates = HexCoordinates.FromPosition(position);
			int index = coordinates.X + coordinates.Z * width + coordinates.Z / 2;
			HexCell cell = cells[index];
			cell.color = color;
			hexMesh.Triangulate(cells);
		}

		/// <summary>
		/// x��ǥ�� z��ǥ, ���� �ε����� ������ ���� �����մϴ�.
		/// </summary>
		/// <param name="x">���� ��ġ�� ���� ��ǥ�� x�� ��ǥ</param>
		/// <param name="z">���� ��ġ�� ���� ��ǥ�� y�� ��ǥ</param>
		/// <param name="i">���� �ε��� ��ȣ</param>
		void CreateCell(int x, int z, int i)
		{
			Vector3 position;
			// �� ���� x���� ���� �������� 2�辿 �������ֽ��ϴ�.
			// x ���� Ȧ�� �ึ�� ���� ��������ŭ ���ϴ�.
			position.x = (x + z * 0.5f - z / 2) * (HexMetrics.innerRadius * 2f);
			position.y = 0f;
			// �� ���� z���� �ܺ� �������� 1.5�辿 �������ֽ��ϴ�.
			position.z = z * (HexMetrics.outerRadius * 1.5f);

			// �� ��ü ����
			HexCell cell = cells[i] = Instantiate<HexCell>(cellPrefab);
			cell.transform.SetParent(transform, false);
			cell.transform.localPosition = position;
			// ���� ��ǥ ����
			cell.coordinates = HexCoordinates.FromOffsetCoordinates(x, z);
			// �� ����
			cell.color = defaultColor;
			
			if (x > 0)
			{
				cell.SetNeighbor(HexDirection.W, cells[i - 1]);
			}
			if (z > 0)
			{
				if ((z & 1) == 0)
				{
					cell.SetNeighbor(HexDirection.SE, cells[i - width]);
					if (x > 0)
					{
						cell.SetNeighbor(HexDirection.SW, cells[i - width - 1]);
					}
				}
				else
				{
					cell.SetNeighbor(HexDirection.SW, cells[i - width]);
					if (x < width - 1)
					{
						cell.SetNeighbor(HexDirection.SE, cells[i - width + 1]);
					}
				}
			}


			// ���� �����Ϳ����� ����.
#if UNITY_EDITOR
			// ��ǥ ������ �� ����
			Text label = Instantiate<Text>(cellLabelPrefab);
			label.rectTransform.SetParent(gridCanvas.transform, false);
			label.rectTransform.anchoredPosition =
				new Vector2(position.x, position.z);
			label.text = cell.coordinates.ToStringOnSeparateLines();
#endif

		}


	}
}
