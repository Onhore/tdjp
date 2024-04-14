using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitController : MonoBehaviour
{
    public FlowFieldController gridController;
    public GameObject unitPrefab;
    public GameObject Spawner;
    public int numUnitsPerSpawn;
    public float moveSpeed;

    public int count = 0;

    private List<GameObject> unitsInGame;

    private void Awake()
    {
        unitsInGame = new List<GameObject>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            count += 12;
            
            SpawnUnit(Spawner.transform.position);
            SpawnUnit(Spawner.transform.position+new Vector3(0,0,3));
            SpawnUnit(Spawner.transform.position+new Vector3(0,0,-3));
            SpawnUnit(Spawner.transform.position+new Vector3(3,3));
            SpawnUnit(Spawner.transform.position+new Vector3(3,0,-3));
            SpawnUnit(Spawner.transform.position+new Vector3(3,0,0));

            SpawnUnit(Spawner.transform.position + new Vector3(0,0,5));
            SpawnUnit(Spawner.transform.position+new Vector3(0,0,3) + new Vector3(0,0,5));
            SpawnUnit(Spawner.transform.position+new Vector3(0,0,-3) + new Vector3(0,0,5));
            SpawnUnit(Spawner.transform.position+new Vector3(3,0,3) + new Vector3(0,0,5));
            SpawnUnit(Spawner.transform.position+new Vector3(3,0,-3) + new Vector3(0,0,5));
            SpawnUnit(Spawner.transform.position+new Vector3(3,0,0) + new Vector3(0,0,5));
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            DestroyUnits();
        }
    }

    private void FixedUpdate()
    {
        if (gridController.curFlowField == null) { return; }
        foreach (GameObject unit in unitsInGame)
        {//
            //Cell cellBelow = gridController.curFlowField.GetCellFromWorldPos(unit.transform.position);
            //Vector3 moveDirection = new Vector3(cellBelow.bestDirection.Vector.x, 0, cellBelow.bestDirection.Vector.y);
            //Rigidbody unitRB = unit.GetComponent<Rigidbody>();
            ////unitRB.velocity = moveDirection * moveSpeed;
            //unitRB.AddForce(moveDirection * moveSpeed, ForceMode.Force);
        }//
    }   

    private void SpawnUnit(Vector3 position)
    {
        GameObject newUnit1 = Instantiate(unitPrefab);
        newUnit1.transform.parent = transform;
        newUnit1.transform.position = position;
        newUnit1.GetComponent<Goblin>().GridController = gridController;
        unitsInGame.Add(newUnit1);
        //
        //Vector2Int gridSize = gridController.gridSize;
        //float nodeRadius = gridController.cellRadius;
        //Vector2 maxSpawnPos = new Vector2(gridSize.x * nodeRadius * 2 + nodeRadius, gridSize.y * nodeRadius * 2 + nodeRadius);
        //int colMask = LayerMask.GetMask("obstacle", "unit");
        //Vector3 newPos;
        //for (int i = 0; i < numUnitsPerSpawn; i++)
        //{
        //    GameObject newUnit = Instantiate(unitPrefab);
        //    newUnit.transform.parent = transform;
        //    unitsInGame.Add(newUnit);
        //    do
        //    {
        //        newPos = new Vector3(Random.Range(0, maxSpawnPos.x), 0, Random.Range(0, maxSpawnPos.y));
        //        newUnit.transform.position = newPos;
        //    }
        //    while (Physics.OverlapSphere(newPos, 0.25f, colMask).Length > 0);
        //}
    }//

    private void DestroyUnits()
    {
        foreach (GameObject go in unitsInGame)
        {
            Destroy(go);
        }
        unitsInGame.Clear();
    }
}