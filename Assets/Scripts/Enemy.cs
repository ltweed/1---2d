using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float _speed = 4.0f;
    private Player _player;
    private Animator _animator;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(0, 6.5f, 0);
        _player = GameObject.Find("Player").GetComponent<Player>();
        if(_player == null)
        {
            Debug.LogError("player is null");
        }
        _animator = gameObject.GetComponent<Animator>();
        if(_animator==null)
        {
            Debug.LogError("animator is null");
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);
        if(transform.position.y < -6.5)
        {
            float randomX = Random.Range(-11, 11);
            transform.position = new Vector3(randomX, 6.5f, 0);
        }

        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            Player player = other.transform.GetComponent<Player>();
            if(player != null)
            {
                player.Damage();
            }
            _animator.SetTrigger("OnEnemyDeath");
            _speed = 0;
            Destroy(this.gameObject, 2.8f);
           
        }
        
        if(other.tag == "Laser")
        {
            Destroy(other.gameObject);
            if (_player != null)
            {
                Debug.Log("in Enemy/OnTriggerEnter2D, player != null");
                _player.AddScore(10);
             
            }
            _animator.SetTrigger("OnEnemyDeath");
            _speed = 0;
            Destroy(this.gameObject, 2.8f);
            
        }


    }
}
