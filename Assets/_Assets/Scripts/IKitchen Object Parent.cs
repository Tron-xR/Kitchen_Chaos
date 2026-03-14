using UnityEngine;

public interface IKitchenObjectParent 
{
    public Transform GetKitchenObjectFollowTransform();
   
    public void SetKitchenObjects(KitchenObjects kitchenObjects);

    public KitchenObjects GetKitchenObject();

    public void ClearKitchenObjects();

    public bool HasKitchenObject();
    
}
