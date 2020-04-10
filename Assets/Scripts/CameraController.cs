using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    [SerializeField] float cameraZoomPower = .3f;
    [SerializeField] float maxZoomOut = 10f;
    Camera camera;
    float defaultSize;
    Vector2 playerSpeed;
    Transform player;
    Rigidbody2D rb;

    void Start()
    {
        camera = Camera.main;
        defaultSize = camera.orthographicSize;
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        rb = player.GetComponent<Rigidbody2D>();
        playerSpeed = rb.velocity;
        camera.orthographicSize = Mathf.Clamp(cameraZoomPower * playerSpeed.magnitude, defaultSize, maxZoomOut);
        Debug.Log("Cam size: " + camera.orthographicSize + " Speed: " + playerSpeed.magnitude +  "    Calc: " + cameraZoomPower * playerSpeed.magnitude);
    }
}
