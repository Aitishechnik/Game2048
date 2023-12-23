using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

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
        float elapsedTime = 0f;

        var tile = TileFactory.Instance.Create(from.Tile, transform);
        tile.transform.GetChild(0).gameObject.SetActive(false);
        var sorting = tile.AddComponent<SortingGroup>();
        sorting.sortAtRoot = true;

        while (elapsedTime < time)
        {
            float t = elapsedTime / time;
            tile.transform.position = Vector3.Lerp(from.transform.position, to.transform.position, t);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        sorting.sortAtRoot = false;
        Destroy(sorting);

        tile.transform.position = to.transform.position;

        tile.ReturnToPool();
        from.Tile.SetValue(from.ThisTileData.Value);
    }
}
