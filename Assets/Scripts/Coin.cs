using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public float _speed = 0.1f;
    private int _coinVal = 5;

    [SerializeField]
    private GameManager _gameManager;
    // Start is called before the first frame update
    void Start()
    {
        if(_gameManager == null)
        {
            _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        }

        if(transform.position.y > 2)
        {
            transform.position.Set(transform.position.x, 1.8f, 0.0f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.up * _speed, Space.Self);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.tag == "Player")
        {
            _gameManager.IncreaseScore(_coinVal);
            Destroy(gameObject);
        }
    }
}
