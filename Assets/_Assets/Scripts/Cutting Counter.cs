using System;
using UnityEngine;

public class CuttingCounter : BaseCounter
{
    public event EventHandler<OnProgressChangedEventArgs> OnProgressChanged;
    public class OnProgressChangedEventArgs : EventArgs
    {
        public float progressNormalized;
    }

    public event EventHandler OnCut;

    [SerializeField] private CuttingRecipeSO[] cutKitchenRecipesSOArray;

    private int cuttingProgress;
    public override void Interact(Player player)
    {
        if (!HasKitchenObject())
        {
            if (player.HasKitchenObject())
            {
                if (HasRecipeWithInput(player.GetKitchenObjects().GetKitchenObjectSO()))
                {
                    player.GetKitchenObjects().SetKitchenObjectParent(this);
                    CuttingRecipeSO cuttingRecipeSO = GetCuttingRecipeSOWithInput(GetKitchenObjects().GetKitchenObjectSO());

                    cuttingProgress = 0;

                    OnProgressChanged?.Invoke(this, new OnProgressChangedEventArgs
                    {
                        progressNormalized = (float)cuttingProgress / cuttingRecipeSO.cuttingProgressMax
                    }); 
                }
            }
            else
            {

            }

        }
        else
        {
            if (player.HasKitchenObject())
            {

            }
            else
            {
                GetKitchenObjects().SetKitchenObjectParent(player);
            }

        }

    }

    public override void InteractAlternate(Player player)
    {
        if(HasKitchenObject() && HasRecipeWithInput(GetKitchenObjects().GetKitchenObjectSO()))
        {
            cuttingProgress++;
            OnCut?.Invoke(this, EventArgs.Empty);

            CuttingRecipeSO cuttingRecipeSO = GetCuttingRecipeSOWithInput(GetKitchenObjects().GetKitchenObjectSO());
            OnProgressChanged?.Invoke(this, new OnProgressChangedEventArgs
            {
                progressNormalized = (float)cuttingProgress / cuttingRecipeSO.cuttingProgressMax
            });
            
            if (cuttingProgress >= cuttingRecipeSO.cuttingProgressMax)
            {
                //cut
                KitchenObjectSO outputKitchenObjectSO = GetInputOfOutput(GetKitchenObjects().GetKitchenObjectSO());
                GetKitchenObjects().DestroySelf();
                KitchenObjects.SpawnKitchenObject(outputKitchenObjectSO, this);
            }
            
        }
    }

    private bool HasRecipeWithInput(KitchenObjectSO inputKitchenObjectSO)
    {
        CuttingRecipeSO cuttingRecipeSO = GetCuttingRecipeSOWithInput(inputKitchenObjectSO);
        return cuttingRecipeSO != null;
    }

    private KitchenObjectSO GetInputOfOutput(KitchenObjectSO inputKitchenObjectSO)
    {

        CuttingRecipeSO cuttingRecipeSO = GetCuttingRecipeSOWithInput(inputKitchenObjectSO);

        if (cuttingRecipeSO != null)
        {
            return cuttingRecipeSO.output;
        }
        else
        {
            return null;
        }
    }

        private CuttingRecipeSO GetCuttingRecipeSOWithInput(KitchenObjectSO inputKitchenObjectSO)
        {
            foreach (CuttingRecipeSO cuttingRecipeSO in cutKitchenRecipesSOArray)
            {
                if (cuttingRecipeSO.input == inputKitchenObjectSO)
                {
                    return cuttingRecipeSO;
                }
            }

        return null;
    }
}
