using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public int ironOre = 0;
    public int money = 0;

    public void incrementResource(string resourceType){
        if(resourceType == "iron")ironOre++;
    }

    public void adjustMoney(int amount){
        money = money + amount;
    }

    public void setMoney(int amount){
        money = amount;
    }

}
