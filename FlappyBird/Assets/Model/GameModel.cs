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

    private long _forwardCount;
    private long _updateCount;
    public long ForwardCount { get { return _forwardCount; } }

    public UnityEvent ModelForwarded;
    public UnityEvent ModelBackToZero;
    public UnityEvent GameOver;

    public Vector2 BirdPosition { get { return _bird.Position; } }

    public GameModel(int columnSize = 7, int rowSize = 30, float birdSize = 2.5f, float birdJumpTimeSec = 0.5f, float birdSpeed = 0.1f, float birdJumpSpeed = 0.1f)
    {
        ModelForwarded = new UnityEvent();
        ModelBackToZero = new UnityEvent();
        GameOver = new UnityEvent();

        _columnSize = columnSize;
        _rowSize = rowSize;
        _columns = new List<Column>();
        _bird = new Bird(birdSize, birdJumpTimeSec, birdSpeed, birdJumpSpeed);
        _birdSpeed = birdSpeed;
        
        for(int i = 0; i < rowSize; i++)
        {
            if(i % 2 == 0)
                _columns.Add(new Column(_columnSize, false));
            else
                _columns.Add(new Column(_columnSize, true));
        }
    }

    public void UpdateModel()
    {
        _bird.Update();

        Vector2 forwardPosition = _bird.Position - new Vector2(_forwardCount, 0);
        if(forwardPosition.y <= 0 || forwardPosition.y >= _columnSize ||
            PositionToTile(forwardPosition) == TileType.Pipe || PositionToTile(forwardPosition) == TileType.PipeUp || PositionToTile(forwardPosition) == TileType.PipeDown)
        {
            GameOver.Invoke();
        }
            

        _updateCount++;
        if (_updateCount >= 32000)
        {
            //_updateCount = 0;
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
        if (_forwardCount >= 100)
        {
            //_forwardCount = 0;
            //ModelBackToZero.Invoke();
        }
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
