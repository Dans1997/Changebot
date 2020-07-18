using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSizeSwitcher : MonoBehaviour
{
    enum Size { Tiny = 0, Normal, Big, Count };
    Size previousSize = Size.Tiny;
    Size currentSize = Size.Tiny;

    [Header("Enable Size Randomizer")]
    [Tooltip("If true, player will change sizes according to variables below")]
    [SerializeField]  bool isRandomizerEnabled = false;
    [SerializeField] float randomizeMaxTime = 6f;
    float randomizeCounter = 0f;

    // Start is called before the first frame update
    void Start()
    {
        ChangePlayerSize(Size.Normal);
    }

    // Update is called once per frame
    void Update()
    {
        HandleKeyInput();
        HandleSizeRandomizer();
    }

    private void ChangePlayerSize(Size newSize)
    {
        if (currentSize.Equals(newSize)) return;
        previousSize = currentSize;
        currentSize = newSize;

        int playerIndex = 0;
        Vector2 lastPlayerPosition = transform.position;
        Transform playerToBeActivated = null;

        // Iterate through all child objects
        foreach (Transform player in transform)
        {
            player.gameObject.SetActive(false);
            if ((int)previousSize == playerIndex) lastPlayerPosition = player.position;
            if ((int)currentSize == playerIndex) playerToBeActivated = player;
            playerIndex++;
        }
        
        if (playerToBeActivated)
        {
            Enemy.player = playerToBeActivated;
            playerToBeActivated.gameObject.SetActive(true);
            playerToBeActivated.transform.position = lastPlayerPosition;
        }
    }

    // Control Powers Are Controlled By Keys (FOR NOW)
    private void HandleKeyInput()
    {
        if (Input.GetKeyDown(KeyCode.T)) ChangePlayerSize(Size.Tiny);

        else if (Input.GetKeyDown(KeyCode.Y)) ChangePlayerSize(Size.Normal);

        else if (Input.GetKeyDown(KeyCode.U)) ChangePlayerSize(Size.Big);
    }

    private void HandleSizeRandomizer()
    {
        if (!isRandomizerEnabled) return;

        randomizeCounter += Time.deltaTime;
       
        if(randomizeCounter >= randomizeMaxTime)
        {
            randomizeCounter = 0f;
            Size randomSize = currentSize;

            do randomSize = (Size) Random.Range(0, (int) Size.Count);
            while (randomSize == currentSize);

            ChangePlayerSize(randomSize);
        }
    }

}
