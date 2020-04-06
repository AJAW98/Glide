using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundTracker
{

    float distanceUnitMultiplier = 10f;
    float roundDistance = 1240f, roundCash = 125f, roundTime = 3513f;

    float startPos;

    public void ResetRound() {
        roundCash = 0;
        roundDistance = 0;
        roundTime = Time.time;
    }

    public float GetDistance() {
        roundDistance = GameObject.FindGameObjectWithTag("Player").transform.position.x - startPos;
        return roundDistance * distanceUnitMultiplier;
    }

    public float GetRoundCash() {
        return roundCash;
    }

    public float GetRoundTime() {
        return roundTime;
    }

    public void SetStartPos(float pos) {
        startPos = pos;
    }

}
