using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TileType
{
    Sky, Pipe, PipeUp, PipeDown, Illegal
}

public class Column
{
    private int _size;
    private TileType[] _column;

    private bool _hasPipe;
    public bool HasPipe { get { return _hasPipe; } }

    public Column(int size = 7, bool hasPipe = true)
    {
        _size = size;
        _column = new TileType[_size];

        _hasPipe = hasPipe;

        if(_hasPipe)
        {
            int height = Random.Range(1, _size / 2 + 2);

            for (int i = 0; i < height - 1; i++)
            {
                _column[i] = TileType.Pipe;
            }
            _column[height - 1] = TileType.PipeUp;
            _column[height] = TileType.Sky;
            _column[height + 1] = TileType.Sky;
            _column[height + 2] = TileType.PipeDown;
            for (int i = height + 3; i < _size; i++)
            {
                _column[i] = TileType.Pipe;
            }
        }
        else
        {
            for(int i = 0; i < _size; i++)
            {
                _column[i] = TileType.Sky;
            }
        }
        
    }

    public TileType getTileType(int index)
    {
        if (index < 0 || index >= _size)
        {
            Debug.Log("column getTileType: illegal tile, " + index);
            return TileType.Illegal;
        }
            

        return _column[index];
    }
}