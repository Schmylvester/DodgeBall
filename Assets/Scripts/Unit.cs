using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    ushort m_actionsSincePickup = 0;
    Ball m_hasBall = null;
    [SerializeField] GameObject m_moveRadius = null;
    [SerializeField] GameObject m_aimCone = null;
    [SerializeField] SpriteRenderer[] m_actionQueueRenderers = null;
    Action[] m_actionQueue = { Action.Null, Action.Null };
    EntitySpawner m_entitySpawner = null;
    ushort m_team = 0;
    float m_pickUpRange = 0.5f;

    Vector2[] m_moveTargets = { Vector2.zero, Vector2.zero };
    public Vector2 m_throwDirection = Vector3.zero;
    public Unit m_passTo = null;
    public Transform m_handTransform = null;

    void pickUp()
    {
        Ball ball = nearBall();
        m_hasBall = ball;
        ball.m_state = BallState.Carried;
        m_actionsSincePickup = 0;
        ball.transform.parent = m_handTransform;
        ball.transform.position = Vector3.zero;
    }

    public bool hasBall()
    {
        switch (m_actionQueue[0])
        {
            case Action.Throw:
            case Action.Pass:
                return false;
            case Action.PickUp:
                return true;
            default: break;
        }
        return m_hasBall != null;
    }

    /// <summary>
    /// Looks for a nearby ball
    /// </summary>
    /// <returns>Returns a ball within unit's reach or null if there is not one</returns>
    public Ball nearBall()
    {
        foreach (Ball ball in m_entitySpawner.m_balls)
        {
            Vector2 myPos = m_actionQueue[0] == Action.Move ? m_moveTargets[0] : (Vector2)transform.position;
            if (ball.m_state == BallState.OnGround && Vector3.Distance(ball.transform.position, myPos) < m_pickUpRange)
            {
                return ball;
            }
        }
        return null;
    }

    public bool checkViolation()
    {
        return m_actionsSincePickup >= GameManager.Get.constants.g_maxActionsWithBall;
    }

    public void showMoveRadius(bool _value)
    {
        m_moveRadius.SetActive(_value);
    }

    public void showAimCone(bool _value)
    {
        m_aimCone.SetActive(_value);
    }

    public void setActionQueueRenderer(int _index, Color _colour)
    {
        m_actionQueueRenderers[_index].color = _colour;
    }

    public int actionQueueLength()
    {
        int i;
        for (i = 0; i < m_actionQueue.Length; ++i)
        {
            if (m_actionQueue[i] == Action.Null) return i;
        }
        return i;
    }

    public void addActionToQueue(Action _action)
    {
        m_actionQueue[actionQueueLength()] = _action;
    }

    public void initialise(EntitySpawner _entitySpawner)
    {
        m_entitySpawner = _entitySpawner;
    }

    public Action getActionInQueue(int index)
    {
        return m_actionQueue[index];
    }

    public void addMoveTarget(Vector2 _moveTarget)
    {
        m_moveTargets[actionQueueLength()] = _moveTarget;
    }
}
