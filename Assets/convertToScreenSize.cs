using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class convertToScreenSize : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Camera cam = GetComponent<Camera>();
        Vector3 screenPos = cam.WorldToScreenPoint(transform.position);
        transform.position = screenPos;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
