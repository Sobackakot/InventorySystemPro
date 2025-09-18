using UnityEngine.EventSystems;

public class LootBoxGridDrop : InventoryGridDropBase
{
    public override void OnDrop(PointerEventData eventData)
    {
        print("drop from Loot Box");
        base.OnDrop(eventData);
    }
}
