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

    Dictionary<Rarity, int> rarityWeightDictionary = new Dictionary<Rarity, int>();
    [Serializable]
    public class RarityWeight
    {
        public Rarity rarity;
        public int weight;
    }
    
    [SerializeField] private List<RarityWeight> rarityWeights;
    private void Start()
    {
        itemDataDictionary = itemData.ToDictionary((data => data.ItemType));
        rarityWeightDictionary = rarityWeights.ToDictionary((data => data.rarity), (data => data.weight));
    }


    public Items.Item CreateItem(Items.ItemType itemType)
    {
        Items.Item item = null;
        Items.ItemFunctionBase itemPersonFunction = null;
        ItemData itemData = itemDataDictionary[itemType];

        string className = "Items.ItemFunctions." + itemType.ToString();
        Type artPersonDataType = Type.GetType(className, true);
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

    private int GetWeightSum()
    {
        int sum = 0;
        foreach (var keyValuePair in rarityWeightDictionary)
        {
            sum += keyValuePair.Value;
        }

        return sum;
    }

    private Rarity GetRandomRarity()
    {
        int totalWeights = GetWeightSum();
        Debug.Log($"total Weights: {totalWeights}");
        int randomWeight = UnityEngine.Random.Range(0, totalWeights);
        Debug.Log($"random Weight: {randomWeight}");

        int currentWeight = 0;
        Rarity randomRarity = Rarity.NONE;
        foreach (var keyValuePair in rarityWeightDictionary)
        {
            Debug.Log($"current Weight: {currentWeight}");
            Debug.Log($"Checking: {keyValuePair.Key} with weight: {keyValuePair.Value}");
            currentWeight += keyValuePair.Value;
            if (currentWeight > randomWeight)
            {
                randomRarity = keyValuePair.Key;
                Debug.Log("Random Rarity is " + randomRarity);
                break;
            }
        }
        Debug.Log($"random Rarity: {randomRarity}");

        if (randomRarity == Rarity.NONE)
        {
            Debug.LogError("We have a problem here");
        }
        return randomRarity;
    }
    
    public Items.Item CreateRandomItem()
    {
        Rarity rarity = GetRandomRarity();
        Debug.Log("Random rarity is " + rarity);
        List<ItemData> itemDatas = itemData.Where((data => data.Rarity == rarity)).ToList();
       Debug.Log(itemDatas.ToFormattedString());
        int randomData = UnityEngine.Random.Range(0, itemDatas.Count);
        return CreateItem(itemDatas[randomData].ItemType);
    }
}