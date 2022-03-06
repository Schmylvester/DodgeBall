using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitSelection : MonoBehaviour
{
    [SerializeField] GameObject m_noUnitSelected = null;
    [SerializeField] GameObject m_unitSelected = null;
    [SerializeField] ActionManager m_actionManager = null;
    [SerializeField] EntitySpawner m_entitySpawner = null;

    bool m_canSelectUnit = true;

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && m_canSelectUnit)
        {
            Vector2 clickPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            List<Unit> possiblyClickedUnits = getNearbyUnitsToClick(clickPos);
            if (possiblyClickedUnits.Count == 0)
                return;
            possiblyClickedUnits.Sort((Unit a, Unit b) =>
            {
                int aActionQueue = a.actionQueueLength();
                int bActionQueue = b.actionQueueLength();
                if (aActionQueue != bActionQueue)
                {
                    return a.actionQueueLength() - b.actionQueueLength();
                }
                return distanceToUnit(a, clickPos).magnitude > distanceToUnit(b, clickPos).magnitude ? -1 : 1;
            });
            selectUnit(possiblyClickedUnits[0]);
        }
    }

    public List<Unit> getNearbyUnitsToClick(Vector2 clickPos)
    {
        List<Unit> possiblyClickedUnits = new List<Unit>();
        foreach (Unit unit in m_entitySpawner.m_allUnits)
        {
            Vector2 distance = distanceToUnit(unit, clickPos);
            if (distance.x < 0.25f && distance.y < 0.6f)
            {
                possiblyClickedUnits.Add(unit);
            }
        }
        return possiblyClickedUnits;
    }

    void selectUnit(Unit _unit)
    {
        m_actionManager.onUnitSelected(_unit);
        m_unitSelected.SetActive(true);
        m_noUnitSelected.SetActive(false);
    }

    Vector2 distanceToUnit(Unit _unit, Vector2 _clickPos)
    {
        float xDIst = Mathf.Abs(_unit.transform.position.x - _clickPos.x);
        float yDIst = Mathf.Abs(_unit.transform.position.y - _clickPos.y);
        return new Vector2(xDIst, yDIst);
    }

    public void setCanSelectUnit(bool _setTo)
    {
        m_canSelectUnit = _setTo;
    }
}
