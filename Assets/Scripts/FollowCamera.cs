using UnityEngine;
using UnityEngine.EventSystems;

namespace Cameras
{
    public class FollowCamera : MonoBehaviour
    {
        [SerializeField] private Transform target = null;
        [SerializeField] private float horizRotateSpeed = 3.0f;
        [SerializeField] private float vertRotateSpeed = 1.0f;

        [Space, Header("Rotation Limits")]
        [SerializeField] float maxVertRot = 45f;
        [SerializeField, Tooltip("In euler angles (Inspector units) the closest to the floor you want the camera to be. This will automatically be" +
            " converted to work with Quaternions.")] 
        float minVertRot = -38f;

        float minVertRot_Actual = -1f;
        float betweenLimits = -1f;

        private void Start()
        {
            CalculateLimits();
        }

        //In case the limits are changed in Play mode
        private void OnValidate()
        {
            if (Application.isPlaying) 
                CalculateLimits();
        }

        void Update()
        {
            transform.position = target.position;

            float mouseMoveY = Input.GetAxis("Mouse Y");
            float mouseMoveX = Input.GetAxis("Mouse X");
            
            transform.RotateAround(transform.position, transform.right, mouseMoveY * vertRotateSpeed);
            transform.RotateAround(transform.position, Vector3.up, mouseMoveX * horizRotateSpeed);

            float xAngle = transform.rotation.eulerAngles.x;

            if (xAngle < minVertRot_Actual && xAngle > maxVertRot) 
            {
                float dist = Mathf.Abs(xAngle - minVertRot_Actual);

                xAngle = dist < betweenLimits ? minVertRot_Actual : maxVertRot;
            }
            float y = transform.rotation.eulerAngles.y;

            transform.rotation = Quaternion.Euler(xAngle, y, 0);
        }

        private bool PointerWithinBounds()
        {
            if (EventSystem.current.IsPointerOverGameObject()) //This is annoying as fuck
                return false;

            Vector2 pointerPos = Input.mousePosition;

            if (pointerPos.x > Screen.width || pointerPos.x < 0)
                return false;
            if (pointerPos.y > Screen.height || pointerPos.y < 0)
                return false;

            return true;
        }

        void CalculateLimits() 
        {
            minVertRot_Actual = minVertRot < 0 ? 360 + minVertRot : 360 - minVertRot;

            betweenLimits = minVertRot_Actual / 2;
        }
    }
}
