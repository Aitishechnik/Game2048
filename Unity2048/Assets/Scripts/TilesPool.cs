using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game_2048;
using System;

public class TilesPool
{
    private List<TileView> _tilesView = new List<TileView>();
    public int TilesAmount => _tilesView.Count;

    private TileView _tileViewPrefab;

    private Transform _objectsTransform;

    public TilesPool(int amount, TileView tileViewPrefab, Transform objectsTransform)
    {
        _tileViewPrefab = tileViewPrefab;
        _objectsTransform = objectsTransform;

        for (int i = 0; i < amount; i++)
        {
            Create();
        }        
    }

    public TileView GetTileView()
    {
        if(_tilesView.Count == 0)
            Create();

        var tileView = _tilesView[_tilesView.Count - 1];
        _tilesView.Remove(tileView);
        tileView.gameObject.SetActive(true);
        return tileView;
    }

    private TileView Create()
    {
        var newTileView = GameObject.Instantiate(_tileViewPrefab, _objectsTransform);
        _tilesView.Add(newTileView);
        newTileView.gameObject.SetActive(false);
        return newTileView;
    }

    public void Return(TileView tileView)
    {
        tileView.transform.SetParent(_objectsTransform);
        _tilesView.Add(tileView);
        tileView.gameObject.SetActive(false);
    }
}
