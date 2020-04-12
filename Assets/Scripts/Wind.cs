using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wind : MonoBehaviour
{

    [SerializeField] float windStrength;

    GameObject player;
    Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        rb = player.GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Player") {
            
            PlayerPhysics physics = player.GetComponent<PlayerPhysics>();
            physics.AddWind(transform.forward * windStrength);
            

        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if (other.tag == "Player") {
            PlayerPhysics physics = player.GetComponent<PlayerPhysics>();
            physics.AddWind(Vector2.zero);
        }
    }
}
