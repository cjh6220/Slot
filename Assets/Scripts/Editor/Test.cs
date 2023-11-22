using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    public class ItemData
    {
        public string name;
        public ItemType itemType;
        public int lv;
        public string res;
    }

    public enum ItemType
    {
        None = 0,
        Alcohol,
        Fruit,
    }

    public List<ItemData> ItemList = new List<ItemData>();

    void Start()
    {
        SetItemData();
    }

    void SetItemData()   
    {
        var item_1 = new ItemData();
        item_1.name = "물";
        item_1.itemType = ItemType.Alcohol;
        item_1.lv = 1;
        item_1.res = "Alcohol_1";
        ItemList.Add(item_1);

        var item_11 = new ItemData();
        item_11.name = "낑깡";
        item_11.itemType = ItemType.Fruit;
        item_11.lv = 1;
        item_11.res = "Fruit_1";
        ItemList.Add(item_11);
    }
}
