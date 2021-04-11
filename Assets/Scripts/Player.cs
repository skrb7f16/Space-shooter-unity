using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    
    // Start is called before the first frame update
    [SerializeField]
    private float _speed = 5.0f;
    private float multiplierSpeed = 2.0f;
    [SerializeField]
    private GameObject _laserPrefab;
    [SerializeField]
    private GameObject _tripleShotPrefab;
    [SerializeField]
    private float _fireRate = 0.5f;
    private float _canFire = -1.0f;
    [SerializeField]
    private int _lives = 3;
    [SerializeField]
    private bool _isTripleActive = false;
    [SerializeField]
    private bool _isSpeedActive = false;
    [SerializeField]
    private bool _isShieldActive = false;
    [SerializeField]
    private GameObject _shieldVisualizer;
    [SerializeField]
    private GameObject _leftEngine;
    [SerializeField]
    private GameObject _rightEngine;
    [SerializeField]
    private AudioSource _audioSource;
    [SerializeField]
    private AudioClip _laserClip,_powerpClip;
    [SerializeField]
    private int _score = 0;

    private UIManager _uiManager;
    private SpawnManager _spawnManager;
    void Start()
    {
        transform.position = new Vector3(0, 0, 0);
        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        _uiManager.updateLives(_lives);
        _audioSource = GetComponent<AudioSource>();



    }

    // Update is called once per frame
    void Update()
    {
        CalculateMovement();
        if (Input.GetKeyDown(KeyCode.Space) && Time.time > _canFire)
        {
            ShootLaser();
        }
        
    }

    void CalculateMovement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 directions = new Vector3(horizontalInput, verticalInput, 0);
        transform.Translate(directions * _speed * Time.deltaTime);

        if (transform.position.y >= 0)
        {
            transform.position = new Vector3(transform.position.x, 0, 0);
        }
        else if (transform.position.y <= -3.8f)
        {
            transform.position = new Vector3(transform.position.x, -3.8f, 0);
        }
        if (transform.position.x >= 11.3f)
        {
            transform.position = new Vector3(-11.3f, transform.position.y, 0);
        }
        else if (transform.position.x <= -11.3f)
        {
            transform.position = new Vector3(11.3f, transform.position.y, 0);
        }
    }


    void ShootLaser()
    {

            _canFire = Time.time + _fireRate;
        if (!_isTripleActive)
        {
            Instantiate(_laserPrefab, new Vector3(transform.position.x, transform.position.y + 1.05f, transform.position.z), Quaternion.identity);
        }
        else
        {
            Instantiate(_tripleShotPrefab, new Vector3(transform.position.x, transform.position.y + 1.05f, transform.position.z), Quaternion.identity);
        }
        _audioSource.clip = _laserClip;
        _audioSource.Play();
    }

    public void Damage()
    {
        if (!_isShieldActive)
        {
            _lives -= 1;
            if (_lives == 2)
            {
                _leftEngine.SetActive(true);
            }
            else if (_lives == 1)
            {
                _rightEngine.SetActive(true);
            }
            _uiManager.updateLives(_lives);
            if (_lives < 1)
            {
                _spawnManager.onPlayerDeath();
                

                Destroy(this.gameObject);
            }
        }
    }
    public void addLives(int n)
    {
        _lives += n;
    }
    public int livesLeft()
    {
        return _lives;
    }

    public void GainedTripleShot()
    {
        _isTripleActive = true;
        _audioSource.clip = _powerpClip;
        _audioSource.Play();
        StartCoroutine(TripleShotPowerDownCoroutine());
    }

    IEnumerator TripleShotPowerDownCoroutine()
    {
        yield return new WaitForSeconds(5);
        _isTripleActive = false;
    }

    public void GainedSpeedBoost()
    {
        _isSpeedActive = true;
        _audioSource.clip = _powerpClip;
        _audioSource.Play();
        _speed *= multiplierSpeed;
        StartCoroutine(SpeedPowerDownCoroutine());
    }

    IEnumerator SpeedPowerDownCoroutine()
    {
        yield return new WaitForSeconds(5);
        _isSpeedActive = false;
        _speed /= multiplierSpeed;
    }

    public void GainedShieldPowerUp()
    {
        _isShieldActive = true;
        _audioSource.clip = _powerpClip;
        _audioSource.Play();
        _shieldVisualizer.SetActive(true);
        StartCoroutine(ShieldPowerDownCoroutine());
    }

    IEnumerator ShieldPowerDownCoroutine()
    {
        yield return new WaitForSeconds(5);
        _isShieldActive = false;
        _shieldVisualizer.SetActive(false);
    }

    public void updateScore(int points)
    {
        _score += points;
        _uiManager.scoreChanged(_score);

    }

    public int getScore()
    {
        return _score;
    }
}
