using Game_2048;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using UnityEngine.Rendering;

public class TileView : MonoBehaviour
{
    [SerializeField]
    private Tile _tile;
    public Tile Tile => _tile;
    private TilesPool _tilesPool;

    [SerializeField]
    private TilesConfig _tilesConfig;

    [SerializeField]
    private TextMeshProUGUI _tileValueTMPro;

    public TileData ThisTileData { get; private set; }

    [SerializeField]
    private Image _image;

    [SerializeField]
    private SortingGroup _sortingGroup;
    public SortingGroup SortingGroup => _sortingGroup;

    public void SetTilesPool(TilesPool tilesPool)
    {
        _tilesPool = tilesPool;
    }

    public void NullifyTile()
    {
        _tile = null;
        ThisTileData = null;
    }

    public void SetValue(Tile tile)
    {
        _tile = tile;
        ThisTileData = _tilesConfig.GetTileData(_tile.Value);
        if (ThisTileData.Value > 0)
        {           
            _tile.SetValue(ThisTileData.Value, false);
            _image.color = ThisTileData.Color;
            _tileValueTMPro.text = ThisTileData.Value.ToString();
        }       
        _image.gameObject.SetActive(ThisTileData.Value > 0);
    }

    public void SetValue(TileData tileData)
    {
        if(tileData.Value > 0)
        {
            _image.color = tileData.Color;
            _tileValueTMPro.text = tileData.Value.ToString();
        }
        _image.gameObject.SetActive(tileData.Value > 0);
    }

    public void Sync()
    {
        SetValue(_tile);
    }

    public void ReturnToPool()
    {
        _tilesPool?.Return(this);        
    }
}
