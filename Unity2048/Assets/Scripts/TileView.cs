using Game_2048;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

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

    public void SetTilesPool(TilesPool tilesPool)
    {
        _tilesPool = tilesPool;
    }

    public void SetAnimationCoordinates(int[,] coords)
    {
        if (ThisTileData != null && ThisTileData.Value != _tile.Value)
        {
            coords[_tile.Y, _tile.X] = ThisTileData.Value > _tile.Value ? -1 : 1;
        }
    }

    public void SetValue(Tile tile)
    {
        if (ThisTileData != null && ThisTileData.Value != tile.Value)
        {
            //написать логику сбора инфы о каждом тайле с передачей в fieldView через event или новое поле в fieldView
            //попробовать добавить координаты в TileView для определения перехода каждого тайла 
        }
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

    public void Sync()
    {
        SetValue(_tile);
    }

    public void ReturnToPool()
    {
        _tile.SetValue(0);
        _tilesPool.Return(this);
        _tile = null;
    }
}
