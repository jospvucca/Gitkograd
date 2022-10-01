using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private bool _hideMenu;

    [SerializeField]
    private AudioSource _audio;
    // Start is called before the first frame update
    void Start()
    {
        _hideMenu = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void HideMenu(string animationKey)
    {
        _hideMenu = true;
        gameObject.GetComponent<Animator>().SetBool(animationKey, _hideMenu);
    }

    public void ShowMenu(string animationKey)
    {
        _hideMenu = false;
        gameObject.GetComponent<Animator>().SetBool(animationKey, _hideMenu);
    }

    public void LoadGame(int sceneId)
    {
        StartCoroutine(LoadGameAfterSound(sceneId));
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    IEnumerator LoadGameAfterSound(int sceneId)
    {
        while(_audio.isPlaying)
        {
            yield return null;
        }

        SceneManager.LoadScene(sceneId);
    }
}
