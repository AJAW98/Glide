using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Shop : MonoBehaviour
{
    
    [SerializeField] TextMeshProUGUI dayText;
    [SerializeField] TextMeshProUGUI cashText;

    [SerializeField] Transform skillContainer;
    [SerializeField] GameObject skillPrefab; 
    [SerializeField] GameObject buttonPrefab; 
    
    [SerializeField] float animDelay = 0.3f;

    App app;

    Skill[] skills;
    List<GameObject> buttons = new List<GameObject>();

    public void OnPlayClicked() {
        LevelLoader.instance.LoadScene(3);
        
    }

    void Start()
    {
        
        app = App.instance;

        dayText.text = "Day " + app.GetCurrentDay();
        UpdateCash();

        skills = app.GetSkills();


        foreach(Skill skill in skills) {
            Transform spawnedSkill = Instantiate(skillPrefab, Vector3.zero, Quaternion.identity).transform;
            spawnedSkill.SetParent(skillContainer, false);

            TextMeshProUGUI itemTitle = spawnedSkill.gameObject.FindComponentInChildWithTag<TextMeshProUGUI>("ShopItemTitle");
            itemTitle.text = skill.name;

            Transform buttonContainer = spawnedSkill.GetChild(0).GetChild(0);

            int levels = skill.levels;
            int currentLevel = skill.currentLevel;

            for (int i = 0; i < levels; i++) {


                UpgradeButton buttonScript = CreateButton(skill);

                if (i < currentLevel) {
                    buttonScript.UpdateButton(ButtonStatus.Paid, skill.costPerLevel, buttonContainer);
                }

                if (i == currentLevel) {
                    buttonScript.UpdateButton(ButtonStatus.Unpaid, skill.costPerLevel, buttonContainer);
                }

                //if (i > currentLevel) {
                  //  buttonScript.UpdateButton(ButtonStatus.Locked, skill.costPerLevel, buttonContainer);
                //}
                
            }
        }
    }


    public UpgradeButton CreateButton(Skill skill) {
        GameObject buttonObj = Instantiate(buttonPrefab);
        UpgradeButton buttonScript = buttonObj.GetComponent<UpgradeButton>();
        buttonScript.SetSkill(skill);
        return buttonScript;
    }

    public void UpdateCash() {
        cashText.text = "$" + app.GetCash();
    }

    IEnumerator AnimateSkill() {
        yield return new WaitForSeconds(animDelay);
        //animate
    }

}
