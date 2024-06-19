using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomRayInteractor : MonoBehaviour
{
    [Tooltip("The origin of the ray.")]
    [SerializeField]
    private Transform _rayOrigin;

    [Tooltip("The maximum length of the ray.")]
    [SerializeField]
    private float _maxRayLength = 5f;

    public Vector3 Origin { get; protected set; }
    public Quaternion Rotation { get; protected set; }
    public Vector3 Forward { get; protected set; }
    public Vector3 End { get; protected set; }

    public Ray Ray { get; protected set; }

    private void Awake()
    {
        if (_rayOrigin == null)
        {
            Debug.LogError("Ray Origin is not assigned!");
        }
    }

    private void Update()
    {
        DoPreprocess();
        DetectSelectableObjects();
    }

    private void DoPreprocess()
    {
        Origin = _rayOrigin.position;
        Rotation = _rayOrigin.rotation;
        Forward = Rotation * Vector3.forward;
        Ray = new Ray(Origin, Forward);
    }

    private void DetectSelectableObjects()
    {
        RaycastHit[] hits = Physics.RaycastAll(Ray, _maxRayLength);
        GameObject closestObject = null;
        float closestDist = float.MaxValue;

        foreach (RaycastHit hit in hits)
        {
            if (hit.collider.CompareTag("Selectable"))
            {
                float dist = hit.distance;
                if (dist < closestDist)
                {
                    closestDist = dist;
                    closestObject = hit.collider.gameObject;
                }
            }
        }

        if (closestObject != null)
        {
            Debug.Log("Closest Selectable Object: " + closestObject.name);
            End = Origin + closestDist * Forward;
        }
        else
        {
            End = Origin + _maxRayLength * Forward;
        }
    }
}

