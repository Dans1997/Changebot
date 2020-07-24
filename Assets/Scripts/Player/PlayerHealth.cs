using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] int maxHits = 3;
    [SerializeField] Sprite[] hearts = null;

    [Header("Health Canvas Instance")]
    [SerializeField] Image canvasHearts = null;

    [Header("Game Over Canvas Instance")]
    [SerializeField] Canvas gameOverCanvas;

    [Header("SFXs")]
    [SerializeField] AudioClip damageSFX;

    int hitsTaken = 0;

    void OnEnable()
    {
        HandleHealthUI();
    }

    // Start is called before the first frame update
    void Start()
    {
        HandleHealthUI();
        gameOverCanvas.enabled = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Debug.Log("Player Collided with " + collision.gameObject.name);

        if(collision.gameObject.GetComponent<EnemyAI>())
        {
            TakeHit();
        }
    }

    public void TakeHit()
    {
        hitsTaken++;
        HandleHealthUI();
        HandleDeath();
        GetComponent<Player>().PlaySFX(damageSFX);
    }

    public void KillPlayer()
    {
        gameOverCanvas.enabled = true;
        FindObjectOfType<PlayerSizeSwitcher>().enabled = false;
        GetComponent<Player>().enabled = false;
        FindObjectOfType<MusicPlayer>().ChangeToLoseMusic();
        canvasHearts.sprite = hearts[hearts.Length - 1];
    }

    private void HandleHealthUI()
    {
        if(hitsTaken < hearts.Length)
            canvasHearts.sprite = hearts[hitsTaken];
    }

    private void HandleDeath()
    {
        if (hitsTaken < maxHits) return;
        KillPlayer();
    }

}
