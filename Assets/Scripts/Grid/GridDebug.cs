using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GridSystem;
using BuildSystem;

public class GridDebug : MonoBehaviour
{
    public GridController gridController;
    public bool displayGrid;
    public Cell target;
    public Tower Tower;
    public LayerMask groundLayer;
    private void Update()
    {
        //BuildManager.Instance.Test();
        if (Input.GetMouseButtonDown(0))
        {
            //LayerMask layer = LayerMask.GetMask("UI");
            Ray position = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(position, out var hitInfo, 200, groundLayer) && hitInfo.transform.gameObject.layer == 10)
            {
                target = gridController.grid.GetCellFromWorldPos(hitInfo.point);
                Debug.Log(gridController.grid.IsCellOccupied(target));
                
                
            }
        }
    }
    private void OnDrawGizmos()
    {
        if (gridController == null) return;
        if (displayGrid) DrawGrid(gridController.gridSize, Color.magenta, gridController.cellRadius);
        if (target != null) DrawCell(target, Color.green, gridController.cellRadius);
    }
    private void DrawGrid(Vector2Int drawGridSize, Color drawColor, float drawCellRadius)
    {
        Gizmos.color = drawColor;
        for (int x = 0; x < drawGridSize.x; x++)
        {
            for (int y = 0; y < drawGridSize.y; y++)
            {
                Vector3 center = new Vector3(drawCellRadius * 2 * x + drawCellRadius, 0, drawCellRadius * 2 * y + drawCellRadius);
                Vector3 size = (Vector3.right+Vector3.forward) * drawCellRadius * 2;
                Gizmos.DrawWireCube(center, size);
            }
        }
    }
    private void DrawCell(Cell cell, Color drawColor, float drawCellRadius)
    {
        Gizmos.color = drawColor;
        Vector3 size = (Vector3.right + Vector3.forward) * drawCellRadius * 2;
        Gizmos.DrawWireCube(cell.WorldPos, size);
    }
}
