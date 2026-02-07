// DoorUnlock.cs
// Checks if player has enough score to unlock door and load next level
// Uses TextMeshPro for UI and coroutines for timed messages
// Author: Makayla Hardware

using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections;

public class DoorUnlock : MonoBehaviour
{
	public int requiredScore = 3;          // how many points needed to unlock and since its public it can be changed in inspector
	public string nextSceneName;           // scene to load when unlocked
	public TextMeshProUGUI doorMessage;    // UI text for "not enough points" message

	void Start()
	{
		// hide message initially
		if (doorMessage != null)
			doorMessage.gameObject.SetActive(false);
	}

	// trigger when player walks into door
	void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Player"))
		{
			// check if player has enough score
			if (ScoreManager.instance.score >= requiredScore)
			{
				// loads next level
				SceneManager.LoadScene(nextSceneName);
			}
			else
			{
				// show "locked" message
				StartCoroutine(ShowMessage());
			}
		}
	}

	// displays message for 2 seconds then hide it
	IEnumerator ShowMessage()
	{
		if (doorMessage != null)
		{
			doorMessage.gameObject.SetActive(true);
			yield return new WaitForSeconds(2f);
			doorMessage.gameObject.SetActive(false);
		}
	}
}





