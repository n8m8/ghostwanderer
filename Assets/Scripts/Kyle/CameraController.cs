using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Kyle
public class CameraController : MonoBehaviour {

    // Target that the camera follows
    [SerializeField] private GameObject target;
    // The orthographic size of the camera
    [SerializeField] private float cameraSize;
    // The initial position of the camera at the start of the game
    [SerializeField] private Vector3 INIT_POSITION;

    // The Camera componnent
    private Camera currentCamera;
    // The area the Camera focuses on. If the player gets out of that area, the Camera will shift
    private FocusArea focusArea;
    // The Collider2D of the target
    private Collider2D targetCollider;
    // The Collider Bounds of the target
    private Bounds targetBounds;
    // The Rigidbody2D of the target
    private Rigidbody2D targetRigidbody;

    // Whether the camera should reset position
    private bool resetPosition;
    // The initial rotation of the camera at the start of the game
    private Vector3 FIXED_ROTATION = Vector3.zero;
    // The smoothing multiplier for shifting the camera, not using right now
    private float SMOOTHING_MULTIPLIER = 0.07f;
    // The x threshold ratio of the focus area size
    private float X_THRESHOLD = 0.3f;
    // The y threshold ratio of the focus area size
    private float Y_THRESHOLD = 0.3f;

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

    // Update the position of the Camera for several cases
    void LateUpdate()
    {
        if (resetPosition)
        {
            RefocusOnRespawn();
        }
        else
        {
            NormalCameraShift();
        }
    }

    // Refocus the camera when the main character respawns
    private void RefocusOnRespawn()
    {
        GameObject player = target;
        player.GetComponent<PlayerController>().playerStatus.moveAllowed = false;
        Vector3 currentPos = transform.position;
        Vector3 newPos = target.transform.position + Vector3.back * 10;
        transform.position = Vector3.Slerp(currentPos, newPos, 2 * Time.deltaTime);
        Debug.Log("This?");
        if (Vector3.Magnitude(newPos - currentPos) < 1)
        {
            Debug.Log("Does the threshold work?");
            resetPosition = false;
            player.GetComponent<PlayerController>().playerStatus.moveAllowed = true;
        }
    }

    // Shift the camera normally when the main character gets out of the focus area
    private void NormalCameraShift()
    {
        Vector2 targetPosition = currentCamera.WorldToScreenPoint(target.transform.position);
        if (targetPosition.x > currentCamera.pixelWidth / 2 * (1 + X_THRESHOLD)
            || targetPosition.x < currentCamera.pixelWidth * (1 - X_THRESHOLD) / 2)
        {
            this.transform.localPosition += Vector3.right * targetRigidbody.velocity.x * Time.fixedDeltaTime;
        }
        if (targetPosition.y > currentCamera.pixelHeight / 2 * (1 + Y_THRESHOLD)
            || targetPosition.y < currentCamera.pixelHeight * (1 - Y_THRESHOLD) / 2)
        {
            this.transform.localPosition += Vector3.up * targetRigidbody.velocity.y * Time.fixedDeltaTime;
        }
    }

    // Trigger the camera to refocus
    public void ResetCameraPosition()
    {
        resetPosition = true;
    }

    // A struct that represents the Focus Area of the camera
    struct FocusArea
    {
        // Center of the focus area
        public Vector2 center;
        // Velocity
        public Vector2 velocity;
        // left, right positions of the 
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
