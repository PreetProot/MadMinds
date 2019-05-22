using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LivingObjects : MonoBehaviour, IDamagable
{
    public float startingHealth;
    public float health;
    public bool dead;

    public event System.Action OnDeath;

    // Start is called before the first frame update
   public virtual void Start()
    {
        //virtual void to avoid livingobject to override enemy and player script
        health = startingHealth;
    }

    public void TakeHit (float damage, RaycastHit hit)
    {
        health -= damage;
        if(health <=0 && !dead)
        {
            Die();
        }
    }

    public void Die()
    {
        dead = true;
        if (OnDeath != null)
        {
            OnDeath(); //when die, what event comes next
        }
        GameObject.Destroy(gameObject);
    }
}
