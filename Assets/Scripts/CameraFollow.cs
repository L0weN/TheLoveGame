using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public float cameraSpeed = 0.3f;
    public float resSpeed = 0.5f;

    public Bounds cameraBounds;

    private Transform player;

    private float offsetZ;

    private Vector3 lastPlayerPosition;
    private Vector3 currentVelocity;

    private bool followPlayer;

    void Awake()
    {
        BoxCollider2D myCollider = GetComponent<BoxCollider2D>();
        myCollider.size = new Vector2(Camera.main.aspect * 2f * Camera.main.orthographicSize, 15f);
        cameraBounds = myCollider.bounds;
    }


    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        lastPlayerPosition = player.position;
        offsetZ = (transform.position - player.position).z;
        followPlayer = true;
    }

    void FixedUpdate()
    {
        if (followPlayer)
        {
            Vector3 rightMovePosition = player.position + Vector3.forward * offsetZ;
            Vector3 leftMovePosition = player.position - Vector3.back * offsetZ;

            if (rightMovePosition.x >= transform.position.x)
            {
                Vector3 newCameraPosition = Vector3.SmoothDamp(transform.position, rightMovePosition, ref currentVelocity, cameraSpeed);

                transform.position = new Vector3(newCameraPosition.x, transform.position.y, newCameraPosition.z);

                lastPlayerPosition = player.position;
            }

            if (leftMovePosition.x <= transform.position.x)
            {
                Vector3 newCameraPosition = Vector3.SmoothDamp(transform.position, leftMovePosition, ref currentVelocity, cameraSpeed);

                transform.position = new Vector3(newCameraPosition.x, transform.position.y, newCameraPosition.z);

                lastPlayerPosition = player.position;
            }
        }
        Vector3 temp = transform.position;
        temp.y = player.position.y;
        transform.position = temp;
    }
}