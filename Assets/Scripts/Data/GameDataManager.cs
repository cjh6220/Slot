using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDataManager : SingletonBase<GameDataManager>
{
    public SlotItemData[] ItemList;
    private void Start()
    {
        SetItemData();
    }

    public class ItemDatas
    {
        public SlotItemData[] datas;
    }

    void SetItemData()
    {
        var ta = Resources.Load<TextAsset>("JsonData/ItemData");
        ItemDatas itemDatas = JsonUtility.FromJson<ItemDatas>(ta.text);
        ItemList = itemDatas.datas;

        foreach (var item in itemDatas.datas)
        {
            Debug.LogError(item.name);
        }
    }

    public Sprite GetItemImg(string resName, bool isSpin)
    {
        Sprite img;
        if (!isSpin)
        {
            img = Resources.Load<Sprite>("ItemImg/" + resName);
        }
        else
        {
            img = Resources.Load<Sprite>("SpinImg/Symbol Blur " + resName);
        }

        if (img != null)
        {
            return img;
        }
        return null;
    }
}
