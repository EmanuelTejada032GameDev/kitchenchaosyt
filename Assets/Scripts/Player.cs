using System;
using UnityEngine;

public class Player : MonoBehaviour, IKitchenObjectParent
{
    public static Player Instance { get; private set; }

    [SerializeField] private float moveSpeed = 7f;
    private bool isWalking;

    [SerializeField] private GameInputs gameInputs;

    Vector3 lastInteractionDir = new Vector3 (0, 0, 0);
    [SerializeField] private LayerMask clearCounterLayerMask;

    private ClearCounter selectedCounter;
    public EventHandler<OnSelectCounterChangedEventArgs> OnSelectedCounterChanged;
    public class OnSelectCounterChangedEventArgs
    {
        public ClearCounter selectedCounter;
    };

    [SerializeField] private KitchenObject kitchenObject;
    [SerializeField] private Transform kitchenObjectHoldPoint;


    private void Awake()
    {
        if(Instance != null)
        {
            throw new Exception("There is more than one player instance");
        }
        Instance = this;
    }

    private void Start()
    {
        gameInputs.OnInteractAction += GameInputs_OnInteractAction;
    }

    private void GameInputs_OnInteractAction(object sender, System.EventArgs e)
    {
        if (selectedCounter != null) { 
            selectedCounter.Interact(this);
        }
    }

    void Update()
    {
        HandleMovement();
        HandleInteractions();
    }

    private void HandleInteractions()
    {
        Vector2 inputVector = gameInputs.GetMovementVectorNormalized();
        Vector3 moveDirection = new Vector3(inputVector.x, 0, inputVector.y);
        float interactionDistance = 2f;

        if(moveDirection != Vector3.zero)
        {
            lastInteractionDir = moveDirection;
        }
        
        if (Physics.Raycast(transform.position, lastInteractionDir, out RaycastHit hitInfo, interactionDistance, clearCounterLayerMask))
        {
            if(hitInfo.transform.TryGetComponent(out ClearCounter clearCounter))
            {
                if(clearCounter != selectedCounter)
                {
                    SetSelectedCounter(clearCounter);
                }
            }else
            {
                SetSelectedCounter(null);
            }
        }
        else
        {
            SetSelectedCounter(null); 
        }
    }

    public bool IsWalking => isWalking;


    private void HandleMovement()
    {
        Vector2 inputVector = gameInputs.GetMovementVectorNormalized();
        Vector3 moveDirection = new Vector3(inputVector.x, 0, inputVector.y);

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

    private void SetSelectedCounter(ClearCounter selectedCounter)
    {
        this.selectedCounter = selectedCounter;
        OnSelectedCounterChanged?.Invoke(this, new OnSelectCounterChangedEventArgs { selectedCounter = selectedCounter });
    }


    public Transform GetKitchenObjectFollowTransform()
    {
        return kitchenObjectHoldPoint;
    }


    public void SetKitchenObject(KitchenObject kitchenObject)
    {
        this.kitchenObject = kitchenObject;
    }

    public KitchenObject GetKitchenObject()
    {
        return kitchenObject;
    }

    public void ClearKitchenObject()
    {
        kitchenObject = null;
    }

    public bool HasKitchenObject => kitchenObject != null;
}
