using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public LayerMask collisionMask; //only collides with projectile mask- for void checkcollisions
    float speed = 10;
    float damage = 1;
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
        if(Physics.Raycast(ray, out hit, moveDistance, collisionMask, QueryTriggerInteraction.Collide))
        {
            OnHitObject(hit);//checks if the raycast hits
        }
    }

    void OnHitObject(RaycastHit hit)
    {
        IDamagable damagableObject = hit.collider.GetComponent<IDamagable>();
        if(damagableObject != null)
        {
            damagableObject.TakeHit(damage, hit);
        }

        //print(hit.collider.gameObject.name);
        GameObject.Destroy(gameObject);

    }
}
