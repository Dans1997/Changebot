using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour
{
    [SerializeField] bool isXEnabled = false, isYEnabled = false, isZEnabled = false;
    [SerializeField] float xSpeed = 0f, ySpeed = 0f, zSpeed = 0f;

    // Start is called before the first frame update
    void Start()
    {

    }

    void FixedUpdate()
    {
        MoveX();
        MoveY();
        MoveZ();
    }

    private void MoveX()
    {
        if (isXEnabled)
        {
            transform.position = new Vector3(transform.position.x + xSpeed, transform.position.y, transform.position.z);
        }
    }

    private void MoveY()
    {
        if (isYEnabled)
        {
            transform.position = new Vector3(transform.position.y, transform.position.y + ySpeed, transform.position.z);
        }
    }

    private void MoveZ()
    {
        if (isZEnabled)
        {
            transform.position = new Vector3(transform.position.y, transform.position.y, transform.position.z + zSpeed);
        }
    }
}
