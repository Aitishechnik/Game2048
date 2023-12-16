using Game_2048;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class TileView : MonoBehaviour
{
    private Tile _tile;

    [SerializeField]
    private TilesConfig _tilesConfig;

    [SerializeField]
    private TextMeshProUGUI _tileValueTMPro;

    public TileData ThisTileData { get; private set; }

    [SerializeField]
    private Image _image;

    public void SetValue(Tile tile)
    {
        _tile = tile;
        ThisTileData = _tilesConfig.GetTileData(_tile.Value);
        if (tile.Value > 0)
        {           
            _tile.SetValue(ThisTileData.Value);
            _image.color = ThisTileData.Color;
            _tileValueTMPro.text = ThisTileData.Value.ToString();
        }       
        _image.gameObject.SetActive(ThisTileData.Value > 0);
    }

    public void Sync()
    {
        SetValue(_tile);
    }
}
