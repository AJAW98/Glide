using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameUI : MonoBehaviour
{

    [Header("Round Summary")]
    [SerializeField] Animator roundSummaryAnim;
    [SerializeField] TextMeshProUGUI distanceText, timeText, cashText;
    bool distBool, timeBool, cashBool;
    float distanceLerp, timeLerp, cashLerp;
    [SerializeField] float lerpTime = 1f;


    [Header("Altitude Meter")]
    [SerializeField] TextMeshProUGUI altitudeText;
    [SerializeField] float altitudeMultiplier = 1f; 
    [SerializeField] RectTransform parachuteTransform;
    [SerializeField] RectTransform playerHeightTransform;
    [SerializeField] float altitudeTop = -500f;
    [SerializeField] float altitudeBot = 500f;
    [SerializeField] float parachuteTop = 1061.42f;
    [SerializeField] float parachuteBot = 34.9549f;

    App app;
    Transform player;

    void Awake()
    {
        app = App.instance;
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {

        float altitude = Mathf.Clamp(0f, (player.position.y * altitudeMultiplier), float.MaxValue);
        altitudeText.text = altitude.ToString("#ft");

        if (distBool) {

            distanceLerp += Time.deltaTime / lerpTime;

            float distance = 0;
            float maxDistance = app.round.GetDistance();
            distance = Mathf.Lerp(0, maxDistance, distanceLerp);
            distanceText.text = distance.ToString("#.00 meters");
        }

        if (timeBool) {
            timeLerp += Time.deltaTime / lerpTime;

            float time = 0;
            float maxTime = Time.time - app.round.GetRoundTime();
            time = Mathf.Lerp(0, maxTime, timeLerp);

            string minutes = Mathf.Floor(time / 60).ToString("00");
            string seconds = (time % 60).ToString("00");

            timeText.text = "" + minutes + ":" + seconds;
        }

        if (cashBool) {
            cashLerp += Time.deltaTime / lerpTime;

            float cash = 0;
            float maxCash = app.round.GetRoundCash();
            cash = Mathf.Lerp(0, maxCash, cashLerp);
            cashText.text = "$" + cash;
        }
    }


    public void ShowRoundSummary() {
        roundSummaryAnim.SetTrigger("New Trigger");
    }

    public void OnSummaryFadeIn() {
        Debug.Log("Updating stats");
    }

    public void OnNextLevel() {
        LevelLoader.instance.RestartScene();
    }

    public void OnShop() {
        LevelLoader.instance.LoadScene(2);
    }

    public void SetPlayerHeight(float perc) {
        float playerHeight = Mathf.Lerp(altitudeBot, altitudeTop, perc); 
        playerHeightTransform.anchoredPosition = new Vector2(playerHeightTransform.anchoredPosition.x, -playerHeight);
        
    }

    public void SetParachuteHeight(float perc) {
        float parachuteOpenHeight = Mathf.Lerp(parachuteTop, parachuteBot, perc);
        parachuteTransform.offsetMax = new Vector2(parachuteTransform.offsetMax.x, -parachuteOpenHeight);
    }

    public void LerpDistText() {
        StartCoroutine(SetDistBoolForTime(lerpTime));
    }

    public void LerpTimeText() {
        StartCoroutine(SetTimeBoolForTime(lerpTime));
    }

    public void LerpCashText() {
        StartCoroutine(SetCashBoolForTime(lerpTime));
    }

    IEnumerator SetDistBoolForTime(float time) {
        distanceLerp = 0f;
        distBool = true;
        yield return new WaitForSeconds(time);
        distBool = false;
    }

    IEnumerator SetCashBoolForTime(float time) {
        cashLerp = 0f;
        cashBool = true;
        yield return new WaitForSeconds(time);
        cashBool = false;
    } 

    IEnumerator SetTimeBoolForTime(float time) {
        timeLerp = 0f;
        timeBool = true;
        yield return new WaitForSeconds(time);
        timeBool = false;
    } 
}
