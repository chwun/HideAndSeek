using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
	private CharacterController characterController;

	private float currentXRotation = 0f;

	public Camera PlayerCam;
	public float RotationSpeed;
	public float MoveSpeed;


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
		float horizontalMove = Input.GetAxis("Horizontal");
		float verticalMove = Input.GetAxis("Vertical");

		Vector3 move = transform.forward * verticalMove + transform.right * horizontalMove;
		characterController.Move(move * MoveSpeed * Time.deltaTime);
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
