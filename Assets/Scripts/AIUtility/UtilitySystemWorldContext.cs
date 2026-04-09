using System;
using System.Collections.Generic;
using Node;
using UnityEngine;

namespace AIUtility
{
    public class UtilitySystemWorldContext: MonoBehaviour
    {

        public NodeManager nodeManager;
        
        public static UtilitySystemWorldContext Instance;

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
            }

            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        
        private void GetUnitsPerTeam()
        {
    
        }

        private void GetRelativeStrength()
        {

        }
        
    }
}