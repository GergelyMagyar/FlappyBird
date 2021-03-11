using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum Mode
{
    AI, Manual
}

public class GameControllerScript : MonoBehaviour
{
    private GameModel _gameModel;

    [SerializeField] private GameViewScript _gameView;

    [SerializeField] private int _columnSize;
    [SerializeField] private int _rowSize;
    [SerializeField] private float _bidSize;
    [SerializeField] private float _birdJumpTimeSec;
    [SerializeField] private float _birdSpeed;
    [SerializeField] private float _birdJumpSpeed;

    private bool _gameOver;

    private Mode _mode;

    public UnityEvent GameOver;
    public UnityEvent NewGameStarted;

    public Vector2 BirdPosition
    {
        get
        {
            if(_gameModel != null)
            {
                return _gameModel.BirdPosition;
            }
            else
            {
                return new Vector2(-1, -1);
            }
        }
    }

    public bool BirdInJump
    {
        get
        {
            if (_gameModel != null)
            {
                return _gameModel.BirdInJump;
            }
            else
            {
                return false;
            }
        }
    }

    //get next tiles

    private void Start()
    {
        _gameOver = true;
        _gameView.CanvasInit();

        _gameView.manualButton.onClick.AddListener(() => { StartGame(Mode.Manual); });
        _gameView.aiButton.onClick.AddListener(() => { StartGame(Mode.AI); });
        _gameView.replayButton.onClick.AddListener(() => { StartGame(Mode.Manual); });
    }

    public void StartGame(Mode mode)
    {
        _mode = mode;

        _gameOver = false;

        _gameModel = new GameModel(_columnSize, _rowSize, _bidSize, _birdJumpTimeSec, _birdSpeed, _birdJumpSpeed);
        _gameModel.GameOver.AddListener(OnGameOver);
        _gameModel.ModelForwarded.AddListener(OnModelForwarded);

        _gameView.SetupView(_mode, _gameModel, _columnSize, _rowSize, _birdSpeed);
    }

    private void Update()
    {
        if (!_gameOver)
        {
            if (Input.GetKey("space"))
                _gameModel.JumpBird();

            _gameModel.UpdateModel();

            _gameView.UpdateView();
        }

        if(Input.GetKey("q"))
        {
            CloseGame();
        }
    }

    private void OnGameOver()
    {
        if(_mode == Mode.AI)
        {
            _gameOver = true;
            GameOver.Invoke();

            StartGame(Mode.AI);
        }
        else
        {
            _gameOver = true;
            _gameView.GameOver();
        }
    }

    private void OnModelForwarded()
    {
        _gameView.Forward();
    }
    public void CloseGame()
    {
        Application.Quit();
    }

    public void JumpBird()
    {
        if(_gameModel != null)
            _gameModel.JumpBird();
    }
}
