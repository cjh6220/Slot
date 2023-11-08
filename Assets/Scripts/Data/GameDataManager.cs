using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDataManager : SingletonBase<GameDataManager>
{
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

        foreach (var item in itemDatas.datas)
        {
            Debug.LogError(item.name);
        }
    }

}
