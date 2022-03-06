using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimCone : MonoBehaviour
{
    [SerializeField] float m_range = 1;
    [SerializeField] float m_angle = 25;
    [SerializeField] Transform m_firstMask = null;
    [SerializeField] Transform m_secondMask = null;

    private void Start()
    {
        initialise(m_range, m_angle);
    }

    public void initialise(float _range, float _angle)
    {
        m_range = _range;
        m_angle = _angle;

        transform.localScale = Vector3.one * m_range;
        m_firstMask.RotateAround(m_firstMask.position, Vector3.back, (180 - m_angle) / 2);
        m_secondMask.RotateAround(m_secondMask.position, Vector3.forward, (180 - m_angle) / 2);
    }

    void Update()
    {
        aimWithMouse();
    }

    void aimWithMouse()
    {
        Vector2 mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 directionToMouse = mouse - (Vector2)transform.position;
        transform.up = directionToMouse;
    }
}