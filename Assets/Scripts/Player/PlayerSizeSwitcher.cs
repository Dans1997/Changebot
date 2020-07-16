using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSizeSwitcher : MonoBehaviour
{
    enum Size { Tiny = 0, Normal, Big };

    [SerializeField] Size previousSize = Size.Normal;
    [SerializeField] Size currentSize = Size.Normal;

    // Start is called before the first frame update
    void Start()
    {
        ChangePlayerSize();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessKeyInput();
    }

    private void ProcessKeyInput()
    {
        // Control Powers Are Controlled By Keys (FOR NOW)
        if (Input.GetKeyDown(KeyCode.T))
        {
            if (currentSize.Equals(Size.Tiny)) return;
            previousSize = currentSize;
            currentSize = Size.Tiny;
        }
        else if (Input.GetKeyDown(KeyCode.Y))
        {
            if (currentSize.Equals(Size.Normal)) return;
            previousSize = currentSize;
            currentSize = Size.Normal;
        }
        else if (Input.GetKeyDown(KeyCode.U))
        {
            if (currentSize.Equals(Size.Big)) return;
            previousSize = currentSize;
            currentSize = Size.Big;
        }
        else return;
        ChangePlayerSize();
    }

    private void ChangePlayerSize()
    {
        int playerIndex = 0; 
        Vector2 lastPlayerPosition = transform.position;
        Transform playerToBeActivated = null;

        foreach (Transform player in transform)
        {
            player.gameObject.SetActive(false);
            if ((int)previousSize == playerIndex) lastPlayerPosition = player.position;
            if ((int)currentSize == playerIndex) playerToBeActivated = player;
            playerIndex++;
        }

        if (playerToBeActivated)
        {
            playerToBeActivated.gameObject.SetActive(true);
            playerToBeActivated.transform.position = lastPlayerPosition;
        }
    }
}
