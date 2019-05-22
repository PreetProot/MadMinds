using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


[RequireComponent(typeof(NavMeshAgent))]

public class Enemy : LivingObjects
{
    NavMeshAgent pathfinder;
    Transform target;
    

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start(); //pathfinder and target gets called
        pathfinder = GetComponent<NavMeshAgent>();
        target = GameObject.FindGameObjectWithTag("Player").transform;//assign playertag in edtior

        StartCoroutine(UpdatePath());
    }

    // Update is called once per frame
    void Update()
    {
        //follow player -calcs everyframe(could cause lag)
        //pathfinder.SetDestination(target.position); 
    }

    IEnumerator UpdatePath()
    {
        //updates coroutine
        float refreshRate = 0.25f;
        while (target!= null)
        {
            Vector3 targetPosition = new Vector3(target.position.x, 0, target.position.z);

            if (!dead)
            {
                pathfinder.SetDestination(target.position);
            }

            yield return new WaitForSeconds(refreshRate);
        }
    }
}
