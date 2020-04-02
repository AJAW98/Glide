using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : MonoBehaviour
{

    [SerializeField] float slowPercentage;

    [SerializeField] ParticleSystem[] effects;

    public void Hit() {
        foreach (ParticleSystem effect in effects) {
            Instantiate(effect, transform.position, Quaternion.identity);
            
        }


        //Play sound
        Destroy(gameObject);
    }

    public float GetSlowPower() {
        return slowPercentage;
    }
}
