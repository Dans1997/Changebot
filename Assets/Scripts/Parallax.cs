using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    [SerializeField] Camera camera;
    [SerializeField] float parallaxStrength = 0.5f;

    float length, startpos; 

    // Start is called before the first frame update
    void Start()
    {
        startpos = transform.position.x;
        length = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    // Update is called once per frame
    void Update()
    {
        float aux = (camera.transform.position.x * (1 - parallaxStrength));
        float distance = (camera.transform.position.x * parallaxStrength);

        transform.position = new Vector2(startpos + distance, transform.position.y);

        if (aux > startpos + length) startpos += length;
        else if (aux < startpos - length) startpos -= length;
    }
}
