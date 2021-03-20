using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{

    [SerializeField]
    private float _speed = 5f;
    [SerializeField]
    private GameObject _laserPrefab;
    [SerializeField]
    private GameObject _tripleShot;
    [SerializeField]
    private float _laserOffset = 1.05f;
    [SerializeField]
    private float _fireRate = 0.5f;
    private float _canFire = -1f;
    [SerializeField]
    private int _lives = 3;
    private SpawnManager _spawnManager;
    [SerializeField]
    private bool _isTripleShotActive = false;
    [SerializeField]
    private bool _isSpeedBoostActive = false;
    [SerializeField]
    private float _speedMultiplier = 2;
    [SerializeField]
    private bool _isShieldActive = false;
    [SerializeField]
    private GameObject Shields;
    [SerializeField]
    private int _score;
    [SerializeField]
    private UI_Manager _ui_Manager;




    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(0, 0, 0);
        _spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
        if(_spawnManager == null)
        {
            Debug.LogError("spawn manager is null");
        }
        _ui_Manager = GameObject.Find("Canvas").GetComponent<UI_Manager>();
        if(_ui_Manager == null)
        {
            Debug.LogError("spawn manager is null");
            
        }

    }

    // Update is called once per frame
    void Update()
    {
        CalculateMovement();

        if(Input.GetKeyDown(KeyCode.Space) && Time.time > _canFire)
        {
            FireLaser();
        }

    }

    private void FireLaser()
    {
        if (_isTripleShotActive)
        {
            Instantiate(_tripleShot, transform.position, Quaternion.identity);
        }
        else
        {
            Instantiate(_laserPrefab, transform.position + new Vector3(0, _laserOffset, 0), Quaternion.identity);
        }
        _canFire = Time.time + _fireRate;
        
    }

    private void CalculateMovement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 direction = new Vector3(horizontalInput, verticalInput, 0);

        if (_isSpeedBoostActive == false)
        { 
            transform.Translate(direction * _speed * Time.deltaTime);
        }
        else if(_isSpeedBoostActive == true)
        {
            transform.Translate(direction * _speed * _speedMultiplier * Time.deltaTime);

        }
        


        transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, -5.5f, 0), 0);

        if (transform.position.x > 11.3f)
        {
            transform.position = new Vector3(-11.3f, transform.position.y, 0);
        }
        else if (transform.position.x < -11.3f)
        {
            transform.position = new Vector3(11.3f, transform.position.y, 0);
        }
    }

    public void Damage()
    {
        if(_isShieldActive == true)
        {
            _isShieldActive = false;
            // shield visualizer disabled
            Shields.SetActive(false);
            return;
        }
        _lives--;
        _ui_Manager.UpdateLives(_lives);
        if (_lives < 1)
        {
            //communicate with spawnmanager0
            _spawnManager.OnPlayerDeath();
            // let them know to stop spawning
            
            Destroy(this.gameObject);


        }
    }

    public void TripleShotActive()
    {
        _isTripleShotActive = true;
        StartCoroutine(PowerDown());

    }

    public void SpeedBoostActive()
    {
        _isSpeedBoostActive = true;
        StartCoroutine(SpeedBoostPowerDown());

        // TODO:  currently does not "refresh" time when a duplicate powerup is collected while
        // the first one is still running

    }

    public void ShieldsActive()
    {
        _isShieldActive = true;
        Shields.SetActive(true);
        // shield visualizer enabled
        //StartCoroutine(ShieldsPowerDown());
    }

    IEnumerator PowerDown()
    {
        yield return new WaitForSeconds(5.0f);
        _isTripleShotActive = false;
    }

    IEnumerator SpeedBoostPowerDown()
    {
        
        yield return new WaitForSeconds(5.0f);
        _isSpeedBoostActive = false;
    }

    public void AddScore(int points)
    {
        _score += points;
        Debug.Log("In Player/AddScore");
        _ui_Manager.UpdateScore(_score);

    }



}
