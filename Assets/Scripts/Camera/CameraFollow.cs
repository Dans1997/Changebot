using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] Vector2 followOffset;
    [SerializeField] float defaultFollowSpeed = 5f;

    Vector2 threshold;
    Rigidbody2D followObjectBody = null;
    [SerializeField] GameObject followObject = null;
    [SerializeField] float cameraOrthoSize = 4.5f;

    // Start is called before the first frame update
    void Start()
    {
        threshold = CalculateThreshold();
        followObject = FindObjectOfType<Player>().gameObject;
        followObjectBody = followObject.GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        Vector2 followPos = followObject.transform.position;
        float xDiff = Vector2.Distance(Vector2.right * transform.position.x, Vector2.right * followPos.x);
        float yDiff = Vector2.Distance(Vector2.up * transform.position.y, Vector2.up * followPos.y);

        Vector3 newCameraPosition = transform.position;

        if (Mathf.Abs(xDiff) >= threshold.x) newCameraPosition.x = followPos.x;
        if (Mathf.Abs(yDiff) >= threshold.y) newCameraPosition.y = followPos.y;

        // Outputs the highest speed
        float moveSpeed = followObjectBody.velocity.magnitude > defaultFollowSpeed ? followObjectBody.velocity.magnitude : defaultFollowSpeed;

        transform.position = Vector3.MoveTowards(transform.position, newCameraPosition, defaultFollowSpeed * Time.deltaTime);
        Debug.Log("Move Towards: " + newCameraPosition);
    }

    public void SetFollowObject(GameObject newfollowObject) { followObject = newfollowObject; }

    private Vector3 CalculateThreshold()
    {
        Rect cameraAspectRatio = Camera.main.pixelRect;
        Vector2 dimensions = new Vector2(cameraOrthoSize * cameraAspectRatio.width / cameraAspectRatio.height, cameraOrthoSize);
        dimensions.x -= followOffset.x;
        dimensions.y -= followOffset.y;
        return dimensions;
    }

    // See Threshold in Editor Mode
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        Vector2 border = CalculateThreshold();
        Gizmos.DrawWireCube(transform.position, new Vector3(border.x * 2, border.y * 2, 1));
    }
}
