using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateManager : MonoBehaviour
{
    public bool firstLoad = true;
    public GameObject startScreen;

    public static StateManager Instance {
        get;
        set;
    }

    void Awake(){
        GameObject[] objs = GameObject.FindGameObjectsWithTag("Manager");
        if(objs.Length > 1){
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(this.gameObject);
        Instance = this;
    }

    void Start(){
        if(firstLoad){
            GameObject player = GameObject.FindWithTag("Player"); 
            startScreen.SetActive(true);
            player.GetComponent<PlayerMovement>().enabled = false;
            player.GetComponent<CharacterDig>().enabled = false;
            firstLoad = false;
        } 
    }
}
