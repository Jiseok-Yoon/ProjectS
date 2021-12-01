using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

		void Update()
		{
			if (Input.GetMouseButton(0))
			{
				HandleInput();
			}
		}
		/// <summary>
		/// 마우스 좌클릭시 셀을 터치합니다.
		/// </summary>
		void HandleInput()
		{
			Ray inputRay = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;
			if (Physics.Raycast(inputRay, out hit))
			{
				TouchCell(hit.point);
			}
		}

		void TouchCell(Vector3 position)
		{
			position = transform.InverseTransformPoint(position);
			HexCoordinates coordinates = HexCoordinates.FromPosition(position);
			Debug.Log("touched at " + coordinates.ToString());
		}


		void CreateCell(int x, int z, int i)
		{

			Vector3 position;
			/// 각 셀의 x축은 내부 반지름의 2배씩 떨어져있습니다.
			/// x 축은 홀수 행마다 내부 반지름만큼 들어갑니다.
			position.x = (x + z * 0.5f - z / 2) * (HexMetrics.innerRadius * 2f);
			position.y = 0f;
			// 각 셀의 z축은 외부 반지름의 1.5배씩 떨어져있습니다.
			position.z = z * (HexMetrics.outerRadius * 1.5f);

			HexCell cell = cells[i] = Instantiate<HexCell>(cellPrefab);
			cell.transform.SetParent(transform, false);
			cell.transform.localPosition = position;
			cell.coordinates = HexCoordinates.FromOffsetCoordinates(x, z);

			Text label = Instantiate<Text>(cellLabelPrefab);
			label.rectTransform.SetParent(gridCanvas.transform, false);
			label.rectTransform.anchoredPosition =
				new Vector2(position.x, position.z);
			label.text = cell.coordinates.ToStringOnSeparateLines();


		}


	}
}
