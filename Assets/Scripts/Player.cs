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
        transform.position += moveDirection * moveSpeed * Time.deltaTime;

        isWalking = moveDirection != Vector3.zero;

        float rotationSpeed = 10f;
        transform.forward = Vector3.Slerp(transform.forward, moveDirection, Time.deltaTime * rotationSpeed);

    }

    public bool IsWalking => isWalking;

}
