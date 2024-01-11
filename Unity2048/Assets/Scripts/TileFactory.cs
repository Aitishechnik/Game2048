using Game_2048;
using UnityEngine;

public class TileFactory : MonoBehaviour
{
    public static TileFactory Instance { get; private set; }

    [SerializeField]
    private TileView _tilePrefab;

    [SerializeField]
    private TilesConfig _tileConfig;

    private TilesPool _tilesPool;

    private void Awake()
    {
        Instance = this;
    }

    public void InitializePool(int boardLength)
    {
        if(_tilesPool == null || (_tilesPool?.TilesAmount != boardLength * boardLength))
        {
            _tilesPool = new TilesPool(boardLength * boardLength, _tilePrefab, transform);
        }
    }

    public TileView Create(Tile tile, Transform parent)
    {
        var tileView = _tilesPool.GetTileView();
        tileView.transform.SetParent(parent);
        tileView.SetValue(tile);
        tileView.transform.localScale = Vector3.one;
        tileView.SetTilesPool(_tilesPool);
        return tileView;
    }

    public TileView Create(TileData tileData, Transform parent)
    {
        var tileView = _tilesPool.GetTileView();
        tileView.NullifyTile();
        tileView.transform.SetParent(parent);
        tileView.SetValue(tileData);
        tileView.transform.localScale = Vector3.one;
        tileView.SetTilesPool(_tilesPool);
        return tileView;
    }
}
