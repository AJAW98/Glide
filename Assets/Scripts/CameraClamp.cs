using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraClamp : MonoBehaviour
{
    [SerializeField] Transform target;
    Transform maxHeight;
    Transform t;
    Rigidbody2D rb;
    float cameraSize;

    void Awake()
    {
        maxHeight = GameObject.FindGameObjectWithTag("MapBounds").transform;
        t = transform;
        cameraSize = Camera.main.orthographicSize;
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        if (maxHeight == null)
            return;
        float y = Mathf.Clamp(target.position.y, cameraSize, maxHeight.position.y - cameraSize);
        t.position = new Vector3(target.position.x, y, t.position.z);
        rb.position = t.position;
    }
}

