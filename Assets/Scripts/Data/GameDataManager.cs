using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameDataManager : SingletonBase<GameDataManager>
{
    public SlotItemData[] ItemList;
    public LineRateData[] LineRates = new LineRateData[5];
    public CalRandom _calRandom;
    
    private void Start()
    {
        SetItemData();
        SetRateData();
        
        _calRandom = this.gameObject.AddComponent<CalRandom>();
        // var test = _calRandom.GetRandom(LineRates[0]);
        // for (int i = 0; i < test.Count; i++)
        // {
        //     Debug.LogError(test[i]);
        // }
    }

    public class ItemDatas
    {
        public SlotItemData[] datas;
    }

    [Serializable]
    public class LineRateData
    {
        public LineData[] Line = new LineData[7];
    }

    [Serializable]
    public class LineData
    {
        public int id;
        public float rate;

        public LineData(int Id, float Rate)
        {
            id = Id;
            rate = Rate;
        }
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

    void SetRateData()
    {
        var line_1 = new LineRateData();

        var line_1_1 = new LineData(1, 5f);
        line_1.Line[0] = line_1_1;

        var line_1_2 = new LineData(2, 10f);
        line_1.Line[1] = line_1_2;

        var line_1_3 = new LineData(3, 15f);
        line_1.Line[2] = line_1_3;

        var line_1_4 = new LineData(4, 20f);
        line_1.Line[3] = line_1_4;

        var line_1_5 = new LineData(5, 20f);
        line_1.Line[4] = line_1_5;

        var line_1_6 = new LineData(6, 20f);
        line_1.Line[5] = line_1_6;

        var line_1_7 = new LineData(7, 10f);
        line_1.Line[6] = line_1_7;

        LineRates[0] = line_1;

        var line_2 = new LineRateData();

        var line_2_1 = new LineData(1, 5f);
        line_2.Line[0] = line_2_1;

        var line_2_2 = new LineData(2, 10f);
        line_2.Line[1] = line_2_2;

        var line_2_3 = new LineData(3, 15f);
        line_2.Line[2] = line_2_3;

        var line_2_4 = new LineData(4, 20f);
        line_2.Line[3] = line_2_4;

        var line_2_5 = new LineData(5, 20f);
        line_2.Line[4] = line_2_5;

        var line_2_6 = new LineData(6, 20f);
        line_2.Line[5] = line_2_6;

        var line_2_7 = new LineData(7, 10f);
        line_2.Line[6] = line_2_7;

        LineRates[1] = line_2;

        var line_3 = new LineRateData();

        var line_3_1 = new LineData(1, 5f);
        line_3.Line[0] = line_3_1;

        var line_3_2 = new LineData(2, 10f);
        line_3.Line[1] = line_3_2;

        var line_3_3 = new LineData(3, 15f);
        line_3.Line[2] = line_3_3;

        var line_3_4 = new LineData(4, 20f);
        line_3.Line[3] = line_3_4;

        var line_3_5 = new LineData(5, 20f);
        line_3.Line[4] = line_3_5;

        var line_3_6 = new LineData(6, 20f);
        line_3.Line[5] = line_3_6;

        var line_3_7 = new LineData(7, 10f);
        line_3.Line[6] = line_3_7;

        LineRates[2] = line_3;

        var line_4 = new LineRateData();

        var line_4_1 = new LineData(1, 5f);
        line_4.Line[0] = line_4_1;

        var line_4_2 = new LineData(2, 10f);
        line_4.Line[1] = line_4_2;

        var line_4_3 = new LineData(3, 15f);
        line_4.Line[2] = line_4_3;

        var line_4_4 = new LineData(4, 20f);
        line_4.Line[3] = line_4_4;

        var line_4_5 = new LineData(5, 20f);
        line_4.Line[4] = line_4_5;

        var line_4_6 = new LineData(6, 20f);
        line_4.Line[5] = line_4_6;

        var line_4_7 = new LineData(7, 10f);
        line_4.Line[6] = line_4_7;

        LineRates[3] = line_4;

        var line_5 = new LineRateData();

        var line_5_1 = new LineData(1, 5f);
        line_5.Line[0] = line_5_1;

        var line_5_2 = new LineData(2, 10f);
        line_5.Line[1] = line_5_2;

        var line_5_3 = new LineData(3, 15f);
        line_5.Line[2] = line_5_3;

        var line_5_4 = new LineData(4, 20f);
        line_5.Line[3] = line_5_4;

        var line_5_5 = new LineData(5, 20f);
        line_5.Line[4] = line_5_5;

        var line_5_6 = new LineData(6, 20f);
        line_5.Line[5] = line_5_6;

        var line_5_7 = new LineData(7, 10f);
        line_5.Line[6] = line_5_7;

        LineRates[4] = line_5;
    }
}
