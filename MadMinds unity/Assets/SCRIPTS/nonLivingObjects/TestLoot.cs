using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestLoot : MonoBehaviour
{

    public void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            print("item picked up");
            Destroy(gameObject);
        }
    }

 
}

