using UnityEngine;
using UnityEngine.EventSystems;

public class BackPackGridDrop : InventoryGridDropBase
{ 
    public override void OnDrop(PointerEventData eventData)
    {
        print("drop from Inventory");
        base.OnDrop(eventData);
    } 
}
