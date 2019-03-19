using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class galaxy_mover : MonoBehaviour
{
    float scrollSpeed = 0f;
    Renderer rend;
    void Start()
    {
        rend = GetComponent<Renderer>();
    }
        
    void Update()
    {
        scrollSpeed= scrollSpeed + 0.0006f;
        float offset = scrollSpeed;
               
        rend.material.mainTextureOffset = new Vector3(offset, 0, 0);
    }
}
