using Game_2048;
using System.Collections.Generic;
using UnityEngine;

public class FieldView : MonoBehaviour
{
    [SerializeField]
    private TilesConfig _config;

    private List<TileView> _tileViews = new List<TileView>();

    public void CreateView(Field field)
    {
        Clear();

        foreach (var tile in field.Tiles)
        {
            var tileView = TileFactory.Instance.Create(tile);
            _tileViews.Add(tileView);
        }
    }

    public void SyncField()
    {
        foreach(var tile in _tileViews)
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
