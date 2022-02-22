using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kart : MonoBehaviour
{
    #region FIELDS
    [SerializeField]
    private float _forwardAcceleration = 16f;
    [SerializeField]
    private float _reverseAcceleration = 4f;
    [SerializeField]
    private float _turnTorque = 180f;
    [SerializeField]
    private float _gravityForce = 10f;
    [SerializeField]
    private float _dragGroundValue = 3f;
    [SerializeField]
    private LayerMask _whatIsGround;
    [SerializeField]
    private Transform _groundRayPoint;
    [SerializeField]
    private float _groundRayLength = .5f;
    [SerializeField]
    private Transform _leftFrontWheel;
    [SerializeField]
    private Transform _rightFrontWheel;
    [SerializeField]
    private Transform _rearWheels;
    [SerializeField]
    private float _maxWheelTurn = 25f;
    [SerializeField]
    private float _wheelRotationSpeed = 2f;

    private bool _grounded;
    private float _speedInput;
    private float _turnInput;

    private Rigidbody _rb;

    public ParticleSystem[] _dustTrail;
    public float _maxEmission = 25f;
    private float _emissionRate;

    #endregion

    public void Awake()
    {
        _rb = GetComponentInChildren<Rigidbody>();

        _rb.transform.parent = null;
    }

    public void FixedUpdate()
    {
        _grounded = false;
        RaycastHit hit;

        if(Physics.Raycast(_groundRayPoint.position, -transform.up, out hit, _groundRayLength, _whatIsGround))
        {
            _grounded = true;

            transform.rotation = Quaternion.FromToRotation(transform.up, hit.normal) * transform.rotation;
        }

        _emissionRate = 0f;

        if(_grounded)
        {
            _rb.drag = _dragGroundValue;

            if(Mathf.Abs(_speedInput) > 0)
            {
                _rb.AddForce(transform.forward * -_speedInput);

                _emissionRate = _maxEmission;
            }

        }
        else
        {
            _rb.drag = 0.1f;

            _rb.AddForce(Vector3.up * -_gravityForce * 100f);
        }

        foreach(ParticleSystem part in _dustTrail)
        {
            var emissionModule = part.emission;
            emissionModule.rateOverTime = _emissionRate;
        }

    }

    public void Update()
    {
        _speedInput = 0f;

        if(Input.GetAxis("Vertical") > 0) // forwards
        {
            _speedInput = Input.GetAxis("Vertical") * _forwardAcceleration * 1000f;

            _leftFrontWheel.transform.Rotate(Vector3.forward, _wheelRotationSpeed); 
            _rightFrontWheel.transform.Rotate(Vector3.forward, _wheelRotationSpeed); 
            _rearWheels.transform.Rotate(Vector3.forward, _wheelRotationSpeed); 

        }
        else if(Input.GetAxis("Vertical") < 0) // reverse
        {
            _speedInput = Input.GetAxis("Vertical") * _reverseAcceleration * 1000f;

            _leftFrontWheel.transform.Rotate(Vector3.forward, -_wheelRotationSpeed);
            _rightFrontWheel.transform.Rotate(Vector3.forward, -_wheelRotationSpeed);
            _rearWheels.transform.Rotate(Vector3.forward, -_wheelRotationSpeed);
        }

        _turnInput = Input.GetAxis("Horizontal");

        if(_grounded)
        {
            transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles + new Vector3(0f, _turnInput * _turnTorque * Time.deltaTime * Input.GetAxis("Vertical"), 0f));
        }

        _leftFrontWheel.localRotation = Quaternion.Euler(_turnInput * -_maxWheelTurn, _leftFrontWheel.localRotation.eulerAngles.y, _leftFrontWheel.localRotation.eulerAngles.z);
        _rightFrontWheel.localRotation = Quaternion.Euler(_turnInput * -_maxWheelTurn, _rightFrontWheel.localRotation.eulerAngles.y, _rightFrontWheel.localRotation.eulerAngles.z);

        transform.position = _rb.transform.position;
    }
}
