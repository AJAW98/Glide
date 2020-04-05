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

    void RemoveCash(float quantity) {
        cash -= quantity;
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

    public void AddSkill(Skill _skill, int quantity = 1) {
        foreach (Skill skill in skills) {
            if (skill == _skill) {
                skill.currentLevel += quantity;
                RemoveCash(skill.costPerLevel);
                Debug.Log("Upgraded skill " + skill.name + " = " + skill.currentLevel + ", cost: " + skill.costPerLevel);
            }
        }
    }

    public Skill FindSkillByName(string name) {
        foreach (Skill skill in skills) {
            if (skill.name == name)
                return skill;
        }

        return null;
    }
}
