using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileShooter : MonoBehaviour
{
    [SerializeField] Projectile projectilePrefab;

    [Tooltip("Cooldown in seconds")]
    [SerializeField] float cooldown = 1f;

    bool isShootEnabled = true;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            StartCoroutine(InstantiateProjectile());
        }
    }

    //TODO: make shooter accept whatever
    private IEnumerator InstantiateProjectile()
    {
        isShootEnabled = false;
        Projectile projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
        projectile.transform.SetParent(null);
        yield return new WaitForSeconds(cooldown);
        isShootEnabled = true;
    }
}
