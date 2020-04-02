using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateSprite : MonoBehaviour
{
    public void SetRotation(Vector2 dir)
    {
        float angle = Vector2.Angle(Vector2.right, dir);
        transform.eulerAngles = new Vector3(0, 0, -angle);
    }
}
