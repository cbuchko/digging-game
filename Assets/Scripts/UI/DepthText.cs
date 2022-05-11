using UnityEngine;
using UnityEngine.UI;
using System;
using System.Linq;

public class DepthText : MonoBehaviour
{
    public Text text;
    
    private GameObject player;
    private PlayerController controller;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        controller = player.GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if(controller.gameOver){
            return;
        }
        // 0.39 accounts for the offset from the origin
        if(player.transform.position.y >= 0){
            text.text = "0000";
            return;
        }
        int depth = (int)Math.Floor((player.transform.position.y/1.25));
        string depthString = Math.Abs(depth).ToString();
        int stringLength = depthString.Length;
        text.text = String.Concat(Enumerable.Repeat("0", 4 - stringLength)) + depthString;
    }
}
