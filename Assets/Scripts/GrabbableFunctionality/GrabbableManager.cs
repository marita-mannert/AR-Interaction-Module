using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabbableManager : MonoBehaviour
{
    [SerializeField]
    private string grabbableTag = "Selectable"; // Assign objects in the inspector

    private GrabbableFactory grabbableFactory;

    private void Awake()
    {
        grabbableFactory = new GrabbableFactory();
    }

    private void Start()
    {
        MakeObjectsGrabbable();
    }

    private void Update()
    {
        GameObject[] grabbableObjects = GameObject.FindGameObjectsWithTag(grabbableTag);
        if (Input.GetKeyDown(KeyCode.G)) // Left mouse button click
        {
            foreach (GameObject obj in grabbableObjects)
            {
                SetChildrenGrabbable(obj, "HandGrabbable", true);
            }
        }
    }

    private void MakeObjectsGrabbable()
    {
        GameObject[] grabbableObjects = GameObject.FindGameObjectsWithTag(grabbableTag);

        foreach (GameObject obj in grabbableObjects)
        {
            SetChildrenGrabbable(obj, "HandGrabbable", true);
        }
    }

    private void SetChildrenGrabbable(GameObject parent, string childName, bool grabbable)
    {
        foreach (Transform child in parent.transform)
        {
            if (child.name == childName)
            {
                grabbableFactory.MakeGrabbable(child.gameObject);
                grabbableFactory.SetGrabbable(child.gameObject, grabbable);
            }
        }
    }

}
