using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GridSystem;

public class GridController : MonoBehaviour
{
    #region Singleton
    private static GridController _instance;
    public static GridController Instance
    {
        get
        {
            if(_instance == null)
            {
                _instance = FindObjectOfType<GridController>();
                if (_instance ==null)
                {
                    _instance = new GameObject("GridController").AddComponent<GridController>();

                }
            }
            return _instance;
        }
    }
    #endregion
    
    public Vector2Int gridSize;
    public float cellRadius = 0.5f;
    public GridSystem.Grid grid;

    private void Start()
    {
        InitializeGrid();
        GetComponent<FlowFieldController>().Init();
    }
    private void Update()
    {

    }

    private void InitializeGrid()
    {
        grid = new GridSystem.Grid(cellRadius, gridSize);
        grid.CreateGrid();
    }
    public void UpdateStaticFields()
    {
        GetComponent<FlowFieldController>().UpdateStaticFields();
    }
}