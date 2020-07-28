using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSizeSwitcher : MonoBehaviour
{
    [Header("Enable Size Randomizer")]
    [Tooltip("If true, player will change sizes according to variables below")]
    [SerializeField]  bool isRandomizerEnabled = false;
    [SerializeField] CameraFollow cameraFollow;

    [Header("SFXs")]
    [SerializeField] AudioClip changeToTinySFX;
    [SerializeField] AudioClip changeToNormalSFX;
    [SerializeField] AudioClip changeToBigSFX;
    [SerializeField] AudioClip aboutToChangeSFX;


    public int TIME_BETWEEN_SIZE_CHANGE = 6;

    enum Size { Tiny = 0, Normal, Big, Count };
    Size previousSize = Size.Normal;
    Size currentSize = Size.Normal;
    int randomizeTime;

    // Cached Components
    AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        randomizeTime = TIME_BETWEEN_SIZE_CHANGE;
        transform.position = FindObjectOfType<CheckPointMaster>().GetCheckPoint();
        audioSource = GetComponent<AudioSource>();
        ChangePlayerSize(Size.Tiny);
    }

    // Update is called once per frame
    void Update()
    {
        //HandleKeyInput();
    }

    public void DeactivateSwitcher() { isRandomizerEnabled = false; }
    public bool IsPlayerBig() { return currentSize == Size.Big; }

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

        if (newSize == Size.Normal) lastPlayerPosition.y += 0.5f;
        else if (newSize == Size.Big) lastPlayerPosition.y += 1f;

        if (playerToBeActivated)
        {
            EnemyAI.player = playerToBeActivated;
            playerToBeActivated.gameObject.SetActive(true);
            playerToBeActivated.transform.position = lastPlayerPosition;
            cameraFollow.SetFollowObject(playerToBeActivated.gameObject);
        }

        // Handle SFXs
        switch (newSize)
        {
            case Size.Tiny:
                audioSource.PlayOneShot(changeToTinySFX, 0.5f);
                break;
            case Size.Normal:
                audioSource.PlayOneShot(changeToNormalSFX);
                break;
            case Size.Big:
                audioSource.PlayOneShot(changeToBigSFX);
                break;
            default:
                Debug.LogError("Invalid Size");
                break;
        }
    }

    // For Debugging
    private void HandleKeyInput()
    {
        if (Input.GetKeyDown(KeyCode.T)) ChangePlayerSize(Size.Tiny);

        else if (Input.GetKeyDown(KeyCode.Y)) ChangePlayerSize(Size.Normal);

        else if (Input.GetKeyDown(KeyCode.U)) ChangePlayerSize(Size.Big);
    }

    public void HandleSizeRandomizer(int timeElapsed)
    {
        if (!isRandomizerEnabled) return;

        if(timeElapsed % randomizeTime == (randomizeTime - 2)) audioSource.PlayOneShot(aboutToChangeSFX);

        if (timeElapsed % randomizeTime == 0)
        {
            Size randomSize = currentSize;

            do randomSize = (Size) Random.Range(0, (int) Size.Count);
            while (randomSize == currentSize);

            ChangePlayerSize(randomSize);
        }
    }
}
