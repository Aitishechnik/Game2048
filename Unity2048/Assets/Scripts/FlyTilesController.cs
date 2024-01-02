using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

public class FlyTilesController : MonoBehaviour
{
    [SerializeField]
    private TileFactory _tileFactory;

    private const float TILE_TRIANSITION_TIME = 0.15f;
    public float TileTransitionTime => TILE_TRIANSITION_TIME;

    public void Fly(TileView from, TileView to)
    {
        StartCoroutine(FlyRoutine(from, to, TILE_TRIANSITION_TIME));
    }

    private IEnumerator FlyRoutine(TileView from, TileView to, float TILE_TRIANSITION_TIME)
    {
        if (from == null)
            throw new System.Exception("From is null");
        if(to == null)
            throw new System.Exception("To is null");

        float elapsedTime = 0f;

        var tile = TileFactory.Instance.Create(from.ThisTileData, transform);

        from.transform.GetChild(1).gameObject.SetActive(false);

        tile.transform.GetChild(0).gameObject.SetActive(false);

        tile.SortingGroup.sortAtRoot = true;

        while (elapsedTime < TILE_TRIANSITION_TIME)
        {
            float t = elapsedTime / TILE_TRIANSITION_TIME;
            tile.transform.position = Vector3.Lerp(from.transform.position, to.transform.position, t);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        tile.SortingGroup.sortAtRoot = false;

        tile.transform.position = to.transform.position;

        tile.ReturnToPool();

        from.transform.GetChild(1).gameObject.SetActive(true);
        from.Sync();
        to.Sync();
        from.Tile.SetValue(from.ThisTileData.Value);
    }
}
