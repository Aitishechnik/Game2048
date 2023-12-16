using Game_2048;
using System.Collections.Generic;
using UnityEngine;

public class FieldView : MonoBehaviour
{
    [SerializeField]
    private TilesConfig _config;

    [SerializeField]
    private Transform _gameFieldParent;

    private List<TileView> _tileViews = new List<TileView>();

    private Field _field;

    public void CreateView(Field field)
    {
        _field = field;

        Clear();

        foreach (var tile in field.GameField)
        {
            var tileView = TileFactory.Instance.Create(tile, _gameFieldParent);
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

    }
}
