using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameUI : MonoBehaviour
{

    [SerializeField] Animator roundSummaryAnim;

    [SerializeField] RectTransform parachuteTransform;
    [SerializeField] RectTransform playerHeightTransform;
    [SerializeField] float altitudeTop = -500f;
    [SerializeField] float altitudeBot = 500f;
    [SerializeField] float parachuteTop = 1061.42f;
    [SerializeField] float parachuteBot = 34.9549f;


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
}
