using UnityEngine;
using TMPro;

public class PlayerHealth : MonoBehaviour
{
	public int maxHealth = 3;
	public int currentHealth;

	public Transform spawnPoint;
	public TMP_Text healthText;

	public float invincibilityTime = 1.5f;
	private bool canTakeDamage = true;

	void Start()
	{
		currentHealth = maxHealth;
		UpdateHealthUI();
	}

	public void TakeDamage(int amount)
	{
		if (!canTakeDamage) return;

		currentHealth -= amount;
		if (currentHealth < 0) currentHealth = 0;

		UpdateHealthUI();

		if (currentHealth <= 0)
		{
			Respawn();
		}
		else
		{
			StartCoroutine(DamageCooldown());
		}
	}

	void Respawn()
	{
		currentHealth = maxHealth;
		UpdateHealthUI();

		transform.position = spawnPoint.position;
		transform.rotation = spawnPoint.rotation;

		Rigidbody rb = GetComponent<Rigidbody>();
		if (rb != null)
		{
			rb.linearVelocity = Vector3.zero;
			rb.angularVelocity = Vector3.zero;
		}

		StartCoroutine(DamageCooldown());
	}

	System.Collections.IEnumerator DamageCooldown()
	{
		canTakeDamage = false;
		yield return new WaitForSeconds(invincibilityTime);
		canTakeDamage = true;
	}

	void UpdateHealthUI()
	{
		if (healthText != null)
			healthText.text = "Health: " + currentHealth;
	}
}



