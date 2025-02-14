using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Items;
using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;

public class ItemLibrary : MonoBehaviour
{
    [FormerlySerializedAs("artPersonData")] [SerializeField] List<ItemData> itemData;

    Dictionary<Items.ItemType, ItemData> itemDataDictionary =
        new Dictionary<Items.ItemType, ItemData>();

    private void Start()
    {
        itemDataDictionary = itemData.ToDictionary((data => data.ItemType));
    }


    public Items.Item CreateItem(Items.ItemType itemType)
    {
        Items.Item item = null;
        Items.ItemFunctionBase itemPersonFunction = null;
        ItemData itemData = itemDataDictionary[itemType];
        Debug.Log($"ART PERSON TYPE TO STRING: " + itemType.ToString());

        string className = "Items.ItemFunctions." + itemType.ToString();
        Type artPersonDataType = Type.GetType(className, true);
        Debug.Log($"ART PERSON TYPE: " + className);
        try
        {
            itemPersonFunction = (Items.ItemFunctionBase)(Activator.CreateInstance(artPersonDataType));
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return null;
        }

        itemPersonFunction.SetEffectData(itemData);

        item = new Items.Item(itemPersonFunction);
        item.SetArtPersonData(itemData);

        return item;
    }
}