using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AmmoManager : MonoBehaviour
{
    [SerializeField] public int ammoCount;
    [SerializeField] public Text ammoCountText;

    public Dictionary<AmmoTypes, int> ammoValue = new Dictionary<AmmoTypes, int>(); //key = ammotype, value = ammo amount

    public static AmmoManager instance;
    //public int ammoCount; //ammo amount

    public void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(this); //avoid having duplicates of the ammo
        }

    }

    public void Start()
    {
        //loop through each possible ammotype mand creat new ammotype for each
        for(int i= 0; i < System.Enum.GetNames(typeof (AmmoTypes)).Length; i++) //gets the items listed in Enums script
        {
            //ammoCount.Add((i)AmmoTypes, 0)??
            ammoValue.Add((AmmoTypes)i, 0); //loops through dictionary
        }
    }

    public void AddAmmo(int value, AmmoTypes ammoTypes)
    {
        //add int to the particular weapon
        ammoValue[ammoTypes] += value;

        //ammoCount += value; //when pick ammo -> it adds value -> updates in ammo text UI
        UpdateAmmoCountText();
    }

    //check if got ammo and consume it -true or false

    public bool UseAmmo(AmmoTypes ammoTypes)
    {
        if (ammoValue [ammoTypes] > 0)
        {
            ammoValue[ammoTypes]--;
            UpdateAmmoCountText();
            return true; //we have ammo -> we reduce by 1 -> then we can fire
        }
        else
        {
            return false;
        }
    }

    public void UpdateAmmoCountText()
    {
        ammoCountText.text = "Ammo: " + ammoCount;
    }
}
