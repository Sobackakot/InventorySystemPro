using UnityEngine;

public class LootBoxGridUI : InventoryGridUIBase
{
    public void Awake()
    {
        _gridModel = new LootBoxGridModel(gridConfig); 
    }
    private LootBoxGridModel _gridModel;
    public override InventoryGridModelBase gridModel => _gridModel;


    public override void SetItemNewSlot(ItemConfig itemData, Vector2Int topLeftCoord)
    {
        base.SetItemNewSlot(itemData, topLeftCoord);
    }
    public override void SetNewItemByInventoryCell(ItemConfig itemData, Vector2Int topLeftCoord)
    {
        base.SetNewItemByInventoryCell(itemData, topLeftCoord);
    }
    public override void ResetItemByInventoryCell(ItemConfig itemData)
    {
        base.ResetItemByInventoryCell(itemData);
    }
}
