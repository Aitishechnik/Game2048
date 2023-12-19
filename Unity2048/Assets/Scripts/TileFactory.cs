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

    [SerializeField]
    private Transform _tilesPatrent; // где лучше разместить трансформ field?

    private TilesPool _tilesPool;

    private void Awake()
    {
        _tilesPool = new TilesPool(TILES_VIEW_AMOUNT, _tilePrefab, _tilesPatrent);
        Instance = this;
    }
    public TileView Create(Tile tile)
    {
        var tileView = _tilesPool.GetTileView();
        tileView.SetValue(tile);
        tileView.SetTilesPool(_tilesPool);
        return tileView;
    }
}
