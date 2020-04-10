using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    
    PlayerPhysics physics;

    void Awake()
    {
        physics = GetComponent<PlayerPhysics>();
        App.instance.round.ResetRound();
        App.instance.round.SetStartPos(transform.position.x);
    }


    private void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Coin") {
            Coin coin = other.GetComponent<Coin>();
            if (coin != null) {
                App.instance.AddCash(coin.money);
                Destroy(Instantiate(coin.particlePrefab, coin.transform.position, coin.transform.rotation), 2f);
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
