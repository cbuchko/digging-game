using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    private Transform player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        var maintainHorziontal = transform.position.x;
        Vector3 newPosition = player.transform.position + new Vector3(0, 0, -5);
        newPosition.x = maintainHorziontal;
        transform.position = newPosition;
    }
}
