using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public float restartDelay = 1f;
    public GameObject startScreen;
    public GameOverScreen gameOverScreen;
    public GameObject explosion;
    public GameObject drill;

    private bool gameHasEnded = false;
    private AudioManager audioManager;
    private GameObject player;

    void Start(){
        audioManager = FindObjectOfType<AudioManager>();
        player = GameObject.FindWithTag("Player");    
    }

    public void startGame(){
        Debug.Log("Starting Game!");
        player.GetComponent<PlayerMovement>().enabled = true;
        player.GetComponent<CharacterDig>().enabled = true;
        startScreen.SetActive(false);
    }

    public void endGame(double depth){
        if(gameHasEnded == false){
            gameHasEnded = true;
            explosion.SetActive(true);
            audioManager.Play("Explosion");
            drill.SetActive(false);
            player.GetComponent<SpriteRenderer>().color = new Color(1f,1f,1f,0f);
            StartCoroutine(waitBeforeEnd(depth));
            
        }
    }

    private IEnumerator waitBeforeEnd(double depth){
        yield return new WaitForSeconds(2.5f);
        explosion.SetActive(false);
        audioManager.Stop("Explosion");
        audioManager.Stop("Warning");
        gameOverScreen.Setup(depth);
    }
}
