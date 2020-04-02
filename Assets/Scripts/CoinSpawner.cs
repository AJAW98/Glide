using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinSpawner : MonoBehaviour
{

    [SerializeField] bool active = true;
    [SerializeField] GameObject whitePrefab, bluePrefab, purplePrefab, goldPrefab;
    [SerializeField][Range(0f, 100f)]float chanceForWhite, chanceForBlue, chanceForPurple, chanceForGold;

    [SerializeField] float frequency;
    [SerializeField] float xDistFromPlayer;
    float timer;

    Transform player;
    Transform maxHeight;

    void Awake()
    {
        timer = Time.time + frequency;
        player = GameObject.FindGameObjectWithTag("Player").transform;
        maxHeight = GameObject.FindGameObjectWithTag("MapBounds").transform;
    }

    void Update()
    {
        
        if (timer <= Time.time && active) {
            SpawnCoin();
            timer = Time.time + frequency;
        }
    }

    void SpawnCoin() {

        float randomValue = Random.Range(0f, 100f);
        float randomY = Random.Range(0, maxHeight.position.y);
        Vector2 spawnPos = new Vector2(player.position.x + xDistFromPlayer, randomY);

        if (randomValue <= chanceForGold) {
            Instantiate(goldPrefab, spawnPos, Quaternion.identity);
            return;
        }

        if (randomValue <= chanceForPurple) {
            Instantiate(purplePrefab, spawnPos, Quaternion.identity);
            return;
        }

        if (randomValue <= chanceForBlue) {
            Instantiate(bluePrefab, spawnPos, Quaternion.identity);
            return;
        }

        if (randomValue <= chanceForWhite) {
            Instantiate(whitePrefab, spawnPos, Quaternion.identity);
            return;
        }
    }

    public void Disable(){
        active = false;
    }
}
