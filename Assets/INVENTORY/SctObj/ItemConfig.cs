using System;
using UnityEngine;

[CreateAssetMenu( fileName = "ItemTetris", menuName = "InventoryTetris/NewItem")]
public class ItemConfig : ScriptableObject
{
    [field: SerializeField] public string ItemId { get; private set; } = Guid.NewGuid().ToString();
    [field: SerializeField] public bool Rotatable { get; private set; } = false;
    [field: SerializeField] public bool Stackable { get; private set; } = false;
    [field: SerializeField] public int MaxStack { get; private set; } = 1;
    [field: SerializeField] public string NameItem { get; private set; }
    [field: SerializeField] public Sprite IconItem { get; private set; }

    [field: SerializeField] public Vector2Int Size { get; private set; } = new Vector2Int(1, 1);
}
