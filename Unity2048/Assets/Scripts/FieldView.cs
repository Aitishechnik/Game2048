using Game_2048;
using System;
using System.Collections.Generic;
using UnityEngine;

public class FieldView : MonoBehaviour
{
    [SerializeField]
    private TilesConfig _config;

    private List<TileView> _tileViews = new List<TileView>();

    private int[,] _fromToCoords;
    public void CreateView(Field field)
    {
        Clear();

        _fromToCoords = new int[field.GameField.GetLength(0), field.GameField.GetLength(1)];

        foreach (var tile in field.Tiles)
        {
            var tileView = TileFactory.Instance.Create(tile, transform);
            _tileViews.Add(tileView);
        }
    }

    public void SyncField()
    {
        //=============================
        foreach(var tile in _tileViews)
        {
            tile.SetAnimationCoordinates(_fromToCoords);
        }
        var temp = string.Empty;
        for(int i = 0; i < 4; i++)
        {
            temp = "\n";
            for (int j = 0; j < 4; j++)
            {
                temp += $"{_fromToCoords[i,j]} ";
            }
            Debug.Log(temp);
        }
        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                _fromToCoords[i, j] = 0;
            }
        }
        //=============================

        foreach (var tile in _tileViews)
        {
            tile.Sync();
        }
    }

    private void Clear()
    {
        foreach(var tileView in _tileViews)
        {
            tileView.ReturnToPool();
        }
        _tileViews.Clear();
    }
}
