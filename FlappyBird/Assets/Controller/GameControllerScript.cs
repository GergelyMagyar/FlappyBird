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
        _gameOver = false;

        _gameModel = new GameModel(columnSize, rowSize, bidSize, birdJumpTimeSec, birdSpeed, birdJumpSpeed);
        _gameModel.GameOver.AddListener(OnGameOver);
        _gameModel.ModelForwarded.AddListener(OnModelForwarded);

        gameView.SetupView(_gameModel, columnSize, rowSize, birdSpeed);
    }

    void Update()
    {
        if (_gameOver)
            return; 

        if (Input.GetKey("space"))
            _gameModel.JumpBird();

        _gameModel.UpdateModel();

        gameView.UpdateView();
    }

    void OnGameOver()
    {
        _gameOver = true;
    }

    void OnModelForwarded()
    {
        gameView.Forward();
    }
}
