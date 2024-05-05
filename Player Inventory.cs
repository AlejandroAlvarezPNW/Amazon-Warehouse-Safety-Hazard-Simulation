using System.Collections;
using UnityEngine;

public class Inventory
{
    //To Hold Items
    public ArrayList items = new ArrayList();


    //Adds Items to the player
    public void AddItem(GameObject item)
    {
        items.Add(item);
    }

    //Removes Items from the player
    public void RemoveItem(GameObject item)
    {
        items.Remove(item);
    }
}
