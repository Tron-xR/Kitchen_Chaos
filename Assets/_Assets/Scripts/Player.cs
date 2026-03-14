using System;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.EventSystems;

public class Player : MonoBehaviour ,IKitchenObjectParent
{
    public static Player Instance { get; private set; }
    public event EventHandler <OnSelectedCounterChangedEventsArgs> OnSelectedCounterChanged;
    public class OnSelectedCounterChangedEventsArgs : EventArgs
    {
        public BaseCounter selectedCounter;
    }

    [SerializeField] private float movSpeed = 10f;
    [SerializeField] private GameInput gameInput;
    [SerializeField] private LayerMask counter;
    [SerializeField] Transform kitchenObjectHoldPoint;

    private bool isWalking;
    private Vector3 lastMovDire;
    private BaseCounter selectedCounter;
    private KitchenObjects kitchenObjects;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("more than one player");
        }
        Instance = this;
    }

    private void Start()
    {
        gameInput.OnInteraction += GameInput_OnInteraction;
        gameInput.OnInteractionAlternate += GameInput_OnInteractionAlternate;
    }

     

    private void GameInput_OnInteraction(object sender, System.EventArgs e)
    {
        if (selectedCounter != null)
        {
            selectedCounter.Interact(this);
        }
    }
    private void GameInput_OnInteractionAlternate(object sender, System.EventArgs e)
    {
        if (selectedCounter != null)
        {
            selectedCounter.InteractAlternate(this);
        }
    }

    private void Update()
    {
       HandleMovement();
       HandleInteractions();

    }
    private void HandleInteractions()
    {
        Vector2 inputVector = gameInput.GetMovementVectorNormalized();

        Vector3 moveDire = new Vector3(inputVector.x, 0f, inputVector.y);
        float rayCastLen = 2f;

        if (moveDire != Vector3.zero)
        {
            lastMovDire = moveDire;
        }

        RaycastHit rayCastHit;
        if (Physics.Raycast(transform.position, lastMovDire, out rayCastHit, rayCastLen))
        {
            if (rayCastHit.transform.TryGetComponent(out BaseCounter baseCounter))
            {
                if (baseCounter != selectedCounter)
                {
                    SetSelectedCounter(baseCounter);
                }
            }
            else
            {
                SetSelectedCounter(null);
            }
        }
        else
        {
            SetSelectedCounter(null);
        }
    }
    private void HandleMovement()
    {
        Vector2 inputVector = gameInput.GetMovementVectorNormalized();

        Vector3 moveDire = new Vector3(inputVector.x, 0f, inputVector.y);
        float playerRadius = 0.7f;
        float playerHeight = 2f;
        float moveDistance = movSpeed * Time.deltaTime;
        bool canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDire, moveDistance);
        if (!canMove)
        {
            Vector3 moveDireX = new Vector3(moveDire.x, 0f, 0f);
            canMove = moveDire.x !=0 && !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDireX, moveDistance);
            if (canMove)
            {
                moveDire = moveDireX;
            }
            else
            {
                Vector3 moveDireZ = new Vector3(0f, 0f, moveDire.z);
                canMove = moveDire.z !=0 && !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDireZ, moveDistance);
                if (canMove)
                {
                    moveDire = moveDireZ;
                }
            }

        }
        if (canMove)
        {
            transform.position += moveDire * moveDistance;
        }
        float rotationSpeed = 10f;

        transform.forward = Vector3.Slerp(transform.forward, moveDire, Time.deltaTime * rotationSpeed);
        isWalking = moveDire != Vector3.zero;
    }
    public bool IsWalking()
    {
        return isWalking; 
    }
    private void SetSelectedCounter(BaseCounter selectedCounter)
    {
        this.selectedCounter = selectedCounter;

        OnSelectedCounterChanged?.Invoke(this, new OnSelectedCounterChangedEventsArgs
        {
             selectedCounter = selectedCounter
        });
    }
    public Transform GetKitchenObjectFollowTransform()
    {
        return kitchenObjectHoldPoint;
    }
    public void SetKitchenObjects(KitchenObjects kitchenObjects)
    {
        this.kitchenObjects = kitchenObjects;
    }
    public KitchenObjects GetKitchenObject()
    {
        return kitchenObjects;
    }
    public void ClearKitchenObjects()
    {
        kitchenObjects = null;
    }
    public bool HasKitchenObject()
    {
        return kitchenObjects != null;
    }
}
