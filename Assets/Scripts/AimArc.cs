using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimArc : MonoBehaviour
{
    [SerializeField] float m_range = 1;
    [SerializeField] float m_angle = 25;
    [SerializeField] Transform m_rotationalMaskTransform = null;

    public void initialise(float _range, float _angle)
    {
        m_range = _range;
        m_angle = _angle;

        transform.localScale = Vector3.one * m_range;
        m_rotationalMaskTransform.RotateAround(m_rotationalMaskTransform.position, Vector3.back, m_angle);
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
        transform.RotateAround(transform.position, Vector3.forward, m_angle / 2);
    }
}
