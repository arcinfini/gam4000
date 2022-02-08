using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolSpiritBehavior : SpiritMovementBehavior {
	[Tooltip("The list of waypoints the AI uses to travel between its patrol")]
	public Transform[] waypoints;
	private int currentPoint = 0;

	private Vector3 SelectWanderDestination() {
		currentPoint = (++currentPoint) % waypoints.Length;
		return waypoints[currentPoint].position;
	}
}