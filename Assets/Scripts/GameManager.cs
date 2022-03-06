using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Essentially controls singletons so as to easier keep track of them.
/// Put all singletons on a gameObject with this and reference them here
/// rather than making them all singletons individually
/// </summary>

public class GameManager : MonoBehaviour
{
    public static GameManager Get = null;
    public Constants constants { get; } = new Constants();

    private void Awake()
    {
        if (Get)
        {
            Debug.LogError("Hey why do we have more than one of these?");
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
        Get = this;
    }
}
