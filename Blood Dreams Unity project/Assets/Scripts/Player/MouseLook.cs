using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public enum RotationAxes
    {
        XandY,
        X,
        Y,
        Y_Gun
    }

    public RotationAxes _axes = RotationAxes.XandY;
    public float _rotationSpeedHor = 5.0f;
    public float _rotationSpeedVer = 5.0f;

    public float maxVert = 45.0f;
    public float minVert = -45.0f;

    public float maxVertGun = 45.0f;
    public float minVertGun = -45.0f;

    public float _rotationX = 0;
    public float _GunrotationX = 0;



    public void Start()
    {
        Rigidbody body = GetComponent<Rigidbody>();
        if (body != null)
            body.freezeRotation = true;
    }


    public void Update()
    {
        if(_axes == RotationAxes.XandY)
        {
            _rotationX -= Input.GetAxis("Mouse Y") * _rotationSpeedVer;
            _rotationX = Mathf.Clamp(_rotationX, minVert, maxVert);

            float delta = Input.GetAxis("Mouse X") * _rotationSpeedHor;
            float _rotationY = transform.localEulerAngles.y + delta;

            transform.localEulerAngles = new Vector3(_rotationX, _rotationY, 0);

        }
        else if(_axes == RotationAxes.X)
        {
            transform.Rotate(0, Input.GetAxis("Mouse X") * _rotationSpeedHor, 0);
        }
        else if(_axes == RotationAxes.Y)
        {
            _rotationX -= Input.GetAxis("Mouse Y") * _rotationSpeedVer;
            _rotationX = Mathf.Clamp(_rotationX, minVert, maxVert);

            float _rotationY = transform.localEulerAngles.y;

            transform.localEulerAngles = new Vector3(_rotationX, _rotationY, 0);
        }
        else if (_axes == RotationAxes.Y_Gun)
        {
            _rotationX -= Input.GetAxis("Mouse Y") * _rotationSpeedVer;
            _rotationX = Mathf.Clamp(_rotationX, minVert, maxVert);
            _GunrotationX = Mathf.Clamp(_rotationX, minVertGun, maxVertGun);

            float _rotationY = transform.localEulerAngles.y;

            transform.localEulerAngles = new Vector3(_GunrotationX, _rotationY, 0);
        }

    }
}
