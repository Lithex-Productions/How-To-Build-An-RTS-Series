using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LP.FDG.UI.HUD
{
    [CreateAssetMenu(fileName = "NewPlayerActions", menuName = "PlayerActions")]
    public class PlayerActions : ScriptableObject
    {
        [Space(5)]
        [Header("Units")]
        public Units.BasicUnit[] basicUnits = new Units.BasicUnit[0];

        [Space(5)]
        [Header("Buildings")]
        [Space(15)]
        public Buildings.BasicBuilding[] basicBuildings = new Buildings.BasicBuilding[0];
    }
}

