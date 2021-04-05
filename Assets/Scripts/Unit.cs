using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct UnitStats
{
    // How likely your first action is to be before other units'
    public int actionSpeed;
    // How likely your second action is to be before other units'
    public int actionRecovery;
    // How far the ball stays in the air when you throw it
    public int throwPower;
    // How likely you are to dodge a ball that would otherwise hit you
    public int dodgeChance;
    // How likely you are to catch a ball that comes near you
    public int catchChance;
}

public class Unit : MonoBehaviour
{
    UnitStats stats;

    public bool dodgeBall(Vector3 _ballStart, Vector3 _ballTrajectory)
    {
        Vector3 myTransform = transform.position;
        Vector3 directionFromBallStart = (myTransform - _ballStart).normalized;

        Vector3 direction = _ballTrajectory.normalized;
        Vector3 closestPoint = direction * Vector3.Dot(myTransform, direction);
        float proximity = (closestPoint - myTransform).magnitude;

        float distance = Vector3.Distance(myTransform, _ballStart);
        float speed = (_ballTrajectory.magnitude / distance);


        return false;
    }
}