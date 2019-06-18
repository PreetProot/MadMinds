using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public LayerMask collisionMask; //only collides with projectile mask- for void checkcollisions
    float speed = 10;
    float damage = 1;
    float lifetime = 0.5f; //bullet lifetime 3s
    float skinWidth = 0.1f; //between raycast and projectile

    void Start()
    {
        Destroy(gameObject, lifetime);

        //check see if raycast is inside the colliders
        Collider[] initialCollision = Physics.OverlapSphere(transform.position, 1f, collisionMask);
        if(initialCollision.Length >0)
        {
            OnHitObject(initialCollision[0]); //the first collission that it hits
        }
    }

    public void SetSpeed(float newSpeed)
    {
        speed = newSpeed; //different weapons =different speeds
    }

    // Start is called before the first frame update
    //void Start()
    //{}

    // Update is called once per frame
    void Update()
    {
        float moveDistance = speed * Time.deltaTime;
        //projectile raycast
        CheckCollisions(moveDistance);

        //move proj forward
        transform.Translate(Vector3.forward * Time.deltaTime * speed);
    }

    void CheckCollisions(float moveDistance)
    {
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;

        //                                                           -collides with trigger colliders
        if(Physics.Raycast(ray, out hit, moveDistance+skinWidth, collisionMask, QueryTriggerInteraction.Collide))
        {
            OnHitObject(hit);//checks if the raycast hits
        }
    }

    void OnHitObject(RaycastHit hit)
    {
        IDamageable damagableObject = hit.collider.GetComponent<IDamageable>();
        if(damagableObject != null)
        {
            damagableObject.TakeHit(damage, hit);
        }
        //print(hit.collider.gameObject.name);
        GameObject.Destroy(gameObject);

    }

    void OnHitObject(Collider c)
    {
        IDamageable damageableObject = c.GetComponent<IDamageable>();
        if (damageableObject != null)
        {
            damageableObject.TakeDamage(damage);
        }
        GameObject.Destroy(gameObject);
    }

    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("hit wall");
        Destroy(GameObject.FindWithTag("Projectile"));
    }
}
