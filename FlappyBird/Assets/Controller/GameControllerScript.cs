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

    void Start()
    {
        _gameModel = new GameModel(columnSize, rowSize, bidSize, birdJumpTimeSec, birdSpeed, birdJumpSpeed);

        gameView.SetupView(_gameModel, columnSize, rowSize, birdSpeed);
    }

    void Update()
    {
        if (Input.GetKey("space"))
            _gameModel.JumpBird();

        _gameModel.UpdateModel();

        gameView.UpdateView();
    }
}
