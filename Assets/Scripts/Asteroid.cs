using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    [SerializeField]
    private float _rotSpeed = 20f;
    // Start is called before the first frame update


    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.forward * _rotSpeed * Time.deltaTime);
    }

    // check for laser collision 2d
    // instantiate explosion at asteroid's position
    // destroy explosion after 3 sec
}
