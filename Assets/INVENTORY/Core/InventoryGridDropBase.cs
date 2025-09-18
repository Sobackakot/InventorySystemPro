using UnityEngine;
using UnityEngine.EventSystems;

public abstract class InventoryGridDropBase : MonoBehaviour, IDropHandler
{
    public string tagInventory { get; private set; }
    public RectTransform gridRectTransform { get; private set; } 

    private void Awake()
    {
        gridRectTransform = GetComponent<RectTransform>(); 
    }

    public virtual void OnDrop(PointerEventData eventData)
    {
        InventoryGridUIBase inventoryGridUI = gridRectTransform.GetComponentInChildren<InventoryGridUIBase>(); //target slot
     
        InventoryItemUI itemDragUI = eventData.pointerDrag.GetComponent<InventoryItemUI>();// from slot

        if (itemDragUI == null) return;
        InventoryGridUIBase oldInventoryGridUI = itemDragUI.gridUI;
        ItemConfig itemData = itemDragUI.dataItem; 
        if (itemData == null) return;
        Vector2 localDropPosition;
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(gridRectTransform, eventData.position, eventData.pressEventCamera, out localDropPosition))
        {

            float normalizedLocalY = -localDropPosition.y;

            Vector2Int topLeft = inventoryGridUI.gridModel.GetPointerCellsXY(
                new Vector2(localDropPosition.x, normalizedLocalY), itemData);
            TryMoveItem(inventoryGridUI, itemDragUI, oldInventoryGridUI, itemData, topLeft);

        }
    }

    private static void TryMoveItem(InventoryGridUIBase inventoryGridUI, InventoryItemUI itemDragUI, InventoryGridUIBase oldInventoryGridUI, ItemConfig itemData, Vector2Int newTopLeftCoord)
    {
        if (inventoryGridUI.gridConfig.invnetoryType != oldInventoryGridUI.gridConfig.invnetoryType)
        {
            itemDragUI.SetNewParent(inventoryGridUI.transform);
            if (inventoryGridUI.gridModel.TryFindFreePosition(itemData, out Vector2Int topLeft))
            {
                inventoryGridUI.gridModel.PlaceItem(itemData, topLeft);
                oldInventoryGridUI.gridModel.RemoveItemFromList(itemData);
            }
        }
        else
        {
            bool moveSuccessful = inventoryGridUI.gridModel.TryMoveItem(itemData, newTopLeftCoord);
        }
    }
}
