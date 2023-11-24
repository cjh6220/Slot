using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mkey;

public class GameController : MonoBehaviour
{
    public SlotLine[] SlotLines;

    void Start()
    {
        foreach (SlotLine sGB in SlotLines)
        {
            sGB.CreateSlotCylinder();
        }
    }

    public void OnClickSpin()
    {
        var selected = new List<int>();
        for (int i = 0; i < SlotLines.Length; i++)
        {
            var items = GameDataManager.Instance._calRandom.GetRandom(GameDataManager.Instance.LineRates[i]);
            SlotLines[i].StartSpin(i ,items);

            for (int a = 0; a < items.Count; a++)
            {
                selected.Add(items[a]);
            }
        }

        var matchList = MatchChecker.Instance.CheckMatch(selected);
        Debug.LogError("Match = " +  matchList.Count);
        for (int i = 0; i < matchList.Count; i++)
        {
            Debug.LogError(matchList[i].ToString());
        }
    }
}

