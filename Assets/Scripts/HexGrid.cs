using UnityEngine;
using UnityEngine.UI;

public class HexGrid : MonoBehaviour
{
    public int Width = 6;
    public int Height = 6;

    public HexCell CellPrefab;
    public Text HexCellLabelPrefab;

    private HexCell[] _cells;
    private Canvas _gridCanvas;

    void Awake()
    {
        _gridCanvas = GetComponentInChildren<Canvas>();
        
        _cells = new HexCell[Height * Width];

        for (int z = 0, i = 0; z < Height; z++) {
            for (int x = 0; x < Width; x++) {
                CreateCell(x, z, i++);
            }
        }
    }
	
    void CreateCell(int x, int z, int i)
    {
        Vector3 position;
        position.x = x * 10f;
        position.y = 0f;
        position.z = z * 10f;

        HexCell hexCell = _cells[i] = Instantiate(CellPrefab);
        hexCell.transform.SetParent(transform, false);
        hexCell.transform.localPosition = position;
        
        Text labelText = Instantiate(HexCellLabelPrefab);
        labelText.rectTransform.SetParent(_gridCanvas.transform, false);
        labelText.rectTransform.anchoredPosition = new Vector2(position.x, position.z);
        labelText.text = x + "\n" + z;
    }
}
