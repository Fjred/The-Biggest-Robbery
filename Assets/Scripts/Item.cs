using UnityEngine;

[System.Serializable]
public class Item
{
    public string itemName;
    public Sprite itemImage;

    public Item(string name, Sprite image)
    {
        itemName = name;
        itemImage = image;
    }
}
