using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{

    Transform player;
    public float money;

    [SerializeField] float destroyOffset = 10f;
    [SerializeField] public GameObject particlePrefab;

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        if (transform.position.x + destroyOffset < player.position.x) {
            Destroy(gameObject);
        }
    }

}
