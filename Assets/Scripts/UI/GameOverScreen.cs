using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverScreen : MonoBehaviour
{
    public Text depthText;
    
    public void Setup(double depth){
        gameObject.SetActive(true);
        depthText.text = string.Format("{0:N2}", depth) + " Depth";
    }

    public void Restart(){
        SceneManager.LoadScene("SampleScene");
    }
}
