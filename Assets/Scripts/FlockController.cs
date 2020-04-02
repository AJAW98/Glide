using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlockController : MonoBehaviour
{

    [SerializeField] Vector2 velocity; 

    Rigidbody2D rb;
    Transform player;

    float destroyOffset = 8f;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = velocity;
    }

    void Update()
    {
        if (transform.position.x + destroyOffset < player.position.x) {
            Destroy(gameObject);
        }
    }
}
