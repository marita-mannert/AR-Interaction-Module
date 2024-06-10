using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;
using Oculus.Interaction;
using Oculus.Interaction.HandGrab;

public class AssignScript : ScriptableWizard
{
    public bool addRotation = false; 
    public bool addGrabFunctionality = false;
    // public Rotation theRotationScript;
    String strHelp = "Select Game Objects";
    GameObject[] gos;


    void OnWizardUpdate()
    {
        helpString = strHelp;
        //isValid = (theRotationScript != null);
    }

    void OnWizardCreate()
    {
        gos = Selection.gameObjects;
        foreach (GameObject go in gos)
        {
            assignScript(go);
        }
    }

    void assignScript(GameObject go)
    {
        if (addRotation)
        {
            go.AddComponent<Rotation>();
        }

        if (addGrabFunctionality)
        {
            go.AddComponent<Grabbable>();
            go.AddComponent<HandGrabInteractable>();
            go.AddComponent<GrabInteractable>();
          
        } else
        {
            Grabbable grabbable = go.GetComponent<Grabbable>();
            DestroyImmediate(grabbable);

            HandGrabInteractable handGrabInteractable = go.GetComponent<HandGrabInteractable>();
            DestroyImmediate(handGrabInteractable);

            GrabInteractable grabInteractable = go.GetComponent<GrabInteractable>();
            DestroyImmediate(grabInteractable);
         
        }

    }

    
    [MenuItem("MyMenu/Assign Script", false)]
    static void assignscript()
    {
        ScriptableWizard.DisplayWizard("Assign Script", typeof(AssignScript), "Apply");
    }
}
