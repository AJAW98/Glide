using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : MonoBehaviour
{

    [SerializeField] float slowPercentage;

    [SerializeField] ParticleSystem[] effects;

    public void Hit() {
        foreach (ParticleSystem effect in effects) {
            Destroy(Instantiate(effect, transform.position, Quaternion.identity), 2f);
            
        }


        //Play sound
        Destroy(gameObject);
    }

    public float GetSlowPower() {
        return slowPercentage;
    }
}
