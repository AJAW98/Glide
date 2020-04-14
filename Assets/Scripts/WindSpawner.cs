using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindSpawner : MonoBehaviour
{

    [SerializeField] bool active = true;
    [SerializeField] float frequency;
    [SerializeField] GameObject prefab;
    [SerializeField] float xDistFromPlayer;
    [SerializeField] float minHeight;
    [SerializeField] float windDirectionMin, windDirectionMax;

    float timer;
    
    Transform maxHeight;
    Transform player;

    // Start is called before the first frame update
    void Start()
    {
        timer = Time.time + frequency;
        maxHeight = GameObject.FindGameObjectWithTag("MapBounds").transform;
        player = GameObject.FindGameObjectWithTag("Player").transform;

    }

    // Update is called once per frame
    void Update()
    {
        if (timer <= Time.time && active) {
            SpawnWind();
            timer = Time.time + frequency;
        }
    }

    void SpawnWind() {
        float randomY = Random.Range(minHeight, maxHeight.position.y);
        Vector2 spawnPos = new Vector2(player.position.x + xDistFromPlayer, randomY);

        float randomVal = Random.Range(windDirectionMin, -windDirectionMax);
        Quaternion angle = Quaternion.Euler(0, 0, randomVal);

        Instantiate(prefab, spawnPos, angle);
    }

    public void Disable(){
        active = false;
    }
}
