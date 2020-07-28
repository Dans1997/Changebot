using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointMaster : MonoBehaviour
{
    private static CheckPointMaster instance;
    [SerializeField] Vector2 lastCheckpoint = new Vector2(0, 0);

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

    public void SetCheckPoint(Vector2 newCheckpoint) { lastCheckpoint = newCheckpoint; }

    public Vector2 GetCheckPoint() { return lastCheckpoint; }
}
