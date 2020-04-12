using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlagPole : MonoBehaviour
{

    float recordDistance;

    Transform player;
    App app;

    // Start is called before the first frame update
    void Start()
    {
        app = App.instance;
        player = GameObject.FindGameObjectWithTag("Player").transform;
        recordDistance = app.GetPersonalBest();
        transform.position = new Vector3(player.position.x + recordDistance, transform.position.y, transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
