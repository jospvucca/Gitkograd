using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    //Trajectory
    private Vector3 _mousePos;
    private Camera _camera;
    private Rigidbody _rb;
    public float _force = 5.0f;

    private float _lifetime = 5.0f;
    IEnumerator _coroutine;

    [SerializeField]
    private GameObject _explosion;

    //If we dont want to control projectile while its alive save onStart Vector3 as a endpoint, and send it to coroutine
    Vector3 _finalPosition = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        _camera = Camera.main;
        _rb = GetComponent<Rigidbody>();
        _mousePos = _camera.ScreenToWorldPoint(Input.mousePosition);
        Vector3 direction = _mousePos - transform.position;
        Vector3 rotation = transform.position - _mousePos;
        _rb.velocity = new Vector3(direction.x, direction.y, 0).normalized * _force;

        float rot = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rot + 90);


        //transform.position = gameObject.transform.position + new Vector3(Input.mousePosition.x / Screen.width, Input.mousePosition.y / Screen.height, 0);

        //Debug.Log("Instantiated on click weapon: " + gameObject.name + " at position: " + transform.position.ToString());

        _coroutine = DoTrajectory();
        StartCoroutine(_coroutine);

        //_finalPosition = new Vector3(Input.mousePosition.x / Screen.width, Input.mousePosition.y / Screen.height, 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator DoTrajectory()
    {
        //TODO: do this in Weapon script
        while (_lifetime > 0)
        {
            yield return null;
            _lifetime -= Time.deltaTime;
            //transform.Translate(new Vector3(Input.mousePosition.x / Screen.width, Input.mousePosition.y / Screen.height, 0) * _speed);
            //transform.Rotate(new Vector3(Input.mousePosition.x / Screen.width, Input.mousePosition.y / Screen.height, 0));

            if (_lifetime <= 0)
            {
                Destroy(gameObject);
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.tag == "Enemy")
        {
            Instantiate(_explosion, transform.position, Quaternion.identity);
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
    }
}
