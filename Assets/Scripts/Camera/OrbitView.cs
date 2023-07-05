using UnityEngine;

namespace FinimenSniperC.Camera
{
    [AddComponentMenu("Camera-Control/Mouse Orbit")]
    internal class OrbitView : MonoBehaviour
    {
        [SerializeField] private Transform target;

        [SerializeField] private float distance = 10.0f;
        [SerializeField] private float xSpeed = 250.0f;
        [SerializeField] private float ySpeed = 120.0f;

        [SerializeField] private float yMinLimit = -20;
        [SerializeField] private float yMaxLimit = 80;

        [SerializeField, Range(0, 1)] private float movingSpeed;
        [SerializeField, Range(0, 1)] private float rotatingSpeed;

        private Vector3 position;
        private Quaternion rotation;

        private float x = 0.0f;
        private float y = 0.0f;

        private void Awake()
        {
            if (GetComponent<Rigidbody>())
            {
                GetComponent<Rigidbody>().freezeRotation = true;
            }
        }

        private void Start()
        {
            var angles = transform.eulerAngles;

            x = angles.y;
            y = angles.x;

            UpdateTransform();
        }

        private void LateUpdate()
        {
            if (Input.GetMouseButton(1))
            {
                UpdateTransform();
            }

            transform.position = Vector3.Lerp(transform.position, position, movingSpeed);
            transform.rotation = Quaternion.Lerp(transform.rotation, rotation, rotatingSpeed);
        }

        private void UpdateTransform()
        {
            x += Input.GetAxis("Mouse X") * xSpeed * 0.02f;
            y -= Input.GetAxis("Mouse Y") * ySpeed * 0.02f;

            y = ClampAngle(y, yMinLimit, yMaxLimit);

            var rotation = Quaternion.Euler(y, x, 0);
            var position = rotation * new Vector3(0.0f, 0.0f, -(distance + Mathf.Abs(0))) + target.position;

            this.position = position;
            this.rotation = rotation;
        }

        private float ClampAngle(float angle, float min, float max)
        {
            if (angle < -360)
            {
                angle += 360;
            }
            else if (angle > 360)
            {
                angle -= 360;
            }

            return Mathf.Clamp(angle, min, max);
        }
    }
}