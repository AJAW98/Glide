using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wind : MonoBehaviour
{

    [SerializeField] float windStrength;
    [SerializeField] float windTime;
    [SerializeField] float destroyOffset = 20f;
    GameObject player;
    Rigidbody2D rb;
    PlayerPhysics physics;
    bool dampingWind;
    Vector2 newWind;
    Vector2 exitWind;
    float timer;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        rb = player.GetComponent<Rigidbody2D>();
        physics = player.GetComponent<PlayerPhysics>();
    }


    void Update()
    {

        if (transform.position.x + destroyOffset < player.transform.position.x) {
            Destroy(gameObject);
        }

        if (dampingWind && timer > 0) {
            
            newWind = Vector3.Lerp(Vector3.zero, exitWind, timer);
            //Debug.Log("Timer:" + timer + " | Exit wind: " + exitWind + " | new wind: " + newWind);
            timer -= Time.deltaTime*windTime;
            physics.SetWind(newWind);
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Player") {
            
            
            Debug.Log("Forward vec:" + transform.forward);
            physics.SetWind(transform.up * windStrength);
            dampingWind = false;
        }
    }


    private void OnTriggerExit2D(Collider2D other) {
        if (other.tag == "Player") {         
            dampingWind = true;
            timer = 1f;
            exitWind = physics.GetWind();
        }
    }
}
