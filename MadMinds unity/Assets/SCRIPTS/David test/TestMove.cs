using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TestMove : MonoBehaviour
{

    public Transform target;
    NavMeshAgent agent;
    WaitForSeconds delay = new WaitForSeconds(1f);
    NavMeshPath path;
    LineRenderer line;

    // Use this for initialization
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        StartCoroutine(FindPathRoutine());
        line = GetComponent<LineRenderer>();
    }

    public void Update()
    {
        GetCurrentPath();
        line.positionCount = path.corners.Length; // set size of line points

        Vector3[] modifiedCorners = path.corners;
        for (int i = 0; i < modifiedCorners.Length; i++)
        {
            modifiedCorners[i].y += 1f; // Push path up by one unit
        }


        line.SetPositions(modifiedCorners); // pass all corner positions to line points
    }

    void GetCurrentPath()
    {
        path = agent.path;
    }

    IEnumerator FindPathRoutine()
    {
        while (true)
        {
            //yield return new WaitForSeconds (1f); // Wait for one second
            yield return delay; //wait for X seconds 
                                //yield return null;	
            agent.SetDestination(target.position);
        }
    }

    public void OnDrawGizmos()
    {
        if (agent != null && agent.path != null)
        {
            for (int i = 0; i < path.corners.Length - 1; i++)
            {
                Debug.DrawLine(path.corners[i], path.corners[i + 1]);
            }
        }
    }


}


