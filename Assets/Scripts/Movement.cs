using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Movement : MonoBehaviour
{
    private float _horizontalSpeed = 50.0f;
    public AudioClip _quitAudio;
    [SerializeField]
    private AudioSource _audio;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Destroy(gameObject.GetComponentInChildren<EnemySpawn>());
            gameObject.GetComponent<Rigidbody>().detectCollisions = false;
            _audio.Stop();
            _audio.loop = false;
            _audio.clip = _quitAudio;
            _audio.Play();
            StartCoroutine(LoadMainMenu());
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(Input.GetKey(KeyCode.A))
        {
            //transform.Translate(Vector3.left * _horizontalSpeed);
            gameObject.GetComponent<Rigidbody>().AddForce(Vector3.left * _horizontalSpeed);
            //PlayKeySound();
        }

        if(Input.GetKey(KeyCode.D))
        {
            //transform.Translate(Vector3.right * _horizontalSpeed);
            gameObject.GetComponent<Rigidbody>().AddForce(Vector3.right * _horizontalSpeed);
            //PlayKeySound();
        }

        if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D))
        {
            gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
            //StopPlayingSound();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.tag == "Enemy")
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    private void PlayKeySound()
    {
        if(!GetComponent<AudioSource>().isPlaying)
        {
            GetComponent<AudioSource>().Play();
        }
    }

    private void StopPlayingSound()
    {
        GetComponent<AudioSource>().Stop();
    }

    IEnumerator LoadMainMenu()
    {
        while (_audio.isPlaying)
        {
            yield return null;
        }

        SceneManager.LoadScene(0);
    }
}
