using UnityEngine;

namespace HideAndSeek
{
    public class PlayerMovement : MonoBehaviour
    {
        private CharacterController characterController;

        private float currentXRotation = 0f;
        private Vector3 currentVelocity;
        private Vector3[] scanDirections;
		private float normalHeight;
		private float croucheHeight;

        public Camera PlayerCam;
        public float RotationSpeed = 100f;
		public float WalkingSpeed = 5f;
		public float RunningSpeed = 10f;
		public float SneakingSpeed = 2f;
        public float JumpHeight = 1.1f;
        public float Gravity = -9.81f;



        // Start is called before the first frame update
        void Start()
        {
			characterController = GetComponent<CharacterController>();
			normalHeight = transform.localScale.y;
			croucheHeight = normalHeight * 3f/4f;
			InitUpwardsCollisionSystem();

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
			
			// Player Inputs
			bool isRunning = Input.GetKey(KeyCode.LeftShift);
			bool isSneaking = Input.GetKey(KeyCode.LeftControl);
			bool isCrouching = Input.GetKey(KeyCode.C);

			float horizontalMove = Input.GetAxis("Horizontal");
			float verticalMove = Input.GetAxis("Vertical");


			if (isGrounded && currentVelocity.y < 0f)
			{
				currentVelocity.y = 0f;
			}

			// Handeling player speed and direction
			Vector3 moveDirection = (transform.forward * verticalMove + transform.right * horizontalMove) * Time.deltaTime;
			
			if (isRunning && !isCrouching)
			{
				moveDirection *= RunningSpeed;
			}
			else if(isSneaking)
			{
				moveDirection *= SneakingSpeed;
			}
			else
			{
				moveDirection *= WalkingSpeed;
			}

			if (!isGrounded)
			{
				moveDirection *= 0.7f;
			}


			// Changing Player Height while Crouching
			if (isCrouching && transform.localScale.y != croucheHeight)
			{
				transform.localScale = new Vector3(transform.localScale.x, croucheHeight, transform.localScale.z);
				
				characterController.enabled = false;
				int dir = isGrounded ? -1 : 1;
				transform.position += Vector3.up * normalHeight/4f * dir;
				characterController.enabled = true;
			}
			if (!isCrouching && transform.localScale.y != normalHeight && !checkUpwardsCollisions())
			{
				characterController.enabled = false;
				int dir = isGrounded ? 1 : -1;
				transform.position += Vector3.up * normalHeight/4f * dir;
				characterController.enabled = true;

				transform.localScale = new Vector3(transform.localScale.x, normalHeight, transform.localScale.z);
			}
			
			// Check if crouching through height not through button, to change speed
			if (transform.localScale.y == croucheHeight)
			{
				moveDirection *= 0.5f;
			}

			// Handeling jumps, gravity and head collisions
			if (isGrounded && Input.GetButton("Jump")) // TODO: Check for Slope Angle to avoid jumping further on a slope. Consider sliding down to steep slopes. [Check hit.normal on func OnControllerColliderHit]
			{
				currentVelocity.y -= JumpHeight/1.55f * Gravity;
			}

			currentVelocity.y += 2f * Gravity * Time.deltaTime;

			if (checkUpwardsCollisions() && currentVelocity.y > 0f)
			{
				currentVelocity.y = 0f;
			}

			// Move the player
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

		private bool checkUpwardsCollisions()
		{
			RaycastHit hit;
			Vector3 headPoint = transform.position + Vector3.up * (characterController.height/2 - characterController.radius);
			foreach(Vector3 dir in scanDirections)
			{
				if (Physics.Raycast(headPoint, dir, out hit, 2*JumpHeight))
				{
					// Debug.DrawRay(headPoint, hit.point - headPoint, Color.red);
					if (hit.distance <=  characterController.radius + 0.1f)
					{
						return true;
					}
				}
			}
			return false;
		}

		private void InitUpwardsCollisionSystem()
		{
			scanDirections = new Vector3[5] {
				Vector3.up,
				new Vector3(0.5f, 1f, 0f),
				new Vector3(-0.5f, 1f, 0f),
				new Vector3(0f, 1f, 0.5f),
				new Vector3(0f, 1f, -0.5f)
				};
		}
	}
}