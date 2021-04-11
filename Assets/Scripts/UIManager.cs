using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIManager : MonoBehaviour
{
    [SerializeField]
    private Text _scoreText;
    [SerializeField]
    private Text _gameOverText;
    [SerializeField]
    private Text _restartText;
    [SerializeField]
    private Image _livesImage;
    [SerializeField]
    private Sprite[] _livesSprites;

    private GameManager _gameManager;

    
    void Start()
    {
        _scoreText.text = "Score : " + 0;
        _gameOverText.text = "GAME OVER MEOW!!!";
        _gameManager = GameObject.Find("Game_Manger").GetComponent<GameManager>();
    }


    public void scoreChanged(int currentScore)
    {
        _scoreText.text = "Score : " + currentScore;
    }

    public void updateLives(int currentLive)
    {
        _livesImage.sprite = _livesSprites[currentLive];
        if (currentLive == 0)
        {
            gameOverActive();
        }
    }

    public void gameOverActive()
    {
        _gameManager.gameOverTrue();
        _gameOverText.gameObject.SetActive(true);
        _restartText.gameObject.SetActive(true);
        StartCoroutine(GameOverFlickerRoutine());
    }

    IEnumerator GameOverFlickerRoutine()
    {
        while (true)
        {
            _gameOverText.text = "GAME OVER MEOW!!!";
            yield return new WaitForSeconds(0.2f);
            _gameOverText.text = "";
            yield return new WaitForSeconds(0.2f);
        }
    }
}
