using Game_2048;
using UnityEngine;

public class TileFactory : MonoBehaviour
{
    public static TileFactory Instance { get; private set; }

    [SerializeField]
    private TileView _tilePrefab;

    [SerializeField]
    private TilesConfig _tileConfig;

    

    private void Awake()
    {
        
        Instance = this;
    }
    public TileView Create(Tile tile, Transform parent)
    {
        var tileView = Instantiate(_tilePrefab, parent);
        tileView.SetValue(tile);
        return tileView;
    }
}
