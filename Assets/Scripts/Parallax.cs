using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    [Range(0,1)]
    [SerializeField] float parallaxStrength = 0.5f;
    [Tooltip("If true, image will not repeat itself")]
    [SerializeField] bool isRepeatable = true;

    Camera camera;
    float length, startpos; 

    // Start is called before the first frame update
    void Start()
    {
        camera = FindObjectOfType<Camera>();
        startpos = transform.position.x;
        length = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    // Update is called once per frame
    void Update()
    {
        float aux = (camera.transform.position.x * (1 - parallaxStrength));
        float distance = (camera.transform.position.x * parallaxStrength);

        transform.position = new Vector2(startpos + distance, transform.position.y);

        if (!isRepeatable) return;

        if (aux > startpos + length) startpos += length;
        else if (aux < startpos - length) startpos -= length;
    }
}
