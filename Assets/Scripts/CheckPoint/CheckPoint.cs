using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerHealth playerHealth = collision.GetComponent<PlayerHealth>();
        if(playerHealth)
        {
            GetComponentInParent<CheckPointMaster>().SetCheckPoint(transform.position);
        }
    }
}
