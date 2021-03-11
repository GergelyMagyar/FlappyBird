using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class GameModel
{
    private int _columnSize;
    private int _rowSize;
    private List<Column> _columns;
    private Bird _bird;

    private float _birdSpeed;
    private float _birdSize;

    private int _forwardCount;
    private int _updateCount;
    public int ForwardCount { get { return _forwardCount; } }

    private float _lastX;
    private int _score;
    public int Score { get { return _score; } }

    public Vector2 BirdPosition{ get { return _bird.Position; } }
    public bool BirdInJump { get { return _bird.InJump; } }

    public UnityEvent ModelForwarded;
    public UnityEvent GameOver;

    

    public GameModel(int columnSize = 7, int rowSize = 30, float birdSize = 2.5f, float birdJumpTimeSec = 0.5f, float birdSpeed = 0.1f, float birdJumpSpeed = 0.1f)
    {
        ModelForwarded = new UnityEvent();
        GameOver = new UnityEvent();

        _columnSize = columnSize;
        _rowSize = rowSize;
        _columns = new List<Column>();
        _bird = new Bird(birdSize, birdJumpTimeSec, birdSpeed, birdJumpSpeed);
        _birdSpeed = birdSpeed;
        _birdSize = birdSize;
        
        for(int i = 0; i < rowSize; i++)
        {
            if(i < 3 || i % 2 == 0)
                _columns.Add(new Column(_columnSize, false));
            else
                _columns.Add(new Column(_columnSize, true));
        }
    }

    public void UpdateModel()
    {
        _bird.Update();

        Vector2 forwardPosition = _bird.Position - new Vector2(_forwardCount, 0);
        Vector2 forwardPositionTop = _bird.Position - new Vector2(_forwardCount, - _birdSize / 2);
        Vector2 forwardPositionBottom = _bird.Position - new Vector2(_forwardCount, _birdSize / 2);
        Vector2 forwardPositionLeft = _bird.Position - new Vector2(_forwardCount - _birdSize / 2, 0);
        Vector2 forwardPositionRight = _bird.Position - new Vector2(_forwardCount + _birdSize / 2, 0);
        if (forwardPosition.y <= 0 || forwardPosition.y >= _columnSize ||
            PositionToTile(forwardPosition) == TileType.Pipe || PositionToTile(forwardPosition) == TileType.PipeUp || PositionToTile(forwardPosition) == TileType.PipeDown ||
            PositionToTile(forwardPositionTop) == TileType.Pipe || PositionToTile(forwardPositionTop) == TileType.PipeUp || PositionToTile(forwardPositionTop) == TileType.PipeDown ||
            PositionToTile(forwardPositionBottom) == TileType.Pipe || PositionToTile(forwardPositionBottom) == TileType.PipeUp || PositionToTile(forwardPositionBottom) == TileType.PipeDown ||
            PositionToTile(forwardPositionLeft) == TileType.Pipe || PositionToTile(forwardPositionLeft) == TileType.PipeUp || PositionToTile(forwardPositionLeft) == TileType.PipeDown ||
            PositionToTile(forwardPositionRight) == TileType.Pipe || PositionToTile(forwardPositionRight) == TileType.PipeUp || PositionToTile(forwardPositionRight) == TileType.PipeDown)
        {
            GameOver.Invoke();
        }
        else
        {
            if(Mathf.FloorToInt(_lastX) < Mathf.FloorToInt(BirdPosition.x) && _columns[Mathf.FloorToInt(_lastX - _forwardCount)].HasPipe)
            {
                _score++;
            }
        }

        _lastX = BirdPosition.x;

        _updateCount++;
        if (_updateCount >= 128000)
        {
            _updateCount = 64000;
        }


        if (_birdSpeed <= 1 && _updateCount >= (_rowSize/2)*Mathf.FloorToInt(1.0f / _birdSpeed) && _updateCount % Mathf.FloorToInt(2.0f / _birdSpeed) == 0)
            Forward();
    }

    public void Forward()
    {
        _columns.Add(new Column(_columnSize, false));
        _columns.Add(new Column(_columnSize, true));
        _columns.RemoveAt(0);
        _columns.RemoveAt(0);

        _forwardCount += 2;
        ModelForwarded.Invoke();
    }

    public void JumpBird()
    {
        _bird.Jump();
    }

    public TileType getTileType(Vector3Int index)
    {
        if (index.x < 0 || index.y >= _columnSize)
        {
            return TileType.Illegal;
        }

        return _columns[index.x].getTileType(index.y);
    }

    public TileType PositionToTile(Vector2 birdPosition)
    {
        return getTileType(new Vector3Int(Mathf.FloorToInt(birdPosition.x), Mathf.FloorToInt(birdPosition.y), 0));
    }
}
