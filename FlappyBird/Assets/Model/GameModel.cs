using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameModel
{
    private int _columnSize;
    private List<Column> _columns;
    private Bird _bird;

    public GameModel(int columnSize, int rowSize, int cellSize = 10)
    {
        _columnSize = columnSize;
        _columns = new List<Column>();
        _bird = new Bird();
        
        for(int i = 0; i < rowSize; i++)
        {
            _columns.Add(new Column(_columnSize));
        }
    }

    public void Update()
    {
        _bird.Update();

        //check for collission
    }

    public void Forward()
    {
        _columns.Add(new Column(_columnSize));
        _columns.RemoveAt(0);
    }
}
