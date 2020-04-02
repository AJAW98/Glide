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
    [SerializeField] GameObject purchasedButton;
    [SerializeField] GameObject buyButton;
    [SerializeField] GameObject lockedButton;
    [SerializeField] float animDelay = 0.3f;

    App app;

    Skill[] skills;

    public void OnPlayClicked() {
        LevelLoader.instance.LoadScene(3);
        
    }

    void Start()
    {
        
        app = App.instance;

        dayText.text = "Day " + app.GetCurrentDay();
        cashText.text = "$" + app.GetCash();

        skills = app.GetSkills();
        foreach(Skill skill in skills) {
            Transform spawnedSkill = Instantiate(skillPrefab, Vector3.zero, Quaternion.identity).transform;
            spawnedSkill.SetParent(skillContainer, false);

            TextMeshProUGUI itemTitle = spawnedSkill.gameObject.FindComponentInChildWithTag<TextMeshProUGUI>("ShopItemTitle");
            itemTitle.text = skill.name;

            Transform buttonContainer = spawnedSkill.GetChild(0).GetChild(0);
            Debug.Log(buttonContainer.name);

            int levels = skill.levels;
            int currentLevel = skill.currentLevel;

            for (int i = 0; i < levels; i++) {
                if (i < currentLevel) {
                    Transform button = Instantiate(purchasedButton).transform;
                    button.SetParent(buttonContainer, false);
                }
                if (i == currentLevel && app.GetCash() >= skill.costPerLevel) {
                    Transform button = Instantiate(buyButton).transform;
                    button.SetParent(buttonContainer, false);
                    button.onClick.AddListener(() => {LogName(myButtonGameObject); });
                }
                if (i > currentLevel || (i == currentLevel && app.GetCash() < skill.costPerLevel)) {
                    Transform button = Instantiate(lockedButton).transform;
                    button.SetParent(buttonContainer, false);
                }
                
            }
        }
    }
    

    public void BuySkill() {

    }

    public void LogName(GameObject buttonGameObject = null){
        Debug.Log(myButtonGameObject);
    }

    IEnumerator AnimateSkill() {
        yield return new WaitForSeconds(animDelay);
        //animate
    }

}
