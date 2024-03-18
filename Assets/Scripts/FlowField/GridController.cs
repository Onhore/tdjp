using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridController : MonoBehaviour
{
    public Vector2Int gridSize;
    public float cellRadius = 0.5f;
    public FlowField curFlowField;
    public GridDebug gridDebug;
    public GameObject gate;

    private void InitializeFlowField()
    {
        Debug.Log("Create FlowField");
        curFlowField = new FlowField(cellRadius, gridSize);
        curFlowField.CreateGrid();
        Debug.Log("Create StaticCosts");
        curFlowField.CreateStaticCostField();
        Debug.Log("Done");
        gridDebug.SetFlowField(curFlowField);
    }
    private void Start()
    {
        //InitializeFlowField();
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            InitializeFlowField();
            Debug.Log("Set GateField");
            //Vector3 mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10f);
            //Vector3 worldMousePos = Camera.main.ScreenToWorldPoint(mousePos);
            Cell destinationCell = curFlowField.GetCellFromWorldPos(gate.transform.position);
            curFlowField.GateField(destinationCell);
            Debug.Log("Done");
            curFlowField.CreateFlowField();

            gridDebug.DrawFlowField();
        }
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