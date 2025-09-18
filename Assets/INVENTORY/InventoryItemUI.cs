using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(CanvasGroup), typeof(Image))] 
public class InventoryItemUI : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler 
{
    public InventoryGridUIBase gridUI { get; private set; }
    public ItemConfig dataItem { get; private set; } 
 
    public RectTransform pickItemTransform { get; private set; }
    public Transform originalParent { get; private set; }
    private CanvasGroup canvasGroup;
    private Canvas canvas; 

    private Image itemIcon;
    public Vector2Int CurrentTopLeftCoord { get; private set; }

    float itemPixelWidth;
    float itemPixelHeight;

    private void Awake()
    {
        gridUI = GetComponentInParent<InventoryGridUIBase>();
        originalParent = transform.parent; //transform parent object
        pickItemTransform = GetComponent<RectTransform>();//current position of the item
        canvasGroup = GetComponent<CanvasGroup>();
        canvas = pickItemTransform.GetComponentInParent<Canvas>(); //UI canvasEq with inventoryController

        itemIcon = transform.GetChild(0).GetComponent<Image>(); //image of the current item 

        SetPivotByItem(0, 1);  
    }
    public virtual void SetItemUI(ItemConfig newItem, Vector2Int topLeftCoord) // coll from InventorySlot
    {
        InventoryGridConfig gridConfig = gridUI.gridConfig;
        if (newItem == null) return;
        dataItem = newItem;
        itemIcon.sprite = dataItem.IconItem;
        CurrentTopLeftCoord = topLeftCoord;
        pickItemTransform.SetParent(originalParent);
        // Рассчитываем размер предмета с учетом spacing
        itemPixelWidth = dataItem.Size.x * gridConfig.CellSize.x + (dataItem.Size.x - 1) * gridConfig.Spacing.x;
        itemPixelHeight = dataItem.Size.y * gridConfig.CellSize.y + (dataItem.Size.y - 1) * gridConfig.Spacing.y;

        pickItemTransform.sizeDelta = new Vector2(itemPixelWidth, itemPixelHeight);
        

        // Рассчитываем локальную позицию (anchoredPosition)
        // Она отсчитывается от верхнего левого угла родительского RectTransform.
        float posX = topLeftCoord.x * (gridConfig.CellSize.x + gridConfig.Spacing.x) + gridConfig.Padding.Left;
        float posY = topLeftCoord.y * (gridConfig.CellSize.y + gridConfig.Spacing.y) + gridConfig.Padding.Top;

        // Присваиваем локальные координаты
        // Ось Y в UI направлена вниз, поэтому используем отрицательное значение
        print(posX + " " + -posY);
        pickItemTransform.anchoredPosition = new Vector2(posX, -posY);

    }
    public virtual void ResetItemUI() // coll from InventorySlot
    {
        dataItem = null;
        itemIcon.sprite = null;
        Destroy(gameObject);
    }
    public virtual void OnBeginDrag(PointerEventData eventData)
    {
        if (dataItem == null) return;
        canvasGroup.blocksRaycasts = false;
        canvasGroup.alpha = 0.6f;
        originalParent = transform.parent; //save the parent object of the item  

        SetPivotByItem(0.5f, 0.5f);

        pickItemTransform.SetParent(canvas.transform); //changing the parent object of an item
        pickItemTransform.SetAsLastSibling(); //sets item display priority   
         
    }
    public virtual void OnDrag(PointerEventData eventData) //moves an item to the mouse cursor position
    {
        if (dataItem == null) return; 
        pickItemTransform.position = eventData.position; 
    }
    public virtual void OnEndDrag(PointerEventData eventData)
    {
        if (dataItem == null) return;
        canvasGroup.blocksRaycasts = true;
        canvasGroup.alpha = 1f;
        SetPivotByItem(0,1);
        pickItemTransform.SetParent(originalParent);
        print("end drag");
    }

    private void SetPivotByItem(float x, float y)
    {
        pickItemTransform.pivot = new Vector2(x, y);
        pickItemTransform.anchorMin = new Vector2(x, y);
        pickItemTransform.anchorMax = new Vector2(x, y);
    }
    public void SetNewParent(Transform newParent)
    {
        originalParent = newParent;
    } 
}
