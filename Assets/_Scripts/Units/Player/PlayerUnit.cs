using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

namespace LP.FDG.Units.Player
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class PlayerUnit : MonoBehaviour
    {
        private NavMeshAgent navAgent;

        public UnitStatTypes.Base baseStats;

        private void OnEnable()
        {
            navAgent = GetComponent<NavMeshAgent>();
        }

        public void MoveUnit(Vector3 _destination)
        {
            navAgent.SetDestination(_destination);
        }
    }
}

