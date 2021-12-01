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

		void Update()
		{
			if (Input.GetMouseButton(0))
			{
				HandleInput();
			}
		}
		/// <summary>
		/// ���콺 ��Ŭ���� ���� ��ġ�մϴ�.
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
			/// �� ���� x���� ���� �������� 2�辿 �������ֽ��ϴ�.
			/// x ���� Ȧ�� �ึ�� ���� ��������ŭ ���ϴ�.
			position.x = (x + z * 0.5f - z / 2) * (HexMetrics.innerRadius * 2f);
			position.y = 0f;
			// �� ���� z���� �ܺ� �������� 1.5�辿 �������ֽ��ϴ�.
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
