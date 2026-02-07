using UnityEngine;


/*public class BoundaryMove : MonoBehaviour
{
	public float speed = 3f;
	public float xLimit = 5f;
	public float zLimit = 5f;

	void Update()
	{
		float moveX = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
		float moveZ = Input.GetAxis("Vertical") * speed * Time.deltaTime;

		Vector3 newPos = transform.position + new Vector3(moveX, 0, moveZ);

		// Keep capsule inside boundaries
		newPos.x = Mathf.Clamp(newPos.x, -xLimit, xLimit);
		newPos.z = Mathf.Clamp(newPos.z, -zLimit, zLimit);

		transform.position = newPos;
	}
}
*/

using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
	public float speed = 5f;
	public float jumpForce = 5f;

	private Rigidbody rb;
	private bool isGrounded;

	void Start()
	{
		rb = GetComponent<Rigidbody>();
		rb.freezeRotation = true; // prevents tipping over
	}

	void Update()
	{
		float moveX = Input.GetAxis("Horizontal");
		float moveZ = Input.GetAxis("Vertical");

		// Calculate movement vector in world space
		Vector3 move = new Vector3(moveX, 0, moveZ) * speed;

		// Set Rigidbody velocity (physics-aware, respects walls)
		rb.linearVelocity = new Vector3(move.x, rb.linearVelocity.y, move.z);

		// Jump
		if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
		{
			rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
			isGrounded = false;
		}
	}

	void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.CompareTag("Ground"))
			isGrounded = true;
	}
}





