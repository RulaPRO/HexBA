using UnityEngine;
using UnityEngine.UI;

public class HexGrid : MonoBehaviour
{
    public int Width = 6;
    public int Height = 6;
    
    public Color DefaultColor = Color.white;
    public Color TouchedColor = Color.magenta;

    public HexCell CellPrefab;
    public Text HexCellLabelPrefab;

    private HexCell[] _cells;
    private Canvas _gridCanvas;
    private HexMesh _hexMesh;

    private void Awake()
    {
        _gridCanvas = GetComponentInChildren<Canvas>();
        _hexMesh = GetComponentInChildren<HexMesh>();
        
        _cells = new HexCell[Height * Width];

        for (int z = 0, i = 0; z < Height; z++) {
            for (int x = 0; x < Width; x++) {
                CreateCell(x, z, i++);
            }
        }
    }

    private void Start()
    {
        _hexMesh.Triangulate(_cells);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            HandleInput();
        }
    }

    private void HandleInput()
    {
        Ray inputRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(inputRay, out hit))
        {
            TouchCell(hit.point);
        }
    }

    private void TouchCell(Vector3 position)
    {
        position = transform.InverseTransformPoint(position);
        HexCoordinates coordinates = HexCoordinates.FromPosition(position);
        int index = coordinates.X + coordinates.Z * Width + coordinates.Z / 2;
        HexCell hexCell = _cells[index];
        hexCell.Color = TouchedColor;
        _hexMesh.Triangulate(_cells);
        
        Debug.Log("Touched at " + coordinates);
    }

    private void CreateCell(int x, int z, int i)
    {
        Vector3 position;
        position.x = (x + z * 0.5f - z / 2) * (HexMetrics.InnerRadius * 2f);
        position.y = 0f;
        position.z = z * (HexMetrics.OuterRadius * 1.5f);

        HexCell hexCell = _cells[i] = Instantiate(CellPrefab);
        hexCell.transform.SetParent(transform, false);
        hexCell.transform.localPosition = position;
        hexCell.HexCoordinates = HexCoordinates.FromOffsetCoordinates(x, z);
        hexCell.Color = DefaultColor;
        
        Text labelText = Instantiate(HexCellLabelPrefab);
        labelText.rectTransform.SetParent(_gridCanvas.transform, false);
        labelText.rectTransform.anchoredPosition = new Vector2(position.x, position.z);
        labelText.text = hexCell.HexCoordinates.ToStringOnSeparateLines();
    }
}
