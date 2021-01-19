using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
	private CharacterController characterController;

	private float currentXRotation = 0f;
	private Vector3 currentVelocity;

	public Camera PlayerCam;
	public float RotationSpeed = 100f;
	public float WalkingSpeed = 4f;
	public float RunningSpeed = 8f;
	public float JumpHeight = 1f;
	public float Gravity = -9.81f;


	// Start is called before the first frame update
	void Start()
	{
		characterController = GetComponent<CharacterController>();

		Cursor.lockState = CursorLockMode.Locked;
	}

	// Update is called once per frame
	void Update()
	{
		Rotate();
		Move();
	}

	private void Move()
	{
		bool isGrounded = characterController.isGrounded;
		if (isGrounded && currentVelocity.y < 0)
		{
			currentVelocity.y = 0f;
		}

		bool isRunning = Input.GetKey(KeyCode.LeftShift);

		float horizontalMove = Input.GetAxis("Horizontal");
		float verticalMove = Input.GetAxis("Vertical");

		Vector3 moveDirection = (transform.forward * verticalMove + transform.right * horizontalMove) * Time.deltaTime;

		if (isRunning)
		{
			moveDirection *= RunningSpeed;
		}
		else{
			moveDirection *= WalkingSpeed;
		}

		if (!isGrounded)
		{
			moveDirection *= 0.7f;
		}

		if (isGrounded && Input.GetButton("Jump"))
		{
			currentVelocity.y += JumpHeight * -0.8f * Gravity;
		}

		currentVelocity.y += 2f * Gravity * Time.deltaTime;

		characterController.Move(moveDirection + currentVelocity * Time.deltaTime);
	}

	private void Rotate()
	{
		float mouseX = Input.GetAxis("Mouse X") * RotationSpeed * Time.deltaTime;
		float mouseY = Input.GetAxis("Mouse Y") * RotationSpeed * Time.deltaTime;

		currentXRotation -= mouseY;
		currentXRotation = Mathf.Clamp(currentXRotation, -90f, 90f);

		PlayerCam.transform.localRotation = Quaternion.Euler(currentXRotation, 0f, 0f);

		transform.Rotate(Vector3.up * mouseX);
	}
}
