﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyExplo : MonoBehaviour
{    
    void Update()
    {
        Destroy(this.gameObject, 3f);
    }
}
