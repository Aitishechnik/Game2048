using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GridChilderResizer : MonoBehaviour
{
    [SerializeField]
    private GridLayoutGroup _grid;

    [SerializeField]
    private RectTransform _gridTransform;

    public float Ratio = 4.46f;
    public float GridSize;

    // Start is called before the first frame update
    void Start()
    {
        GridSize = _gridTransform.rect.size.magnitude;
    }

    // Update is called once per frame
    void Update()
    {
        ShiftCellsSize();
    }

    private void ShiftCellsSize()
    {
        float width = _gridTransform.rect.width;
        Vector2 newSize = new Vector2(width / Ratio, width / Ratio);
        _grid.cellSize = newSize;
    }
}
