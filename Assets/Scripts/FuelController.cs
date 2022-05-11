using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuelController : MonoBehaviour
{
    public int fuelCost;
    private PlayerController playerController;
    private PlayerInventory playerInventory;
    // Start is called before the first frame update
    void Start()
    {
        playerController = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
        playerInventory = GameObject.FindWithTag("Player").GetComponent<PlayerInventory>();
    }

    public void buyFuel(){
        float currentFuel = (float)playerController.fuel;
        float maxFuel = (float)playerController.maxFuel;
        float money = (float)playerInventory.money;

        if(money == 0){
            return;
        }

        float requiredFuelPercent = (maxFuel - currentFuel)/maxFuel;
        float derivedCost = requiredFuelPercent * fuelCost;
        float fuelPercentage = money/derivedCost;
        int newMoney = (int)money - (int)derivedCost;

        if(fuelPercentage > 1) fuelPercentage = 1;
        if (newMoney < 0){ 
            playerInventory.setMoney(0);
        } else {
            playerInventory.setMoney(newMoney);
        }
        
        playerController.fillFuel(fuelPercentage);
        FindObjectOfType<AudioManager>().Play("Fuel");
    }
}
