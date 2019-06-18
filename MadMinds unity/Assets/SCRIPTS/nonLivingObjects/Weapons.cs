using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon", menuName = "Weapon")]
public class Weapons : ScriptableObject
{
    //min damage
    //max dam
    //range

    //store names
    
    public string weaponName;
    public GameObject weaponPrefab;

    [Header("Stats")]
    public int minDamage; //if further away from enemy = min damage; if nearest = max damage
    public int maxDamage;
    public float maxRange;//in units
}
