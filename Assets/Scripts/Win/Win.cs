using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Win : MonoBehaviour
{
    [SerializeField] Canvas winCanvas;

    bool hasWon = false;

    // Start is called before the first frame update
    void Start()
    {
        winCanvas.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (hasWon) return;
        Player player = other.gameObject.GetComponent<Player>();
        if (player)
        {
            hasWon = true;
            winCanvas.enabled = true;
            winCanvas.GetComponentInChildren<Text>().text = 
            winCanvas.GetComponentInChildren<Text>().text = "Time: " + FindObjectOfType<Timer>().GetTimeString();
            player.enabled = false;
            player.GetComponent<Rigidbody2D>().velocity = new Vector2(0,0);
            FindObjectOfType<Timer>().StopAllCoroutines();
            FindObjectOfType<MusicPlayer>().ChangeToWinMusic();
            FindObjectOfType<PlayerSizeSwitcher>().DeactivateSwitcher();
            Destroy(FindObjectOfType<CheckPointMaster>().gameObject);
        }
    }

}
