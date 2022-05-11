using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class Destructable : MonoBehaviour
{
    public GameObject particleEffect;
    public Animator animator;
    public GameObject text;

    private GameObject player;
    private CharacterDig digScript;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        digScript = player.GetComponent<CharacterDig>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision){
        if(collision.gameObject.CompareTag("Player")){
            digScript.DigOptions.Add(gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D collision){
        if(collision.gameObject.CompareTag("Player")){
            digScript.DigOptions.Remove(gameObject);
        }
    }


    public void startCracking(){
        animator.SetBool("destroy", true);
    }

    void OnDestroy(){
        if(!this.gameObject.scene.isLoaded){
            return;
        }
        FindObjectOfType<AudioManager>().PlayForTime("BlockBreak", 0.75f);
        Instantiate(particleEffect, transform.position, transform.rotation);
        if(text && player){
            text.GetComponent<TextMeshPro>().text = "+1 Iron";
            Instantiate(text, player.transform);
        }
    }
}
