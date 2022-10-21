using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject objectToFollow;
    public float followSpeed;
    public float minSize;
    public float maxSize;
    public float zoomSpeed;
    public GameObject player;
    public GameObject boss;

    private Camera cam;
    private bool zoomActive = false;

    public void SetZoomActive(bool active)
    {
        zoomActive = active;
    }

    // Start is called before the first frame update
    void Start()
    {
        cam = GetComponent<Camera>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector2 lerpVec = Vector2.Lerp(transform.position, objectToFollow.transform.position, followSpeed);
        transform.position = new Vector3(lerpVec.x, lerpVec.y, -10);

        if (zoomActive)
        {
            float distance = Vector2.Distance(player.transform.position, boss.transform.position);
            distance -= 20;
            if (distance < 0)
            {
                distance = 0;
            }
            else if (distance > 150)
            {
                distance = 150;
            }

            float optimalZoom = minSize + (maxSize - minSize) * (distance / 150) + 10 * (distance/150);
            cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, optimalZoom, zoomSpeed);
        }
    }
}
