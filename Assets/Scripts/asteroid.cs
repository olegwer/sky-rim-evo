using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class asteroid : MonoBehaviour
{

    private float _speed;
    private UIManager _uiManager;
    public GameObject _enemyExploPrefab;
    float x = 0; // asteroid rotation
    private AudioSource _hitAudio;

    void Start()
    {
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        float a = 0.9f * Mathf.Sqrt(Time.timeSinceLevelLoad);
        _speed = Random.Range(0.5f, a);
        _hitAudio = GetComponent<AudioSource>();
    }

    void FixedUpdate()
    {
        x++;        
        transform.rotation = Quaternion.Euler(0, 0, -7*x);
        transform.Translate(Vector3.down * Time.deltaTime*_speed, Space.World);
        
        transform.position = new Vector3(transform.position.x + 0.05f * Mathf.Sin(Time.time*2), transform.position.y+ 0.01f * Mathf.Sin(Time.time), transform.position.z);

        if (transform.position.y < -6)
        {
            Destroy(this.gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
       if (other.tag == "Player")
        {
            player _player = other.GetComponent<player>();
            if (_player != null)
            {
                _player.Damage();
            }
            Instantiate(_enemyExploPrefab, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
       else if (other.tag == "LaserTag")
        {
            Behaviour halo = (Behaviour)GetComponent("Halo");
            halo.enabled = true;
            StartCoroutine(halodRoutine());
            _hitAudio.Play();
        }
    }
    public IEnumerator halodRoutine()
    {
        yield return new WaitForSeconds(0.01f);
        Behaviour halo = (Behaviour)GetComponent("Halo");
        halo.enabled = false;
    }
}
