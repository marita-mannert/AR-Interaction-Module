using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GrabbableStateListener : MonoBehaviour
{
    public List<GrabbableStateMonitor> grabbableStateMonitors;
    private Renderer _renderer;
    private bool _isRed;


    void Start()
    {
        _renderer = GetComponent<Renderer>();
        if (_renderer == null)
        {
            Debug.LogError("Renderer component not found!");
        }
        FindChildrenWithGrabbableStateMonitor();
    }

    void OnEnable()
    {
        foreach (var monitor in grabbableStateMonitors)
        {
            if (monitor != null)
            {
                monitor.OnGrab.AddListener(HandleGrab);
            }
        }
    }

    void OnDisable()
    {
        foreach (var monitor in grabbableStateMonitors)
        {
            if (monitor != null)
            {
                monitor.OnGrab.RemoveListener(HandleGrab);
            }
        }
    }

    private void HandleGrab()
    {
        Debug.Log("Object grabbed");

        // Toggle the color between red and original
        if (_isRed)
        {
            _renderer.material.color = Color.white; // or the original color
        }
        else
        {
            _renderer.material.color = Color.red;
        }

        _isRed = !_isRed;
    }

    private void FindChildrenWithGrabbableStateMonitor()
    {
        GameObject[] selectableObjects = GameObject.FindGameObjectsWithTag("Selectable");

        foreach (GameObject parent in selectableObjects)
        {
            foreach (Transform child in parent.transform)
            {
                var grabbableMonitor = child.GetComponent<GrabbableStateMonitor>();
                if (grabbableMonitor != null)
                {
                    grabbableStateMonitors.Add(grabbableMonitor);
                }
            }
        }
    }
}



/*
 * public class GrabbableStateListener : MonoBehaviour
{
    public GrabbableStateMonitor grabbableStateMonitor;

    void OnEnable()
    {
        if (grabbableStateMonitor != null)
        {
            grabbableStateMonitor.OnGrab.AddListener(HandleGrab);
            grabbableStateMonitor.OnRelease.AddListener(HandleRelease);
        }
    }

    void OnDisable()
    {
        if (grabbableStateMonitor != null)
        {
            grabbableStateMonitor.OnGrab.RemoveListener(HandleGrab);
            grabbableStateMonitor.OnRelease.RemoveListener(HandleRelease);
        }
    }

    private void HandleGrab()
    {
        Debug.Log("Object grabbed");
        // Handle the grab state here
    }

    private void HandleRelease()
    {
        Debug.Log("Object released");
        // Handle the release state here
    }
}
 * 
 * 
 * 
 * */