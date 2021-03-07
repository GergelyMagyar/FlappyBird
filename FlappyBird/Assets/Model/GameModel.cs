using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class GameModel
{
    private int _columnSize;
    private List<Column> _columns;
    private Bird _bird;

    private long _forwardCount;
    private long _updateCount;
    public long ForwardCount { get { return _forwardCount; } }

    Event ModelForwarded;
    Event ModelBackToZero;

    public Vector2 BirdPosition { get { return _bird.Position; } }

    public GameModel(int columnSize = 7, int rowSize = 30, float birdSize = 2.5f, float birdJumpTimeSec = 0.5f, float birdSpeed = 0.1f, float birdJumpSpeed = 0.1f)
    {
        ModelForwarded = new Event();
        ModelBackToZero = new Event();

        _columnSize = columnSize;
        _columns = new List<Column>();
        _bird = new Bird(birdSize, birdJumpTimeSec, birdSpeed, birdJumpSpeed);
        
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

        //check for collission


        _updateCount++;
        if (_updateCount >= 32000)
            _updateCount = 0;
    }

    public void Forward()
    {
        _columns.Add(new Column(_columnSize, false));
        _columns.Add(new Column(_columnSize, true));
        _columns.RemoveAt(0);
        _columns.RemoveAt(0);

        _forwardCount += 2;
        if (_forwardCount >= 100)
        {
            _forwardCount = 0;
        }
    }

    public void JumpBird()
    {
        _bird.Jump();
    }

    public TileType getTileType(Vector3Int index)
    {
        if (index.x - _forwardCount < 0 || index.y >= _columnSize)
            return TileType.Illegal;

        return _columns[index.x].getTileType(index.y);
    }
}
