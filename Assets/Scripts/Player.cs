using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HideAndSeek
{
    public class Player
    {
        public GameObject PlayerObject{get; private set;}
        public int Points{get; set;}

        public bool IsAlive{get; set;}

        public string Name{get; set;}

        public Player(GameObject playerObject, string name)
        {
            PlayerObject = playerObject;
            Points = 0;
            IsAlive = true;
            Name = name;
        }

    }
}
