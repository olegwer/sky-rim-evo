using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class laserBig : MonoBehaviour
{
   
    void Update()
    {       
        Destroy(this.gameObject, 0.1f);
    }
}
