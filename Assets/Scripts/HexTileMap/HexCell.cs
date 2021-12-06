using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static ProjectS.Define.HexTileMap;

namespace ProjectS.TileMap
{
    [Serializable]
    public class HexCell : MonoBehaviour
    {
        // ���� ��ǥ ����
        public HexCoordinates coordinates;

        // ���� ĥ���� ��
        public Color color;

        // ���� ��
        [SerializeField]
        HexCell[] neighbors;

        /// <summary>
        /// Ư�� ���⿡ ������ ���� �����ɴϴ�.
        /// </summary>
        /// <param name="direction">�ϵ����� 6����</param>
        /// <returns></returns>
        public HexCell GetNeighbor(HexDirection direction)
        {
            return neighbors[(int)direction];
        }
        /// <summary>
        /// Ư�� ���⿡ ������ ���� �����մϴ�. ������ �������� ���������� �����մϴ�.
        /// </summary>
        /// <param name="direction"></param>
        /// <param name="cell"></param>
        public void SetNeighbor(HexDirection direction, HexCell cell)
        {
            neighbors[(int)direction] = cell;
            cell.neighbors[(int)direction.Opposite()] = this;
        }
    }

    public static class HexDirectionExtensions
    {

        public static HexDirection Opposite(this HexDirection direction)
        {
            return (int)direction < 3 ? (direction + 3) : (direction - 3);
        }
    }
}
