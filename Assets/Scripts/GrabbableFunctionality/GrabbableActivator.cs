using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabbableActivator : MonoBehaviour
{
    private GrabbableFactory grabbableFactory;

    // Start is called before the first frame update
    void Awake()
    {
        grabbableFactory = new GrabbableFactory();
    }

    // Update is called once per frame
    void Update()
    {
        //grabbableFactory.SetGrabbable(obj, true);
    }
}
