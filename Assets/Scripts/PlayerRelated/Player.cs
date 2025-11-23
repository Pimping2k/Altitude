using System;
using UnityEngine;

namespace PlayerRelated
{
    public class Player : MonoBehaviour
    {
        public static Player Instance;
        
        public PlayerController Controller {get; private set;}
        
        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        private void Start()
        {
            Controller = GetComponent<PlayerController>();
        }
    }
}