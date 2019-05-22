using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{

    public Transform weaponHold;
    public Gun startingGun; //allaw starting weapon
    Gun equippedGun;


    // Start is called before the first frame update
    void Start()
    {
        if(startingGun != null)
        {
            EquipGun(startingGun); //if no gun, eqip a gun
        }
    }


    // Update is called once per frame
    void Update()
    {

    }

    public void EquipGun(Gun gunToEquip)
    {
        //check if currently equipped gun
        if (equippedGun != null)
        {
            Destroy(equippedGun.gameObject);
        }

        //                                                                               as Gun - cast this as a type of gun
        equippedGun = Instantiate (gunToEquip , weaponHold.position, weaponHold.rotation) as Gun; //equip new gun 
        equippedGun.transform.parent = weaponHold; //make gun child of player- moves with player

    }

    //mousebuttondown shoot
    public void Shoot()
    {
        //check if weapon is equipped
        if(equippedGun != null)
        {
            equippedGun.Shoot();
        }
    }

}
