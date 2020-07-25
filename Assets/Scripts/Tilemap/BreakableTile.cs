using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableTile : MonoBehaviour
{
    [SerializeField] AudioClip breakSFX;

    void OnCollisionEnter2D(Collision2D other)
    {
        Player player = other.gameObject.GetComponent<Player>();
        if (!player) return;
        if (!player.GetComponentInParent<PlayerSizeSwitcher>().IsPlayerBig()) return;
        GetComponent<Animator>().SetTrigger("break");
        AudioSource.PlayClipAtPoint(breakSFX, Camera.main.transform.position);
        Destroy(gameObject, .5f);
    }

    void DeactivateCollider()
    {
        GetComponent<BoxCollider2D>().enabled = false;
    }
}
