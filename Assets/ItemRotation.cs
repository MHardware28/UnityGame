/*using UnityEngine;

public class ItemRotation : MonoBehaviour
{
	public float MoveSpeed = 10f;
	public float RotateSpeed = 75f;

	private float _vInput;
	private float _hInput;
	private Rigidbody _rb;

	void Start()
	{
			_rb = GetComponent<Rigidbody>();
			if (_rb == null)
				Debug.LogError("Rigidbody not found! Please attach a Rigidbody component.");

	}

	void Update()
	{
		_vInput = Input.GetAxis("Vertical");
		_hInput = Input.GetAxis("Horizontal");
	}

	void FixedUpdate()
	{
		// 2
		//Vector3 rotation = Vector3.up * _hInput;
		// 3
		//Quaternion angleRot = Quaternion.Euler(rotation *
			//Time.fixedDeltaTime);
		// 4
		// 5
	
	//_rb.MovePosition(this.transform.position +
	//this.transform.forward* _vInput * Time.fixedDeltaTime);
	//_rb.MoveRotation(_rb.rotation* angleRot);

		//Forward movement (soft jello push)
		_rb.AddForce(transform.forward * _vInput * MoveSpeed);

		//Rotation using physics
		Quaternion turnOffset = Quaternion.Euler(0, _hInput * RotateSpeed * Time.fixedDeltaTime, 0);
		_rb.MoveRotation(_rb.rotation * turnOffset);
	}
}*/

using UnityEngine;

public class ItemBehavior : MonoBehaviour
{

	void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Player"))
		{
			Debug.Log("Item collected!");
			Destroy(gameObject);
		}
	}

}




