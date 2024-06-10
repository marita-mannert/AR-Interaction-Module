using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Oculus.Interaction;
using Oculus.Interaction.HandGrab;

public class GrabbableScriptManager : MonoBehaviour

{
    private GameObject interactionGrab;
    private Rigidbody newRigidbody;
    private GameObject otherObject;
        

    // Start is called before the first frame update
    void Start()
    {
        otherObject = GameObject.Find("CubeMoreTest");
        newRigidbody = otherObject.GetComponent<Rigidbody>();

        interactionGrab = GameObject.Find("InteractionGrab");

      

    }

    // Update is called once per frame
    void Update()
    {
        if (newRigidbody != null && Input.GetKeyDown(KeyCode.DownArrow))
        {

            // Get the Grabbable component attached to the InteractionGrab GameObject
            Grabbable targetGrabbable = interactionGrab.GetComponent<Grabbable>();
            HandGrabInteractable handGrabInteractable = interactionGrab.GetComponent<HandGrabInteractable>();
            if (targetGrabbable != null && handGrabInteractable != null)
            {
                // Inject the new Rigidbody
                targetGrabbable.InjectOptionalRigidbody(newRigidbody);
                handGrabInteractable.InjectRigidbody(newRigidbody);
                //handGrabInteractable.InjectOptionalPointableElement(otherObject.GetComponent<Grabbable>());
                Debug.Log("Rigidbody has been set successfully.");
            }
            else
            {
                Debug.LogError("Grabbable component not found on the InteractionGrab GameObject.");
            }
        }
        else
        {
            Debug.LogError("Please ensure both interactionGrab and newRigidbody are assigned.");
        }

    }
}
