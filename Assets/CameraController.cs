using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    [SerializeField] private Vector3 initPosition;
    [SerializeField] private Transform target;
    [SerializeField] private float cameraSize;
    [SerializeField] private float verticalOffset;
    [SerializeField] private float lookAheadDstX;
    [SerializeField] private float lookSmoothTimeX;
    [SerializeField] private float verticalSmoothTime;

    private Vector3 FIXED_ROTATION = new Vector3(30, 45, 0);
    private float SMOOTHING_MULTIPLIER = 1f;
    private float X_THRESHOLD = 0.5f;
    private float Y_THRESHOLD = 0.5f;

    private Camera currentCamera;
    private FocusArea focusArea;
    private Collider targetCollider;
    private Bounds targetBounds2D;

    private float lookAheadDirX;
    private float currentLookAheadX;
    private float targetLookAheadX;
    private float smoothLookVelocityX;
    private float smoothVelocityY;

    // Use this for initialization
    void Start () {
        currentCamera = GetComponent<Camera>();
        currentCamera.orthographicSize = cameraSize;
        this.transform.position = initPosition;
        this.transform.eulerAngles = FIXED_ROTATION;
        targetCollider = target.GetComponent<Collider>();
        Vector3 targetBoundsCenter = currentCamera.WorldToScreenPoint(targetCollider.bounds.center);
        Vector3 targetBoundsSize = currentCamera.WorldToScreenPoint(targetCollider.bounds.center + targetCollider.bounds.size);
        targetBounds2D = new Bounds(targetBoundsCenter, targetBoundsCenter);
        Vector2 focusAreaSize = new Vector2(currentCamera.pixelWidth * X_THRESHOLD, currentCamera.pixelHeight * Y_THRESHOLD);
        focusArea = new FocusArea(targetBounds2D, focusAreaSize);
        
    }
	
	// Update is called once per frame
	void Update () {
        Vector3 targetPosition = currentCamera.WorldToScreenPoint(target.position);
        Debug.Log(targetPosition);
        Debug.Log(new Vector2(currentCamera.pixelWidth, currentCamera.pixelHeight));
	}

    void LateUpdate()
    {
        focusArea.Update(targetBounds2D);

        //Vector2 focusPosition = focusArea.centre + Vector2.up * verticalOffset;

        //if (focusArea.velocity.x != 0)
        //{
        //    lookAheadDirX = Mathf.Sign(focusArea.velocity.x);
        //    if (Mathf.Sign(target.playerStatus.currentVelocity.x) == Mathf.Sign(focusArea.velocity.x) && target.playerStatus.currentVelocity.x != 0)
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
        public Vector2 centre;
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
            centre = new Vector2((left + right) / 2, (top + bottom) / 2);
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
            centre = new Vector2((left + right) / 2, (top + bottom) / 2);
            velocity = new Vector2(shiftX, shiftY);
        }
    }
}
