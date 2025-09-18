using System;
using UnityEngine;

public class LootBoxGridModel : InventoryGridModelBase
{
    public LootBoxGridModel(InventoryGridConfig gridConfig) : base(gridConfig)
    {
    }

    public override event Action<ItemConfig, Vector2Int> OnItemPlaced;
    public override event Action<ItemConfig> OnItemRemove;

    public override bool TryMoveItem(ItemConfig itemData, Vector2Int newTopLeft)
    {
        return base.TryMoveItem(itemData, newTopLeft);
    }

    public override bool Checking—ellsForOutOfGridLimits(ItemConfig itemData, Vector2Int topLeft)
    {
        return base.Checking—ellsForOutOfGridLimits(itemData, topLeft);
    }
    public override bool CanPlaceItem(ItemConfig itemData, Vector2Int topLeft)
    {
        return base.CanPlaceItem(itemData, topLeft);
    }

    public override void PlaceItem(ItemConfig itemData, Vector2Int topLeft)
    {
        base.PlaceItem(itemData, topLeft);
    }

    public override void RemoveItemFromList(ItemConfig itemData)
    {
        base.RemoveItemFromList(itemData);
    }

    public override bool TryFindFreePosition(ItemConfig itemData, out Vector2Int topLeft)
    {
        return base.TryFindFreePosition(itemData, out topLeft);
    }

    public override Vector2Int GetPointerCellsXY(Vector2 pixelPosition, ItemConfig dataItem)
    {
        return base.GetPointerCellsXY(pixelPosition, dataItem);
    }

    public override void OnItemRemoveFromUI(ItemConfig itemData)
    {
        OnItemRemove?.Invoke(itemData);
    }

    public override void OnItemPlaceForUI(ItemConfig itemData, Vector2Int topLeft)
    {
        OnItemPlaced?.Invoke(itemData, topLeft);
        Debug.Log("placeitem LootBox");
    }
     
}
