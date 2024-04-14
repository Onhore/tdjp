using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GridSystem.Flowfields;
using UnityEngine.EventSystems;

public class FlowFieldController : MonoBehaviour
{
    [SerializeField] GridSystem.Grid grid;
    public Vector2Int gridSize => grid.gridSize;
    public float cellRadius => grid.cellRadius;
    public FlowField curFlowField;
    //public Dictionary<GridSystem.Grid, Dictionary<string, FlowField>> FlowfieldsDictionary;
    //public Dictionary<string, FlowField> Flowfields;
    public FlowFieldDebug gridDebug;
    public GameObject gate;

    private void InitializeFlowField()
    {
        curFlowField = new FlowField(GridController.Instance.grid);
        curFlowField.CreateGrid();
        Debug.Log("Create FlowField " + " " + GridController.Instance.grid.cellRadius + " " + GridController.Instance.grid.gridSize + " ");
        Debug.Log("Create FlowField " + " " + curFlowField.cellRadius + " " + curFlowField.gridSize + " ");
        Debug.Log("Create StaticCosts");
        curFlowField.CreateStaticCostField();
        Debug.Log("Done");
        gridDebug.SetFlowField(curFlowField);
    }
    private void Start()
    {
        grid = GridController.Instance.grid;
        Debug.Log(grid.cellRadius + " " + GridController.Instance.grid.cellRadius);
        //Flowfields = new Dictionary<string, FlowField>();

    }
    public void Init()
    {
            InitializeFlowField();
            Debug.Log("Set GateField");
            //Vector3 mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10f);
            //Vector3 worldMousePos = Camera.main.ScreenToWorldPoint(mousePos);
            Debug.Log(gate.transform.position+" "+ curFlowField.gridSize.x+" "+ curFlowField.gridSize.y);
            FlowCell destinationCell = curFlowField.GetCellFromWorldPos(gate.transform.position);
            curFlowField.GateField(destinationCell);
            curFlowField.CreateTowerField();
            Debug.Log("Done");
            curFlowField.CreateFlowField();

            gridDebug.DrawFlowField();
    }
    private void Update()
    {
        // if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject() && curFlowField == null)
        // {
        //     InitializeFlowField();
        //     Debug.Log("Set GateField");
        //     //Vector3 mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10f);
        //     //Vector3 worldMousePos = Camera.main.ScreenToWorldPoint(mousePos);
        //     Debug.Log(gate.transform.position+" "+ curFlowField.gridSize.x+" "+ curFlowField.gridSize.y);
        //     FlowCell destinationCell = curFlowField.GetCellFromWorldPos(gate.transform.position);
        //     curFlowField.GateField(destinationCell);
        //     Debug.Log("Done");
        //     curFlowField.CreateFlowField();

        //     gridDebug.DrawFlowField();
        // }
    }

    private void UpdateStaticFields()
    {

    }
    private void UpdateDynamicFields()
    {

    }
    private void DetectStaticObstacles()
    { 
    
    }
    
}