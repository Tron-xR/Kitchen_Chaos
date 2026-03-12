using UnityEngine;

public class BaseCounter : MonoBehaviour,IKitchenObjectParent
{
    [SerializeField] Transform counterTopPoint;


    private KitchenObjects kitchenObjects;


    public virtual void Interact(Player player)
    {

    }
    public virtual void InteractAlternate(Player player)
    {
       
    }
    public Transform GetKitchenObjectFollowTransform()
    {
        return counterTopPoint;
    }
    public void SetKitchenObjects(KitchenObjects kitchenObjects)
    {
        this.kitchenObjects = kitchenObjects;
    }
    public KitchenObjects GetKitchenObjects()
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
