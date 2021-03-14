using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    [SerializeField]
    private float _speed = 3.0f;
    // ID for powerups
    // 0 Triple shot
    // 1 Speed
    // 2 Shields

    [SerializeField]
    private int powerupID;


    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);
        if(transform.position.y < -6.5)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            Player _player = collision.transform.gameObject.GetComponent<Player>();
            if (_player != null)
            {
                switch(powerupID)
                {
                    case 0:
                        _player.TripleShotActive();
                        break;
                    case 1:
                        _player.SpeedBoostActive();
                        break;
                    case 2:
                        _player.ShieldsActive();
                        break;
                }
            }
            Destroy(this.gameObject);
        }
    }


}
