using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UI_Manager : MonoBehaviour
{    
    [SerializeField]
    private Text _scoreText;
    [SerializeField]
    private Image _LivesImg;
    [SerializeField]
    private Sprite[] _liveSprites;
    [SerializeField]
    private Text _gameOverText;
    [SerializeField]
    private Text _restartGameText;
    private GameManager _gameManager;

    // Start is called before the first frame update
    void Start()
    {
        // assign Text component to handle
        _scoreText.text = "Score: " + 0;
        _gameOverText.gameObject.SetActive(false);
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        if(_gameManager==null)
        {
            Debug.LogError("gamemanager is null"); 
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateScore(int score)
    {
        _scoreText.text = "Score: " + score;
        Debug.Log("in UI_Manager/UpdateScore()");
    }

    public void UpdateLives(int currentLives)
    {
        // display image sprite
        // give it a new one based on index
        _LivesImg.sprite = _liveSprites[currentLives];
        if(currentLives == 0)
        {
            GameOverSequence();
        }
    }

    private void GameOverSequence()
    {
        _gameManager.GameOver();
        _gameOverText.gameObject.SetActive(true);
        _restartGameText.gameObject.SetActive(true);
        StartCoroutine(GameOverFlickerRoutine());
    }

    IEnumerator GameOverFlickerRoutine()
    {
        while (true)
        {
            
                _gameOverText.text = "Game Over";
                yield return new WaitForSeconds(0.5f);
                _gameOverText.text = "";
                yield return new WaitForSeconds(0.5f);

        }
    }
    // method to update score
}
