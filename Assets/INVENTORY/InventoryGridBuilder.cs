using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class InventoryGridBuilder : MonoBehaviour
{
    [SerializeField] RectTransform GridBackground; 
    [SerializeField] InventoryGridConfig config;
    [SerializeField] GameObject cellGridPrefab; 
    private CanvasGroup[,] cellsGrid;
    private void Awake()
    {
        cellsGrid = new CanvasGroup[config.ColumnCount,config.RowCount]; 
        BuildGrid();
    }
    private void BuildGrid()
    {
        // ������� ������ ������, ���� ��� ����
        foreach (Transform child in GridBackground)
        {
            Destroy(child.gameObject);
        }

        // ����������� GridLayoutGroup �� GridBackground
        var gridLayout = GridBackground.GetComponent<GridLayoutGroup>();
        if (gridLayout != null)
        {
            gridLayout.cellSize = config.CellSize;
            gridLayout.spacing = config.Spacing;
            gridLayout.padding.left = (int)config.Padding.Left;
            gridLayout.padding.right = (int)config.Padding.Right;
            gridLayout.padding.top = (int)config.Padding.Top;
            gridLayout.padding.bottom = (int)config.Padding.Bottom;
        }

        // ������ ����������� �����
        for (ushort y = 0; y < config.RowCount; y++)
        {
            for (ushort x = 0; x < config.ColumnCount; x++)
            {
                // ������� ������ UI, ������� ����� ��������� � GridLayoutGroup
                cellsGrid[x,y] = Instantiate(cellGridPrefab, GridBackground).GetComponent<CanvasGroup>();
            }
        } 
    }   
}
