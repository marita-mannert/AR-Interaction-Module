using UnityEngine;

public class ObjectMovementHandler : MonoBehaviour
{
    private float xMoveSpeed = 0.0f;
    private float yMoveSpeed = 0.0f;
    private float zMoveSpeed = 0.0f;

    public void UpdateMovement(Transform transform)
    {
        MoveInXDirection(transform, xMoveSpeed * Time.deltaTime);
        MoveInYDirection(transform, yMoveSpeed * Time.deltaTime);
        MoveInZDirection(transform, zMoveSpeed * Time.deltaTime);
    }

    private void MoveInXDirection(Transform transform, float amount)
    {
        transform.position = new Vector3(transform.position.x + amount, transform.position.y, transform.position.z);
    }

    private void MoveInYDirection(Transform transform, float amount)
    {
        transform.position = new Vector3(transform.position.x, transform.position.y + amount, transform.position.z);
    }

    private void MoveInZDirection(Transform transform, float amount)
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + amount);
    }

    public void ActivateUpMovement()
    {
        yMoveSpeed = 1.0f;
    }

    public void ActivateDownMovement()
    {
        yMoveSpeed = -1.0f;
    }

    public void ActivateLeftMovement()
    {
        xMoveSpeed = -1.0f;
    }

    public void ActivateRightMovement()
    {
        xMoveSpeed = 1.0f;
    }

    public void ActivateForwardsMovement()
    {
        zMoveSpeed = 1.0f;
    }

    public void ActivateBackwardsMovement()
    {
        zMoveSpeed = -1.0f;
    }

    public void DeactivateMovement()
    {
        xMoveSpeed = 0;
        yMoveSpeed = 0;
        zMoveSpeed = 0;
    }
}
