using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSwitch : MonoBehaviour
{

    //name of weapon
    public int selectedWeapon = 0;

    // Start is called before the first frame update
    void Start()
    {
        SelectWeapon();
    }

    // Update is called once per frame
    void Update()
    {
        int prevSelectedWeapon = selectedWeapon;

        //player input        
        //----------------------------scroll switch

        if (Input.GetAxis("Mouse ScrollWheel") > 0f)
        {
            if (selectedWeapon >= transform.childCount - 1)
                selectedWeapon = 0;
            else
                selectedWeapon++;
        }
        if (Input.GetAxis("Mouse ScrollWheel") < 0f)
        {
            if (selectedWeapon <= transform.childCount - 1)
                selectedWeapon = 0;
            else
                selectedWeapon--;
        }
        //----------------------------number switch

        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            selectedWeapon= 0; //weapon1
        }

        if (Input.GetKeyDown(KeyCode.Alpha2) && transform.childCount >0)
        {
            selectedWeapon = 1; //weapon2
        }

        //----------------------------number switch function

        if (prevSelectedWeapon != selectedWeapon)
        {
            SelectWeapon();
        }
    }

    void SelectWeapon()
    {
        int i = 0;//loop over all the weapons
        foreach(Transform weapon in transform)
        {
            if(i == selectedWeapon)
            {
                weapon.gameObject.SetActive(true);
            }
            else
            {
                weapon.gameObject.SetActive(false);
                i++;
            }
        }
    }

}
