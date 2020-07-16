using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] int maxHits = 3;
    [SerializeField] Sprite[] hearts = null;

    [Header("Canvas Instance")]
    [SerializeField] Image canvasHearts = null;

    int hitsTaken = 0;

    // Start is called before the first frame update
    void Start()
    {
        HandleHealthUI();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Debug.Log("Player Collided with " + collision.gameObject.name);

        if(collision.gameObject.GetComponent<Enemy>())
        {
            TakeHit();
        }
    }

    public void TakeHit()
    {
        hitsTaken++;
        HandleHealthUI();
        HandleDeath();
    }

    private void HandleHealthUI()
    {
        if(hitsTaken < hearts.Length)
            canvasHearts.sprite = hearts[hitsTaken];
    }

    private void HandleDeath()
    {
        if (hitsTaken < maxHits) return;
        // Death
        Debug.Log("Player is dead");
        FindObjectOfType<SceneLoader>().LoadGameOverScreen();
        canvasHearts.sprite = hearts[hearts.Length - 1];
    }
}
