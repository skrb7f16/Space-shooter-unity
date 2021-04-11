using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    [SerializeField]
    private float _speed = 3.0f;
    [SerializeField]
    private int _powerUpId;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        MovePowerUp();
    }

    void MovePowerUp()
    {
        Vector3 directions = new Vector3(0, -1f, 0);
        transform.Translate(directions * _speed * Time.deltaTime);
        if (transform.position.y < -4.5f)
        {
            Destroy(this.gameObject);
        }

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Player player = other.transform.GetComponent<Player>();
            if (player != null)
            {
                if (_powerUpId == 0)
                {
                    player.GainedTripleShot();
                }
                else if (_powerUpId == 1)
                {
                    player.GainedSpeedBoost();
                }
                else if (_powerUpId == 2)
                {
                    player.GainedShieldPowerUp();
                }
            }
            Destroy(this.gameObject);
        }
    }
}
