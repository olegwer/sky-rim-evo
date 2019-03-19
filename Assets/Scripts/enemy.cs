using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy : MonoBehaviour
{
    private float _speed;
    [SerializeField]
    private GameObject _enemyExploPrefab;
    private UIManager _uiManager;    

    void Start()
    {
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        float a = 0.9f * Mathf.Sqrt(Time.timeSinceLevelLoad);
        _speed = Random.Range(2f, a);
    }

   
    void FixedUpdate()
    {
       transform.Translate(-Vector3.down*_speed*Time.deltaTime);

        if (transform.position.y<-6)
        {
            float randomX = Random.Range(-2.35f, 2.35f);
            transform.position = new Vector3(randomX, 6, 0);
            _uiManager.UpdateLiveEnemy();
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag=="LaserTag")
        {
            Destroy(other.gameObject);
            Instantiate(_enemyExploPrefab, transform.position, Quaternion.Euler(0, 0, 180));
            _uiManager.UpdateScore();
            Destroy(this.gameObject);
        }
        else if (other.tag == "Player")
        {
            player _player = other.GetComponent<player>();
            if (_player!=null)
            {
                _player.Damage();
            }
            Instantiate(_enemyExploPrefab, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
    }
}
