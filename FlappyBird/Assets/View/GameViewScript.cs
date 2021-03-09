using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class GameViewScript : MonoBehaviour
{
    private int _columnSize;
    private int _rowSize;
    private float _birdSpeed;
    private GameModel _gameModel;

    public Tilemap tileGrid;
    public GameObject cameraObject;
    public GameObject birdObject;
    public Text scoreText;
    public Canvas scoreCanvas;
    public Canvas gameOverCanvas;
    public Text gameOverScoreText;
    public Button replayButton;

    public TileBase skyTile;
    public TileBase pipeTile;
    public TileBase pipeUpTile;
    public TileBase pipeDownTile;
    public TileBase grassTile;
    public TileBase illegalTile;

    private Camera _camera;


    public void SetupView(GameModel gameModel, int columnSize = 7, int rowSize = 30, float birdSpeed = 0.5f)
    {
        tileGrid.ClearAllTiles();
        EventSystem.current.SetSelectedGameObject(null);

        scoreCanvas.enabled = true;
        gameOverCanvas.enabled = false;

        _columnSize = columnSize;
        _rowSize = rowSize;
        _birdSpeed = birdSpeed;
        _gameModel = gameModel;

        if (cameraObject == null)
        {
            _camera = Camera.main;
        }
        else
        {
            _camera = cameraObject.GetComponent<Camera>();
            cameraObject.transform.localPosition = new Vector3(_gameModel.BirdPosition.x, _gameModel.BirdPosition.y, -10);
        }

        birdObject.transform.localPosition = _gameModel.BirdPosition;

        for(int i = 0; i < _rowSize; i++)
        {
            for (int j = 0; j < _columnSize; j++)
            {
                Vector3Int index = new Vector3Int(i, j, 0);

                TileType tileTpe = _gameModel.getTileType(index);

                switch(tileTpe)
                {
                    case TileType.Sky:
                        tileGrid.SetTile(index, skyTile);
                        break;
                    case TileType.Pipe:
                        tileGrid.SetTile(index, pipeTile);
                        break;
                    case TileType.PipeUp:
                        tileGrid.SetTile(index, pipeUpTile);
                        break;
                    case TileType.PipeDown:
                        tileGrid.SetTile(index, pipeDownTile);
                        break;
                    case TileType.Illegal:
                        tileGrid.SetTile(index, illegalTile);
                        break;
                }
            }
        }

        for(int i = -10; i < 0; i++)
        {
            for(int j = 0; j < _columnSize; j++)
            {
                Vector3Int index = new Vector3Int(i, j, 0);

                tileGrid.SetTile(index, skyTile);
            }
        }
    }

    public void UpdateView()
    {
        if (_columnSize <= 0 || _birdSpeed <= 0 || _gameModel == null)
            return;


        _camera.transform.localPosition += new Vector3(_birdSpeed, 0f, 0f);

        birdObject.transform.localPosition = _gameModel.BirdPosition;

        scoreText.text = "Score: " + _gameModel.Score;
    }

    public void Forward()
    {
        int forwardCount = _gameModel.ForwardCount;

        for (int j = 0; j < _columnSize; j++)
        {
            Vector3Int index1 = new Vector3Int(forwardCount- 1, j, 0);
            Vector3Int index2 = new Vector3Int(forwardCount - 2, j, 0);

            tileGrid.SetTile(index1, null);
            tileGrid.SetTile(index2, null);
        }


        for(int i = 1; i <= 2; i++)
        {
            for (int j = 0; j < _columnSize; j++)
            {
                Vector3Int modelIndex = new Vector3Int(_rowSize - i, j, 0);
                Vector3Int viewIndex = new Vector3Int(_rowSize + forwardCount - i, j, 0);

                TileType tileTpe = _gameModel.getTileType(modelIndex);

                switch (tileTpe)
                {
                    case TileType.Sky:
                        tileGrid.SetTile(viewIndex, skyTile);
                        break;
                    case TileType.Pipe:
                        tileGrid.SetTile(viewIndex, pipeTile);
                        break;
                    case TileType.PipeUp:
                        tileGrid.SetTile(viewIndex, pipeUpTile);
                        break;
                    case TileType.PipeDown:
                        tileGrid.SetTile(viewIndex, pipeDownTile);
                        break;
                    case TileType.Illegal:
                        tileGrid.SetTile(viewIndex, illegalTile);
                        break;
                }
            }
        }
    }

    public void GameOver()
    {
        scoreCanvas.enabled = false;
        gameOverCanvas.enabled = true;
        gameOverScoreText.text = _gameModel.Score + " points";
    }
}
