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
    
    Dictionary<Rarity, int> rarityPriceDictionary = new Dictionary<Rarity, int>();
    [Serializable]
    public class RarityIntPair
    {
        public Rarity rarity;
        public int weight;
    }
    
    [SerializeField] private List<RarityIntPair> rarityWeights;
    [SerializeField] private List<RarityIntPair> rarityPrices;
    private void Start()
    {
        itemDataDictionary = itemData.ToDictionary((data => data.ItemType));
        rarityWeightDictionary = rarityWeights.ToDictionary((data => data.rarity), (data => data.weight));
        rarityPriceDictionary = rarityPrices.ToDictionary((data => data.rarity), (data => data.weight));
    }


    public Items.Item CreateItem(Items.ItemType itemType)
    {
        Items.Item item = null;
        Items.ItemFunctionBase itemPersonFunction = null;
        ItemData itemData = itemDataDictionary[itemType];

        string className = "Items.ItemFunctions." + itemType.ToString();
        Type itemTypeConcat = Type.GetType(className, true);
        try
        {
            itemPersonFunction = (Items.ItemFunctionBase)(Activator.CreateInstance(itemTypeConcat));
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return null;
        }

        itemPersonFunction.SetEffectData(itemData);

        item = new Items.Item(itemPersonFunction);
        item.SetArtPersonData(itemData);
        item.Price = rarityPrices.First((pair => pair.rarity == itemData.Rarity)).weight;

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
        int randomWeight = UnityEngine.Random.Range(0, totalWeights);

        int currentWeight = 0;
        Rarity randomRarity = Rarity.NONE;
        foreach (var keyValuePair in rarityWeightDictionary)
        {
            currentWeight += keyValuePair.Value;
            if (currentWeight > randomWeight)
            {
                randomRarity = keyValuePair.Key;
                break;
            }
        }

        if (randomRarity == Rarity.NONE)
        {
            Debug.LogError("We have a problem here");
        }
        return randomRarity;
    }
    
    public Items.Item CreateRandomItem(Func<ItemData, bool> filter)
    {
        Rarity rarity = GetRandomRarity();
        //Get Item of Rarity where filter is true and is not basic
        List<ItemData> itemDatas = itemData.Where((data => data.Rarity == rarity && filter(data) && !data.IsBasic)).ToList();
       if (itemDatas.Count == 0)
       {
           return null;
       }
        int randomData = UnityEngine.Random.Range(0, itemDatas.Count);
        return CreateItem(itemDatas[randomData].ItemType);
    }
    
    public Items.Item CreateRandomItem(Rarity rarity)
    {
        List<ItemData> itemDatas = itemData.Where((data => data.Rarity == rarity)).ToList();
        int randomData = UnityEngine.Random.Range(0, itemDatas.Count);
        return CreateItem(itemDatas[randomData].ItemType);
    }
    
    public Item CreateRandomItem(ItemClass itemClass)
    {
        return CreateRandomItem((x) => x.ItemClass == itemClass);
    }

    public Item CreateRandomItem()
    {
        return CreateRandomItem((x) => true);
    }
}