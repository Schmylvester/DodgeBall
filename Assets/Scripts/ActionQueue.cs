using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionQueue : MonoBehaviour
{
    [SerializeField] GameObject[] m_players;
    
    IEnumerator commence()
    {
        yield return null;
    }

    public void startRound()
    {
        StartCoroutine(commence());
    }
}
