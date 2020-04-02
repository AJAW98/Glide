using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{

    LevelLoader levelLoader;

    void Awake()
    {
        levelLoader = LevelLoader.instance;
    }

    public void OnPlay() {
        levelLoader.LoadNextScene();
    }

    public void OnSettings() {

    }

    public void OnAchievements() {

    }
}
