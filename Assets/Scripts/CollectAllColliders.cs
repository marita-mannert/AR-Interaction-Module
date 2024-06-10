using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Oculus.Interaction.PoseDetection;

public class CollectAllColliders : MonoBehaviour
{

    private ColliderContainsHandJointActiveState containsHandJointActiveState;
    private GameObject[] allColliders;

    // Start is called before the first frame update
    void Start()
    {
        allColliders = GameObject.FindGameObjectsWithTag("Selectable");
        for (int i = 0; i <allColliders.Length; i++)
        {
            containsHandJointActiveState.InjectEntryColliders(allColliders[i].GetComponents<BoxCollider>());
            containsHandJointActiveState.InjectExitColliders(allColliders[i].GetComponents<BoxCollider>());
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
