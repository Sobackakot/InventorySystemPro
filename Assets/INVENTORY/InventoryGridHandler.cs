using UnityEngine;

public class InventoryGridHandler : InventoryGridHandlerBase
{
    public InventoryGridHandler(InventoryGridModelBase gridModel) : base(gridModel)
    {
    }

    public override bool MoveItem(ItemConfig itemData)
    {
        return base.MoveItem(itemData);
    }
     
    public override bool PlaceItemByGrid(ItemConfig itemData)
    {
        return base.PlaceItemByGrid(itemData);
    }
     
    public override void RemoveItemByGrid(ItemConfig itemData)
    {
        base.RemoveItemByGrid(itemData);
    }
     
}

