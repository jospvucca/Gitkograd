using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MovementGame2 : MonoBehaviour
{
    private float _horizontalSpeed = 50.0f;
    public AudioClip _quitAudio;
    [SerializeField]
    private AudioSource _audio;

    public AudioClip _keyAudio;
    public AudioClip _barkAudio;

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
        if (Input.GetKey(KeyCode.A) || Input.GetKey("4"))
        {
            //transform.Translate(Vector3.left * _horizontalSpeed);
            gameObject.GetComponent<Rigidbody>().AddForce(Vector3.left * _horizontalSpeed);
            PlayKeySound(_keyAudio);
        }

        if (Input.GetKey(KeyCode.D) || Input.GetKey("6"))
        {
            //transform.Translate(Vector3.right * _horizontalSpeed);
            gameObject.GetComponent<Rigidbody>().AddForce(Vector3.right * _horizontalSpeed);
            PlayKeySound(_keyAudio);
        }

        if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D) || Input.GetKey("4") || Input.GetKey("6"))
        {
            gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
            StopPlayingSound();
        }

        if(Input.GetKeyDown("5"))
        {
            PlayKeySound(_barkAudio);
        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Enemy")
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    private void PlayKeySound(AudioClip clip)
    {
        if (!GetComponent<AudioSource>().isPlaying)
        {
            GetComponent<AudioSource>().clip = clip;
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
