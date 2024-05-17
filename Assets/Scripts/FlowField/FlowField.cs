using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace GridSystem.Flowfields
{
    [Serializable]
    public class FlowField
    {
        public string tag;
        public const int MAX_COST = short.MaxValue;
        public FlowCell[,] flowGrid { get; private set; }
        public Vector2Int gridSize => grid.gridSize;
        public float cellRadius => grid.cellRadius;
        public FlowCell GateCell;

        public Grid grid;
        //public uint length = 10;

        private float cellDiameter => cellRadius*2;


        public FlowField(Grid _grid)
        {
            grid = _grid;
        }

        public void CreateGrid()
        {
            flowGrid = new FlowCell[gridSize.x, gridSize.y];
            //Debug.Log(gridSize);

            for (int x = 0; x < gridSize.x; x++)
            {
                for (int y = 0; y < gridSize.y; y++)
                {
                    //Vector3 worldPos = new Vector3(cellDiameter * x + cellRadius, 0, cellDiameter * y + cellRadius);
                    flowGrid[x, y] = new FlowCell(grid[x,y], new Vector2Int(x, y));
                }
            }
        }
        public void EmptyField()
        {
            foreach (FlowCell cell in flowGrid)
            {
                cell.staticCost = 10;
                cell.cashCost = -1;
                cell.bestDirection = GridDirection.None;
                cell.gateCost = MAX_COST;
            }
        }

        public void CreateStaticCostField()
        {
            Vector3 cellHalfExtents = Vector3.one * (cellRadius-0.01f);
            int terrainMask = LayerMask.GetMask("obstacle", "way");
            foreach (FlowCell curCell in flowGrid)
            {
                Collider[] obstacles = Physics.OverlapBox(curCell.WorldPos, cellHalfExtents, Quaternion.identity, terrainMask);
                foreach (Collider col in obstacles)
                {
                    if (col.gameObject.layer == 8)
                    {

                        //Debug.Log(curCell.staticCost);
                        //curCell.staticCost = MAX_COST;
                        StaticField(curCell, 2, 10, Multiply.Increase);
                        curCell.staticCost = MAX_COST;
                        continue;
                    }
                    if (col.gameObject.layer == 9)
                    {
                        curCell.staticCost = 5;
                        continue;
                    }
                }
            }
        }
        public void CreateTowerField()
        {
            foreach (FlowCell curCell in flowGrid)
            {
                    if (grid.IsCellOccupied(curCell.cell))
                    {
                        //Debug.Log(curCell.staticCost);
                        //curCell.staticCost = MAX_COST;
                        RadiusField(curCell, curCell.cell.Tile.GetComponent<BuildSystem.Tower>().Size*2);
                        //continue;
                    }
                    else if (curCell.cell.Tile != null)
                    {
                        RadiusField(curCell, 2);
                        Debug.Log("424135fdgsdg");
                    }
                
            }
        }

        public void StaticField(FlowCell staticCell, int radius, int cost, Multiply multiplier)
        {
            Vector2Int startCell = new Vector2Int(Math.Max(0, (staticCell.GridIndex.x - radius)), Math.Max(0, (staticCell.GridIndex.y - radius)));
            Vector2Int endCell = new Vector2Int(Math.Min((staticCell.GridIndex.x + radius), gridSize.x - 1), Math.Min((staticCell.GridIndex.y + radius), gridSize.y - 1));
            //Debug.DrawLine()
            //Debug.Log(startCell + " " + endCell);
            for (int x = startCell.x; x <= endCell.x; x++)
            {
                for (int y = startCell.y; y <= endCell.y; y++)
                {
                    //if (staticCell.staticCost == short.MaxValue) continue;
                    if ((Math.Abs(x - staticCell.GridIndex.x) + Math.Abs(y - staticCell.GridIndex.y)) <= radius)
                    {
                        flowGrid[x, y].staticCost += cost * (int)multiplier;
                        //Debug.Log(grid[x, y].staticCost.ToString() +" "+cost.ToString()+" "+ ((int)multiplier).ToString());
                    }
                }
            }
        }
        public void GateField(FlowCell gateCell)
        {
            GateCell = gateCell;

            GateCell.staticCost = 0;
            GateCell.gateCost = 0;

            //Vector2Int MainPos = GateCell.gridIndex;

            Queue<FlowCell> cellsToCheck = new Queue<FlowCell>();

            cellsToCheck.Enqueue(GateCell);

            while (cellsToCheck.Count > 0)
            {
                FlowCell cell = cellsToCheck.Dequeue();
                List<FlowCell> neighbours = GetNeighborCells(cell.GridIndex, GridDirection.CardinalDirections);
                //int cashCost = int.MaxValue;
                foreach (FlowCell neighbour in neighbours)
                {
                    if (neighbour.staticCost == MAX_COST) continue;
                    if (neighbour.staticCost + cell.gateCost < neighbour.gateCost)
                    {
                        neighbour.gateCost = neighbour.staticCost + cell.gateCost;
                        cellsToCheck.Enqueue(neighbour);
                    }
                }
            }

        }
        public void RadiusField(FlowCell radiusCell, int radius)
        {
            Vector2Int startCell = new Vector2Int(Math.Max(0, (radiusCell.GridIndex.x - radius)), Math.Max(0, (radiusCell.GridIndex.y - radius)));
            Vector2Int endCell = new Vector2Int(Math.Min((radiusCell.GridIndex.x + radius), gridSize.x - 1), Math.Min((radiusCell.GridIndex.y + radius), gridSize.y - 1));
            //Debug.DrawLine()
            //Debug.Log(startCell + " " + endCell);
            for (int x = startCell.x; x <= endCell.x; x++)
            {
                for (int y = startCell.y; y <= endCell.y; y++)
                {
                    //if (staticCell.staticCost == short.MaxValue) continue;
                    if ((Math.Abs(x - radiusCell.GridIndex.x) + Math.Abs(y - radiusCell.GridIndex.y)) <= radius && flowGrid[x, y].cashCost<0)
                    {
                        flowGrid[x, y].cashCost = MAX_COST;
                        //Debug.Log(grid[x, y].staticCost.ToString() +" "+cost.ToString()+" "+ ((int)multiplier).ToString());
                    }
                }
            }

            //radiusCell.staticCost = 0;
            radiusCell.cashCost = 0;

            Queue<FlowCell> cellsToCheck = new Queue<FlowCell>();

            cellsToCheck.Enqueue(radiusCell);

            while (cellsToCheck.Count > 0)
            {
                FlowCell cell = cellsToCheck.Dequeue();
                List<FlowCell> neighbours = GetNeighborCells(cell.GridIndex, GridDirection.CardinalDirections);
                //int cashCost = int.MaxValue;
                foreach (FlowCell neighbour in neighbours)
                {
                    if ((Math.Abs(neighbour.GridIndex.x - radiusCell.GridIndex.x) + Math.Abs(neighbour.GridIndex.y - radiusCell.GridIndex.y)) > radius) continue;
                    if (neighbour.staticCost == MAX_COST) continue;
                    if (neighbour.staticCost + cell.cashCost < neighbour.cashCost)
                    {
                        neighbour.cashCost = neighbour.staticCost + cell.cashCost;
                        cellsToCheck.Enqueue(neighbour);
                    }
                }
            }
        }

        public void CreateFlowField()
        {
            foreach (FlowCell curCell in flowGrid)
            {
                bool nearWall = false;
                List<FlowCell> curNeighbors = GetNeighborCells(curCell.GridIndex, GridDirection.CardinalDirections);
                foreach (FlowCell neighbour in curNeighbors)
                {
                    if (neighbour.staticCost == MAX_COST)
                        nearWall = true;
                }

                if (!nearWall)
                    curNeighbors = GetNeighborCells(curCell.GridIndex, GridDirection.AllDirections);

                int bestCost = curCell.Cost;

                foreach (FlowCell curNeighbor in curNeighbors)
                {
                    if (curNeighbor.Cost < bestCost)
                    {
                        bestCost = curNeighbor.Cost;
                        curCell.bestDirection = GridDirection.GetDirectionFromV2I(curNeighbor.GridIndex - curCell.GridIndex);
                        //Debug.Log(curCell.bestDirection.Vector);

                    }
                }
            }
        }

        public GridDirection GetDirection(FlowCell cell)
        {
            bool nearWall = false;
                List<FlowCell> curNeighbors = GetNeighborCells(cell.GridIndex, GridDirection.AllDirections);
                foreach (FlowCell neighbour in curNeighbors)
                {
                    if (neighbour.staticCost == MAX_COST)
                        nearWall = true;
                }

                if (nearWall)
                    curNeighbors = GetNeighborCells(cell.GridIndex, GridDirection.CardinalDirections);

                int bestCost = cell.Cost;

                foreach (FlowCell curNeighbor in curNeighbors)
                {
                    if (curNeighbor.Cost < bestCost)
                    {
                        bestCost = curNeighbor.Cost;
                        cell.bestDirection = GridDirection.GetDirectionFromV2I(curNeighbor.GridIndex - cell.GridIndex);
                        //Debug.Log(curCell.bestDirection.Vector);

                    }
                }
                return cell.bestDirection;
        }

        public List<FlowCell> GetNeighborCells(Vector2Int nodeIndex, List<GridDirection> directions)
        {
            List<FlowCell> neighborCells = new List<FlowCell>();

            foreach (Vector2Int curDirection in directions)
            {
                FlowCell newNeighbor = GetCellAtRelativePos(nodeIndex, curDirection);
                if (newNeighbor != null)
                {
                    neighborCells.Add(newNeighbor);
                }
            }
            return neighborCells;
        }

        public FlowCell GetCellAtRelativePos(Vector2Int orignPos, Vector2Int relativePos)
        {
            Vector2Int finalPos = orignPos + relativePos;

            if (finalPos.x < 0 || finalPos.x >= gridSize.x || finalPos.y < 0 || finalPos.y >= gridSize.y)
            {
                return null;
            }

            else { return flowGrid[finalPos.x, finalPos.y]; }
        }

        public FlowCell GetCellFromWorldPos(Vector3 worldPos)
        {
            float percentX = worldPos.x / (gridSize.x * cellDiameter);
            float percentY = worldPos.z / (gridSize.y * cellDiameter);

            percentX = Mathf.Clamp01(percentX);
            percentY = Mathf.Clamp01(percentY);

            int x = Mathf.Clamp(Mathf.FloorToInt((gridSize.x) * percentX), 0, gridSize.x - 1);
            int y = Mathf.Clamp(Mathf.FloorToInt((gridSize.y) * percentY), 0, gridSize.y - 1);
            return flowGrid[x, y];
        }

        public FlowCell this[int x, int y]
        {
            get => flowGrid[x,y];
            private set => flowGrid[x,y] = value;
        }
        public FlowCell this[GridSystem.Cell key]
        {
            get => flowGrid[key.GridIndex.x, key.GridIndex.y];
            private set => flowGrid[key.GridIndex.x, key.GridIndex.y] = value;
        }
        
        public enum Multiply
        {
            Decrease = -1,
            Increase = 1
        }
    }
}