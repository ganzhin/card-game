using System.Collections.Generic;
using UnityEngine;

public class CameraControls : MonoBehaviour
{
    [SerializeField] private float _pitchMinimum, _pitchMaximum;
    [SerializeField] private float _yawMinimum, _yawMaximum;

    private float _pitch;
    private float _yaw;

    [SerializeField] private float _sensetivity;
    [SerializeField] private float _speed;
    [SerializeField] private float _zoomSpeed;

    [SerializeField] private float _zoomFov = 45;
    [SerializeField] private float _doubleZoomFov = 35;
    [SerializeField] private float _defaultFov = 60;

    [SerializeField] private List<Collider> _zoomObjects;

    private float _angle = 50;

    void Update()
    {
        if (Input.GetMouseButton(1))
        {
            _pitch -= Input.GetAxis("Mouse Y") * _sensetivity;
            _yaw += Input.GetAxis("Mouse X") * _sensetivity;

            _pitch = Mathf.Clamp(_pitch, _pitchMinimum, _pitchMaximum);
            _yaw = Mathf.Clamp(_yaw, _yawMinimum, _yawMaximum);
            transform.localRotation = Quaternion.Lerp(transform.localRotation, Quaternion.Euler(_pitch, _yaw, 0), Time.fixedDeltaTime * _speed);

            RaycastHit hit;
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit))
            {
                if (_zoomObjects.Contains(hit.collider))
                {
                    Camera.main.fieldOfView = Mathf.Lerp(Camera.main.fieldOfView, _doubleZoomFov, Time.deltaTime * _zoomSpeed/2f);
                    return;
                }
            }
        }
        else
        {
            _angle = Board.PlayerTurn ? 50 : 43;

            transform.localRotation = Quaternion.Lerp(transform.localRotation, Quaternion.Euler(_angle, 0, 0), Time.fixedDeltaTime * _speed);
            _pitch = Mathf.Lerp(_pitch, 50, Time.deltaTime);
            _yaw = Mathf.Lerp(_yaw, 0, Time.deltaTime);
        }

        Camera.main.fieldOfView = Mathf.Lerp(Camera.main.fieldOfView, (Input.GetMouseButton(1) || !Board.PlayerTurn) ? _zoomFov : _defaultFov, Time.deltaTime * _zoomSpeed);
    }
}