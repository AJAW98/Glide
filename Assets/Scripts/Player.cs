using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    
    PlayerPhysics physics;

    void Awake()
    {
        physics = GetComponent<PlayerPhysics>();
    }


    private void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Coin") {
            Coin coin = other.GetComponent<Coin>();
            if (coin != null) {
                Debug.Log("Collected coin + $" + coin.money);
                Destroy(coin.gameObject);
            }
        }

        if (other.tag == "Bird") {
            Bird bird = other.GetComponent<Bird>();
            if (bird != null) {
                bird.Hit();
                
            }

            physics.Slow(bird.GetSlowPower());
        }
    }
}
