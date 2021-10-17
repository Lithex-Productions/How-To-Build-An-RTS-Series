using UnityEngine;

namespace LP.FDG.Interactables
{
    public class IBuilding : Interactable
    {
        public UI.HUD.PlayerActions actions;

        public override void OnInteractEnter()
        {
            UI.HUD.ActionFrame.instance.SetActionButtons(actions, gameObject);
            base.OnInteractEnter();
        }

        public override void OnInteractExit()
        {
            UI.HUD.ActionFrame.instance.ClearActions();
            base.OnInteractExit();
        }
    }
}

