using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RaycastController : MonoBehaviour
{
    [SerializeField]
    private float raycastDistance = 5.0f;
    [SerializeField] 
    private float sphereRadius = 3.0f;

    [SerializeField]
    //The layer that will determine what the raycast will hit
    LayerMask layerMask;
    //The UI text component that will display the name of the interactable hit
    public TextMeshProUGUI interactionInfo;

    // Update is called once per frame
    private void Update()
    {
        //TODO: Raycast
        //1. Perform a raycast originating from the gameobject's position towards its forward direction.
        //   Make sure that the raycast will only hit the layer specified in the layermask

        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, raycastDistance, layerMask))
        {
            //2. Check if the object hits any Interactable. If it does, set the text of the UI
            //   to the id of the Interactable hit. When the player interacts with the Interactable, 
            //   check first if the Interactable is an Item that can be added to the inventory. If it is, 
            //   make sure to perform the PickUp behaviour, otherwise just perform the Interact behaviour of the 
            //   Interactable component.
            if (hit.collider.gameObject.TryGetComponent<Interactable>(out Interactable interactable))
            {
                //call the interact function of the interactable class
                interactionInfo.gameObject.SetActive(true);
                interactionInfo.text = interactable.id;
                if (Input.GetMouseButtonDown(0))
                {
                    interactable.Interact();
                }
            }
        }
        else
        {
            interactionInfo.gameObject.SetActive(false);
        }

        //Or you also have the option to use a sphere cast instead
        
        /*
        if (Physics.SphereCast(transform.position, sphereRadius, transform.forward, out hit, raycastDistance, layerMask))
        {
            //do something
            Debug.Log("Raycast hits " + hit.collider.gameObject.name);
            // Convert the world position to screen space
            Vector2 screenPosition = Camera.main.WorldToScreenPoint(hit.point);
            interactionInfo.gameObject.SetActive(true);
            // Set the UI element's position based on the screen position
            interactionInfo.transform.position = screenPosition;
            //if we press the mouse button to interact,
            if (Input.GetMouseButtonDown(0))
            {
                //interact with the object if it is an interactable
                if (hit.collider.gameObject.TryGetComponent<Interactable>(out Interactable interactable))
                {
                    //call the interact function of the interactable class
                    interactable.Interact();
                }
            }
        }
        */
   
    }
}