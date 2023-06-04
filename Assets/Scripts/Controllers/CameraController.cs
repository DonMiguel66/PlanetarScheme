using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : BaseController
{
    private Camera _camera;
    private Camera_Controlls _cameraActions;
    private InputAction _inputAction;

    //private float _zoomInput = 1;
    private Vector3 _previousPos;
    private Transform _targetTransform;
    
    private float _minZoom = 1f; 
    private float _maxZoom = 5f;
     
    //private float _zoomSpeed = 2f;
    private float _xSpeed = 120.0f;
    private float _ySpeed = 120.0f;

    private float _yMinLimit = -90f;
    private float _yMaxLimit = 90f;

    private float _distance = 1.2f;
    private float x = 0.0f;

    private float y = 0.0f;


    public event Action OnCameraMoveStart;
    
    public CameraController(Camera camera, Transform pivot)
    {
        _camera = camera;
        _cameraActions = new Camera_Controlls();
        _targetTransform = pivot;
        InitCamRotation();
        _cameraActions.Player.RotateCamera.performed += RotateCamera;
        //_cameraActions.Player.Zoom.performed += ZoomCamera;
        _cameraActions.Player.Enable();
    }

    private void InitCamRotation()
    {
        if (_targetTransform != null)
        {
            Vector3 angles = _camera.transform.eulerAngles;
            x = angles.y;
            y = angles.x;
            _distance = 1f;
            ApplyCamPosRotChanges();
        }
    }

    public void SetPivot(Transform newPivot, bool isMain)
    {
        _distance = isMain ? 1.2f : 0.65f;
        _targetTransform = newPivot;
        ClampToPos();
    }

    private void ClampToPos()
    {
        Quaternion rotation = Quaternion.Euler(y, x, 0);
        Vector3 position = rotation * new Vector3(0.0f, 0.0f, -_distance) +_targetTransform.position;
        _camera.transform.DOMove(position, 0.5f);
        _camera.transform.rotation = rotation;
        /*_camera.transform.position = Vector3.Lerp(_targetTransform.position, newPivot.position, 0.05f);
        _targetTransform = newPivot;*/
    }
    
    private float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360f)
            angle += 360f;
        if (angle > 360f)
            angle -= 360f;
        return Mathf.Clamp(angle, min, max);
    }
    private void RotateCamera(InputAction.CallbackContext context)
    {
        if(!Mouse.current.leftButton.isPressed)
            return;
        var rotationInput = context.ReadValue<Vector2>();
        if (_targetTransform != null)
        {
            float mouseX = rotationInput.x;
            float mouseY = rotationInput.y;

            x += mouseX * _xSpeed * Time.deltaTime;
            y -= mouseY * _ySpeed * Time.deltaTime;
            y = ClampAngle(y, _yMinLimit, _yMaxLimit);

            ApplyCamPosRotChanges();
        }
    }

    private void ZoomCamera(InputAction.CallbackContext context)
    {
        var scroll = context.ReadValue<float>();
        _distance -= (scroll/120);
        if (_distance < _minZoom)
            _distance = _minZoom;
        else if (_distance > _maxZoom)
            _distance = _maxZoom;
        _distance = Mathf.Clamp(_distance, _minZoom, _maxZoom);
        ApplyCamPosRotChanges();
    }

    private void ApplyCamPosRotChanges()
    {
        Quaternion rotation = Quaternion.Euler(y, x, 0);
        Vector3 position = rotation * new Vector3(0.0f, 0.0f, -_distance) +_targetTransform.position;

        _camera.transform.rotation = rotation;
        _camera.transform.position = position;
    }
}
