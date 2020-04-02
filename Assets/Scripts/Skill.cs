using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Category {Wingsuit, Tech, Other};

[System.Serializable]
public class Skill
{
    
    

    public string name;
    public Category category;
    public int levels;
    public int currentLevel;
    public float costPerLevel;  


}
