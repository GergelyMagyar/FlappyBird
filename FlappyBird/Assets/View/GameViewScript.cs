using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GameViewScript : MonoBehaviour
{
    private int _columnSize;
    private int _rowSize;
    private float _birdSpeed;
    private GameModel _gameModel;

    public Tilemap tileGrid;
    public GameObject cameraObject;
    public GameObject birdObject;

    public TileBase skyTile;
    public TileBase pipeTile;
    public TileBase pipeUpTile;
    public TileBase pipeDownTile;
    public TileBase grassTile;
    public TileBase illegalTile;

    private Camera _camera;


    public void SetupView(GameModel gameModel, int columnSize = 7, int rowSize = 30, float birdSpeed = 0.5f)
    {
        _columnSize = columnSize;
        _rowSize = rowSize;
        _birdSpeed = birdSpeed;
        _gameModel = gameModel;

        if (cameraObject == null)
            _camera = Camera.main;
        else
            _camera = cameraObject.GetComponent<Camera>();

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
    }

    public void UpdateView()
    {
        if (_columnSize <= 0 || _birdSpeed <= 0 || _gameModel == null)
            return;


        _camera.transform.localPosition += new Vector3(_birdSpeed, 0f, 0f);

        birdObject.transform.localPosition = _gameModel.BirdPosition;
    }

    public void Forward()
    {
        //paint new grid cells, delete old?
    }
}
