using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LP.FDG.Units.Player;
using UnityEngine.EventSystems;

namespace LP.FDG.InputManager
{
    public class InputHandler : MonoBehaviour
    {
        public static InputHandler instance;

        private RaycastHit hit; //what we hit with our ray

        public List<Transform> selectedUnits = new List<Transform>();
        public Transform selectedBuilding = null;

        public LayerMask interactableLayer = new LayerMask();

        private bool isDragging = false;

        private Vector3 mousePos;

        private void Awake()
        {
            instance = this;
        }

        private void OnGUI()
        {
            if (isDragging)
            {
                Rect rect = MultiSelect.GetScreenRect(mousePos, Input.mousePosition);
                MultiSelect.DrawScreenRect(rect, new Color(0f, 0f, 0f, 0.25f));
                MultiSelect.DrawScreenRectBorder(rect, 3, Color.blue);
            }
        }

        public void HandleUnitMovement()
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (EventSystem.current.IsPointerOverGameObject())
                {
                    return;
                }

                mousePos = Input.mousePosition;
                //create a ray 
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                // check if we hit something
                if (Physics.Raycast(ray, out hit, 100, interactableLayer))
                {
                    if (addedUnit(hit.transform, Input.GetKey(KeyCode.LeftShift)))
                    {
                        // be able to do stuff with units
                    }
                    else if (addedBuilding(hit.transform))
                    {
                        // be able to do stuff with building
                    }
                }
                else
                {
                    isDragging = true;
                    DeselectUnits();
                }
            }

            if (Input.GetMouseButtonUp(0))
            {
                foreach (Transform child in Player.PlayerManager.instance.playerUnits)
                {
                    foreach (Transform unit in child)
                    {
                        if (isWithinSelectionBounds(unit))
                        {
                            addedUnit(unit, true);
                        }
                    }
                }
                isDragging = false;
            }

            if (Input.GetMouseButtonDown(1) && HaveSelectedUnits())
            {
                //create a ray 
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                // check if we hit something
                if (Physics.Raycast(ray, out hit))
                {
                    // if we do, then do something with that data
                    LayerMask layerHit = hit.transform.gameObject.layer;

                    switch (layerHit.value)
                    {
                        case 8: // Units Layer
                            // do something  
                            break;
                        case 9: // enemy units layer
                            // attack or set target
                            break;
                        default: // if none of the above happens
                            // do something
                            foreach (Transform unit in selectedUnits)
                            {
                                PlayerUnit pU = unit.gameObject.GetComponent<PlayerUnit>();
                                pU.MoveUnit(hit.point);
                            }
                            break;
                    }
                }
            }
        }

        private void DeselectUnits()
        {
            if (selectedBuilding != null)
            {
                selectedBuilding.gameObject.GetComponent<Interactables.IBuilding>().OnInteractExit();
                selectedBuilding = null;
            }
            if (selectedUnits.Count > 0)
            {
                for (int i = 0; i < selectedUnits.Count; i++)
                {
                    selectedUnits[i].gameObject.GetComponent<Interactables.IUnit>().OnInteractExit();
                }
                selectedUnits.Clear();
            }
        }

        private bool isWithinSelectionBounds(Transform tf)
        {
            if (!isDragging)
            {
                return false;
            }

            Camera cam = Camera.main;
            Bounds vpBounds = MultiSelect.GetVPBounds(cam, mousePos, Input.mousePosition);
            return vpBounds.Contains(cam.WorldToViewportPoint(tf.position));
        }

        private bool HaveSelectedUnits()
        {
            if (selectedUnits.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private Interactables.IUnit addedUnit(Transform tf, bool canMultiselect = false)
        {
            Interactables.IUnit iUnit = tf.GetComponent<Interactables.IUnit>();
            if (iUnit)
            {
                if (!canMultiselect)
                {
                    DeselectUnits();
                }

                selectedUnits.Add(iUnit.gameObject.transform);

                iUnit.OnInteractEnter();

                return iUnit;
            }
            else
            {
                return null;
            }
        }

        private Interactables.IBuilding addedBuilding(Transform tf)
        {
            Interactables.IBuilding iBuilding = tf.GetComponent<Interactables.IBuilding>();

            if (iBuilding)
            {
                Debug.Log(iBuilding.gameObject.name);
                DeselectUnits();
                
                selectedBuilding = iBuilding.gameObject.transform;

                selectedBuilding.gameObject.GetComponent<Interactables.IBuilding>().OnInteractEnter();

                return iBuilding;
            }
            else
            {

                return null;
            }
        }
    }
}

