using Game_2048;
using UnityEngine;

public class TileFactory : MonoBehaviour
{
    public static TileFactory Instance { get; private set; }

    private const int TILES_VIEW_AMOUNT = 16;

    [SerializeField]
    private TileView _tilePrefab;

    [SerializeField]
    private TilesConfig _tileConfig;

    private TilesPool _tilesPool;

    private void Awake()
    {
        _tilesPool = new TilesPool(TILES_VIEW_AMOUNT, _tilePrefab, transform);
        Instance = this;
    }

    public TileView Create(Tile tile, Transform parent)
    {
        var tileView = _tilesPool.GetTileView();
        tileView.transform.SetParent(parent);
        tileView.SetValue(tile);
        tileView.SetTilesPool(_tilesPool);
        return tileView;
    }

    public TileView Create(TileData tileData, Transform parent)
    {
        var tileView = _tilesPool.GetTileView();
        tileView.NullifyTile();
        tileView.transform.SetParent(parent);
        tileView.SetValue(tileData);
        tileView.SetTilesPool(_tilesPool);
        return tileView;
    }
}
