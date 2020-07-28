using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointMaster : MonoBehaviour
{
    private static CheckPointMaster instance;
    Vector2 lastCheckpoint = new Vector2(0, 0);

    int currentTimeInSeconds = 0;

    // Awake is called before Start
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(instance);
        }
        else Destroy(gameObject);
    }

    public int GetCheckPointTime() { return currentTimeInSeconds; }

    public Vector2 GetCheckPoint() { return lastCheckpoint; }

    public void SetCheckPoint(Vector2 newCheckpoint) 
    { 
        lastCheckpoint = newCheckpoint;
        currentTimeInSeconds = FindObjectOfType<Timer>().GetTimeElapsed();
    }
}
