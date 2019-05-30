using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/*
[RequireComponent(typeof(NavMeshAgent))]

public class Enemy : LivingObjects
{
    public enum State { Idle, Chasing, Attacking};
    State currentState; //while attacking, deactivate IEnum UpdatePath. store enemy States to prevent clash 

    NavMeshAgent pathfinder;
    Transform target;

    Material skinMaterial; //vis feedback
    Color originalColor;

    float attackDistanceThreshold = 1.5f; //when enemy comes to a certain distance near player, it will attack

    float timeBetweenAttacks = 1;
    float nextAttackTime;

    float enemyCollisionRadius;
    float targetCollisionRadius;

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start(); //pathfinder and target gets called
        pathfinder = GetComponent<NavMeshAgent>();

        skinMaterial = GetComponent<Renderer>().material;
        originalColor = skinMaterial.color;

        currentState = State.Chasing; //by default, enemy is chasing state
        target = GameObject.FindGameObjectWithTag("Player").transform;//assign playertag in edtior
        enemyCollisionRadius = GetComponent<CapsuleCollider>().radius;
        targetCollisionRadius = target.GetComponent<CapsuleCollider>().radius; //set collision radius around player so enemy wont glitch into the player

        StartCoroutine(UpdatePath());
    }

    // Update is called once per frame
    void Update()
    {
        //follow player -calcs everyframe(could cause lag)
        //pathfinder.SetDestination(target.position); 

        //Vector3.Distance()//not applicatble
        if(Time.time > nextAttackTime)
        {
            float sqrDstToTarget = (target.position - transform.position).sqrMagnitude;
            if (sqrDstToTarget < Mathf.Pow(attackDistanceThreshold + enemyCollisionRadius + targetCollisionRadius, 2))
            {
                nextAttackTime = Time.time + timeBetweenAttacks;
                StartCoroutine(Attack());//repeat attacks for every spawn
            }

        }


    }

    IEnumerator Attack() //store start pos, sort the target pos- start pos > targt pos> start pos -leaping at player
    {
        currentState = State.Attacking;
        pathfinder.enabled = false;//while attacking, deactivate IEnum UpdatePath

        Vector3 originalPosition = transform.position;
        Vector3 dirToTarget = (target.position - transform.position).normalized;
        Vector3 attackPosition = target.position - dirToTarget * (enemyCollisionRadius);

        float attackSpeed = 3; //higher = faster attack leap
        float percent = 0;//how far the leap

        while (percent <=1)
        {
            percent += Time.deltaTime * attackSpeed;
            //float interpolation = 4*(-percent * percent + percent) //--> y= 4(-x^2 + x)
            float interpolation = (-Mathf.Pow(percent, 2) + percent) * 4;
            transform.position = Vector3.Lerp(originalPosition, attackPosition, interpolation); //attacking movemtnt moves 0 to 1 = from ori pos > att pos then repeat

            yield return null;
        }

        currentState = State.Chasing; //after lerp, back to chasing
        pathfinder.enabled = true;
    } 


    IEnumerator UpdatePath()
    {
        //updates coroutine
        float refreshRate = 0.25f;
        while (target!= null)
        {
            if (currentState == State.Chasing) // only updates when enemy is chasing
            {
                Vector3 dirToTarget = (target.position - transform.position).normalized; //direction to the target
                // Vector3 targetPosition = new Vector3(target.position.x, 0, target.position.z);
                Vector3 targetPosition = target.position - dirToTarget * (enemyCollisionRadius + targetCollisionRadius + attackDistanceThreshold/2); 

                if (!dead)
                {
                    pathfinder.SetDestination(target.position);
                }
            }
           

            yield return new WaitForSeconds(refreshRate);
        }
    }
}
*/


[RequireComponent(typeof(NavMeshAgent))]
public class Enemy : LivingObjects
{

    public enum State { Idle, Chasing, Attacking };
    State currentState;

    NavMeshAgent pathfinder;
    Transform target;
    LivingObjects targetEntity;
    Material skinMaterial;

    Color originalColour;

    float attackDistanceThreshold = .5f;
    float timeBetweenAttacks = 1;
    float damage = 1;

    float nextAttackTime;
    float myCollisionRadius;
    float targetCollisionRadius;

    bool hasTarget;

    public override void Start()
    {
        base.Start();
        pathfinder = GetComponent<NavMeshAgent>();
        skinMaterial = GetComponent<Renderer>().material;
        originalColour = skinMaterial.color;

        if (GameObject.FindGameObjectWithTag("Player") != null)
        {
            currentState = State.Chasing;
            hasTarget = true;// when found target then its true

            target = GameObject.FindGameObjectWithTag("Player").transform;
            targetEntity = target.GetComponent<LivingObjects>();
            targetEntity.OnDeath += OnTargetDeath;

            myCollisionRadius = GetComponent<CapsuleCollider>().radius;
            targetCollisionRadius = target.GetComponent<CapsuleCollider>().radius;

            StartCoroutine(UpdatePath());
        }
    }

    void OnTargetDeath()
    {
        hasTarget = false;
        currentState = State.Idle;
    }

    void Update()
    {

        if (hasTarget)
        {
            if (Time.time > nextAttackTime)
            {
                float sqrDstToTarget = (target.position - transform.position).sqrMagnitude;
                if (sqrDstToTarget < Mathf.Pow(attackDistanceThreshold + myCollisionRadius + targetCollisionRadius, 2))
                {
                    nextAttackTime = Time.time + timeBetweenAttacks;
                    StartCoroutine(Attack());
                }

            }
        }

    }

    IEnumerator Attack()
    {

        currentState = State.Attacking;
        pathfinder.enabled = false;

        Vector3 originalPosition = transform.position;
        Vector3 dirToTarget = (target.position - transform.position).normalized;
        Vector3 attackPosition = target.position - dirToTarget * (myCollisionRadius);

        float attackSpeed = 3;
        float percent = 0;

        skinMaterial.color = Color.black;
        bool hasAppliedDamage = false;

        while (percent <= 1)
        {

            if (percent >= .5f && !hasAppliedDamage)
            {
                hasAppliedDamage = true;
                targetEntity.TakeDamage(damage);
            }

            percent += Time.deltaTime * attackSpeed;
            float interpolation = (-Mathf.Pow(percent, 2) + percent) * 4;
            transform.position = Vector3.Lerp(originalPosition, attackPosition, interpolation);

            yield return null;
        }

        skinMaterial.color = originalColour;
        currentState = State.Chasing;
        pathfinder.enabled = true;
    }

    IEnumerator UpdatePath()
    {
        float refreshRate = .25f;

        while (hasTarget)
        {
            if (currentState == State.Chasing)
            {
                Vector3 dirToTarget = (target.position - transform.position).normalized;
                Vector3 targetPosition = target.position - dirToTarget * (myCollisionRadius + targetCollisionRadius + attackDistanceThreshold / 2);
                if (!dead)
                {
                    pathfinder.SetDestination(targetPosition);
                }
            }
            yield return new WaitForSeconds(refreshRate);
        }
    }
}