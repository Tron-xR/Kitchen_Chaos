using UnityEngine;

public interface IKitchenObjectParent 
{
    public Transform GetKitchenObjectFollowTransform();
   
    public void SetKitchenObjects(KitchenObjects kitchenObjects);

    public KitchenObjects GetKitchenObjects();

    public void ClearKitchenObjects();

    public bool HasKitchenObject();
    
}
