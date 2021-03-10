using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameControllerScript : MonoBehaviour
{
    private GameModel _gameModel;
    
    public GameViewScript gameView;

    public int columnSize;
    public int rowSize;
    public float bidSize;
    public float birdJumpTimeSec;
    public float birdSpeed;
    public float birdJumpSpeed;

    private bool _gameOver;

    void Start()
    {
        gameView.replayButton.onClick.AddListener(StartGame);

        StartGame();
    }

    void StartGame()
    {
        _gameOver = false;

        _gameModel = new GameModel(columnSize, rowSize, bidSize, birdJumpTimeSec, birdSpeed, birdJumpSpeed);
        _gameModel.GameOver.AddListener(OnGameOver);
        _gameModel.ModelForwarded.AddListener(OnModelForwarded);

        gameView.SetupView(_gameModel, columnSize, rowSize, birdSpeed);
    }

    void Update()
    {
        if (!_gameOver)
        {
            if (Input.GetKey("space"))
                _gameModel.JumpBird();

            _gameModel.UpdateModel();

            gameView.UpdateView();
        }

        if(Input.GetKey("q"))
        {
            CloseGame();
        }
    }

    void OnGameOver()
    {
        _gameOver = true;
        gameView.GameOver();
    }

    void OnModelForwarded()
    {
        gameView.Forward();
    }
    public void CloseGame()
    {
        Application.Quit();
    }
}
