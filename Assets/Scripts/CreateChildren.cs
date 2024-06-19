using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateChildren : MonoBehaviour
{
    [SerializeField]
    private string grabbableTag = "Selectable"; // Assign objects in the inspector

    [SerializeField]
    private string[] childNames = { "HandGrabbable", "RayInteractable" }; // Array of child names

    void Awake()
    {
        CreateChildrenObjects();
    }

    private void CreateChildrenObjects()
    {
        GameObject[] grabbableObjects = GameObject.FindGameObjectsWithTag(grabbableTag);

        foreach (GameObject obj in grabbableObjects)
        {
            foreach (string childName in childNames)
            {
                GameObject goChild = new GameObject(childName);
                goChild.transform.SetParent(obj.transform);
                goChild.transform.localPosition = Vector3.zero; // Adjust as necessary
                goChild.transform.localScale = Vector3.one;
            }
        }
    }
}
