using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Oculus.Interaction;

public class CustomRayInteractor : MonoBehaviour
{
    [Tooltip("The origin of the ray.")]
    [SerializeField]
    private Transform _rayOrigin;

    [Tooltip("The maximum length of the ray.")]
    [SerializeField]
    private float _maxRayLength = 5f;

    [Tooltip("The IndexPinchSelector to detect pinch gestures.")]
    [SerializeField]
    private IndexPinchSelector _indexPinchSelector;

    [Tooltip("The material to apply when an object is hovered over.")]
    [SerializeField]
    private Material _hoverMaterial;

    [Tooltip("The line renderer to draw the line from the ray origin to the detected object.")]
    [SerializeField]
    private LineRenderer _lineRenderer;

    private Material _originalMaterial;
    private GameObject _hoveredObject;
    private GameObject _selectedObject;

    public Vector3 Origin { get; protected set; }
    public Quaternion Rotation { get; protected set; }
    public Vector3 Forward { get; protected set; }
    public Vector3 End { get; protected set; }

    public Ray Ray { get; protected set; }

    private bool _isPinching;
    private bool _pinchHandled;

    private void Awake()
    {
        if (_rayOrigin == null)
        {
            Debug.LogError("Ray Origin is not assigned!");
        }

        if (_indexPinchSelector == null)
        {
            Debug.LogError("IndexPinchSelector is not assigned!");
        }
        else
        {
            _indexPinchSelector.WhenSelected += HandlePinchSelected;
            _indexPinchSelector.WhenUnselected += HandlePinchUnselected;
        }

        if (_hoverMaterial == null)
        {
            Debug.LogError("Hover Material is not assigned!");
        }

        if (_lineRenderer == null)
        {
            Debug.LogError("Line Renderer is not assigned!");
        }
    }

    private void OnDestroy()
    {
        if (_indexPinchSelector != null)
        {
            _indexPinchSelector.WhenSelected -= HandlePinchSelected;
            _indexPinchSelector.WhenUnselected -= HandlePinchUnselected;
        }
    }

    private void Update()
    {
        if (_rayOrigin == null || _indexPinchSelector == null || _lineRenderer == null)
        {
            Debug.LogError("Ray Origin, IndexPinchSelector, or Line Renderer is not assigned!");
            return;
        }

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

            // Change material on hover
            if (closestObject != _hoveredObject)
            {
                ResetHoveredObject();
                SetHoveredObject(closestObject);
            }

            // Draw line from origin to the object
            DrawLineToTarget(End);

            // Check for pinch gesture
            if (_isPinching && !_pinchHandled)
            {
                Debug.Log("Ray hit and pinch gesture detected.");
                // Add or remove the object from the selection list
                if (UnitSelections.Instance != null)
                {
                    _selectedObject = closestObject;
                    UnitSelections.Instance.ClickSelect(_selectedObject);
                    _pinchHandled = true;
                }
                else
                {
                    Debug.LogError("UnitSelections instance is null.");
                }
            }
        }
        else
        {
            End = Origin + _maxRayLength * Forward;
            ResetHoveredObject();
            _lineRenderer.enabled = false; // Disable the line renderer if no object is detected
        }
    }

    private void SetHoveredObject(GameObject obj)
    {
        _hoveredObject = obj;
        Renderer renderer = _hoveredObject.GetComponent<Renderer>();
        if (renderer != null)
        {
            _originalMaterial = renderer.material;
            renderer.material = _hoverMaterial;
        }
    }

    private void ResetHoveredObject()
    {
        if (_hoveredObject != null)
        {
            Renderer renderer = _hoveredObject.GetComponent<Renderer>();
            if (renderer != null && _originalMaterial != null)
            {
                renderer.material = _originalMaterial;
            }
            _hoveredObject = null;
            _originalMaterial = null;
        }
    }

    private void HandlePinchSelected()
    {
        _isPinching = true;
        _pinchHandled = false;
    }

    private void HandlePinchUnselected()
    {
        _isPinching = false;
        _pinchHandled = false;
    }

    private void DrawLineToTarget(Vector3 targetPosition)
    {
        _lineRenderer.enabled = true;
        _lineRenderer.SetPosition(0, Origin);
        _lineRenderer.SetPosition(1, targetPosition);
    }
}
