using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResourceDisplay : MonoBehaviour
{
    public Text text;
    public string resourceType;
    private PlayerInventory playerInventory;
    // Start is called before the first frame update
    void Start()
    {
        playerInventory = GameObject.FindWithTag("Player").GetComponent<PlayerInventory>();
    }

    // Update is called once per frame
    void Update()
    {
        if(resourceType == "Iron") text.text = playerInventory.ironOre.ToString();
        if(resourceType == "Money") text.text = "$" + playerInventory.money;
    }
}
