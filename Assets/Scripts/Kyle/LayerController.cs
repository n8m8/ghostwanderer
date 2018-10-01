﻿using System.Collections;
using UnityEngine;

// Kyle, for Main Character and NPCs
public class LayerController : MonoBehaviour {

    [SerializeField] private int numberOfRayHorizontal;
    [SerializeField] private int numberOfRayVertical;

    public LayerMask collisionMask;
    public TouchInfo touchInfo;

    private Rigidbody2D characterRB;
    private Collider2D coll;
    private RaycastOrigins raycastOrigins;
    private float raySpacingHorizontal;
    private float raySpacingVertical;
    private Vector2 targetVelocity;
    private SpriteRenderer characterSpriteRenderer;
    private int originalSortingOrder;
    private Bounds colliderBounds;

    private float SKIN_WIDTH = 0.01f;

    private void Awake()
    {
        coll = GetComponent<Collider2D>();
        characterRB = GetComponent<Rigidbody2D>();
        characterSpriteRenderer = GetComponent<SpriteRenderer>();
        originalSortingOrder = characterSpriteRenderer.sortingOrder;
        Debug.Log(originalSortingOrder);

        CalculateBounds();
        CalculateRaySpacing();
    }

    private void FixedUpdate()
    {
        CalculateBounds();
        CalculateRaySpacing();
        targetVelocity = characterRB.velocity;
        RaycastTouchVertical(ref targetVelocity);
    }

    public void UpdateSorting()
    {
        if (touchInfo.touchTop)
        {
            if (touchInfo.topObject.GetComponent<SpriteRenderer>().sortingOrder + 1 != characterSpriteRenderer.sortingOrder)
            {
                touchInfo.topObject.GetComponent<SpriteRenderer>().sortingOrder -= 1;
                characterSpriteRenderer.sortingOrder = touchInfo.topObject.GetComponent<SpriteRenderer>().sortingOrder + 1;
            }
            else
                Debug.Log("Stop decrement");
            Debug.Log("Touch Top???");
        }
        else if (touchInfo.topObject != null)
        {
            touchInfo.topObject.GetComponent<SpriteRenderer>().sortingOrder += 1;
            //characterSpriteRenderer.sortingOrder = originalSortingOrder;
            touchInfo.ResetTop();
        }
        else
        {
            //characterSpriteRenderer.sortingOrder = originalSortingOrder;
            touchInfo.ResetTop();
            //Debug.Log("Reset");
        }

        if (touchInfo.touchBottom)
        {
            if (touchInfo.bottomObject.GetComponent<SpriteRenderer>().sortingOrder - 1 != characterSpriteRenderer.sortingOrder)
            {
                touchInfo.bottomObject.GetComponent<SpriteRenderer>().sortingOrder += 1;
                characterSpriteRenderer.sortingOrder = touchInfo.bottomObject.GetComponent<SpriteRenderer>().sortingOrder - 1;
            }
            else
                Debug.Log("Stop increment");
            Debug.Log("Touch Bottom???");
        }
        else if (touchInfo.bottomObject != null)
        {
            touchInfo.bottomObject.GetComponent<SpriteRenderer>().sortingOrder -= 1;
            //characterSpriteRenderer.sortingOrder = originalSortingOrder;
            touchInfo.ResetBottom();
        }
        else
        {
            //characterSpriteRenderer.sortingOrder = originalSortingOrder;
            touchInfo.ResetBottom();
            //Debug.Log("Reset");
            //Debug.Log("Not Touch Bottom???");
        }

        if (!touchInfo.touchTop && !touchInfo.touchBottom)
        {
            characterSpriteRenderer.sortingOrder = originalSortingOrder;
        }
    }

    public void CalculateBounds()
    {
        colliderBounds = coll.bounds;
        colliderBounds.Expand(-SKIN_WIDTH * 2);

        raycastOrigins = new RaycastOrigins();
        raycastOrigins.topLeft = new Vector2(colliderBounds.min.x, colliderBounds.min.y + 5 * (colliderBounds.max.y - colliderBounds.min.y));
        raycastOrigins.topRight = new Vector2(colliderBounds.max.x, colliderBounds.min.y + 5 * (colliderBounds.max.y - colliderBounds.min.y));
        raycastOrigins.bottomLeft = new Vector2(colliderBounds.min.x, colliderBounds.min.y);
        raycastOrigins.bottomRight = new Vector2(colliderBounds.max.x, colliderBounds.min.y);
    }

    private void CalculateRaySpacing()
    {
        numberOfRayHorizontal = numberOfRayHorizontal < 2 ? 2 : numberOfRayHorizontal;
        numberOfRayVertical = numberOfRayVertical < 2 ? 2 : numberOfRayVertical;

        raySpacingVertical = (colliderBounds.extents.x * 2) / (numberOfRayHorizontal - 1);
        raySpacingHorizontal = (colliderBounds.extents.y * 2) / (numberOfRayVertical - 1);
    }

    public void RaycastTouchVertical(ref Vector2 velocity)
    {

        Vector2 raycastBaseTop = raycastOrigins.topLeft;
        Vector2 raycastBaseBottom = raycastOrigins.bottomLeft;
        //Debug.Log(direction);
        for (int i = 0; i < numberOfRayVertical; i++)
        {
            //float distance = Mathf.Abs(velocity.y) * 0.15f;
            float distance = 0.12f;
            Vector2 raycastOriginTop = raycastBaseTop + raySpacingVertical * i * Vector2.right;
            Vector2 raycastOriginBottom = raycastBaseBottom + raySpacingVertical * i * Vector2.right;

            RaycastHit2D[] hitsBottom =
                Physics2D.RaycastAll(raycastOriginBottom, new Vector2(0, -1), - (distance + SKIN_WIDTH), collisionMask);
            Debug.DrawRay(raycastOriginBottom, new Vector2(0, -distance), Color.red, Time.fixedDeltaTime, false);


            foreach (RaycastHit2D hit in hitsBottom)
            {
                if (!hit.collider.isTrigger && hit.transform.gameObject.GetComponent<SpriteRenderer>() != null 
                    && (hit.transform.gameObject.Equals(touchInfo.bottomObject) || touchInfo.bottomObject == null))
                {
                    //if (Mathf.Abs(velocity.y) > Mathf.Abs(hit.distance - SKIN_WIDTH))
                    //{
                    //    velocity.y = (hit.distance - SKIN_WIDTH) * -1;
                    //}
                    touchInfo.touchBottom = true;
                    touchInfo.bottomObject = hit.transform.gameObject;
                    UpdateSorting();
                }
                else
                {
                    if (touchInfo.bottomObject != null)
                    {
                        touchInfo.bottomObject.GetComponent<SpriteRenderer>().sortingOrder -= 1;
                    }
                    characterSpriteRenderer.sortingOrder = originalSortingOrder;
                    touchInfo.ResetBottom();
                }
            }

            RaycastHit2D[] hitsTop =
                Physics2D.RaycastAll(raycastOriginTop, new Vector2(0, 1), distance + SKIN_WIDTH, collisionMask);
            Debug.DrawRay(raycastOriginTop, new Vector2(0, distance), Color.red, Time.fixedDeltaTime, false);

            foreach (RaycastHit2D hit in hitsTop)
            {
                if (!hit.collider.isTrigger && hit.transform.gameObject.GetComponent<SpriteRenderer>() != null
                    && (hit.transform.gameObject.Equals(touchInfo.topObject) || touchInfo.topObject == null))
                {
                    //if (Mathf.Abs(velocity.y) > Mathf.Abs(hit.distance - SKIN_WIDTH))
                    //{
                    //    velocity.y = (hit.distance - SKIN_WIDTH) * 1;
                    //}
                    touchInfo.touchTop = true;
                    touchInfo.topObject = hit.transform.gameObject;
                    UpdateSorting();
                }
                else
                {
                    if (touchInfo.topObject != null)
                    {
                        touchInfo.topObject.GetComponent<SpriteRenderer>().sortingOrder -= 1;
                    }
                    characterSpriteRenderer.sortingOrder = originalSortingOrder;
                    touchInfo.ResetTop();
                }
            }
        }
    }

    //public void RaycastTouchHorizontal(ref Vector2 velocity)
    //{
    //    Vector2 raycastBaseLeft = raycastOrigins.bottomLeft;
    //    Vector2 raycastBaseRight = raycastOrigins.bottomRight;
    //    //Debug.Log(direction);
    //    for (int i = 0; i < numberOfRayHorizontal; i++)
    //    {
    //        float distance = Mathf.Abs(velocity.x);
    //        Vector2 raycastOriginLeft = raycastBaseLeft + raySpacingHorizontal * i * Vector2.up;
    //        Vector2 raycastOriginRight = raycastBaseRight + raySpacingHorizontal * i * Vector2.up;

    //        RaycastHit2D[] hitsLeft =
    //            Physics2D.RaycastAll(raycastOriginLeft, new Vector2(-1, 0), distance + SKIN_WIDTH, collisionMask);
    //        RaycastHit2D[] hitsRight =
    //            Physics2D.RaycastAll(raycastOriginRight, new Vector2(1, 0), distance + SKIN_WIDTH, collisionMask);
    //        Debug.DrawRay(raycastOriginLeft, new Vector2(velocity.x, 0), Color.red, Time.fixedDeltaTime, false);
    //        Debug.DrawRay(raycastOriginRight, new Vector2(velocity.x, 0), Color.red, Time.fixedDeltaTime, false);

    //        foreach (RaycastHit2D hit in hitsLeft)
    //        {
    //            if (!hit.collider.isTrigger && hit.transform.gameObject.GetComponent<SpriteRenderer>() != null)
    //            {
    //                if (Mathf.Abs(velocity.x) > Mathf.Abs(hit.distance - SKIN_WIDTH))
    //                {
    //                    velocity.x = (hit.distance - SKIN_WIDTH) * -1;
    //                }
    //                touchInfo.touchLeft = true;
    //                touchInfo.leftObject = hit.transform.gameObject;
    //                UpdateSorting();
    //            }
    //            else
    //            {
    //                touchInfo.Reset();
    //            }
    //        }

    //        foreach (RaycastHit2D hit in hitsRight)
    //        {
    //            if (!hit.collider.isTrigger && hit.transform.gameObject.GetComponent<SpriteRenderer>() != null)
    //            {
    //                if (Mathf.Abs(velocity.x) > Mathf.Abs(hit.distance - SKIN_WIDTH))
    //                {
    //                    velocity.x = (hit.distance - SKIN_WIDTH) * 1;
    //                }
    //                touchInfo.touchRight = true;
    //                touchInfo.rightObject = hit.transform.gameObject;
    //                UpdateSorting();
    //            }
    //            else
    //            {
    //                touchInfo.Reset();
    //            }
    //        }
    //    }
    //}

}

public struct RaycastOrigins
{
    public Vector2 topLeft, topRight;
    public Vector2 bottomLeft, bottomRight;
}

public struct TouchInfo
{
    public bool touchLeft, touchRight, touchBottom, touchTop;
    public GameObject leftObject, rightObject, bottomObject, topObject;

    public void Reset()
    {
        touchBottom = touchTop = touchLeft = touchRight = false;
        leftObject = rightObject = bottomObject = topObject = null;
    }

    public void ResetTop()
    {
        touchTop = false;
        topObject = null;
    }

    public void ResetBottom()
    {
        touchBottom = false;
        bottomObject = null;
    }
}
