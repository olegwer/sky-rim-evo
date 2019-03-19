using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class laserBonus : MonoBehaviour
{   
    [SerializeField]
    private int powerID;

    void Update()
    {
        transform.Translate(Vector3.down * Time.deltaTime*0.9f);
        Destroy(this.gameObject,25f);
        transform.position = new Vector3(transform.position.x + 0.01f * Mathf.Sin(Time.time), transform.position.y, transform.position.z);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Collided with:" + other.name);
        if (other.tag=="Player")
        {
            player _player = other.GetComponent<player>();
            if (_player != null)
            {
                if (powerID == 0)
                {
                    //big lazer
                    _player.BigLazerOn();
                }
                else if (powerID==1)
                {
                    //speed
                    _player.SpeedBoostOn();
                }
                else if (powerID == 2)
                {
                    //sheld
                    _player.EnabledShield();
                }

            }
            Destroy(this.gameObject);
        }
    }
}
