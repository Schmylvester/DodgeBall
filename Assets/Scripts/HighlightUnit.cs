using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighlightUnit : MonoBehaviour
{
    [SerializeField] Transform m_arrow = null;
    [SerializeField] SpriteRenderer m_glow = null;

    [SerializeField] Vector2 m_arrowRange;
    [SerializeField] float m_arrowSpeed;

    bool m_isHighlighted = false;
    
    void Start()
    {
        highlight();
    }

    public void highlight()
    {
        m_isHighlighted = true;
        m_arrow.gameObject.SetActive(true);
        m_glow.gameObject.SetActive(true);

        StartCoroutine(bounceArrow());
    }

    IEnumerator bounceArrow()
    {
        bool arrowDir = false;
        while (m_isHighlighted)
        {
            if (!arrowDir)
            {
                m_arrow.localPosition += Vector3.down * m_arrowSpeed * Time.deltaTime;
                if (m_arrow.localPosition.y < m_arrowRange.x)
                {
                    arrowDir = true;
                }
            }
            else
            {
                m_arrow.localPosition += Vector3.up * m_arrowSpeed * Time.deltaTime;
                if (m_arrow.localPosition.y > m_arrowRange.y)
                {
                    arrowDir = false;
                }
            }
            yield return null;
        }
        yield return null;
    }
}
