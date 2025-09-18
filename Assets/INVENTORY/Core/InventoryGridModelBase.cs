using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class InventoryGridModelBase 
{
    public InventoryGridModelBase(InventoryGridConfig gridConfig)
    {
        this.gridConfig = gridConfig;
        occupied = new bool[gridConfig.ColumnCount, gridConfig.RowCount];
    }
    protected InventoryGridConfig gridConfig;
    protected bool[,] occupied { get; private set; }

    public readonly Dictionary<ItemConfig, Vector2Int> itemPositions = new();

    public abstract event Action<ItemConfig, Vector2Int> OnItemPlaced;
    public abstract event Action<ItemConfig> OnItemRemove;

    
    public abstract void OnItemRemoveFromUI(ItemConfig itemData);
    public abstract void OnItemPlaceForUI(ItemConfig itemData, Vector2Int topLeft);
    public virtual bool TryMoveItem(ItemConfig itemData, Vector2Int newTopLeft)
    {
        if (!itemPositions.ContainsKey(itemData)) return false;

        if (itemPositions.TryGetValue(itemData, out var oldTopLeft))
            RemoveItemFromGrid(itemData, oldTopLeft);

        if (CanPlaceItem(itemData, newTopLeft))
        {
            PlaceItem(itemData, newTopLeft);
            return true;
        }
        else
        {
            PlaceItem(itemData, oldTopLeft);
            return false;
        }
    }
    public virtual bool CheckingСellsForOutOfGridLimits(ItemConfig itemData, Vector2Int topLeft)
    {
        for (int y = 0; y < itemData.Size.y; y++)
        {
            for (int x = 0; x < itemData.Size.x; x++)
            {
                int gridX = x + topLeft.x;
                int gridY = y + topLeft.y;

                if (gridX < 0 || gridX >= gridConfig.ColumnCount
                    || gridY < 0 || gridY >= gridConfig.RowCount)
                    return false;
            }
        }
        return true;
    }
    public virtual bool CanPlaceItem(ItemConfig itemData, Vector2Int topLeft)
    {
        for (int y = 0; y < itemData.Size.y; y++)
        {
            for (int x = 0; x < itemData.Size.x; x++)
            {
                int gridX = x + topLeft.x;
                int gridY = y + topLeft.y;

                if (!CheckingСellsForOutOfGridLimits(itemData, topLeft) || occupied[gridX, gridY])
                    return false;
            }
        }
        return true;
    }

    public virtual void PlaceItem(ItemConfig itemData, Vector2Int topLeft)
    {
        for (int y = 0; y < itemData.Size.y; y++)
        {
            for (int x = 0; x < itemData.Size.x; x++)
            {
                int gridX = x + topLeft.x;
                int gridY = y + topLeft.y;
                occupied[gridX, gridY] = true;
            }
        }
        itemPositions[itemData] = topLeft;
        OnItemPlaceForUI(itemData, topLeft);
        Debug.Log("placeItem");
    }

    public virtual void RemoveItemFromList(ItemConfig itemData)
    {
        if (!itemPositions.TryGetValue(itemData, out var topLeft)) return;

        RemoveItemFromGrid(itemData, topLeft);
        itemPositions.Remove(itemData);
        OnItemRemoveFromUI(itemData);
    }

    private void RemoveItemFromGrid(ItemConfig itemData, Vector2Int topLeft)
    {
        for (int y = 0; y < itemData.Size.y; y++)
        {
            for (int x = 0; x < itemData.Size.x; x++)
            {
                int gridX = x + topLeft.x;
                int gridY = y + topLeft.y;

                if (gridX >= 0 && gridX < gridConfig.ColumnCount
                    && gridY >= 0 && gridY < gridConfig.RowCount)
                {
                    occupied[gridX, gridY] = false;
                }

            }
        }
    }

    public virtual bool TryFindFreePosition(ItemConfig itemData, out Vector2Int topLeft)
    {
        for (int y = 0; y <= gridConfig.RowCount - itemData.Size.y; y++)
        {
            for (int x = 0; x <= gridConfig.ColumnCount - itemData.Size.x; x++)
            {
                Vector2Int pos = new(x, y);
                if (CanPlaceItem(itemData, pos))
                {
                    topLeft = pos;
                    return true;
                }
            }
        }

        topLeft = default;
        return false;
    }

    public virtual Vector2Int GetPointerCellsXY(Vector2 pixelPosition, ItemConfig dataItem)
    {
        // Учитываем отступы 
        float xWithoutPadding = pixelPosition.x - gridConfig.Padding.Left;
        float yWithoutPadding = pixelPosition.y - gridConfig.Padding.Top;

        // Рассчитываем размер ячейки + отступ между ячейками
        float effectiveCellWidth = gridConfig.CellSize.x + gridConfig.Spacing.x;
        float effectiveCellHeight = gridConfig.CellSize.y + gridConfig.Spacing.y;

        // Определяем координаты ячейки
        int gridX = Mathf.FloorToInt(xWithoutPadding / effectiveCellWidth);
        int gridY = Mathf.FloorToInt(yWithoutPadding / effectiveCellHeight);

        // Рассчитываем, где должен быть верхний левый угол предмета.
        // Это смещение нужно, чтобы центр предмета совпал с курсором.
        int offsetCorrectionX = Mathf.FloorToInt(dataItem.Size.x / 2.0f);
        int offsetCorrectionY = Mathf.FloorToInt(dataItem.Size.y / 2.0f);

        int fixGridX = gridX - offsetCorrectionX;
        int fixGridY = gridY - offsetCorrectionY;

        // Ограничиваем координаты границами сетки
        fixGridX = Mathf.Clamp(fixGridX, 0, (int)gridConfig.ColumnCount - 1);
        fixGridY = Mathf.Clamp(fixGridY, 0, (int)gridConfig.RowCount - 1);

        Debug.Log(fixGridX + ", " + fixGridY);
        return new Vector2Int(fixGridX, fixGridY);
    }
     
}
