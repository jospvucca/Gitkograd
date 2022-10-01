using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[RequireComponent(typeof(Rigidbody))]
public class Enemy : MonoBehaviour
{
    private float _lifetime = 5.0f;
    IEnumerator _coroutine;

    // Start is called before the first frame update
    void Start()
    {
        _coroutine = Lifecycle();
        StartCoroutine(_coroutine);
    }

    // Update is called once per frame
    void Update()
    {
        //gameObject.GetComponent<Rigidbody>().AddForce(Vector3.down * _ySpeed);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.name == "Terrain")
        {
            //I have decided its better without destroying on collision
            //Destroy(gameObject);
        }
    }

    IEnumerator Lifecycle()
    {
        while(_lifetime > 0)
        {
            yield return null;
            _lifetime-= Time.deltaTime;

            if(_lifetime <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
