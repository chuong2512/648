using UnityEngine;

[ExecuteInEditMode]
public class CameraAspect : MonoBehaviour
{
    [SerializeField] Camera _mainCam;
    [SerializeField] float _screenHalfInWorldUnits = 7f;
    [SerializeField] float _minOrthographicSize = 14f;

    private void Start()
    {
        SetupCameraSize();
    }

    private void Update()
    {
        SetupCameraSize();
    }

    void SetupCameraSize()
    {
        _mainCam.orthographicSize = _screenHalfInWorldUnits / _mainCam.aspect;

        if (_mainCam.orthographicSize < _minOrthographicSize)
            _mainCam.orthographicSize = _minOrthographicSize;
    }
}
