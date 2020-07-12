using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//removing mono will make this into a custom class where the 
//stat information will show up on the inspector
[System.Serializable]
public class Stat
{
    //this will be the starting value for the given stat,
    //we dont want to access this directly, set in the inspection, and use a method.
    [SerializeField]
    private int baseValue;

    //----Here from PlayerStats.cs----
    // we create a new list called modifiers wich will be = to a new list of <int>
    private List<int> modifiers = new List<int>();

    //here is how we add modifiers like the armour/damage/health.
    public int GetValue()
    {
        //we start off by saying that finalValue is = to basevalue
        int finalValue = baseValue;
        //
        modifiers.ForEach(x => finalValue += x);
        return finalValue;
    }

    //here we create the method called AddModifier which takes in the int we just created called modifiers
    public void AddModifier (int modifier)
    {
        //here want to check that the modifier is not equal to 0, so there is anubmer to add
        if (modifier != 0)
            //then we simple add the modifier feeding in the int modifier
            modifiers.Add(modifier);
    }

    //same thing as add but for removing modifiers
    public void RemoveModifier (int modifier)
    {
        if (modifier != 0)
            modifiers.Remove(modifier);
    }

}
