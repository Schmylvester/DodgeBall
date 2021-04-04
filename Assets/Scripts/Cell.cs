using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour
{
    Vector2Int m_cellIndex = new Vector2Int();
    public Vector2Int cellIndex
    {
        set { m_cellIndex = value; }
        get { return m_cellIndex; }
    }
}
