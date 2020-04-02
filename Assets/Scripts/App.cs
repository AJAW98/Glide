using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class App : MonoBehaviour
{

    public static App instance = null;

    [SerializeField] float cash;
    [SerializeField] int currentDay = 1;
    [SerializeField] Skill[] skills;


    void Awake()
    {
        if (instance == null) {
            instance = this;
        } else if (instance != this)
            Destroy(gameObject);
    }  
       
    void Start()
    {
        SceneManager.LoadScene(1);
    }

    public float AddCash(float quantity) {
        cash += quantity;
        return cash;
    }

    public float GetCash() {
        return cash;
    }

    public void NextDay() {
        currentDay++;
    }

    public int GetCurrentDay() {
        return currentDay;
    }

    public Skill[] GetSkills() {
        return skills;
    }
}
