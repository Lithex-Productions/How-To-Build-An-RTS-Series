using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace LP.FDG.UI.HUD
{
    public class ActionFrame : MonoBehaviour
    {
        public static ActionFrame instance = null;

        [SerializeField] private Button actionButton = null;
        [SerializeField] private Transform layoutGroup = null;

        private List<Button> buttons = new List<Button>();

        private void Awake()
        {
            instance = this;
        }

        public void SetActionButtons(PlayerActions actions)
        {
            if (actions.basicUnits.Length > 0)
            {
                foreach(Units.BasicUnit unit in actions.basicUnits)
                {
                    Button btn = Instantiate(actionButton, layoutGroup);
                    btn.name = unit.name;
                    //add text etc?...
                    buttons.Add(btn);
                }
            }

            if (actions.basicBuildings.Length > 0)
            {
                foreach(Buildings.BasicBuilding building in actions.basicBuildings)
                {
                    Button btn = Instantiate(actionButton, layoutGroup);
                    btn.name = building.name;
                    //add text etc?...
                    buttons.Add(btn);
                }
            }
        }

        public void ClearActions()
        {
            foreach (Button btn in buttons)
            {
                buttons.Remove(btn);
                Destroy(btn);
            }
        }
    }
}

