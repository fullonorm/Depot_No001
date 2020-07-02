using System.Collections.Generic;
using UnityEngine;

public class CombinationController: MonoBehaviour
{
    public int selectedItemId;
    private ItemBox itemBox;
    private Recipe recipe;

    private void Start()
    {
        recipe = new Recipe();
        recipe.addRecipe(1, "201,2/202,5");
        recipe.addRecipe(2, "1,3/204,2/ 205,1");
        recipe.addRecipe(3, "2,2/204,9/205,9");
        recipe.addRecipe(101, "201,10");
        recipe.addRecipe(102, "101,1/201,20");
        recipe.addRecipe(103, "102,1/201,100");

        itemBox = GameManager.GetInstance().itemBox;
    }

    public Recipe getRecipe()
    {
        return recipe;
    }

    public Dictionary<int, int> getRecipeComponent()
    {
        return recipe.getComponent(selectedItemId);
    }

    public void setItemId(int ID)
    {
        selectedItemId = ID;
        Debug.Log(selectedItemId);
    }
 
    public void combination()
    {
        process(Operation.COMBINATION);
    }

    public void decombination()
    {
        process(Operation.DECOMBINATION);
    }
    
    private void process(Operation operation)
    {
        Dictionary<int, int> component = recipe.getComponent(selectedItemId);

        int loss = 0;
        if (operation == Operation.DECOMBINATION)
        {
            loss = -1;
        }

        foreach (KeyValuePair<int, int> item in component)
        {
            Debug.Log(item.Key + ", " + item.Value);
            itemBox.ModifyItem(item.Key, -(int)operation * (item.Value + loss));
        }
        itemBox.ModifyItem(selectedItemId, (int)operation);
    }

    public bool checkCombination()
    {   
        Dictionary<int, int> component = recipe.getComponent(selectedItemId);
        foreach (KeyValuePair<int, int> item in component)
        {
            int amount = itemBox.GetItemAmount(item.Key);
            Debug.Log(item.Key + ", " + item.Value);
            if (item.Value > amount)
                return false;

            if (item.Key <= 200 && amount <= 1)
                return false;
        }
        return true;
    }

    public bool checkDecombination()
    {
        if (itemBox.GetItemAmount(selectedItemId) > 1)
            return true;

        return false;
    }

    public enum Operation
    {
        COMBINATION = 1,
        DECOMBINATION = -1
    }

}
