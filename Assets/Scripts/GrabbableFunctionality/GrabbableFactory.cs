using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Oculus.Interaction;
using Oculus.Interaction.HandGrab;

public class GrabbableFactory : MonoBehaviour
{
    private GrabFreeTransformer _grabFreeTransformer;

    public void MakeGrabbable(GameObject obj)
    {
        AddGrabFreeTranformerComponent(obj);
        // Configure the components if necessary
        ConfigureGrabFreeTransformer(obj);
        AddGrabbableComponent(obj);
        AddHandGrabInteractable(obj);
        //AddGrabbableStateMonitor(obj);
    }

    private void AddGrabFreeTranformerComponent(GameObject obj)
    {
        if ( obj.GetComponent<GrabFreeTransformer>() == null)
        {
            obj.AddComponent<GrabFreeTransformer>();
        }
    }

    private void AddGrabbableComponent (GameObject obj)
    {
       if (obj.GetComponent<Grabbable>() == null)
        {
            obj.AddComponent<Grabbable>();
            obj.GetComponent<Grabbable>().InjectOptionalRigidbody(obj.transform.GetComponentInParent<Rigidbody>());
            obj.GetComponent<Grabbable>().InjectOptionalTargetTransform(obj.transform.parent);

            obj.GetComponent<Grabbable>().InjectOptionalOneGrabTransformer(_grabFreeTransformer);
            obj.GetComponent<Grabbable>().InjectOptionalTwoGrabTransformer(_grabFreeTransformer);
           
        }
    }

    private void AddHandGrabInteractable(GameObject obj)
    {
        if (obj.GetComponent<HandGrabInteractable>() == null)
        {
            obj.AddComponent<HandGrabInteractable>();

            obj.GetComponent<HandGrabInteractable>().enabled = true;
            obj.GetComponent<HandGrabInteractable>().InjectOptionalPointableElement(obj.GetComponent<Grabbable>());
            obj.GetComponent<HandGrabInteractable>().InjectRigidbody(obj.transform.GetComponentInParent<Rigidbody>());
        }  
    }

    private void ConfigureGrabFreeTransformer(GameObject obj)
    {
        _grabFreeTransformer = obj.GetComponent<GrabFreeTransformer>();
        // Example configuration
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
    }

    public void AddGrabbableStateMonitor (GameObject obj)
    {
        if ( obj.GetComponent<GrabbableStateMonitor>() == null)
        {
            obj.AddComponent<GrabbableStateMonitor>();
        }
    }

    public void SetGrabbable(GameObject obj, bool isGrabbable)
    {
        // Enable or disable the grabbable functionality

        var grabble = obj.GetComponent<Grabbable>();
        if (grabble != null)
        {
            grabble.enabled = isGrabbable;
        }

        var handGrabInteractable = obj.GetComponent<HandGrabInteractable>();
        if (handGrabInteractable != null)
        {
            handGrabInteractable.enabled = isGrabbable;
        }

    }
}
