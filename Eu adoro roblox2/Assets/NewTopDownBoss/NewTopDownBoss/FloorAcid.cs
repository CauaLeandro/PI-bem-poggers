using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorAcid : MonoBehaviour
{
    public int damage = 1; 
    public float damageInterval = 1f; 
    private float nextDamageTime = 0f;

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (Time.time >= nextDamageTime)
            {
                nextDamageTime = Time.time + damageInterval;
                other.GetComponent<PlayerHealthTopDownBoss>().TakeDamage(damage);
            }


        }

    }



}
