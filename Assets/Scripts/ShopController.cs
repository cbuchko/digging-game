using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShopController : MonoBehaviour
{
    public int ironSell = 5;
    public GameObject text;

    private GameObject player;
    private PlayerInventory playerInventory;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        playerInventory = player.GetComponent<PlayerInventory>();
    }


    /**
    * Activated by the interactable prefab.
    * Open the shop UI.
    * TEMP: Just making this auto sell for now, because shop UI not a thing.
    */
    public void OpenShop(){
        int money = playerInventory.ironOre * ironSell;
        playerInventory.ironOre = 0;
        playerInventory.adjustMoney(money);
        text.GetComponent<TextMeshPro>().text = "+ $" + money;
        Instantiate(text, player.transform);
        FindObjectOfType<AudioManager>().Play("Shop");
    }
}
