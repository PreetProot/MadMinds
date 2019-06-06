using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loots : MonoBehaviour
{
    [System.Serializable]

    public class DropCurrency
    {
        public string name;
        public GameObject item;
        public int dropRarity;
    }

    public List<DropCurrency> Lootable = new List<DropCurrency>();
    public int dropChance;

    public void calcLoot()
    {
        //dropchnce
        int calcDropChance = Random.Range(0, 101);

        if(calcDropChance > dropChance)
        {
            Debug.Log("no loot for me");
            return;
        }

        if(calcDropChance <= dropChance)
        {
            //do in weight
            int itemWeight = 0; //itwem is calculate in weigth
            for(int i = 0; i< Lootable.Count; i++)
            {
                itemWeight += Lootable[i].dropRarity;
            }
            Debug.Log("itemWeight = " + itemWeight);

            //random value
            int randomValue = Random.Range(0, itemWeight); //80%
            for(int j = 0; j< Lootable.Count; j++)
            {
                if(randomValue <= Lootable[j].dropRarity )
                {
                    Instantiate(Lootable[j].item,transform.position, Quaternion.identity);
                    return;//break for loop
                }
                randomValue -= Lootable[j].dropRarity;
            }
        }
    }

    void Update()
    {
        calcLoot();
    }
}

   /* // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }*/

