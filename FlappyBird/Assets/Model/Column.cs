using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TileType
{
    Sky, Pipe, PipeUp, PipeDown
}

public class Column
{
    private TileType[] _column;

    public Column(int size = 7)
    {
        _column = new TileType[size];

        int height = Random.Range(1, size / 2 + 1);

        for (int i = 0; i < height - 1; i++)
        {
            _column[i] = TileType.Pipe;
        }
        _column[height - 1] = TileType.PipeUp;
        _column[height] = TileType.Sky;
        _column[height + 1] = TileType.Sky;
        for (int i = height + 2; i < size; i++)
        {
            _column[i] = TileType.Pipe;
        }
    }
}