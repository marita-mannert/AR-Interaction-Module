using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Oculus.Interaction;
public class testScript : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject interactionGrab;
    public Rigidbody newRigidbody;
    public GameObject otherObject;

    void Start()
    {
        otherObject = GameObject.Find("CubeTest");
        otherObject.GetComponent<MeshRenderer>().material.SetColor("_Color", Color.blue);

        interactionGrab = GameObject.Find("InteractionGrab");
        newRigidbody = otherObject.GetComponent<Rigidbody>();

         if (newRigidbody != null)
        {
            // Get the Grabbable component attached to the InteractionGrab GameObject
            Grabbable targetGrabbable = interactionGrab.GetComponent<Grabbable>();
            if (targetGrabbable != null)
            {
                // Inject the new Rigidbody
                targetGrabbable.InjectOptionalRigidbody(newRigidbody);
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

    // Update is called once per frame
    void Update()
    {
        Debug.Log("AAA");
    }
}
