﻿using UnityEngine;

namespace LP.LDG.UI
{
    public class UIBuildingEnabler : MonoBehaviour
    {
        [SerializeField]
        private GameObject PotentionalBuilding;

        public void PlacePotentionalBuilding()
        {
            Debug.Log("Button Was Pressed");
            Instantiate(PotentionalBuilding, transform.position, transform.rotation);
            PotentionalBuilding.active = true;
            Debug.Log("Building Enabled");
        }
    }
}