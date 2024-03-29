using Game_2048;
using System;
using System.Collections.Generic;
using UnityEngine;

public class FieldView : MonoBehaviour
{
    public int BiggestTilesValue { get; private set; }

    [SerializeField]
    private TilesConfig _config;

    private List<TileView> _tileViews = new List<TileView>();

    private Dictionary<Tile, TileView> _tileViewDict = new Dictionary<Tile, TileView>();

    public TileView GetTileView(Tile tile)
    {
        if(_tileViewDict.TryGetValue(tile, out var view))
        {
            return view;
        }

        return null;
    }
    public void CreateView(Field field)
    {
        foreach(var data in _config.TileDatas)
        {
            if(data.Value > BiggestTilesValue)
                BiggestTilesValue = data.Value;
        }

        TileFactory.Instance.InitializePool(field.GameField.GetLength(0));
        Clear();

        foreach (var tile in field.Tiles)
        {
            var tileView = TileFactory.Instance.Create(tile, transform);
            _tileViews.Add(tileView);
            _tileViewDict.Add(tile, tileView);
        }
    }

    public void SyncField()
    {       
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
        _tileViewDict.Clear();
    }
}
