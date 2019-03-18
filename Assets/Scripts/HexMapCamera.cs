using UnityEditor.Experimental.UIElements.GraphView;
using UnityEngine;

public class HexMapCamera : MonoBehaviour
{
    private Transform _swivel; // угол под которым камера смотрит на карту
    private Transform _stick;  // расстояние на котором находится камера

    public float StickMinZoom;
    public float StickMaxZoom;

    public float SwivelMinZoom;
    public float SwivelMaxZoom;
    
    private float _zoom = 1.0f;
    
    private void Awake()
    {
        _swivel = transform.GetChild(0);
        _stick = _swivel.GetChild(0);
    }

    private void Update()
    {
        float zoomDelta = Input.GetAxis("Mouse ScrollWheel");
        if (zoomDelta != 0.0f)
        {
            AdjustZoom(zoomDelta);
        }
    }

    private void AdjustZoom(float delta)
    {
        _zoom = Mathf.Clamp01(_zoom + delta);

        // отдаление/приближение камеры
        float distance = Mathf.Lerp(StickMinZoom, StickMaxZoom, _zoom);
        _stick.localPosition = new Vector3(0.0f, 0.0f, distance);
        
        // изменение угла камеры
        float angle = Mathf.Lerp(SwivelMinZoom, SwivelMaxZoom, _zoom);
        _swivel.localRotation = Quaternion.Euler(angle, 0.0f, 0.0f);
    }
}
