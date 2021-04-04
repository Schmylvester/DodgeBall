using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
	[SerializeField] UnityEngine.Grid m_grid = null;
	[SerializeField] GameObject m_cellPrefab = null;

	[SerializeField] protected Vector2Int m_gridSize = new Vector2Int();
    protected List<Cell> m_cells = new List<Cell>();

    void Start()
    {
        createGrid();
    }

    protected virtual void createGrid()
    {
        transform.position += m_gridSize.x % 2 == 0 ? (Vector3.right * 0.5f) : Vector3.zero;
        transform.position += m_gridSize.y % 2 == 0 ? (Vector3.up * 0.5f) : Vector3.zero;
        int i = 0;
        for (int x = 0; x < m_gridSize.x; ++x)
        {
            int moddedX = x - (m_gridSize.x / 2);
            for (int y = 0; y < m_gridSize.y; ++y)
            {
                int moddedY = y - (m_gridSize.y / 2);
                GameObject cellInstance = Instantiate(m_cellPrefab, transform);
                cellInstance.transform.position = m_grid.CellToWorld(new Vector3Int(moddedX, moddedY, 0));
                cellInstance.name = "Cell (" + x + ", " + y + ")";
                Cell cell = cellInstance.GetComponent<Cell>();
                cell.cellIndex = new Vector2Int(x, y);
                m_cells.Add(cell);
            }
        }
    }

    public Cell getCell(int x, int y)
    {
        if (x < 0 || x >= m_gridSize.x || y < 0 || y >= m_gridSize.y)
            return null;
        return m_cells[(x * m_gridSize.y) + y];
    }
}