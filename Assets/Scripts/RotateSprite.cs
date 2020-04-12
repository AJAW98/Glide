using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateSprite : MonoBehaviour
{

    [SerializeField] Animator anim;

    public void SetRotation(Vector2 dir)
    {
        
        float angle = Vector2.Angle(Vector2.right, dir);
        transform.eulerAngles = new Vector3(0, 0, -angle);

        

        float normalizedAngle = Mathf.InverseLerp(0, 90, angle);
        
        anim.Play("Dive", 0, normalizedAngle);
    }
}
