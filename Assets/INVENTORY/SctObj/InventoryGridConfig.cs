using System;
using UnityEngine;

/// <summary>
/// Конфигурация инвентарной сетки для тетрис-подобной системы.
/// Используется для задания размеров ячеек, выравнивания, ограничений и динамического расширения.
/// </summary>

[CreateAssetMenu(fileName = "GridInventory", menuName = "Inventory/GridInventoryConfig")]
public class InventoryGridConfig : ScriptableObject
{

    [field: SerializeField] public InventoryType invnetoryType { get; private set; }
    /// <summary>Внутренние отступы вокруг всей сетки.</summary>
    [field: SerializeField] public Padding Padding { get; private set; } 
    /// <summary>Размер ячейки в пикселях.</summary>
    [field: SerializeField] public Vector2Int CellSize { get; private set; }

    /// <summary>Расстояние между ячейками.</summary>
    [field: SerializeField] public Vector2Int Spacing { get; private set; }


    /// <summary>
    /// Проверяет, нужно ли расширить сетку по вертикали в зависимости от порога заполненности.
    /// </summary>
    /// <param name="occupiedCellCount">Количество занятых ячеек.</param>
    /// <returns>True, если занято больше или равно ThresholdExpand.</returns>
    public bool ShouldExpand(uint occupiedCellCount)
    {
        uint totalCells = RowCount * ColumnCount;
        if (totalCells == 0) return false; // Избегаем деления на ноль
        return (float)occupiedCellCount / totalCells >= ThresholdExpand;
    }

    /// <summary>
    /// Расширяет количество строк на значение <see cref="NewRowsDynamic"/>.
    /// Используется при включённом AutoExpand.
    /// </summary>
    public void ExpandRows()
    {
        RowCount += NewRowsDynamic;
    }

     
    /// <summary>
    /// Количество колонок. 
    /// </summary>
    [field: SerializeField] public uint ColumnCount { get; private set; }

    /// <summary>
    /// Количество строк. 
    /// </summary>
    [field: SerializeField] public uint RowCount { get; private set; }

    /// <summary>
    /// Включает автоматическое расширение сетки по строкам при превышении порога заполнения.
    /// Включается автоматически, если Constraint = Flexible.
    /// </summary>
    [field: SerializeField] public bool AutoExpand { get; private set; } = true;

    /// <summary>
    /// Количество строк, добавляемое при расширении. Значение от 3 до 10.
    /// </summary>
    [field: Range(3, 10), SerializeField] public uint NewRowsDynamic { get; private set; }

    /// <summary>
    /// Порог заполненности (0.5–0.9), при котором активируется <see cref="ShouldExpand"/>.
    /// </summary>
    [field: Range(0.5f, 0.9f), SerializeField] public float ThresholdExpand { get; private set; }
}

/// <summary>
/// Структура отступов для инвентарной сетки.
/// </summary>
[Serializable]
public struct Padding
{
    public Padding(int Left, int Right, int Top, int Bottom)
    {
        this.Left = Left;
        this.Right = Right;
        this.Top = Top;
        this.Bottom = Bottom;
    }
    /// <summary>Отступ слева.</summary>
    public int Left;
    /// <summary>Отступ справа.</summary>
    public int Right;
    /// <summary>Отступ сверху.</summary>
    public int Top;
    /// <summary>Отступ снизу.</summary>
    public int Bottom;
}
public enum InventoryType
{
    BackPack,
    LootBox
}