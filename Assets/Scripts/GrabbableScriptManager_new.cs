using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Oculus.Interaction;
using Oculus.Interaction.HandGrab;

public class GrabbableScriptManager_new : MonoBehaviour

{
    private GameObject interactionGrab;
    private Rigidbody newRigidbody;
    private GameObject otherObject;

    private GrabFreeTransformer _grabFreeTransformer;
    



    // Start is called before the first frame update
    void Start()
    {
        otherObject = GameObject.Find("CubeTest");
        newRigidbody = otherObject.GetComponent<Rigidbody>();

        interactionGrab = GameObject.Find("InteractionGrab");
  

    }

    // Update is called once per frame
    void Update()
    {
          
            Debug.Log("Space pressed");

        if (Input.GetKeyDown(KeyCode.Q))
        {
            addGrabInteraction(otherObject);
        }
        else
        {
            Debug.LogError("Please ensure both interactionGrab and newRigidbody are assigned.");
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            DestroyImmediate(otherObject.GetComponent<Grabbable>());
            DestroyImmediate(otherObject.GetComponent<HandGrabInteractable>());
           // DestroyImmediate(otherObject.GetComponent<GrabInteractable>());
            DestroyImmediate(otherObject.GetComponent<GrabFreeTransformer>());
            DestroyImmediate(otherObject.GetComponent<MoveTowardsTargetProvider>());
        }


    }

    void addGrabInteraction(GameObject o)
    {
    
        o.AddComponent<GrabFreeTransformer>();
        _grabFreeTransformer = o.GetComponent<GrabFreeTransformer>();
        TransformerUtils.ScaleConstraints newScaleConstraints = new TransformerUtils.ScaleConstraints
        {
            ConstraintsAreRelative = true,
            XAxis = new TransformerUtils.ConstrainedAxis
            {
                ConstrainAxis = true,
                AxisRange = new TransformerUtils.FloatRange { Min = 0.5f, Max = 2.0f }
            },
            YAxis = new TransformerUtils.ConstrainedAxis
            {
                ConstrainAxis = true,
                AxisRange = new TransformerUtils.FloatRange { Min = 0.5f, Max = 2.0f }
            },
            ZAxis = new TransformerUtils.ConstrainedAxis
            {
                ConstrainAxis = true,
                AxisRange = new TransformerUtils.FloatRange { Min = 0.5f, Max = 2.0f }
            },
        };

        _grabFreeTransformer.InjectOptionalScaleConstraints(newScaleConstraints);

        o.AddComponent<Grabbable>();
        o.GetComponent<Grabbable>().InjectOptionalRigidbody(otherObject.GetComponent<Rigidbody>());
        o.GetComponent<Grabbable>().InjectOptionalTargetTransform(otherObject.GetComponent<Transform>());
        o.GetComponent<Grabbable>().InjectOptionalOneGrabTransformer(_grabFreeTransformer);
        o.GetComponent<Grabbable>().InjectOptionalTwoGrabTransformer(_grabFreeTransformer);

        o.AddComponent<HandGrabInteractable>();
        // o.AddComponent<GrabInteractable>();


        o.GetComponent<HandGrabInteractable>().enabled = true;
        o.GetComponent<HandGrabInteractable>().InjectOptionalPointableElement(otherObject.GetComponent<Grabbable>());
        o.GetComponent<HandGrabInteractable>().InjectRigidbody(otherObject.GetComponent<Rigidbody>());
        // o.GetComponent<GrabInteractable>().InjectRigidbody(otherObject.GetComponent<Rigidbody>());
    }

}
