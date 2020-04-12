using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    [Header("Camera zoom settings")]
    [SerializeField] float cameraZoomPower = .3f;
    [SerializeField] float cameraZoomSpeed = 0.2f;
    [SerializeField] float maxZoomOut = 10f;

    [Header("Camera shake settings")]
    [SerializeField] float shakePower;
    [SerializeField] bool cameraShake;
    [SerializeField] float minSpeed;
    [SerializeField] float maxSpeed;
    Camera camera;
    float defaultSize;
    float refSize = 0.0f;
    
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
        float newSize = Mathf.Clamp(cameraZoomPower * playerSpeed.magnitude, defaultSize, maxZoomOut);
        camera.orthographicSize = Mathf.SmoothDamp(camera.orthographicSize, newSize, ref refSize, cameraZoomSpeed);

        float shakePower = Mathf.InverseLerp(minSpeed, maxSpeed, -playerSpeed.y);
        if (cameraShake)
           Shake(shakePower);

    }

    void Shake(float magnitude) {
        Vector3 originalPos = transform.localPosition;

        float x = originalPos.x + (Random.Range(-1f, 1f) * magnitude * shakePower);
        float y = originalPos.y + (Random.Range(-1f, 1f) * magnitude * shakePower);

        transform.localPosition = new Vector3(x, y, originalPos.z);

    }
}
