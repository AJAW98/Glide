using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlockSpawner : MonoBehaviour
{

    [SerializeField] bool active = true;
    [SerializeField] float frequency;
    [SerializeField] GameObject[] prefabs;
    [SerializeField] float xDistFromPlayer;
    [SerializeField] float minHeight;

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
            SpawnFlock();
            timer = Time.time + frequency;
        }
    }

    void SpawnFlock() {
        int prefabIndex = Random.Range(0,(prefabs.Length - 1));
        float randomY = Random.Range(minHeight, maxHeight.position.y);
        Vector2 spawnPos = new Vector2(player.position.x + xDistFromPlayer, randomY);

        Instantiate(prefabs[prefabIndex], spawnPos, Quaternion.identity);
    }

    public void Disable(){
        active = false;
    }
}
