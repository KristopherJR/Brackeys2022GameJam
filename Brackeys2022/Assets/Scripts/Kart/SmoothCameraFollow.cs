using UnityEngine;

public class SmoothCameraFollow : MonoBehaviour
{
    private Vector3 _cameraOffset;
    [SerializeField] private Transform _target;
    [SerializeField] private float _smoothTime;
    private Vector3 _currentVelocity = Vector3.zero;

    private void Awake()
    {
        _cameraOffset = transform.position - _target.position;
    }

    private void LateUpdate()
    {
        Vector3 targetPosition = _target.position + _cameraOffset;
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref _currentVelocity, _smoothTime);
    }

}
