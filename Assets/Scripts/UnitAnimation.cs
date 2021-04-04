using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitAnimation : MonoBehaviour
{
    enum AnimState { Idle, Running, PickingUp, Throwing, KnockedDown }
    Animator m_controller;
    AnimState m_currentState = AnimState.Idle;
    
    void Start()
    {
        m_controller = GetComponent<Animator>();
    }

    void Update()
    {
        m_controller.SetBool("Throwing", m_currentState == AnimState.Throwing);
        m_controller.SetBool("PickingUp", m_currentState == AnimState.PickingUp);
        m_controller.SetBool("KnockedDown", m_currentState == AnimState.KnockedDown);

        if (m_currentState != AnimState.Running)
        {
            m_currentState = AnimState.Idle;
        }
    }

    void setAnimState(AnimState _newState)
    {
        m_controller.SetBool("Moving", _newState == AnimState.Running);
        m_currentState = _newState;
    }
}
