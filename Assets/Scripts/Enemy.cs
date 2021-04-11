using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float _speed = -4.0f;
    private Player _player;
    private Animator animator;
    [SerializeField]
    private AudioSource _audioSource;

    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.Find("Player").GetComponent<Player>();
        animator = GetComponent<Animator>();
        _audioSource = GetComponent<AudioSource>();
        if (_player == null || animator == null|| _audioSource==null)
        {
            Debug.LogError("Some null");
        }

    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.up * _speed * Time.deltaTime);
        if (transform.position.y <= -7)
        {
            
            transform.position = new Vector3(Random.Range(-8f, 8f), 7, 0);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        animator.SetTrigger("onEnemyDeath");
        _audioSource.Play();
        if (other.tag == "Laser")
        {
            Destroy(other.gameObject);
            if (_player != null)
            {
                _player.updateScore(Random.Range(5,11));
            }
            animator.SetTrigger("onEnemyDeath");
            _speed = 0;
            this.GetComponent<BoxCollider2D>().enabled = false;
            Destroy(this.gameObject,2.8f);
        }
        if (other.tag == "Player")
        {
            Player player = other.transform.GetComponent<Player>();
            if (player != null)
            {
                player.Damage();
                player.updateScore(Random.Range(5, 11));
            }
            animator.SetTrigger("onEnemyDeath");
            _speed = 0;
            this.GetComponent<BoxCollider2D>().enabled = false;
            Destroy(this.gameObject,2.8f);
        }
    }
  

}
