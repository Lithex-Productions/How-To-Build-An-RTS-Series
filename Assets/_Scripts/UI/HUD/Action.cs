using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LP.FDG.UI.HUD
{
    public class Action : MonoBehaviour
    {
        public void OnClick()
        {
            ActionFrame.instance.StartSpawnTimer(name);
        }
    }
}

