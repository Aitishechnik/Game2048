using System.Collections.Generic;
using UnityEngine;

public class TileFactory : MonoBehaviour
{
    public static TileFactory Instance { get; private set; }

    [SerializeField]
    private TileView _tilePrefab;

    [SerializeField]
    private TilesConfig _tileConfig;

    private Dictionary<int, TileData> _tileDatasDict = new Dictionary<int, TileData>();

    private void Start()
    {
        foreach(var data in _tileConfig.Tiles)
        {
            _tileDatasDict.Add(data.Value, data);
        }
        Instance = this;
    }
    public TileView Create(int tileVale, Transform parent)
    {
        var tile = Instantiate(_tilePrefab, parent);
        tile.SetValue(_tileDatasDict[tileVale]);
        return tile;
    }
}
