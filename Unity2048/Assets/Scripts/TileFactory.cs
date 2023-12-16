using System.Collections.Generic;
using UnityEngine;

public class TileFactory : MonoBehaviour
{
    public static TileFactory Instance { get; private set; }

    [SerializeField]
    private GameObject _parent;

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
    public void Create(int tileVale)
    {
        var tile = Instantiate(_tilePrefab, _parent.transform);
        tile.SetValue(_tileDatasDict[tileVale]);
    }
}
