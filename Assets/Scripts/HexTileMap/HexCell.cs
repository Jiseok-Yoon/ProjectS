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
        // 육각 좌표 정보
        public HexCoordinates coordinates;

        // 셀에 칠해진 색
        public Color color;

        // 인접 셀
        [SerializeField]
        HexCell[] neighbors;

        /// <summary>
        /// 특정 방향에 인접한 셀을 가져옵니다.
        /// </summary>
        /// <param name="direction">북동부터 6방향</param>
        /// <returns></returns>
        public HexCell GetNeighbor(HexDirection direction)
        {
            return neighbors[(int)direction];
        }
        /// <summary>
        /// 특정 방향에 인접한 셀을 세팅합니다. 인접한 셀에서도 마찬가지로 세팅합니다.
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
