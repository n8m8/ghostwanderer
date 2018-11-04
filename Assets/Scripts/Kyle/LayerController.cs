using System;
using UnityEngine;

// Kyle, for Main Character and NPCs
public class LayerController : MonoBehaviour {

    [SerializeField] private int numberOfRayVertical;
    [SerializeField] private int originalSortingOrder;
    [SerializeField] private float height;

    public LayerMask collisionMask;
    public TouchInfo touchInfo;

    //private Rigidbody2D characterRB;
    private Collider2D coll;
    private RaycastOrigins raycastOrigins;
    private float raySpacingHorizontal;
    private float raySpacingVertical;
    private Vector2 targetVelocity;
    private SpriteRenderer characterSpriteRenderer;
    private Bounds colliderBounds;

    private float SKIN_WIDTH = -0.14f;

    private void Awake()
    {
        coll = GetComponent<Collider2D>();
        //characterRB = GetComponent<Rigidbody2D>();
        characterSpriteRenderer = GetComponent<SpriteRenderer>();

        if (originalSortingOrder == 0)
            originalSortingOrder = characterSpriteRenderer.sortingOrder;

        CalculateBounds();
        CalculateRaySpacing();

        touchInfo.topObject = touchInfo.bottomObject = new GameObject[numberOfRayVertical];
        touchInfo.touchBottom = touchInfo.touchTop = new bool[numberOfRayVertical];
    }

    private void FixedUpdate()
    {
        CalculateBounds();
        CalculateRaySpacing();
        //targetVelocity = characterRB.velocity;
        RaycastTouchVertical();
    }

    public void UpdateSortingTop(int rayIndex)
    {
        if (touchInfo.touchTop[rayIndex] && touchInfo.topObject[rayIndex] != null)
        {
            if (touchInfo.topObject[rayIndex].GetComponent<SpriteRenderer>().sortingOrder >= characterSpriteRenderer.sortingOrder)
            {
                characterSpriteRenderer.sortingOrder = touchInfo.topObject[rayIndex].GetComponent<SpriteRenderer>().sortingOrder + 1;
            }
            //Debug.Log("Touch Top???");
        }
        else
        {
            touchInfo.ResetTop(rayIndex);
        }
    }

    public void UpdateSortingBottom(int rayIndex)
    {
        if (touchInfo.touchBottom[rayIndex] && touchInfo.bottomObject[rayIndex] != null)
        {
            if (touchInfo.bottomObject[rayIndex].GetComponent<SpriteRenderer>().sortingOrder <= characterSpriteRenderer.sortingOrder)
            {
                characterSpriteRenderer.sortingOrder = touchInfo.bottomObject[rayIndex].GetComponent<SpriteRenderer>().sortingOrder - 1;
            }
            //Debug.Log("Touch Bottom???");
        }
        else
        {
            touchInfo.ResetBottom(rayIndex);
        }
    }

        public void CheckDontTouch()
    {
        for (int rayIndex = 0; rayIndex < numberOfRayVertical; rayIndex++)
        {
            if (touchInfo.touchTop[rayIndex] || touchInfo.touchBottom[rayIndex])
                //Debug.Log("Still touch");
                return;
        }
        characterSpriteRenderer.sortingOrder = originalSortingOrder;
        touchInfo.ResetAll();
        //Debug.Log("Reset All");
    }

    public void CalculateBounds()
    {
        colliderBounds = coll.bounds;
        colliderBounds.Expand(-SKIN_WIDTH * 2);

        raycastOrigins = new RaycastOrigins();
        //raycastOrigins.topLeft = new Vector2(colliderBounds.min.x, colliderBounds.min.y + 5 * (colliderBounds.max.y - colliderBounds.min.y));
        //raycastOrigins.topRight = new Vector2(colliderBounds.max.x, colliderBounds.min.y + 5 * (colliderBounds.max.y - colliderBounds.min.y));
        raycastOrigins.topLeft = new Vector2(colliderBounds.min.x, colliderBounds.max.y);
        raycastOrigins.topRight = new Vector2(colliderBounds.max.x, colliderBounds.max.y);
        raycastOrigins.bottomLeft = new Vector2(colliderBounds.min.x, colliderBounds.min.y - SKIN_WIDTH);
        raycastOrigins.bottomRight = new Vector2(colliderBounds.max.x, colliderBounds.min.y - SKIN_WIDTH);
    }

    private void CalculateRaySpacing()
    {
        numberOfRayVertical = numberOfRayVertical < 2 ? 2 : numberOfRayVertical;
        raySpacingVertical = (colliderBounds.extents.x * 2) / (numberOfRayVertical - 1);
    }

    public void RaycastTouchVertical()
    {
        touchInfo.ResetAll();

        Vector2 raycastBaseTop = raycastOrigins.topLeft;
        Vector2 raycastBaseBottom = raycastOrigins.bottomLeft;

        for (int rayIndex = 0; rayIndex < numberOfRayVertical; rayIndex++)
        {
            float distance = 0.80f;

            Vector2 raycastOriginBottom = raycastBaseBottom + raySpacingVertical * rayIndex * Vector2.right;

            RaycastHit2D[] hitsBottom =
                Physics2D.RaycastAll(raycastOriginBottom, new Vector2(0, -1), distance + SKIN_WIDTH, collisionMask);
            Debug.DrawRay(raycastOriginBottom, new Vector2(0, -distance - SKIN_WIDTH), Color.red, Time.fixedDeltaTime, false);


            foreach (RaycastHit2D hit in hitsBottom)
            {
                if (!hit.collider.isTrigger && hit.transform.gameObject.GetComponent<SpriteRenderer>() != null
                    && hit.transform.gameObject != touchInfo.topObject[rayIndex])
                {
                    touchInfo.touchBottom[rayIndex] = true;
                    touchInfo.bottomObject[rayIndex] = hit.transform.gameObject;
                    UpdateSortingBottom(rayIndex);
                    //break;
                }
            }
            if (touchInfo.touchBottom[rayIndex] == false)
            {
                touchInfo.ResetBottom(rayIndex);
                //Debug.Log("Reset Bottom");
            }


            distance = 0.40f + height;

            Vector2 raycastOriginTop = raycastBaseTop + raySpacingVertical * rayIndex * Vector2.right + Vector2.up * 0.05f;

            RaycastHit2D[] hitsTop =
                Physics2D.RaycastAll(raycastOriginTop, new Vector2(0, 1), distance + SKIN_WIDTH, collisionMask);
            Debug.DrawRay(raycastOriginTop, new Vector2(0, distance + SKIN_WIDTH), Color.red, Time.fixedDeltaTime, false);

            foreach (RaycastHit2D hit in hitsTop)
            {
                if (!hit.collider.isTrigger && hit.transform.gameObject.GetComponent<SpriteRenderer>() != null
                    && hit.transform.gameObject != touchInfo.bottomObject[rayIndex])
                {
                    touchInfo.touchTop[rayIndex] = true;
                    touchInfo.topObject[rayIndex] = hit.transform.gameObject;
                    UpdateSortingTop(rayIndex);
                    //break;
                }
            }
            if (touchInfo.touchTop[rayIndex] == false)
            {
                touchInfo.ResetTop(rayIndex);
                //Debug.Log("Reset Top");
            }
        }

        CheckDontTouch();
        //Debug.Log("Reach end of method");
    }
}

public struct RaycastOrigins
{
    public Vector2 topLeft, topRight;
    public Vector2 bottomLeft, bottomRight;
}

public struct TouchInfo
{
    //public bool[] touchLeft, touchRight;
    public bool[] touchBottom, touchTop;

    //public GameObject[] leftObject, rightObject;
    public GameObject[] bottomObject, topObject;

    public void ResetAll()
    {
        Array.Clear(touchBottom, 0, touchBottom.Length - 1);
        Array.Clear(touchTop, 0, touchTop.Length - 1);
        Array.Clear(bottomObject, 0, bottomObject.Length - 1);
        Array.Clear(topObject, 0, topObject.Length - 1);
    }

    public void ResetTop(int rayIndex)
    {
        touchTop[rayIndex] = false;
        topObject[rayIndex] = null;
    }

    public void ResetBottom(int rayIndex)
    {
        touchBottom[rayIndex] = false;
        bottomObject[rayIndex] = null;
    }
}
