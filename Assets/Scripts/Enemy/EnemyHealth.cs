using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] int maxHits = 1;

    [Header("SFXs")]
    [SerializeField] AudioClip deathSFX;

    int hitsTaken = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerHealth>())
        {
            hitsTaken++;
            HandleDeath();
        }
    }

    private void HandleDeath()
    {
        if (hitsTaken < maxHits) return;
        GetComponent<Animator>().SetTrigger("deathTrigger");
        Destroy(gameObject, 0.5f);
    }
}

