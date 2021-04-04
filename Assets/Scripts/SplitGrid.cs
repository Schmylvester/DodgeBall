using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplitGrid : Grid
{
    enum SplitMode
    {
        Null = -1,

        Horizontal,
        Vertical,
        Quad
    }

    [SerializeField] SplitMode m_mode;
    [SerializeField]
    Color[] m_colours =
    {
        Color.Lerp(Color.red, Color.white, 0.6f),
        Color.Lerp(Color.blue, Color.white, 0.6f),
        Color.Lerp(Color.green, Color.white, 0.6f),
        Color.Lerp(Color.yellow, Color.white, 0.6f)
    };

    protected override void createGrid()
    {
        base.createGrid();
        split();
    }

    void split()
    {
        switch (m_mode)
        {
            case SplitMode.Horizontal:
                splitHorizontal();
                break;
            case SplitMode.Vertical:
                splitVertical();
                break;
            case SplitMode.Quad:
                splitQuad();
                break;

            default:
                Debug.LogError("Grid split mode missing");
                break;
        }
    }

    void splitHorizontal()
    {
        foreach (Cell cell in m_cells)
        {
            if (cell.cellIndex.y < m_gridSize.y / 2)
            {
                cell.GetComponent<SpriteRenderer>().color = m_colours[0];
            }
            else if (cell.cellIndex.y > m_gridSize.y / 2 || (cell.cellIndex.y == m_gridSize.y / 2 && m_gridSize.y % 2 == 0))
            {
                cell.GetComponent<SpriteRenderer>().color = m_colours[1];
            }
        }
    }

    void splitVertical()
    {
        foreach (Cell cell in m_cells)
        {
            if (cell.cellIndex.x < m_gridSize.x / 2)
            {
                cell.GetComponent<SpriteRenderer>().color = m_colours[0];
            }
            else if (cell.cellIndex.x > m_gridSize.x / 2 || (cell.cellIndex.x == m_gridSize.x / 2 && m_gridSize.x % 2 == 0))
            {
                cell.GetComponent<SpriteRenderer>().color = m_colours[1];
            }
        }
    }

    void splitQuad()
    {
        foreach (Cell cell in m_cells)
        {
            if (cell.cellIndex.x < m_gridSize.x / 2)
            {
                if (cell.cellIndex.y < m_gridSize.y / 2)
                {
                    cell.GetComponent<SpriteRenderer>().color = m_colours[0];
                }
                else if (cell.cellIndex.y > m_gridSize.y / 2 || (cell.cellIndex.y == m_gridSize.y / 2 && m_gridSize.y % 2 == 0))
                {
                    cell.GetComponent<SpriteRenderer>().color = m_colours[1];
                }
            }
            else if (cell.cellIndex.x > m_gridSize.x / 2 || (cell.cellIndex.x == m_gridSize.x / 2 && m_gridSize.x % 2 == 0))
            {
                if (cell.cellIndex.y < m_gridSize.y / 2)
                {
                    cell.GetComponent<SpriteRenderer>().color = m_colours[2];
                }
                else if (cell.cellIndex.y > m_gridSize.y / 2 || (cell.cellIndex.y == m_gridSize.y / 2 && m_gridSize.y % 2 == 0))
                {
                    cell.GetComponent<SpriteRenderer>().color = m_colours[3];
                }
            }
        }
    }
}
