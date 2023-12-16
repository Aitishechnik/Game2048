using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TilesConfig", menuName = "Configs/TilesConfig")]
public class TilesConfig : ScriptableObject
{
    [SerializeField]
    private List<TileData> _tiles;

    public List<TileData> Tiles => _tiles;
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
