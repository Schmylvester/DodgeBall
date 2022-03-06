using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntitySpawner : MonoBehaviour
{
    [SerializeField] GameObject m_unitPrefab = null;
    [SerializeField] Transform m_court = null;
    public List<Unit> m_allUnits { get; } = new List<Unit>();
    public Ball[] m_balls = null;

    void Start()
    {
        Vector2[] positions =
        {
            new Vector2(-0.9f, 1.6f),
            new Vector2(-0.9f, -1.6f),
        };
        spawnUnits(positions);
    }

    public void spawnUnits(Vector2[] _positions)
    {
        foreach (Vector2 pos in _positions)
        {
            GameObject instance = Instantiate(m_unitPrefab, m_court, false);
            instance.transform.Translate(pos, Space.Self);
            Unit unit = instance.GetComponent<Unit>();
            m_allUnits.Add(unit);
            unit.initialise(this);
        }
    }
}
