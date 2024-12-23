using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 7f;

    private bool isWalking;


    [SerializeField] private GameInputs gameInputs;


    void Update()
    {
        Vector2 inputVector = gameInputs.GetMovementVectorNormalized();
        Vector3 moveDirection = new Vector3(inputVector.x, 0 , inputVector.y);

        float playerHeigth = 2f;
        float playerRadius = .65f;
        float movementDistance = moveSpeed * Time.deltaTime;
        bool canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeigth, playerRadius, moveDirection, movementDistance);

        if (!canMove)
        {
            Vector3 moveDirectionX = new Vector3(inputVector.x, 0, 0);
            canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeigth, playerRadius, moveDirectionX, movementDistance);

            if (canMove)
            {
                moveDirection = moveDirectionX.normalized;
            }
            else
            {
                Vector3 moveDirectionZ = new Vector3(0, 0, inputVector.y);
                canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeigth, playerRadius, moveDirectionZ, movementDistance);
                moveDirection = moveDirectionZ.normalized;

            }
        }


        if (canMove)
        {
            transform.position += moveDirection * movementDistance;
        }

        isWalking = moveDirection != Vector3.zero;

        float rotationSpeed = 10f;
        transform.forward = Vector3.Slerp(transform.forward, moveDirection, Time.deltaTime * rotationSpeed);

    }

    public bool IsWalking => isWalking;

}
