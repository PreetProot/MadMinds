using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoPickUp : MonoBehaviour, LootInterface
{

    [SerializeField] public int ammoCount;
    [SerializeField] public AmmoTypes ammoTypes; //different values for different ammos


    public void OnStartLook()
    {
        //show tooltip UI
    }

    public void OnInteract()
    {
        AmmoManager.instance.AddAmmo(ammoCount, ammoTypes);
        Destroy(gameObject);
    }

    public void OnEndLook()
    {
        //hide tooltip UI
    }

    //initial pick up Interation
    public void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.GetComponent<PlayerController>() != null) //when touch player add to player ammunition
        {
            AmmoManager.instance.AddAmmo(ammoCount, ammoTypes);
            Destroy(gameObject);
        }
    }


}
