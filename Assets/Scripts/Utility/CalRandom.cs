using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CalRandom : MonoBehaviour
{
    public List<int> GetRandom(GameDataManager.LineRateData data)
    {
        var selectedNum = new List<int>();
        while (selectedNum.Count < 3)
        {
            Debug.LogError("들어옴");
            var num = Random.Range(0, 10000);

            int addNum = 0;

            for (int i = 0; i < data.Line.Length; i++)
            {
                var rate = (int)(data.Line[i].rate * 100);
                addNum += rate;
                if (num < addNum)
                {
                    if (!selectedNum.Contains(data.Line[i].id))
                    {
                        Debug.LogError("값 추가 = " + data.Line[i].id);
                        selectedNum.Add(data.Line[i].id);
                    }
                    break;
                }
            }
        }

        return selectedNum;
    }
}
