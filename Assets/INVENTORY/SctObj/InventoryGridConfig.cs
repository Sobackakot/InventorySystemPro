using System;
using UnityEngine;

/// <summary>
/// ������������ ����������� ����� ��� ������-�������� �������.
/// ������������ ��� ������� �������� �����, ������������, ����������� � ������������� ����������.
/// </summary>

[CreateAssetMenu(fileName = "GridInventory", menuName = "Inventory/GridInventoryConfig")]
public class InventoryGridConfig : ScriptableObject
{

    [field: SerializeField] public InventoryType invnetoryType { get; private set; }
    /// <summary>���������� ������� ������ ���� �����.</summary>
    [field: SerializeField] public Padding Padding { get; private set; } 
    /// <summary>������ ������ � ��������.</summary>
    [field: SerializeField] public Vector2Int CellSize { get; private set; }

    /// <summary>���������� ����� ��������.</summary>
    [field: SerializeField] public Vector2Int Spacing { get; private set; }


    /// <summary>
    /// ���������, ����� �� ��������� ����� �� ��������� � ����������� �� ������ �������������.
    /// </summary>
    /// <param name="occupiedCellCount">���������� ������� �����.</param>
    /// <returns>True, ���� ������ ������ ��� ����� ThresholdExpand.</returns>
    public bool ShouldExpand(uint occupiedCellCount)
    {
        uint totalCells = RowCount * ColumnCount;
        if (totalCells == 0) return false; // �������� ������� �� ����
        return (float)occupiedCellCount / totalCells >= ThresholdExpand;
    }

    /// <summary>
    /// ��������� ���������� ����� �� �������� <see cref="NewRowsDynamic"/>.
    /// ������������ ��� ���������� AutoExpand.
    /// </summary>
    public void ExpandRows()
    {
        RowCount += NewRowsDynamic;
    }

     
    /// <summary>
    /// ���������� �������. 
    /// </summary>
    [field: SerializeField] public uint ColumnCount { get; private set; }

    /// <summary>
    /// ���������� �����. 
    /// </summary>
    [field: SerializeField] public uint RowCount { get; private set; }

    /// <summary>
    /// �������� �������������� ���������� ����� �� ������� ��� ���������� ������ ����������.
    /// ���������� �������������, ���� Constraint = Flexible.
    /// </summary>
    [field: SerializeField] public bool AutoExpand { get; private set; } = true;

    /// <summary>
    /// ���������� �����, ����������� ��� ����������. �������� �� 3 �� 10.
    /// </summary>
    [field: Range(3, 10), SerializeField] public uint NewRowsDynamic { get; private set; }

    /// <summary>
    /// ����� ������������� (0.5�0.9), ��� ������� ������������ <see cref="ShouldExpand"/>.
    /// </summary>
    [field: Range(0.5f, 0.9f), SerializeField] public float ThresholdExpand { get; private set; }
}

/// <summary>
/// ��������� �������� ��� ����������� �����.
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
    /// <summary>������ �����.</summary>
    public int Left;
    /// <summary>������ ������.</summary>
    public int Right;
    /// <summary>������ ������.</summary>
    public int Top;
    /// <summary>������ �����.</summary>
    public int Bottom;
}
public enum InventoryType
{
    BackPack,
    LootBox
}