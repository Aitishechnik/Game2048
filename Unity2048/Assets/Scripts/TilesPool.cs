using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game_2048;

public class TilesPool
{
    private List<TileView> _tilesView = new List<TileView>();

    private TileView _tileViewPrefab;

    private Transform _objectsTransform;

    public TilesPool(int amount, TileView tileViewPrefab, Transform objectsTransform)
    {
        _tileViewPrefab = tileViewPrefab;
        _objectsTransform = objectsTransform;

        for (int i = 0; i < amount; i++)
        {
            var tileView = GameObject.Instantiate(_tileViewPrefab, _objectsTransform);
            _tilesView.Add(tileView);
            tileView.gameObject.SetActive(false);
        }        
    }

    public TileView GetTileView()
    {
        if (_tilesView.Count > 0)
        {
            var tileView = _tilesView[_tilesView.Count - 1];
            _tilesView.Remove(tileView);
            tileView.gameObject.SetActive(true);
            return tileView;
        }
        else
        {
            return Create();
        }
    }

    private TileView Create()
    {
        var newTileView = GameObject.Instantiate(_tileViewPrefab, _objectsTransform);
        _tilesView.Add(newTileView);
        return newTileView;
    }

    public void Return(TileView tileView)
    {
        _tilesView.Add(tileView);
        tileView.gameObject.SetActive(false);
    }
}
