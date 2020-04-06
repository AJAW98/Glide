using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Helper {
    public static T FindComponentInChildWithTag<T>(this GameObject parent, string tag)where T:Component{
        Transform t = parent.transform;
        foreach(Transform tr in t)
        {
            if(tr.tag == tag)
            {
                return tr.GetComponent<T>();
            }
        }
        return null;
    }

    public static Transform FindChildWithTag(this GameObject parent, string tag) {
        Transform t = parent.transform;
        Debug.Log("HELPER: t = " + t.name);
        foreach(Transform child in t)
        {
            Debug.Log("Found child " + child.name);
            if(child.tag == tag) {
                return t;
            }
                
        }
        return null;
    }

    
 }
