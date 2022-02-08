public class PatrolSpritiBehavior : SpiritMovementBehavior {
	public Transform[] waypoints;
	private int currentPoint = 0;

	private Vector3 SelectWanderDestination() {
		currentPoint = (++currentPoint) % waypoints.Length;
		return waypoints[currentPoint].position;
	}
}