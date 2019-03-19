using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class player : MonoBehaviour {
    
    public float horizontalInput;
    public float verticalInput;
    public int lives = 3;

    [SerializeField]
    public bool canLazer = false;
    [SerializeField]
    public bool isSpeedBoostActive = false;
    [SerializeField]
    public bool isShieldActive = false;


    public GameObject laserPrefab;
    public GameObject bigLaserPrefab;

    [SerializeField]
    private GameObject playerExploPrefab;

    private UIManager _uiManager;
    private GameManager _gameManager;

    private float fireRate = 0.8F;
    private float nextFire = 0.0F;

    private AudioSource _audioPickUp;

    [SerializeField]
    private GameObject[] engines;
    [SerializeField]
    private GameObject[] hvost;
    private int hitCount = 0;

    private Vector2 touchOffset;

    private Vector3 screenPoint;
    private Vector3 offset;

    void Start () {
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();

        if (_uiManager !=null)
        {
            _uiManager.UpdateLives(lives);
        }
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();        
        _audioPickUp = GetComponent<AudioSource>();
        hitCount = 0;
    }
    
    
    void Update () {
       
        if ( Time.time > nextFire)
        {
            if (isSpeedBoostActive == true || canLazer == true)
            {
                fireRate =.15f;
            }
           
            else
            {
                fireRate = .65f;
            }
            nextFire = Time.time + (fireRate);
            Shoot();            
        }      

    }
    void Shoot()
    {
        
            if (canLazer == true)
            {
            Instantiate(bigLaserPrefab, transform.position + new Vector3(0, 1.4f, 0), Quaternion.identity);
            }
            else
            {
                Instantiate(laserPrefab, transform.position + new Vector3(0, 0.76f, 0), Quaternion.identity);
            }
        
    }

    void OnMouseDown()
    {
        screenPoint = Camera.main.WorldToScreenPoint(transform.position);
        offset = transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
    }

    void OnMouseDrag()
    {
        Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
        Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint) + offset;
        transform.position = curPosition;


        if (transform.position.y > 4)
        {
            transform.position = new Vector3(transform.position.x, 4, 0);
        }
        else if (transform.position.y < -5f)
        {
            transform.position = new Vector3(transform.position.x, -5f, 0);
        }

        if (transform.position.x > 2.9f)
        {
            transform.position = new Vector3(2.9f, transform.position.y, 0);
        }
        else if (transform.position.x < -2.9f)
        {
            transform.position = new Vector3(-2.9f, transform.position.y, 0);

        }
    }             


   /* void move()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

        if (isSpeedBoostActive == true)
        {
            transform.Translate(Vector3.right * horizontalInput * Time.deltaTime * speed * 1.5f);
            transform.Translate(Vector3.up * verticalInput * Time.deltaTime * speed * 1.5f);
        }
        else
        {
            transform.Translate(Vector3.right * horizontalInput * Time.deltaTime * speed);
            transform.Translate(Vector3.up * verticalInput * Time.deltaTime * speed);
        }
       

        if (transform.position.y>4)
        {
            transform.position = new Vector3(transform.position.x, 4, 0);
        }
        else if(transform.position.y < -5f)
                {
            transform.position = new Vector3(transform.position.x, -5f, 0);
        }

        if (transform.position.x>2.5f)
        {
            transform.position = new Vector3(2.5f, transform.position.y, 0);
        }
        else if (transform.position.x < -2.5f)
        {
            transform.position = new Vector3(-2.5f, transform.position.y, 0);//2.35

        }
    }
    */

    //super Laser
    public void BigLazerOn()
    {
        canLazer = true;        
        StartCoroutine(BigLazerRoutineOff());
        _audioPickUp.Play();
    }
    public IEnumerator BigLazerRoutineOff()
    {
        yield return new WaitForSeconds(7);
        canLazer = false;
    }
    //speed Boost
    public void SpeedBoostOn()
    {
        isSpeedBoostActive = true;       
        StartCoroutine(SpeedBoostRoutine());        
        _audioPickUp.Play();
    }
    public IEnumerator SpeedBoostRoutine()
    {
        yield return new WaitForSeconds(5);
        isSpeedBoostActive = false;        
    }

    //shield
    public void EnabledShield()
    {
        isShieldActive = true;
        Behaviour halo = (Behaviour)GetComponent("Halo");
        halo.enabled = true;
        
        StartCoroutine(shieldRoutine());
        _audioPickUp.Play();
    }

    public IEnumerator shieldRoutine()
    {
        yield return new WaitForSeconds(5);
        isShieldActive = false;
        Behaviour halo = (Behaviour)GetComponent("Halo");
        halo.enabled = false;
    }
    
    public void Damage() //Damage Player
    {
       
        if (isShieldActive==true)
        {
            isShieldActive = false;

            Behaviour halo = (Behaviour)GetComponent("Halo");
            halo.enabled = false;
            return;
        }
        if (isShieldActive == false)
        {
            hitCount++;
        }
        if (hitCount == 1)
        {
            engines[0].SetActive(true);
            hvost[0].SetActive(false);
        }
        else if (hitCount == 2)
        {
            engines[1].SetActive(true);
            hvost[1].SetActive(false);
        }

        lives--; // livel-=1; lives=lives-1;
        _uiManager.UpdateLives(lives);

        if (lives<1)
        {
            Instantiate(playerExploPrefab, transform.position, Quaternion.identity);
            _gameManager.gameOver = true;
            _uiManager.ShowGameOverScreen();            
            Destroy(this.gameObject);
            Time.timeScale = 0.0f;            
        }
        else 
        {
            Time.timeScale = 1f;
        }
    }
    IEnumerator TimeScaleFade()
    {
        for (float f = 1f; f >= 0; f -= 0.1f)
        {
            Time.timeScale = f;
            yield return new WaitForSeconds(.1f);
        }
    }

}

