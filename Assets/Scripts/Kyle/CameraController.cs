using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Kyle
//Edited by Jeremy
public class CameraController : MonoBehaviour {

    // Target that the camera follows
    [SerializeField] private GameObject target;
	// The speed the camera can track the player
	[SerializeField] private float cameraSpeed;
	[SerializeField] private float focusAreaWidth;
	[SerializeField] private float focusAreaHeight;

	private bool trackingPlayer = false;

	void LateUpdate () {
		if (trackingPlayer) {
			Vector2 playerOffsetVector = (target.transform.position - transform.position);
			//proportional speed makes the camera move proportionally to the cameraSpeed value and to its distance from the player
			float proportionalSpeed = Mathf.Clamp (cameraSpeed * Mathf.Clamp (playerOffsetVector.magnitude/4, 0f, 2f), 0.05f, cameraSpeed);
			Vector3 cameraMovement = (Vector3) playerOffsetVector.normalized * proportionalSpeed * Time.deltaTime;
			transform.position = transform.position + cameraMovement;
			//when the camera is close enough, it freezes until player exits focus area again
			if (playerOffsetVector.magnitude < 0.13) {
				trackingPlayer = false;
			}
		//once player exits focus area, camera starts to follow
		} else if (!playerInFocusArea ()) {
			trackingPlayer = true;
		}
	}

	private bool playerInFocusArea(){
		float playerOffsetX = target.transform.position.x - transform.position.x;
		float playerOffsetY = target.transform.position.y - transform.position.y;

		return (Mathf.Abs(playerOffsetX) < focusAreaWidth) && (Mathf.Abs(playerOffsetY) < focusAreaHeight);
	}

    public void SwitchFocusArea(Vector3 newPosition)
    {
        float currentZPosition = this.transform.position.z;
        this.transform.position = new Vector3(newPosition.x, newPosition.y, currentZPosition);
    }

}