// CameraFollow.cs
// Handles smooth camera following behavior with scene persistence
// Uses FindWithTag for player detection and Vector3.Lerp for smooth movement
// Reference: Unity Documentation - Camera.LookAt, DontDestroyOnLoad
// Author: Makayla Hardware

using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraFollow : MonoBehaviour
{
	// Camera positioning offset relative to player
	public Vector3 offset = new Vector3(0, 5, -8);

	// Interpolation speed for smooth camera movement (0-1 range)
	public float smoothSpeed = 0.2f;

	// Reference to the player's transform
	private Transform player;

	// Singleton instance to prevent duplicate cameras across scenes
	private static CameraFollow instance;

	// Initialize singleton pattern and persist camera across scenes
	void Awake()
	{
		// Destroy duplicate camera instances
		if (instance != null)
		{
			Destroy(gameObject);
			return;
		}
		instance = this;
		DontDestroyOnLoad(gameObject); // Keep camera between scene loads
	}

	// Subscribe to scene loaded event
	void OnEnable()
	{
		SceneManager.sceneLoaded += OnSceneLoaded;
	}

	// Unsubscribe from scene loaded event to prevent memory leaks
	void OnDisable()
	{
		SceneManager.sceneLoaded -= OnSceneLoaded;
	}

	// Find player on initial scene load
	void Start()
	{
		FindPlayer();
	}

	// Re-find player when new scene loads
	void OnSceneLoaded(Scene scene, LoadSceneMode mode)
	{
		FindPlayer();
	}

	// Locate player GameObject by tag and cache its transform
	void FindPlayer()
	{
		GameObject playerObj = GameObject.FindWithTag("Player");
		if (playerObj != null)
		{
			player = playerObj.transform;
		}
	}

	// Update camera position after all other updates (prevents jitter)
	// Reference: Unity Documentation - LateUpdate execution order
	void LateUpdate()
	{
		// Exit early if no player found
		if (player == null) return;

		// Calculate target position using offset
		Vector3 desiredPosition = player.position + offset;

		// Smoothly interpolate between current and desired position
		transform.position = Vector3.Lerp(
			transform.position,
			desiredPosition,
			smoothSpeed
		);

		/*// Keep camera looking at player horizontally
		Vector3 lookTarget = player.position;
		lookTarget.y = transform.position.y; // Lock Y to prevent vertical tilting
		transform.LookAt(lookTarget);*/
	}
}




