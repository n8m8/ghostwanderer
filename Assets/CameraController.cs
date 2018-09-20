using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    [SerializeField] private Vector3 initPosition;
    [SerializeField] private GameObject target;
    [SerializeField] private float cameraSize;
    [SerializeField] private float verticalOffset;
    [SerializeField] private float lookAheadDstX;
    [SerializeField] private float lookSmoothTimeX;
    [SerializeField] private float verticalSmoothTime;

    private Vector3 FIXED_ROTATION = new Vector3(30, 45, 0);
    private float SMOOTHING_MULTIPLIER = 0.07f;
    private float X_THRESHOLD = 0.4f;
    private float Y_THRESHOLD = 0.4f;

    private Camera currentCamera;
    private FocusArea focusArea;
    private Collider targetCollider;
    private Bounds targetBounds2D;
    private Rigidbody targetRigidbody;

    private float lookAheadDirX;
    private float currentLookAheadX;
    private float targetLookAheadX;
    private float smoothLookVelocityX;
    private float smoothVelocityY;

    private bool lookAheadStopped;

    // Use this for initialization
    void Start () {
        currentCamera = GetComponent<Camera>();
        currentCamera.orthographicSize = cameraSize;
        this.transform.position = initPosition;
        this.transform.eulerAngles = FIXED_ROTATION;

        targetRigidbody = target.GetComponent<Rigidbody>();
        targetCollider = target.GetComponent<Collider>();
        Vector3 targetBoundsCenter = currentCamera.WorldToScreenPoint(targetCollider.bounds.center);
        Vector3 targetBoundsSize = currentCamera.WorldToScreenPoint(targetCollider.bounds.center + targetCollider.bounds.size);
        targetBounds2D = new Bounds(targetBoundsCenter, targetBoundsCenter);
        Vector2 focusAreaSize = new Vector2(currentCamera.pixelWidth * X_THRESHOLD, currentCamera.pixelHeight * Y_THRESHOLD);
        focusArea = new FocusArea(targetBounds2D, focusAreaSize);
        
    }
	
	// Update is called once per frame
	void Update () {
        // Vector2 targetPosition = currentCamera.WorldToScreenPoint(target.transform.position);
        // Debug.Log(targetPosition);
        // Debug.Log(new Vector2(currentCamera.pixelWidth, currentCamera.pixelHeight));
	}

    void LateUpdate()
    {
        Vector2 targetPosition = currentCamera.WorldToScreenPoint(target.transform.position);
        if (targetPosition.x > currentCamera.pixelWidth / 2 * (1 + X_THRESHOLD))
        {
            this.transform.localPosition += new Vector3(1, 0, -1) * SMOOTHING_MULTIPLIER;
        }
        if (targetPosition.x < currentCamera.pixelWidth * (1 - X_THRESHOLD) / 2)
        {
            this.transform.localPosition += new Vector3(-1, 0, 1) * SMOOTHING_MULTIPLIER;
        }
        if (targetPosition.y > currentCamera.pixelHeight / 2 * (1 + Y_THRESHOLD))
        {
            this.transform.position += new Vector3(1, 0, 1) * SMOOTHING_MULTIPLIER;
        }
        if (targetPosition.y < currentCamera.pixelHeight * (1 - Y_THRESHOLD) / 2)
        {
            this.transform.position += new Vector3(-1, 0, -1) * SMOOTHING_MULTIPLIER;
        }


        //focusArea.Update(targetBounds2D);

        //Vector2 targetVelocity2D = currentCamera.WorldToScreenPoint(targetRigidbody.velocity);
        //Vector2 focusPosition = focusArea.center + Vector2.up * verticalOffset;

        //if (focusArea.velocity.x != 0)
        //{
        //    lookAheadDirX = Mathf.Sign(focusArea.velocity.x);
        //    if (Mathf.Sign(targetVelocity2D.x) == Mathf.Sign(focusArea.velocity.x) && targetVelocity2D.x != 0)
        //    {
        //        lookAheadStopped = false;
        //        targetLookAheadX = lookAheadDirX * lookAheadDstX;
        //    }
        //    else
        //    {
        //        if (!lookAheadStopped)
        //        {
        //            lookAheadStopped = true;
        //            targetLookAheadX = currentLookAheadX + (lookAheadDirX * lookAheadDstX - currentLookAheadX) / 4f;
        //        }
        //    }
        //}
        //currentLookAheadX = Mathf.SmoothDamp(currentLookAheadX, targetLookAheadX, ref smoothLookVelocityX, lookSmoothTimeX);

        //focusPosition.y = Mathf.SmoothDamp(transform.position.y, focusPosition.y, ref smoothVelocityY, verticalSmoothTime);
        //focusPosition += Vector2.right * currentLookAheadX;

        //transform.position = (Vector3)focusPosition + Vector3.forward * -10;

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
