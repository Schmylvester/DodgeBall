using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BallState
{
    Null = -1,

    OnGround,
    Carried,
    Thrown,

    COUNT
};

public class Ball : MonoBehaviour
{
    public BallState m_state = BallState.OnGround;
}
