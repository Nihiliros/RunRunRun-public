using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    int maxHealth=1;
    int health;

    void Start()
    {
        health = maxHealth;
    }

    public void GetDamage()
    {
        health--;
        CheckHealth();
    }

    void CheckHealth()
    {
        if (health <= 0)
        {
            this.GetComponentInParent<EnemyPool>().GetHealthAugment();
            this.gameObject.SetActive(false);
        }
    }

    public void HealthAugment()
    {
        maxHealth++;
        health++;
    }
}
