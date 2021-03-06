﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerPhysics : MonoBehaviour
{

    [SerializeField] Text speedText, vertSpeedText, horSpeedText, flyAngleText;
    [SerializeField] CanvasGroup debugGroup;

    [SerializeField] GameUI ui;

    Transform mapBounds;
    Rigidbody2D rb;
    InfiniteScroll[] backgrounds;
    RotateSprite rotator;
    Parachute parachute;
    App app;

    [Space]
    [Header("Physics Settings")]
    [SerializeField] float diveTime = 2f;
    float currentLerpTimeDive;

    [SerializeField] float liftTime = 1f;
    float currentLerpTimeLift;

    //Bird slow effect
    [SerializeField] float slowTime = 1f;
    float slowDisableTime;
    float slowPower;

    //Skill multipliers
    float liftMultiplier;
    float responsiveMultiplier;
    float aerodynamicsMultipliers;

    bool slowed;

    Vector2 velocity;
    Vector2 velRef;
    Vector2 dirVector;
    Vector2 slowVector;
    Vector2 diveStartVector;
    Vector2 windForce;

    [SerializeField] private bool dive;


    
    [SerializeField] float diveSpeed;
    [SerializeField] float glideSpeed;
    [SerializeField] Vector2 diveAngle;
    [SerializeField] Vector2 glideAngle;




    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        velocity = new Vector2(glideSpeed, diveSpeed);
        slowVector = Vector2.one;
        dirVector = Vector2.zero;
        rotator = GetComponentInChildren<RotateSprite>();
        parachute = GetComponent<Parachute>();
        rb.velocity = new Vector2(glideSpeed, diveSpeed);
        mapBounds = GameObject.FindGameObjectWithTag("MapBounds").transform;
        app = App.instance;

        GetSkillMultipliers();
    }

    void FixedUpdate() {
        

        dive = Input.GetButton("Dive");  
           
        if (Input.GetKeyDown(KeyCode.F12) && debugGroup.alpha == 1f) {
            debugGroup.alpha = 0f;
        } 
        
        if (Input.GetKeyDown(KeyCode.F12) && debugGroup.alpha == 0f) {
            debugGroup.alpha = 1f;
        }

        SetUIAltitude();

        //Disable physics when parachute is open
        if (parachute.IsOpen())
            return;

        if (dive)
        {
            dirVector = Vector2.SmoothDamp(dirVector, diveAngle, ref velRef, diveTime * (1 - responsiveMultiplier));
        }
        else
        {
            dirVector = Vector2.SmoothDamp(dirVector, glideAngle, ref velRef, liftTime * (1 - responsiveMultiplier));
        }
        


        

        rb.velocity = (dirVector * velocity) * slowVector;


        //Apply lift multiplier
        rb.velocity = new Vector2(rb.velocity.x * (1 + aerodynamicsMultipliers), rb.velocity.y * (1 - liftMultiplier));

        //Apply wind force
        rb.velocity = rb.velocity + windForce;
        
        if (slowDisableTime < Time.time) {
            slowVector = Vector2.one;
            slowed = false;
        }

        //Set sprite rotation
        rotator.SetRotation(rb.velocity);
            
       


        //Debug texts
        speedText.text = "Player Speed: " + rb.velocity.magnitude;
        vertSpeedText.text = "Vertical Speed: " + rb.velocity.y;
        horSpeedText.text = "Horizontal Speed: " + rb.velocity.x;
        flyAngleText.text = "Fly Angle: " + Vector2.Angle(Vector2.right, dirVector);
        
    }

    void SetUIAltitude() {
        float perc = transform.position.y / mapBounds.position.y;
        ui.SetPlayerHeight(perc);

    }

    public void Slow(float xVel) {
        if (slowed) {
            slowDisableTime += slowTime;
        } else {
            slowDisableTime = Time.time + slowTime;
        }

        Debug.Log("Player slowed");
        slowPower += xVel;
        Mathf.Clamp(0, 1, slowPower);
        slowVector = new Vector2(1 - slowPower, 1f);
    }

    public void SetWind(Vector2 force) {
        windForce = force;
    }

    public Vector2 GetWind() {
        return windForce;
    }

    void GetSkillMultipliers() {
        liftMultiplier = app.FindSkillByName("Lift").currentLevel * app.FindSkillByName("Lift").skillIncreasePerLevel;
        responsiveMultiplier = app.FindSkillByName("Reponsivness").currentLevel * app.FindSkillByName("Reponsivness").skillIncreasePerLevel;
        aerodynamicsMultipliers = app.FindSkillByName("Aerodynamics").currentLevel * app.FindSkillByName("Aerodynamics").skillIncreasePerLevel;;
    }


}
