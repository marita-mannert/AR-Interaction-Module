using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Oculus.Interaction;

public class GrabbableStateMonitor : MonoBehaviour
{
    public UnityEvent OnGrab;
    public UnityEvent OnRelease;

    private Grabbable _grabbable;
    private bool _wasGrabbed;

    void Awake()
    {
        _grabbable = GetComponent<Grabbable>();
        if (_grabbable == null)
        {
            Debug.LogError("Grabbable component not found!");
        }
    }

    void Update()
    {
        if (_grabbable == null) return;

        bool isGrabbed = _grabbable.SelectingPointsCount > 0;

        if (isGrabbed && !_wasGrabbed)
        {
            OnGrab.Invoke();
        }
        else if (!isGrabbed && _wasGrabbed)
        {
            OnRelease.Invoke();
        }

        _wasGrabbed = isGrabbed;
    }
}
