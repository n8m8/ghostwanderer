using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    [SerializeField] private GameObject target;
    [SerializeField] private float cameraSize;

    private Camera currentCamera;
    private FocusArea focusArea;
    private Collider2D targetCollider;
    private Bounds targetBounds;
    private Rigidbody2D targetRigidbody;

    private bool resetPosition;

    private Vector3 INIT_POSITION = new Vector3(0, 0, -10);
    private Vector3 FIXED_ROTATION = Vector3.zero;
    private float SMOOTHING_MULTIPLIER = 0.07f;
    private float X_THRESHOLD = 0.5f;
    private float Y_THRESHOLD = 0.4f;

    // Use this for initialization
    void Start () {
        currentCamera = GetComponent<Camera>();
        currentCamera.orthographicSize = cameraSize;
        this.transform.position = INIT_POSITION;
        this.transform.eulerAngles = FIXED_ROTATION;

        targetRigidbody = target.GetComponent<Rigidbody2D>();
        targetCollider = target.GetComponent<Collider2D>();

        targetBounds = targetCollider.bounds;
        Vector2 focusAreaSize = new Vector2(currentCamera.pixelWidth * X_THRESHOLD, currentCamera.pixelHeight * Y_THRESHOLD);
        focusArea = new FocusArea(targetBounds, focusAreaSize);
    }

    void LateUpdate()
    {
        if (resetPosition)
        {
            GameObject player = target;
            player.GetComponent<PlayerController>().playerStatus.moveAllowed = false;
            Vector3 currentPos = transform.position;
            Vector3 newPos = target.transform.position + Vector3.back * 10;
            transform.position = Vector3.Lerp(currentPos, newPos, Time.deltaTime);
            Debug.Log("This?");
            if (Vector3.Magnitude(newPos - currentPos) < 1)
            {
                Debug.Log("Does the threshold work?");
                resetPosition = false;
                player.GetComponent<PlayerController>().playerStatus.moveAllowed = true;
            }
        }
        else
        {
            Vector2 targetPosition = currentCamera.WorldToScreenPoint(target.transform.position);
            if (targetPosition.x > currentCamera.pixelWidth / 2 * (1 + X_THRESHOLD))
            {
                this.transform.localPosition += Vector3.right * targetRigidbody.velocity.x * Time.fixedDeltaTime;
            }
            if (targetPosition.x < currentCamera.pixelWidth * (1 - X_THRESHOLD) / 2)
            {
                this.transform.localPosition += Vector3.right * targetRigidbody.velocity.x * Time.fixedDeltaTime;
            }
            if (targetPosition.y > currentCamera.pixelHeight / 2 * (1 + Y_THRESHOLD))
            {
                this.transform.localPosition += Vector3.up * targetRigidbody.velocity.y * Time.fixedDeltaTime;
            }
            if (targetPosition.y < currentCamera.pixelHeight * (1 - Y_THRESHOLD) / 2)
            {
                this.transform.localPosition += Vector3.up * targetRigidbody.velocity.y * Time.fixedDeltaTime;
            }
        }
    }

    public void ResetCameraPosition()
    {
        resetPosition = true;
    }


    struct FocusArea
    {
        public Vector2 center;
        public Vector2 velocity;
        float left, right;
        float top, bottom;

        public FocusArea(Bounds targetBounds, Vector2 size)
        {
            left = targetBounds.center.x - size.x / 2;
            right = targetBounds.center.x + size.x / 2;
            bottom = targetBounds.min.y;
            top = targetBounds.min.y + size.y;

            velocity = Vector2.zero;
            center = new Vector2((left + right) / 2, (top + bottom) / 2);
        }

        public void Update(Bounds targetBounds)
        {
            float shiftX = 0;
            if (targetBounds.min.x < left)
            {
                shiftX = targetBounds.min.x - left;
            }
            else if (targetBounds.max.x > right)
            {
                shiftX = targetBounds.max.x - right;
            }
            left += shiftX;
            right += shiftX;

            float shiftY = 0;
            if (targetBounds.min.y < bottom)
            {
                shiftY = targetBounds.min.y - bottom;
            }
            else if (targetBounds.max.y > top)
            {
                shiftY = targetBounds.max.y - top;
            }
            top += shiftY;
            bottom += shiftY;
            center = new Vector2((left + right) / 2, (top + bottom) / 2);
            velocity = new Vector2(shiftX, shiftY);
        }
    }
}
