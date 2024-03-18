using UnityEngine;

public class Cell
{
    public Vector3 worldPos;
    public Vector2Int gridIndex;
    public int staticCost 
    {
        set
        {
            if (value > FlowField.MAX_COST) _staticCost = FlowField.MAX_COST;
            if (value < FlowField.MAX_COST) _cashCost = FlowField.MAX_COST;
            if (value < 0) _staticCost = 0;
            else _staticCost = value;
        }
        get
        {
            return _staticCost;
        }
    }
    public int dynamicCost { set; get; }
    public int Cost { get { return staticCost + dynamicCost; } }
    public int cashCost
    {
        set
        {
            if (value > FlowField.MAX_COST) _cashCost = FlowField.MAX_COST;
            if (value < FlowField.MAX_COST) _cashCost = FlowField.MAX_COST;
            if (value < 0) _cashCost = 0;
            else _cashCost = value;
        }
        get
        {
            return _cashCost;
        }
    }

    //public int bestCost;
    public GridDirection bestDirection;

    private int _staticCost;
    private int _cashCost;

    public Cell(Vector3 _worldPos, Vector2Int _gridIndex, int defaultCost=10)
    {
        worldPos = _worldPos;
        gridIndex = _gridIndex;
        staticCost = defaultCost;
        _cashCost = FlowField.MAX_COST;
        bestDirection = GridDirection.None;
    }

    /*public void IncreaseStaticCost(int value)
    {
        if (staticCost == uint.MaxValue) { return; }
        if (value+staticCost >= uint.MaxValue) { staticCost = int.MaxValue; }
        else { staticCost += value; }
    }
    public void DecreaseStaticCost(int value)
    {
        if (staticCost == 0) { return; }
        if (staticCost - value <= 0) { staticCost = 0; }
        else { staticCost -= value; }
    }
    public void IncreaseDynamicCost(int value)
    {
        if (dynamicCost == uint.MaxValue) { return; }
        if (value + dynamicCost >= uint.MaxValue) { dynamicCost = int.MaxValue; }
        else { dynamicCost += value; }
    }
    public void DecreaseDynamicCost(int value)           
    {
        if (dynamicCost == 0) { return; }
        if (dynamicCost - value <= 0) { dynamicCost = 0; }
        else { dynamicCost -= value; }
    }*/
    
}