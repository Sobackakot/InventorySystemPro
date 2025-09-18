using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class InventoryGridUIBase : MonoBehaviour
{
    public abstract InventoryGridModelBase gridModel { get; }
    [field: SerializeField] public GameObject itemUIPrefab { get; private set; }
    [field: SerializeField] public InventoryGridConfig gridConfig { get; private set; }
    [field: SerializeField] public LayoutElement layoutElementOnGridBackground { get; private set; }

   
    public RectTransform ItemsContainer { get; private set; } 
    public readonly Dictionary<ItemConfig, InventoryItemUI> gridItemsUI = new();
     
     
    private void Start()
    {
        ItemsContainer = GetComponent<RectTransform>();
        ItemsContainer.pivot = new Vector2(0, 1);
        ItemsContainer.anchorMin = new Vector2(0, 1);
        ItemsContainer.anchorMax = new Vector2(0, 1);
        ItemsContainer.anchoredPosition = Vector2.zero;

        float width = layoutElementOnGridBackground.preferredWidth;
        float height = layoutElementOnGridBackground.preferredHeight;
        SetItemsContainerSize(width, height);

    } 
    private void SetItemsContainerSize(float width, float height) => ItemsContainer.sizeDelta = new Vector2(width, height);
    public void OnEnable()
    {
        gridModel.OnItemPlaced += SetNewItemByInventoryCell;
        gridModel.OnItemRemove += ResetItemByInventoryCell;
    }

    public void OnDisable()
    {
        gridModel.OnItemPlaced -= SetNewItemByInventoryCell;
        gridModel.OnItemRemove -= ResetItemByInventoryCell;
    }

    public virtual void SetItemNewSlot(ItemConfig itemData, Vector2Int topLeftCoord)
    {
        if (gridItemsUI.TryGetValue(itemData, out var itemUI))
        {
            gridItemsUI[itemData].SetItemUI(itemData, topLeftCoord);
        }
    }
    public virtual void SetNewItemByInventoryCell(ItemConfig itemData, Vector2Int topLeftCoord)
    {
        if (!gridItemsUI.ContainsKey(itemData))
        {
            InventoryItemUI itemUI = Instantiate(itemUIPrefab, ItemsContainer).GetComponent<InventoryItemUI>();
            
            gridItemsUI.Add(itemData, itemUI);
            gridItemsUI[itemData].SetItemUI(itemData, topLeftCoord);
        }
        else SetItemNewSlot(itemData, topLeftCoord);
    }
    public virtual void ResetItemByInventoryCell(ItemConfig itemData)
    {
        if (gridItemsUI.TryGetValue(itemData, out var inventoryItemUI))
        {
            inventoryItemUI.ResetItemUI();
            gridItemsUI.Remove(itemData);
        }
    }

}
