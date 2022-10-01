using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    private int _currentScore = 0;
    protected bool _gameIsPlaying = false;
    private IEnumerator _coroutine;

    public TextMeshProUGUI _hud;
    public TextMeshProUGUI _highScoreHud;

    // Start is called before the first frame update
    void Start()
    {
        if(!PlayerPrefs.HasKey("HighScore"))
        {
            PlayerPrefs.SetInt("HighScore", 0);
        }
        _highScoreHud.text = Consts.HIGH_SCORE + PlayerPrefs.GetInt("HighScore");

        _gameIsPlaying = true;
        _coroutine = CountScore();
        StartCoroutine(_coroutine);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void IncreaseScore(int coinVal)
    {
        _currentScore += coinVal;
        if(PlayerPrefs.GetInt("HighScore") < _currentScore)
        {
            PlayerPrefs.SetInt("HighScore", _currentScore);
            _highScoreHud.text = Consts.HIGH_SCORE + PlayerPrefs.GetInt("HighScore");
        }
    }

    IEnumerator CountScore()
    {
        while(_gameIsPlaying)
        {
            yield return new WaitForSeconds(1);
            _currentScore++;

            _hud.text = Consts.CURRENT_SCORE + _currentScore;

            if(_currentScore > PlayerPrefs.GetInt("HighScore"))
            {
                PlayerPrefs.SetInt("HighScore", _currentScore);
                _highScoreHud.text = Consts.HIGH_SCORE + PlayerPrefs.GetInt("HighScore");
            }
        }
    }
}
