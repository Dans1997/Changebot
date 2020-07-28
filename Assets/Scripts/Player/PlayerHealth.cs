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

    [Header("Game Over Canvas")]
    [SerializeField] Canvas gameOverCanvas = null;

    [Header("SFXs")]
    [SerializeField] AudioClip damageSFX;

    int hitsTaken = 0;
    bool isDead = false;

    void OnEnable()
    {
        HandleHealthUI();
    }

    // Start is called before the first frame update
    void Start()
    {
        gameOverCanvas.enabled = false;
        HandleHealthUI();
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
        if (isDead) return;
        gameOverCanvas.enabled = true;
        FindObjectOfType<Timer>().StopAllCoroutines();
        FindObjectOfType<MusicPlayer>().ChangeToLoseMusic();
        FindObjectOfType<PlayerSizeSwitcher>().DeactivateSwitcher();
        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<Player>().enabled = false;
        canvasHearts.sprite = hearts[hearts.Length - 1];
        isDead = true;
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
