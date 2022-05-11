using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resource : MonoBehaviour
{
    public string ResourceType;
    private PlayerInventory PlayerInventory;
    // Start is called before the first frame update
    void Start()
    {
        PlayerInventory = GameObject.FindWithTag("Player").GetComponent<PlayerInventory>();
    }

    void OnDestroy()
    {
        if(!PlayerInventory){
            return;
        }
        PlayerInventory.incrementResource(ResourceType);
    }
}
