using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddGrabFunctionality : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        gameObject.AddComponent<OVRGrabbable>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
