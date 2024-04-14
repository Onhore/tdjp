using System;
using System.Collections;
using System.Collections.Generic;
using BuildSystem;
using Unity.VisualScripting;
using UnityEngine;

namespace GridSystem
{
    [System.Serializable]
    public class Grid
    {
        public string tag;
        public Cell[,] grid { get; private set; }
        public Vector2Int gridSize { get; private set; }
        public float cellRadius { get; private set; }

        private float cellDiameter;
        public Grid(float _cellRadius, Vector2Int _gridSize)
        {
            cellRadius = _cellRadius;
            cellDiameter = cellRadius * 2f;
            gridSize = _gridSize;
        }
        public void CreateGrid()
        {
            grid = new Cell[gridSize.x, gridSize.y];

            for (int x = 0; x < gridSize.x; x++)
            {
                for (int y = 0; y < gridSize.y; y++)
                {
                    Vector3 worldPos = new Vector3(cellDiameter * x + cellRadius, 0, cellDiameter * y + cellRadius);
                    grid[x, y] = new Cell(worldPos, new Vector2Int(x, y));
                }
            }
        }
        public List<Cell> GetNeighborCells(Vector2Int nodeIndex, List<GridDirection> directions)
        {
            List<Cell> neighborCells = new List<Cell>();

            foreach (Vector2Int curDirection in directions)
            {
                Cell newNeighbor = GetCellAtRelativePos(nodeIndex, curDirection);
                if (newNeighbor != null)
                {
                    neighborCells.Add(newNeighbor);
                }
            }
            return neighborCells;
        }

        public Cell GetCellAtRelativePos(Vector2Int orignPos, Vector2Int relativePos)
        {
            Vector2Int finalPos = orignPos + relativePos;

            if (finalPos.x < 0 || finalPos.x >= gridSize.x || finalPos.y < 0 || finalPos.y >= gridSize.y)
            {
                return null;
            }

            else { return grid[finalPos.x, finalPos.y]; }
        }

        public Cell GetCellFromWorldPos(Vector3 worldPos)
        {
            float percentX = worldPos.x / (gridSize.x * cellDiameter);
            float percentY = worldPos.z / (gridSize.y * cellDiameter);

            percentX = Mathf.Clamp01(percentX);
            percentY = Mathf.Clamp01(percentY);

            int x = Mathf.Clamp(Mathf.FloorToInt((gridSize.x) * percentX), 0, gridSize.x - 1);
            int y = Mathf.Clamp(Mathf.FloorToInt((gridSize.y) * percentY), 0, gridSize.y - 1);
            return grid[x, y];
        }
        public Collider[] GetCollides(Cell cell)
        {
            Vector3 cellHalfExtents = Vector3.one * cellRadius;
            Vector3 position = cell.WorldPos + new Vector3(0, cellDiameter, 0);
            Collider[] collides = Physics.OverlapBox(position, cellHalfExtents, Quaternion.identity);
            Debug.Log(cellHalfExtents + " " + position + " " + collides.Length + " " + collides);
            return collides;
        }
        public Collider[] GetCollides(Cell cell, LayerMask layerMask)
        {
            Vector3 cellHalfExtents = Vector3.one * (cellRadius-0.01f);
            Vector3 position = cell.WorldPos + new Vector3(0, cellDiameter, 0);
            Collider[] collides = Physics.OverlapBox(position, cellHalfExtents, Quaternion.identity, layerMask);
            return collides;
        }
        public bool IsCellEmpty(Cell cell) => GetCollides(cell, ~LayerMask.GetMask("ground")).Length == 0;
        
        //public bool IsCellOccupied(Cell cell) => cell.Tile == null ? false : cell.Tile.GetType().IsAssignableFrom(typeof(Tower));
        public bool IsCellOccupied(Cell cell) => cell.Tile == null ? false : (cell.Tile is Tower);
        public bool IsCellPosition(Vector3 position)
        {
            
            return false;
        }

        public Cell this[int x, int y]
        {
            get => grid[x,y];
            set => grid[x,y] = value;
        }
    }
}