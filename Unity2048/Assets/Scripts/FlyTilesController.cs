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
        if (from == null)
            throw new System.Exception("From is null");
        if(to == null)
            throw new System.Exception("To is null");

        float elapsedTime = 0f;

        var tile = TileFactory.Instance.Create(from.ThisTileData, transform);

        //from.Sync();
        from.transform.GetChild(1).gameObject.SetActive(false);

        tile.transform.GetChild(0).gameObject.SetActive(false);

        var sorting = tile.GetComponent<SortingGroup>();
        if(sorting == null)
            sorting = tile.AddComponent<SortingGroup>();
        sorting.sortAtRoot = true;

        while (elapsedTime < time)
        {
            float t = elapsedTime / time;
            tile.transform.position = Vector3.Lerp(from.transform.position, to.transform.position, t);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        sorting.sortAtRoot = false;

        tile.transform.position = to.transform.position;

        tile.ReturnToPool();

        from.transform.GetChild(1).gameObject.SetActive(true);
        from.Sync();
        to.Sync();
        from.Tile.SetValue(from.ThisTileData.Value);
    }
}
