using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSize : MonoBehaviour
{
    enum Size { Tiny, Normal, Big };
    private Size playerSize = Size.Normal;

    // Start is called before the first frame update
    void Start()
    {
        playerSize = Size.Normal;
    }

    // Update is called once per frame
    void Update()
    {
        HandleSizeChange();
    }

    private void HandleSizeChange()
    {
        // Control Power Are Controlled By Keys (FOR NOW)
        if (Input.GetKeyDown(KeyCode.T))
        {
            if (playerSize.Equals(Size.Tiny)) { Debug.Log("Player is already tiny!"); return; }
            Debug.Log("Go Tiny!");
            playerSize = Size.Tiny;
            return;
        }
        else if (Input.GetKeyDown(KeyCode.Y))
        {
            if (playerSize.Equals(Size.Normal)) { Debug.Log("Player is already normal!"); return; }
            Debug.Log("Go Normal!");
            playerSize = Size.Normal;
            return;
        }
        else if (Input.GetKeyDown(KeyCode.U))
        {
            if (playerSize.Equals(Size.Big)) { Debug.Log("Player is already big!"); return; }
            Debug.Log("Go Big!");
            playerSize = Size.Big;
            return;
        }
    }
}
