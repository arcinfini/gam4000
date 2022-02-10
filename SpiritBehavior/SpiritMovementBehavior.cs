/*

READ ME:

This behavior is meant to be directly attached to the spirit gameobject
and controls the movement of the AI.
To setup
    1. assign the player gameobject to the player field
    2. make sure nav mesh is baked
    3. assign the PlayerLayerMask field is set to the layer the player
    is on
    4. If the patrol behavior is meant to be used, assign waypoints to
    the waypoint field

*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SpiritMovementBehavior : MonoBehaviour {
    // The player
    public GameObject player;

    private NavMeshAgent agent;

    // Basic required fields filled with dummy variables
    [Header("Speed")]
    [Tooltip("How fast the AI moves when the player is undetected")]
    public float wanderMovementSpeed = 10f;
    [Tooltip("How fast the AI moves when the player is detected")]
    public float chaseMovementSpeed = 15f;

    [Header("Detection")]
    [Tooltip("The general range of detection for the AI to notice the player")]
    public float basicDetectionRange = 10f;
    [Tooltip("The visual range of detection for the AI to notice the player")]
    public float visualDetectionRange = 15f;
    [Tooltip("The visual arc of detection for the AI to notice the player")]
    public float visualDetectionArc = 50f;
    [Tooltip("The layer that the player model resides on")]
    public int playerLayerMask = 7;

    // A queue to store the movement plans
    private Queue<Vector3> movementQueue;
    private NavMeshPath currentPath;

    // Cooldown fields subject to change
    [Header("Wander")]
    [Tooltip("The cooldown in seconds that the AI waits before wander behavior")]
    public float wanderCooldown = 4;
    private float currentWanderCooldown = 0;

    // Wander fields that determine wander selection
    [Tooltip("The radius of the circle used to randomly select a wander destination")]
    public float wanderRadius = 10f;
    private Vector3 wanderCenter;

    [Tooltip("The list of waypoints the AI uses to travel between its patrol")]
    public Transform[] waypoints;
    private int currentPoint = 0;

    // Detection values
    //private bool hasDetectedPlayer = false;

    public void Awake() {
        currentWanderCooldown = wanderCooldown;
        wanderCenter = this.transform.position;
        movementQueue = new Queue<Vector3>();

        agent = GetComponent<NavMeshAgent>();
        currentPath = new NavMeshPath();
    }

    public void Update() {
        // If the player is detected
        // Clear the movement queue and add the player's position to the queue
        
        bool canSeePlayer = IsInstanceVisible(player);
        bool canDetectPlayer = IsPlayerDetectable();
        if (canDetectPlayer || canSeePlayer) {
            string reason = (canSeePlayer) ? "Can See Player" : "Can Detect Player";
            Debug.Log(reason);

            movementQueue.Clear();
            movementQueue.Enqueue(player.transform.position);
            agent.ResetPath();
        }
        
        // If the spirit can see the player
        // Enter chase mode: Increase speed
        // else; exit chase mode: decrease speed
        if (canSeePlayer) agent.speed = chaseMovementSpeed;
        else agent.speed = wanderMovementSpeed;

        // Skip other actions if a path is active
        // otherwise determine if a path is in queue
        // and set the path if true
        if (agent.hasPath) {
            //Debug.Log("Agent has path, continuting to move");
            return;
        } else if (movementQueue.Count >= 1) {
            agent.CalculatePath(movementQueue.Dequeue(), currentPath);
            if (currentPath.status == NavMeshPathStatus.PathPartial) {
                //Debug.Log("AI can't reach destination, ignoring");
                return;
            }

            agent.SetPath(currentPath);
            return;
        }

        // If wander cooldown has exited in this iteration
        // select a random wander destination within the circle
        // validate that it can be traveled to and add to queue
        // If the selection has failed then return and try next
        // update
        if (!InWanderCooldown()) {
            Vector3 destination = SelectWanderDestination();
            agent.CalculatePath(destination, currentPath);
            if (currentPath.status == NavMeshPathStatus.PathPartial) {
                Debug.Log("Path Initialization failed: Returing");
                return;
            }

            currentWanderCooldown = wanderCooldown;
            movementQueue.Enqueue(destination);
        }
    }

    private Vector3 SelectWanderDestination()
    {
        if (waypoints.Length >= 1)
        {
            Debug.Log("Using Waypoint System");
            currentPoint = (++currentPoint) % waypoints.Length;
            return waypoints[currentPoint].position;
        }

        Vector2 destination2D = Random.insideUnitCircle * wanderRadius;
        Vector3 destination = new Vector3(destination2D.x, 0, destination2D.y);
        destination += wanderCenter;
        Debug.Log("Using radius System");
        return destination;
    }

    // Selects a random position within the wander circle
    // Possible Problem: can not walk up stairs


    // Returns true if the gameobject passed is within the view
    // arc and has a clear path of vision to the gameobject
    private bool IsInstanceVisible(GameObject target) {

        // Checks if the target is within the visual arc
        // If so then raycast to determine if there is a line
        // to the arc
        Vector3 direction = (target.transform.position - transform.position).normalized;
        float angle = Vector3.Angle(transform.forward, direction);
        if (angle < visualDetectionArc/2) {
            
            RaycastHit hit;
            bool didHit = Physics.Raycast(
                transform.position, 
                direction, 
                out hit,
                visualDetectionRange
            );
            
            if (didHit && hit.collider.gameObject == target) {
                Debug.DrawRay(transform.position, direction * hit.distance, Color.black);
                return true;
            }
        }

        return false;
    }

    // Decreases the currentWanderCooldown timer and returns false when outside
    // cooldown time.
    private bool InWanderCooldown() {
        currentWanderCooldown -= Time.deltaTime;

        if (currentWanderCooldown <= 0) {
            Debug.Log("Wander cooldown done");
            return false;
        }
        return true;
    }

	// Casts a physics sphere in the detection radius to detect
	// if the player is in the range.
    private bool IsPlayerDetectable() {
        Collider[] collisions = Physics.OverlapSphere(transform.position, basicDetectionRange, 1 << playerLayerMask);
        bool canDetectPlayer = collisions.Length >= 1;

        if (canDetectPlayer) {
            Vector3 direction = (transform.position - player.transform.position).normalized;
            float distance = (transform.position - player.transform.position).magnitude;

            Debug.DrawRay(transform.position, direction * distance, Color.red);
        }

        return canDetectPlayer;
    }

    public Vector3 DirFromAngle(float angleInDegrees, bool angleIsGlobal) {
        if (!angleIsGlobal) {
            angleInDegrees += transform.eulerAngles.y;
        }
        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad),0,Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    }

    // DEBUG VISUALS
    //void OnDrawGizmosSelected() {
    //    UnityEditor.Handles.color = Color.red;
    //    UnityEditor.Handles.DrawWireDisc(transform.position, Vector3.up, basicDetectionRange);
    //    UnityEditor.Handles.DrawWireArc (transform.position, Vector3.up, Vector3.forward, visualDetectionArc/2, visualDetectionRange);
    //    UnityEditor.Handles.DrawWireArc (transform.position, Vector3.up, Vector3.forward, -visualDetectionArc/2, visualDetectionRange);

    //    Vector3 viewAngleA = DirFromAngle (-visualDetectionArc / 2, false);
    //    Vector3 viewAngleB = DirFromAngle (visualDetectionArc / 2, false);

    //    UnityEditor.Handles.DrawLine (transform.position, transform.position + viewAngleA * visualDetectionRange);
    //    UnityEditor.Handles.DrawLine (transform.position, transform.position + viewAngleB * visualDetectionRange);
    //}
}