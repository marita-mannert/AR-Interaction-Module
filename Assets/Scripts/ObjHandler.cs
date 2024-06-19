using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjHandler : MonoBehaviour
{
    private GameObject[] movableObjects;
    private ObjectMovementHandler movementHandler;

    void Start()
    {
      //  movableObjects = GameObject.FindGameObjectsWithTag("Selectable");
        movementHandler = new ObjectMovementHandler();
      
    }

    void Update()
    {
        UpdateAllMovements();
    }

  

    private void UpdateAllMovements()
    {
        //foreach (GameObject obj in movableObjects)
        foreach (GameObject obj in UnitSelections.Instance.unitSelected)
        {
            movementHandler.UpdateMovement(obj.transform);
        }
    }

    private void ActivateUpMovement()
    {
        movementHandler.ActivateUpMovement();
    }

    private void ActivateDownMovement()
    {
        movementHandler.ActivateDownMovement();
    }

    private void ActivateLeftMovement()
    {
        movementHandler.ActivateLeftMovement();
    }

    private void ActivateRightMovement()
    {
        movementHandler.ActivateRightMovement();
    }

    private void ActivateForwardsMovement()
    {
        movementHandler.ActivateForwardsMovement();
    }

    private void ActivateBackwardsMovement()
    {
        movementHandler.ActivateBackwardsMovement();
    }

    private void StopMovement()
    {
        movementHandler.DeactivateMovement();
    }
}
