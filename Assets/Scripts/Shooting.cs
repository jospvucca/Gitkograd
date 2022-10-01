using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    //weaponRotation
    private Camera _camera;
    private Vector3 _mousePos;

    //weaponSelection
    public Weapon[] _arsenal;
    private Weapon _currentWeapon;
    public Canvas _weaponHud;

    //cooldown
    public Transform _weaponTransform;
    private bool _canFire;
    private float _timer;
    public float _fireCooldown = 2.0f;     //->time between firing

    // Start is called before the first frame update
    void Start()
    {
        _camera = Camera.main;
        _currentWeapon = _arsenal[0];
        _canFire = true;
    }

    // Update is called once per frame
    void Update()
    {
        //Weapon rotation
        CalculateWeaponRotation();

        //Weapon arsenal picking open/close
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _weaponHud.gameObject.SetActive(true);
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            _weaponHud.gameObject.SetActive(false);
        }

        //Weapon instantiate
        if (!_canFire)
        {
            _timer += Time.deltaTime;
            if(_timer >= _fireCooldown)
            {
                _canFire = true;
                _timer = 0;
            }
        }

        if(Input.GetMouseButtonDown(0) && _canFire)
        {
            FireWeapon();
        }
    }

    void CalculateWeaponRotation()
    {
        _mousePos = _camera.ScreenToWorldPoint(Input.mousePosition);
        Vector3 rotation = _mousePos - transform.position;
        float rotZ = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
        Debug.Log("Rotz: " + rotZ);

        if(rotZ < 0 && rotZ > -90)
        {
            rotZ = 0.0f;
        }

        if (rotZ > -179.99f && rotZ < -90)
        {
            rotZ = 180.0f;
        }

        transform.rotation = Quaternion.Euler(0, 0, rotZ);
    }

    void FireWeapon()
    {
        _canFire = false;
        Instantiate(_currentWeapon, _weaponTransform.position, Quaternion.identity);
    }
}
