using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    [SerializeField] bool m_debugOn = false;
    [SerializeField] Animator m_anim = null;
    string m_animState = "";
    string m_lastAnimState = "";

    private void Update()
    {
        if (m_debugOn) animationDebug();
        updateAnimation();
    }

    void animationDebug()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                StartMoving();
            }
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                StopMoving();
            }
            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                PickUp();
            }
            if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                Throw();
            }
            if (Input.GetKeyDown(KeyCode.Alpha5))
            {
                Fall();
            }
            if (Input.GetKeyDown(KeyCode.Alpha6))
            {
                Block();
            }
        }
    }

    void updateAnimation()
    {
        if (m_debugOn)
        {
            Debug.Log(gameObject.name + m_animState);
        }
        if (m_animState != "")
        {
            if (m_animState != "Moving" && m_animState != "Ready")
            {
                m_lastAnimState = m_animState;
                m_animState = "";
            }
        }
        else if (m_lastAnimState != "")
        {
            m_anim.SetBool(m_lastAnimState, false);
            m_lastAnimState = "";
        }
    }

    public void StartMoving()
    {
        m_animState = "Moving";
        m_anim.SetBool("Moving", true);
    }

    public void StopMoving()
    {
        m_animState = "";
        m_anim.SetBool("Moving", false);
    }

    public void Ready()
    {
        m_animState = "Ready";
        m_anim.SetBool("Ready", true);
    }

    public void Unready()
    {
        m_animState = "";
        m_anim.SetBool("Ready", false);
    }

    public void Throw()
    {
        m_animState = "Throwing";
        m_anim.SetBool(m_animState, true);
    }

    public void Fall()
    {
        m_animState= "KnockedDown";
        m_anim.SetBool(m_animState, true);
    }

    public void Block()
    {
        m_animState = "Blocking";
        m_anim.SetBool(m_animState, true);
    }

    public void PickUp()
    {
        m_animState = "PickingUp";
        m_anim.SetBool(m_animState, true);
    }
}
