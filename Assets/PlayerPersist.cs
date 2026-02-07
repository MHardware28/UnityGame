// PlayerPersist.cs
// Keeps player object alive between scene loads using singleton pattern
// Reference: Unity Documentation - DontDestroyOnLoad
// Author: Makayla Hardware

using UnityEngine;

public class PlayerPersist : MonoBehaviour
{
	private static PlayerPersist instance;  // singleton reference

	void Awake()
	{
		// destroys duplicate players when loading new scenes
		if (instance != null)
		{
			Destroy(gameObject);
			return;
		}

		instance = this;
		DontDestroyOnLoad(gameObject);  // keeps the player across scenes
	}
}

