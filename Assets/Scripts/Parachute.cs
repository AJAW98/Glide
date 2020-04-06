using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parachute : MonoBehaviour
{

    [SerializeField] GameUI ui;
    [SerializeField] float parachuteFallSpeed;
    [SerializeField] AnimationCurve parachuteYForceCurve, parachuteXForceCurve;
    float yVelocity, xVelocity;
    
    Transform parachuteOpenTarget;
    Transform parachuteLandTarget;
    Transform mapBounds;
    Rigidbody2D rb;
    bool parachuteOpen;
    bool landed;

    Vector2 parachuteOpenVel = Vector2.zero;

    // Start is called before the first frame update
    void Start()
    {
        parachuteOpenTarget = GameObject.FindGameObjectWithTag("ParachuteOpenPoint").transform;
        parachuteLandTarget = GameObject.FindGameObjectWithTag("ParachuteLandPoint").transform;
        mapBounds = GameObject.FindGameObjectWithTag("MapBounds").transform;
        rb = GetComponent<Rigidbody2D>();
        

        float skillMultiplier = App.instance.FindSkillByName("Parachute").currentLevel * App.instance.FindSkillByName("Parachute").skillIncreasePerLevel;
        parachuteOpenTarget.position = new Vector2(parachuteOpenTarget.position.x, parachuteOpenTarget.position.y * (1 - skillMultiplier));

        ui.SetParachuteHeight(ConvertHeightToPercentage());

    }

    // Update is called once per frame
    void FixedUpdate()
    {

        if (transform.position.y <= parachuteOpenTarget.position.y && !landed) {
            OpenParachute();
        }

        if (transform.position.y > parachuteOpenTarget.position.y)
            parachuteOpen = false;

        if (transform.position.y <= parachuteLandTarget.position.y) {
            rb.velocity = new Vector2(0, 0);
            Land();
        }
    }

    void OpenParachute() {
        if (!parachuteOpen) {
            parachuteOpen = true;
            parachuteOpenVel = rb.velocity;
        }
        Debug.Log("Parachute open");

        Vector2 force = rb.velocity;

        
        
        float height = parachuteOpenTarget.position.y - transform.position.y;
        float percY = Mathf.InverseLerp(0, parachuteOpenTarget.position.y, height);
        //float percX = 

        force.y = Mathf.Lerp(parachuteOpenVel.y, -parachuteFallSpeed, parachuteYForceCurve.Evaluate(percY));
        force.x = Mathf.Lerp(parachuteOpenVel.x, 0, parachuteXForceCurve.Evaluate(percY));
        Debug.Log("Parachute force: " + force + " perc: " + percY);
        rb.velocity = force;
        
    }

    void Land() {
        landed = true;
        ui.ShowRoundSummary();
    }

    public bool IsOpen() {
        return parachuteOpen;
    }

    float ConvertHeightToPercentage() {
        return parachuteOpenTarget.position.y / mapBounds.position.y;
    }
}
