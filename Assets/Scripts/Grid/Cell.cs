using BuildSystem;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TerrainUtils;

namespace GridSystem
{
    [Serializable]
    public class Cell
    {
        public Vector3 WorldPos;
        public Vector2Int GridIndex;
        public Tile Tile;
        //public bool isCellOccupied => Tile != null;
        
        public Cell(Vector3 _worldPos, Vector2Int _gridIndex)
        {
            WorldPos = _worldPos;
            GridIndex = _gridIndex;
        }
        
    }
}