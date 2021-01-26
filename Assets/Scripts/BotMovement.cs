using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HideAndSeek
{
    public class BotMovement : MonoBehaviour
    {

        private CharacterController characterController;

        private Vector3 currentVelocity;

        public float RotationSpeed = 100f;
		public float PlayerAcceleration = 10f;
		public float WalkingSpeed = 4f;
		public float RunningSpeed = 8f;
		public float JumpHeight = 1.1f;
		public float Gravity = -9.81f;

        bool catched = false;

        // Start is called before the first frame update
        void Start()
		{
			characterController = GetComponent<CharacterController>();
			
			characterController.detectCollisions = true;
		}

        // Update is called once per frame
        void Update()
        {
            if(!catched)
            {
                Move();
                
            }
            else
            {
                Death();
            }
        }

        private void Move()
		{
			bool isGrounded = characterController.isGrounded;
			if (isGrounded && currentVelocity.y < 0)
			{
				currentVelocity.y = 0f;
			}

			bool isRunning = false;

			float horizontalMove = Random.Range(-1f, 1f);
			float verticalMove = Random.Range(-1f, 1f) + 0.5f;

			Vector3 moveDirection = (transform.forward * verticalMove + transform.right * horizontalMove) * Time.deltaTime;

			if (isRunning)
			{
				moveDirection *= RunningSpeed;
			}
			else
			{
				moveDirection *= WalkingSpeed;
			}

			if (!isGrounded)
			{
				moveDirection *= 0.7f;
			}

			if (isGrounded && Random.Range(0f, 100f) >= 99f)
			{
				currentVelocity.y += Mathf.Sqrt(-1f * Gravity * JumpHeight);
			}

			currentVelocity.y += Gravity * Time.deltaTime;

			characterController.Move(moveDirection + currentVelocity * Time.deltaTime);
			
		}

        private void Death()
        {
            bool isGrounded = characterController.isGrounded;
			if (isGrounded && currentVelocity.y < 0)
			{
				currentVelocity.y = 0f;
                return;
			}

            if (!isGrounded)
			{
				currentVelocity.y += Gravity * Time.deltaTime;
			}
            
            characterController.Move(currentVelocity * Time.deltaTime);
        }

        public void Catch(){
            catched = true;
        }
    }
}
