using System.Collections.Generic;
using UnityEngine;

public class Recipe
{   
    Dictionary<int, string> recipe = new Dictionary<int, string>();
    
    public void addRecipe(int id, string component)
    {
        recipe.Add(id, component);
    }

    public bool checkRecipe(int id)
    {
        if (recipe.ContainsKey(id))
        {
            Debug.Log("키 있음: " + recipe[id].ToString());
            return true;
        }
        return false;
    }

    public Dictionary<int, int> getComponent(int id)
    {
        if (!checkRecipe(id))
        {
            return null;
        }
        string[] recipeString = recipe[id].Split('/');
        Dictionary<int, int> component = new Dictionary<int, int>();

        for (int i = 0; i < recipeString.Length; i++)
        {
            string[] str = recipeString[i].Split(',');
            component.Add(int.Parse(str[0]), int.Parse(str[1]));
        }
        return component;
    }
}
