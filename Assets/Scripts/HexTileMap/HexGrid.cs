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
			// 그리드 캔버스에 좌표 텍스트를 띄울 캔버스를 가져옵니다.
			gridCanvas = GetComponentInChildren<Canvas>();
			// HexMesh를 가져옵니다.
			hexMesh = GetComponentInChildren<HexMesh>();

			cells = new HexCell[height * width];

			// HexCell을 인스턴스화 합니다.
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
			// 메쉬를 삼각측량합니다.
			hexMesh.Triangulate(cells);
		}

		/// <summary>
		/// 셀 터치시 육각 좌표계로 변환한 좌표를 가져옵니다.
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
		/// x좌표와 z좌표, 셀의 인덱스를 가지고 셀을 생성합니다.
		/// </summary>
		/// <param name="x">셀이 위치할 육각 좌표계 x축 좌표</param>
		/// <param name="z">셀이 위치할 육각 좌표계 y축 좌표</param>
		/// <param name="i">셀의 인덱스 번호</param>
		void CreateCell(int x, int z, int i)
		{
			Vector3 position;
			// 각 셀의 x축은 내부 반지름의 2배씩 떨어져있습니다.
			// x 축은 홀수 행마다 내부 반지름만큼 들어갑니다.
			position.x = (x + z * 0.5f - z / 2) * (HexMetrics.innerRadius * 2f);
			position.y = 0f;
			// 각 셀의 z축은 외부 반지름의 1.5배씩 떨어져있습니다.
			position.z = z * (HexMetrics.outerRadius * 1.5f);

			// 셀 객체 생성
			HexCell cell = cells[i] = Instantiate<HexCell>(cellPrefab);
			cell.transform.SetParent(transform, false);
			cell.transform.localPosition = position;
			// 육각 좌표 생성
			cell.coordinates = HexCoordinates.FromOffsetCoordinates(x, z);
			// 색 지정
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


			// 라벨은 에디터에서만 생성.
#if UNITY_EDITOR
			// 좌표 보여줄 라벨 생성
			Text label = Instantiate<Text>(cellLabelPrefab);
			label.rectTransform.SetParent(gridCanvas.transform, false);
			label.rectTransform.anchoredPosition =
				new Vector2(position.x, position.z);
			label.text = cell.coordinates.ToStringOnSeparateLines();
#endif

		}


	}
}
