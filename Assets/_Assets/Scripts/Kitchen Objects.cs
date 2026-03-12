using UnityEngine;

public class KitchenObjects : MonoBehaviour
{
    [SerializeField] private KitchenObjectSO kitchenObjectSO;

    private IKitchenObjectParent kitchenObjectParent;
    public KitchenObjectSO GetKitchenObjectSO()
    {
     return kitchenObjectSO; 
    }
    public void SetKitchenObjectParent(IKitchenObjectParent kitchenObjectParent)
    {
        if (this.kitchenObjectParent != null)
        {
            this.kitchenObjectParent.ClearKitchenObjects();
        }
        this.kitchenObjectParent = kitchenObjectParent;
        if (this.kitchenObjectParent.HasKitchenObject())
        {
            Debug.LogError("Counter has something on it");
        }
        kitchenObjectParent.SetKitchenObjects(this);


        transform.parent = kitchenObjectParent.GetKitchenObjectFollowTransform();
        transform.localPosition = Vector3.zero;
    }
    public IKitchenObjectParent GetKitchenObjectParent()
    {
        return kitchenObjectParent;
    }

    public void DestroySelf()
    {
       kitchenObjectParent.ClearKitchenObjects();
        Destroy(gameObject);
    }


    public static KitchenObjects SpawnKitchenObject(KitchenObjectSO kitchenObjectSO,IKitchenObjectParent kitchenObjectParent)
    {
        Transform kitchenObjectTransform = Instantiate(kitchenObjectSO.prefab);
        KitchenObjects kitchenObjects = kitchenObjectTransform.GetComponent<KitchenObjects>();
        kitchenObjects.SetKitchenObjectParent(kitchenObjectParent);

        return kitchenObjects;
    }
}
