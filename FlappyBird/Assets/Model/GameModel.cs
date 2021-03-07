using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameModel
{
    private int _columnSize;
    private List<Column> _columns;
    private Bird _bird;

    private long _forwardCount;
    public long ForwardCount { get { return _forwardCount; } }

    public GameModel(int columnSize = 7, int rowSize = 30, float birdSize = 2.5f, float birdJumpTimeSec = 0.5f, float birdSpeed = 0.1f, float birdJumpSpeed = 0.1f)
    {
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
    }

    public void Forward()
    {
        _forwardCount += 2;

        _columns.Add(new Column(_columnSize, false));
        _columns.Add(new Column(_columnSize, true));
        _columns.RemoveAt(0);
        _columns.RemoveAt(0);
    }

    public TileType getTileType(Vector3Int index)
    {
        if (index.x - _forwardCount < 0 || index.y >= _columnSize)
        {
            Debug.Log("oof");
            return TileType.Illegal;
        }

        Debug.Log(index.x + " " + index.y + " " + _columns[index.y].getTileType(index.x));
        return _columns[index.x].getTileType(index.y);
    }
}
