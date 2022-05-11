using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{

    public int maxFuel = 2000;
    public int fuel;
    public double depthScore = 0;
    public Text text;

    public FuelBar fuelBar;
    public GameManager gameManager;

    public bool gameOver = false;

    private AudioManager audioSource;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = FindObjectOfType<AudioManager>();
        fuelBar.setMaxFuel(maxFuel);
    }

    // Update is called once per frame
    void Update()
    {
        /**
        *  If fuel is empty, end game
        */
        if(fuel <= 0){
            gameOver = true;
            gameObject.GetComponent<PlayerMovement>().enabled = false;
            gameObject.GetComponent<CharacterDig>().enabled = false;
            gameManager.endGame(depthScore);   
        }

        /**
        *  If fuel is less than 20%, start warning player
        */
       if(((float)fuel/(float)maxFuel) < 0.20){
            text.enabled = true;
            if(!audioSource.IsPlaying("Warning") && !gameOver){
                audioSource.Play("Warning");
            }
        } else if(text.enabled){
            text.enabled = false;
            if(audioSource.IsPlaying("Warning")){
                audioSource.Stop("Warning");
            }
        }
    }

    public void useFuel(int fuelAmount){
        fuel -= fuelAmount;
        fuelBar.setFuel(fuel);
    }

    public void fillFuel(float fuelPercentage){
        int fillAmount = (int)(maxFuel * fuelPercentage);
        int newFuel = fuel + fillAmount;
        if(newFuel > maxFuel){
            fuel = maxFuel;
            fuelBar.setFuel(maxFuel);
        } else {
            fuel = newFuel;
            fuelBar.setFuel(newFuel);
        }
    }

    public void updateDepthScore(){
        double score = Math.Floor((gameObject.transform.position.y/1.25));
        double currentScore = depthScore;
        if(score < currentScore){
            depthScore = score;
        }
    }
}
