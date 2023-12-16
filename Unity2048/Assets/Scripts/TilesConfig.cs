using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TilesConfig", menuName = "Configs/TilesConfig")]
public class TilesConfig : ScriptableObject
{
    [SerializeField]
    private List<TileData> _tiles;

    public List<TileData> TileDatas => _tiles;

    private Dictionary<int, TileData> _tileDatasDict = new Dictionary<int, TileData>();
    public TileData GetTileData(int tileValue)
    {
        if(_tileDatasDict.Count == 0)
        {
            foreach (var data in TileDatas)
            {
                _tileDatasDict.Add(data.Value, data);
            }
        }

        return _tileDatasDict[tileValue];
    }
}

[Serializable]
public class TileData
{
    [SerializeField]
    private Color _color;
    public Color Color => _color;

    [SerializeField]
    private int _value;
    public int Value => _value;
}
