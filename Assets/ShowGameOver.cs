using UnityEngine;

public class ShowGameOver : MonoBehaviour
{
	public float delay = 1.5f;

	void Start()
	{
		gameObject.SetActive(false);
		Invoke(nameof(Show), delay);
	}

	void Show()
	{
		gameObject.SetActive(true);
	}
}

