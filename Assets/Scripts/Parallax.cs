using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    [SerializeField] Camera camera;
    [SerializeField] float parallaxStrength = 0.5f;

    Vector2 startPos;
    float width, height; 

    // Start is called before the first frame update
    void Start()
    {
        startPos = new Vector2(transform.position.x, transform.position.y);
        width = GetComponent<SpriteRenderer>().bounds.size.x;
        height = GetComponent<SpriteRenderer>().bounds.size.y;
    }

    void Update()
    {
        float repeatX = (camera.transform.position.x * (1 - parallaxStrength));
        float distanceX = (camera.transform.position.x * parallaxStrength);

        float distanceY = (camera.transform.position.y * parallaxStrength);

        transform.position = new Vector3(startPos.x + distanceX, startPos.y + distanceY);

        if (repeatX > startPos.x + width) startPos.x += width;
        else if (repeatX < startPos.x - width) startPos.x -= width;
    }
}
