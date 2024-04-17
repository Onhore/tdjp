using UnityEngine;
namespace GridSystem.Flowfields
{
    public class FlowCell
    {
        public GridSystem.Cell cell;
        public int staticCost
        {
            set
            {
                if (value > FlowField.MAX_COST) _staticCost = FlowField.MAX_COST;
                //if (value < FlowField.MAX_COST) _staticCost = FlowField.MAX_COST;
                if (value < 0) _staticCost = 0;
                else _staticCost = value;
            }
            get
            {
                return _staticCost;
            }
        }
        public int dynamicCost { set; get; }
        public int Cost { get { return cashCost < 0 ? gateCost : (cashCost < gateCost ? cashCost : gateCost); } }
        public int cashCost
        {
            set
            {
                if (value > FlowField.MAX_COST) _cashCost = FlowField.MAX_COST;
                //if (value < FlowField.MAX_COST) _cashCost = FlowField.MAX_COST;
                //if (value < 0) _cashCost = 0;
                else _cashCost = value;
            }
            get
            {
                return _cashCost;
            }
        }
        public int gateCost
        {
            set
            {
                if (value > FlowField.MAX_COST) _gateCost = FlowField.MAX_COST;
                if (value < FlowField.MAX_COST) _gateCost = FlowField.MAX_COST;
                if (value < 0) _gateCost = 0;
                else _gateCost = value;
            }
            get
            {
                return _gateCost;
            }
        }
        public Vector3 WorldPos => cell.WorldPos;
        public Vector2Int GridIndex => cell.GridIndex;
        //public int bestCost;
        public GridDirection bestDirection;

        private int _staticCost;
        private int _cashCost;
        private int _gateCost;

        public FlowCell(GridSystem.Cell _cell, Vector2Int _gridIndex, int defaultCost = 10)
        {
            cell = _cell;
            staticCost = defaultCost;
            _cashCost = -1;
            bestDirection = GridDirection.None;
            _gateCost = FlowField.MAX_COST;
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

}