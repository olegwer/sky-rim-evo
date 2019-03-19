using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class laser : MonoBehaviour
{
    void Update()
    {
        transform.Translate(Vector3.up);
        Destroy(this.gameObject, 0.5f);
    }
}
