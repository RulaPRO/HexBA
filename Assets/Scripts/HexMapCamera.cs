using UnityEngine;

public class HexMapCamera : MonoBehaviour
{
    private Transform _swivel;
    private Transform _stick;
    
    private void Awake()
    {
        _swivel = transform.GetChild(0);
        _stick = _swivel.GetChild(0);
    }
}
