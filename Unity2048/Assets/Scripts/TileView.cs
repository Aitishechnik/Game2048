using Game_2048;
using UnityEngine;
using UnityEngine.UI;


public class TileView : MonoBehaviour
{
    private Tile _tile = new Tile();

    public TileData ThisTileData { get; private set; }

    [SerializeField]
    private Image _image;

    public void SetValue(TileData tileData)
    {
        ThisTileData = tileData;
        _tile.SetValue(ThisTileData.Value);
        _image.color = ThisTileData.Color;
    }
}
