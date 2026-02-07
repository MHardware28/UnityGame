// Coin.cs
// Collectible coin that spins and adds points when picked up
// Uses OnTriggerEnter for pickup detection
// Author: Makayla Hardware

using UnityEngine;

public class Coin : MonoBehaviour
{
	public int pointsWorth = 1;        // points added when collected
	public float rotationSpeed = 90f;  // how fast coin spins

	void Update()
	{
		// spins around Y-axis for visual effect
		transform.Rotate(0, rotationSpeed * Time.deltaTime, 0);
	}

	// player picks up coin
	void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Player"))
		{
			// adds points to score
			ScoreManager.instance.AddPoints(pointsWorth);

			// removes coin from scene
			Destroy(gameObject);
		}
	}
}


