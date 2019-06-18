using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LivingObjects : MonoBehaviour, IDamageable
{
    public float startingHealth;
    public float health;
    public bool dead;

    public GameObject lootDrop;

    public Image healthIndicator;

    public event System.Action OnDeath;

    // Start is called before the first frame update
   public virtual void Start()
    {
        //virtual void to avoid livingobject to override enemy and player script
        health = startingHealth;
    }

    public void TakeHit (float damage, RaycastHit hit)
    {
        //edit later
        TakeDamage(damage);
    }

    public void TakeDamage(float damage)
    {
        Score.scoreValue += 10;
        health -= damage;
        /// <Aieman>
        healthIndicator.GetComponent<Image>().color = new Vector4(1.0f, 1.0f, 1.0f, 1 - (health / startingHealth));
        /// </Aieman>

        if (health <= 0 && !dead)//whenb enemy take damage.die
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
            Instantiate(lootDrop, transform.position, Quaternion.identity);

        }
        GameObject.Destroy(gameObject);
    }
}
