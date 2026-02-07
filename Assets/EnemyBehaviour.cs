// EnemyAI.cs
// Basic enemy AI with patrol and chase behavior (or well i tried to have it do this)
// Uses Rigidbody.MovePosition for movement and damages player when in range
// Author: Makayla Hardware

using UnityEngine;

public class EnemyAI : MonoBehaviour
{
	[Header("Movement")]
	public float moveSpeed = 3f;
	public float chaseRange = 10f;
	public Transform[] patrolPoints;       // set these in inspector for patrol route
	public float stopDistance = 0.5f;

	private Transform player;
	private Rigidbody rb;
	private Vector3 startPosition;         // where enemy spawned
	private int currentPatrolPoint = 0;

	void Start()
	{
		startPosition = transform.position;
		rb = GetComponent<Rigidbody>();

		if (rb == null)
		{
			Debug.LogError("Enemy needs a Rigidbody!");
		}

		// kinematic so other objects can't push it around
		rb.isKinematic = true;
		rb.useGravity = true;

		// find player in scene
		player = GameObject.FindGameObjectWithTag("Player")?.transform;
		if (player == null)
			Debug.LogError("Player not found! Make sure the player is tagged 'Player'.");
	}

	// FixedUpdate for physics-based movement
	void FixedUpdate()
	{
		if (player == null) return;

		float distanceToPlayer = Vector3.Distance(player.position, transform.position);
		Vector3 targetPos = transform.position;

		// chase player if close enough
		if (distanceToPlayer <= chaseRange)
		{
			Vector3 direction = (player.position - transform.position).normalized;
			targetPos = transform.position + direction * moveSpeed * Time.fixedDeltaTime;
			RotateTowards(direction);
		}
		// patrol between waypoints if set
		else if (patrolPoints.Length > 0)
		{
			Vector3 direction = (patrolPoints[currentPatrolPoint].position - transform.position);
			targetPos = transform.position + direction.normalized * moveSpeed * Time.fixedDeltaTime;
			RotateTowards(direction);

			// move to next patrol point when close enough
			if (direction.magnitude < stopDistance)
			{
				currentPatrolPoint = (currentPatrolPoint + 1) % patrolPoints.Length;
			}
		}
		// go back to spawn if no patrol points
		else
		{
			Vector3 direction = (startPosition - transform.position);
			targetPos = transform.position + direction.normalized * moveSpeed * Time.fixedDeltaTime;
			RotateTowards(direction);
		}

		rb.MovePosition(targetPos);
	}

	// smooth rotation that only turns on Y axis (no tilting)
	void RotateTowards(Vector3 direction)
	{
		if (direction != Vector3.zero)
		{
			Vector3 lookDir = new Vector3(direction.x, 0, direction.z);
			Quaternion lookRotation = Quaternion.LookRotation(lookDir);
			rb.MoveRotation(Quaternion.Slerp(rb.rotation, lookRotation, 5f * Time.fixedDeltaTime));
		}
	}

	// deal damage when touching player
	void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.CompareTag("Player"))
		{
			PlayerHealth playerHealth = collision.gameObject.GetComponent<PlayerHealth>();
			if (playerHealth != null)
			{
				playerHealth.TakeDamage(1);
			}
		}
	}
}





