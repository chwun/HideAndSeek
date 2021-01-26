using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HideAndSeek
{
    public class PlayerManager : MonoBehaviour
    {

        private GameManager gameManager;

        bool IsSeeker = false;

        void Awake()
        {
            gameManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
        }

        void Start()
        {
            IsSeeker = true;
        }

        void OnTriggerEnter(Collider other)
        {
            if (IsSeeker)

            {
                if (other.gameObject.tag == "HidingPlayer")
                {
                    gameManager.Catch(this.gameObject, other.gameObject);
                }
            }
        }
    }
}
