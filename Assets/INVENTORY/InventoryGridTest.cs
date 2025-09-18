using System;
using System.Collections.Generic;
using UnityEngine;

public class InventoryGridTest : MonoBehaviour
{
    [SerializeField] private List<ItemConfig> testItems;
    private List<ItemConfig> addedItems = new();

    private InventoryGridHandler backPackGridHandler; 


    private int index;
    private int count;
    private void Awake()
    {
        var gridModel = FindObjectOfType<BackPackGridUI>().gridModel;
        backPackGridHandler = new InventoryGridHandler(gridModel);
    }
    public void AddRandomItem()
    {
        index = UnityEngine.Random.Range(0, testItems.Count);
        if (index < 0) return;

        var item = Instantiate(testItems[index]); // Клонируем
        if (backPackGridHandler.PlaceItemByGrid(item))
        {
            addedItems.Add(item);
            count++;
        }
    }

    public void RemoveRandomItem()
    {
        if (count <= 0 || addedItems.Count == 0) return;

        index = UnityEngine.Random.Range(0, count);
        var item = addedItems[index];
        backPackGridHandler.RemoveItemByGrid(item);
        addedItems.RemoveAt(index);
        count--;
    }
}
