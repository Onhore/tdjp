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
public enum BuildSelect { Null, Base, Middle, Window, Top, Mine };
public class BuildManager : MonoBehaviour
{
    [SerializeField] private GameObject TowerPrefab;
    [SerializeField] private Base BasePrefab;
    [SerializeField] private Element MiddlePrefab;
    [SerializeField] private Element WindowPrefab;
    [SerializeField] private Element TopPrefab;
    [SerializeField] private GameObject MinePrefab;
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
            if (Physics.Raycast(position, out var hitInfo, 500, GroundLayer) && (hitInfo.transform.gameObject.layer == 10 || (hitInfo.transform.gameObject.layer == 12 && hitInfo.transform.gameObject.GetComponent<Tower>() != null)))
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
                    case BuildSelect.Mine:
                        TryBuildTile(cell, MinePrefab);
                        break;
                    
            }
            //Select = BuildSelect.Null;
        }
        }
    }
    public void TryBuildTile(Cell cell, GameObject tile)
    {
        if (GridController.Instance.grid.IsCellEmpty(cell))
        {
            if (!GetCost(20, 40))
                return;
            BuildTile(cell, tile);
        }
        else
        {
            Debug.Log("Нельзя поставить тайл!");
        }
        
    }
    public void BuildTile(Cell cell, GameObject tile)
    {
        GameObject gTile = Instantiate(tile, cell.WorldPos, Quaternion.identity, transform);
        cell.Tile = gTile.GetComponent<Tile>();
        GridController.Instance.UpdateStaticFields();
    }
    public void TryBuildTower(Cell cell)
    {
        if (GridController.Instance.grid.IsCellEmpty(cell))
        {
            if (!GetCost(20, 40))
                return;
            BuildTower(cell);
        }
        else
        {
            Debug.Log("Нельзя поставить фундамент!");
        }
    }
    private bool GetCost(int gold, int rock)
    {
        if (PlayerStats.Gold.Score < gold || PlayerStats.Rock.Score < rock)
            return false;
        
        PlayerStats.Gold.Score -= gold;
        PlayerStats.Rock.Score -= rock;
        return true;
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
        if (GridController.Instance.grid.IsCellOccupied(cell) && !(cell.Tile.GetComponent<Tower>().GetTop() is Top))
        {
            if (!GetCost(20, 20))
                return;
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
        GridController.Instance.UpdateStaticFields();
    }
    public void DestroyTower()
    {

    }
}

}