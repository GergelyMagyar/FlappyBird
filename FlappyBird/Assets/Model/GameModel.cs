using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameModel
{
    private int _columnSize;
    private List<Column> _columns;
    public GameModel(int columnSize, int rowSize, int cellSize = 10)
    {
        _columnSize = columnSize;
        _columns = new List<Column>();
        
        for(int i = 0; i < rowSize; i++)
        {
            _columns.Add(new Column(_columnSize));
        }
    }

    public void Forward()
    {
        _columns.Add(new Column(_columnSize));
        _columns.RemoveAt(0);
    }
}
