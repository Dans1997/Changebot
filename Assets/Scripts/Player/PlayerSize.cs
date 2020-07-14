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
    // Tiny - 0.5
    // Normal - 1
    // Big - 2
    private void HandleSizeChange()
    {
        // Control Power Are Controlled By Keys (FOR NOW)
        if (Input.GetKeyDown(KeyCode.T))
        {
            if (playerSize.Equals(Size.Tiny)) { Debug.Log("Player is already tiny!"); return; }
            Debug.Log("Go Tiny!");
            playerSize = Size.Tiny;
            transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
            return;
        }
        else if (Input.GetKeyDown(KeyCode.Y))
        {
            if (playerSize.Equals(Size.Normal)) { Debug.Log("Player is already normal!"); return; }
            Debug.Log("Go Normal!");
            playerSize = Size.Normal;
            transform.localScale = new Vector3(1f,1f,1f);
            return;
        }
        else if (Input.GetKeyDown(KeyCode.U))
        {
            if (playerSize.Equals(Size.Big)) { Debug.Log("Player is already big!"); return; }
            Debug.Log("Go Big!");
            playerSize = Size.Big;
            transform.localScale = new Vector3(2f,2f,2f);
            return;
        }
    }
}
