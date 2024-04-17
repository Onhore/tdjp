using System;
using System.Collections;
using System.Collections.Generic;
using Palmmedia.ReportGenerator.Core.Reporting.Builders;
using Unity.VisualScripting;
using UnityEngine;
using GridSystem;
using UnityEngine.EventSystems;

namespace BuildSystem
{
public enum BuildSelect { Null, Base, Middle, Window, Top };
public class BuildManager : MonoBehaviour
{
    [SerializeField] private GameObject TowerPrefab;
    [SerializeField] private Base BasePrefab;
    [SerializeField] private Element MiddlePrefab;
    [SerializeField] private Element WindowPrefab;
    [SerializeField] private Element TopPrefab;
    [SerializeField] private LayerMask GroundLayer;
    
    #region Singleton
    private static BuildManager _instance;
    public static BuildManager Instance
    {
        get
        {
            if(_instance == null)
            {
                _instance = FindObjectOfType<BuildManager>();
                if (_instance ==null)
                {
                    _instance = new GameObject("BuildManager").AddComponent<BuildManager>();

                }
            }
            return _instance;
        }
    }
    #endregion
    //public void BuildSelect() {}
    public BuildSelect Select;
    
    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && Select != BuildSelect.Null && !EventSystem.current.IsPointerOverGameObject())
        {
            Ray position = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(position, out var hitInfo, 200, GroundLayer) && hitInfo.transform.gameObject.layer == 10)
            {
                Cell cell = GridController.Instance.grid.GetCellFromWorldPos(hitInfo.point);
                switch(Select){
                    case BuildSelect.Base:
                        TryBuildTower(cell);
                        break;
                    case BuildSelect.Middle:
                        TryBuildElement(cell, MiddlePrefab);
                        break;
                    case BuildSelect.Window:
                        TryBuildElement(cell, WindowPrefab);
                        break;
                    case BuildSelect.Top:
                        TryBuildElement(cell, TopPrefab);
                        break;
            }
            //Select = BuildSelect.Null;
        }
        }
    }
    public void TryBuildTower(Cell cell)
    {
        if (GridController.Instance.grid.IsCellEmpty(cell))
        {
            BuildTower(cell);
        }
        else
        {
            Debug.Log("Нельзя поставить фундамент!");
        }
    }
    /*public bool IsCellSuitable()
    {
        // Проверка клетки
        return true;
    }*/
    public void BuildTower(Cell cell)
    {
        GameObject tower = Instantiate(TowerPrefab, cell.WorldPos, Quaternion.identity, transform);
        tower.GetComponent<Tower>().AddElement(BasePrefab);
        cell.Tile = tower.GetComponent<Tower>();
        GridController.Instance.UpdateStaticFields();
    }
    public void TryBuildElement(Cell cell, Element prefab)
    {
        if (GridController.Instance.grid.IsCellOccupied(cell))
        {
            BuildElement(cell, prefab);
        }
        else if (GridController.Instance.grid.IsCellEmpty(cell))
        {
            Debug.Log("Клетка пуста");
        }
        else
        {
            Debug.Log("На клетке не башня");
        }
    }
    public void BuildElement(Cell cell, Element prefab)
    {
        ((Tower)cell.Tile).AddElement(prefab);
    }
    public void DestroyTower()
    {

    }
}

}