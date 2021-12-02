using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectS.TileMap
{
    [Serializable]
    public class HexCell : MonoBehaviour
    {
        public HexCoordinates coordinates;

        public Color color;
    }

}
