using UnityEngine;

public class OrbitCameraController : MonoBehaviour
{
    public Transform target;

    public float distance = 10f;
    public float maxDistance = 25f;
    public float minDistance = 5f;

    public float xSpeed = 0.2f;
    public float ySpeed = 0.2f;
    public float zoomSpeed = 5f;
    public float smoothingZoom = 0.1f;

    public bool limitY = true;
    public float yMinLimit = -60f;
    public float yMaxLimit = 60f;
    public float yLimitOffset = 0f;

    public bool limitX = false;
    public float xMinLimit = -60f;
    public float xMaxLimit = 60f;
    public float xLimitOffset = 0f;

    private float targetDistance = 10f;
    private float x = 0f;
    private float y = 0f;
    private float xVelocity = 0f;
    private float yVelocity = 0f;
    private Vector3 position;
    private float pinchDist = 0f;

    private Transform _transform;

    private float damping = 5.0f;

    void Start()
    {
        _transform = transform;

        if (GetComponent<Rigidbody>() != null)
            GetComponent<Rigidbody>().freezeRotation = true;

        x = 0f;
        y = 0f;
        targetDistance = distance;
        position = _transform.rotation * new Vector3(0.0f, 0.0f, -distance) + target.position;
    }

    void Update()
    {
        if (target != null)
        {
            if (Input.touchCount == 1)
            {
                xVelocity = Mathf.Lerp(xVelocity, Input.GetTouch(0).deltaPosition.x * xSpeed, Time.deltaTime * damping);
                yVelocity = Mathf.Lerp(yVelocity, -Input.GetTouch(0).deltaPosition.y * ySpeed, Time.deltaTime * damping);
            }
            else
            {
                xVelocity = 0f;
                yVelocity = 0f;
            }

            if (limitX)
            {
                x = Mathf.Clamp(x + xVelocity, xMinLimit + xLimitOffset, xMaxLimit + xLimitOffset);
            }
            else
            {
                x += xVelocity;
            }

            if (limitY)
            {
                y = Mathf.Clamp(y + yVelocity, yMinLimit + yLimitOffset, yMaxLimit + yLimitOffset);
            }
            else
            {
                y += yVelocity;
            }

            if (Input.touchCount == 2)
            {
                float newPinchDist = Vector2.Distance(Input.GetTouch(0).position, Input.GetTouch(1).position);
                if (pinchDist != 0f)
                {
                    targetDistance += ((pinchDist - newPinchDist) * 0.005f) * zoomSpeed;
                }
                pinchDist = newPinchDist;
                
                targetDistance = Mathf.Clamp(targetDistance, minDistance, maxDistance);
            }
            else
            {
                pinchDist = 0f;
            }

            distance = Mathf.Lerp(distance, targetDistance, smoothingZoom);
            position = _transform.rotation * new Vector3(0.0f, 0.0f, -distance) + target.position;
            _transform.position = position;

            _transform.rotation = Quaternion.Euler(y, x, 0);
        }
        else
        {
            Debug.LogWarning("Touch Orbit Cam - No Target Given");
        }
    }
}
