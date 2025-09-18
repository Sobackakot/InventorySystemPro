using UnityEngine;

public abstract class InventoryGridHandlerBase  
{
    public InventoryGridHandlerBase(InventoryGridModelBase gridModel)
    {
        this.gridModel = gridModel; 
    } 
    public InventoryGridModelBase gridModel { get; private set; }
     
    public virtual bool MoveItem(ItemConfig itemData)
    {
        if (gridModel.TryFindFreePosition(itemData, out var pos))
        {
            if (gridModel.TryMoveItem(itemData, pos)) return true;
            else return false;
        }
        else return false;
    }

    public virtual bool PlaceItemByGrid(ItemConfig itemData)
    {
        if (gridModel.TryFindFreePosition(itemData, out var pos))
        {
            gridModel.PlaceItem(itemData, pos);
            return true;
        }
        Debug.LogWarning($"No space for item: {itemData.ItemId}");
        return false;
    }

    public virtual void RemoveItemByGrid(ItemConfig itemData)
    {
        gridModel.RemoveItemFromList(itemData);
    }
}
