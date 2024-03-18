using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FlowField
{
    public const int MAX_COST = short.MaxValue;
    public Cell[,] grid { get; private set; }
    public Vector2Int gridSize { get; private set; }
    public float cellRadius { get; private set; }
    public Cell GateCell;
    //public uint length = 10;

    private float cellDiameter;
    

    public FlowField(float _cellRadius, Vector2Int _gridSize)
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

    public void CreateStaticCostField()
    {
        Vector3 cellHalfExtents = Vector3.one * (cellRadius-1);
        int terrainMask = LayerMask.GetMask("obstacle", "way");
        foreach (Cell curCell in grid)
        {
            Collider[] obstacles = Physics.OverlapBox(curCell.worldPos, cellHalfExtents, Quaternion.identity, terrainMask);
            foreach (Collider col in obstacles)
            {
                if (col.gameObject.layer == 8)
                {
                    
                    //Debug.Log(curCell.staticCost);
                    StaticField(curCell, 2, 2, Multiply.Increase);
                    continue;
                }
                if (col.gameObject.layer == 9)
                {
                    curCell.staticCost-=1;
                    continue;
                }
            }
        }
    }

    public void StaticField(Cell staticCell, int radius, int cost, Multiply multiplier)
    {
        staticCell.staticCost = MAX_COST;
        Vector2Int startCell = new Vector2Int(Math.Max(0,(staticCell.gridIndex.x-radius)), Math.Max(0,(staticCell.gridIndex.y-radius)));
        Vector2Int endCell = new Vector2Int(Math.Min((staticCell.gridIndex.x + radius),gridSize.x), Math.Min((staticCell.gridIndex.y + radius),gridSize.y));

        for (int x = startCell.x; x <= endCell.x; x++)
        {
            for (int y = startCell.y; y <= endCell.y; y++)
            {
                //if (staticCell.staticCost == short.MaxValue) continue;
                if ((Math.Abs(x-staticCell.gridIndex.x) + Math.Abs(y-staticCell.gridIndex.y)) <= radius)
                {
                    grid[x, y].staticCost += cost * (int)multiplier;
                    //Debug.Log(grid[x, y].staticCost.ToString() +" "+cost.ToString()+" "+ ((int)multiplier).ToString());
                }
            }
        }
    }
    public void GateField(Cell gateCell)
    {
        GateCell = gateCell;

        GateCell.staticCost = 0;
        GateCell.cashCost = 0;

        //Vector2Int MainPos = GateCell.gridIndex;

        Queue<Cell> cellsToCheck = new Queue<Cell>();

        cellsToCheck.Enqueue(GateCell);

        while (cellsToCheck.Count > 0)
        {
            Cell cell = cellsToCheck.Dequeue();
            List<Cell> neighbours = GetNeighborCells(cell.gridIndex, GridDirection.CardinalDirections);
            //int cashCost = int.MaxValue;
            foreach (Cell neighbour in neighbours)
            {
                if (neighbour.staticCost == MAX_COST) continue;
                if (neighbour.staticCost + cell.cashCost < neighbour.cashCost)
                {
                    neighbour.cashCost = neighbour.staticCost + cell.cashCost;
                    cellsToCheck.Enqueue (neighbour);
                }
            }
        }

    }

   /* public void CreateFlowField()
    {
        foreach (Cell curCell in grid)
        {
            List<Cell> curNeighbors = GetNeighborCells(curCell.gridIndex, GridDirection.AllDirections);

            int bestCost = curCell.bestCost;

            foreach (Cell curNeighbor in curNeighbors)
            {
                if (curNeighbor.bestCost < bestCost)
                {
                    bestCost = curNeighbor.bestCost;
                    curCell.bestDirection = GridDirection.GetDirectionFromV2I(curNeighbor.gridIndex - curCell.gridIndex);
                }
            }
        }
    }*/

    private List<Cell> GetNeighborCells(Vector2Int nodeIndex, List<GridDirection> directions)
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

    private Cell GetCellAtRelativePos(Vector2Int orignPos, Vector2Int relativePos)
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
    public enum Multiply
    { 
    Decrease = -1,
    Increase = 1
    }
}