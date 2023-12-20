using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyTilesController : MonoBehaviour
{
    [SerializeField]
    private TileFactory _tileFactory;
    public void Fly(TileView from, TileView to, float time)
    {
        StartCoroutine(FlyRoutine(from, to, time));
    }

    private IEnumerator FlyRoutine(TileView from, TileView to, float time)
    {
        float startTime = Time.time;
        var tile = _tileFactory.Create(from.Tile, transform);
        tile.transform.position = from.transform.position;
        while(Time.time < startTime + time)
        {
            Vector3.Lerp(from.transform.position, to.transform.position, (startTime + time - Time.time)/time);
            yield return null;
        }
        tile.transform.position = to.transform.position;
    }
}
