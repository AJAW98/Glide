using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public enum ButtonStatus {Paid, Unpaid, Locked};
public class UpgradeButton : MonoBehaviour {

    
    ButtonStatus status;
    float costPerLevel;
    Transform parent;
    Image image;
    Button button;
    Skill skill;
    Shop shop;

    [SerializeField] Sprite purchasedButton;
    [SerializeField] Sprite buyButton;
    [SerializeField] Sprite lockedButton;
    
    void Awake()
    {
        button = GetComponent<Button>();
        image = GetComponent<Image>();
        shop = (Shop)FindObjectOfType(typeof(Shop));
    }

    public void UpdateButton(ButtonStatus _status) {

        button.onClick.RemoveListener(BuySkill);

         /*if (_status == ButtonStatus.Locked || (ButtonStatus.Unpaid == _status && App.instance.GetCash() < costPerLevel)) {
            status = ButtonStatus.Locked;
            image.sprite = lockedButton;
            button.interactable = false;
        }*/
        
        if (_status == ButtonStatus.Unpaid) {
            status = ButtonStatus.Unpaid;
            button.interactable = true;
            image.sprite = buyButton;
            button.onClick.AddListener(BuySkill);
        } 
        if (_status == ButtonStatus.Unpaid && costPerLevel > App.instance.GetCash()) {
            Destroy(gameObject);
        }
        if (_status == ButtonStatus.Paid) {
            image.sprite = purchasedButton;
            button.interactable = false;
            Destroy(this);
        }

        transform.SetParent(parent, false);
    }

    public void UpdateButton(ButtonStatus _status, float _costPerLevel, Transform _parent) {
        
        costPerLevel = _costPerLevel;
        parent = _parent;
        UpdateButton(_status);
        
    }

    public void BuySkill() {
        //Debug.Log("Buying skill, cost: " + costPerLevel + ", cash: " + App.instance.GetCash());
        App.instance.AddSkill(skill);
        UpdateButton(ButtonStatus.Paid);
        shop.UpdateCash();
        if (costPerLevel <= App.instance.GetCash()) {
            UpgradeButton createdButton = shop.CreateButton(skill);
            createdButton.UpdateButton(ButtonStatus.Unpaid, costPerLevel, transform.parent);
            
        }
        UpdateButtons();
    }

    public void SetSkill(Skill _skill) {
        skill = _skill;
    }

    void UpdateButtons() {
        UpgradeButton[] buttons = FindObjectsOfType<UpgradeButton>();
        foreach (UpgradeButton b in buttons) {
            if (b != this) {    
                b.UpdateButton(b.status);
            }
        }
    }

}
