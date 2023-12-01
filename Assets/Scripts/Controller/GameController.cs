using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mkey;

public class GameController : MonoBehaviour
{
    public SlotLine[] SlotLines;
    public MatchLine[] MatchLines;
    List<e_Match> _matchList = new List<e_Match>();
    int _spinCount = 0;

    void Start()
    {
        foreach (SlotLine sGB in SlotLines)
        {
            sGB.CreateSlotCylinder();
        }
    }

    public void OnClickSpin()
    {
        for (int i = 0; i < _matchList.Count; i++)
        {
            MatchLines[(int)_matchList[i]].SetLineVisible(false);
        }
        _matchList.Clear();

        _spinCount = 0;
        var selected = new List<int>();
        for (int i = 0; i < SlotLines.Length; i++)
        {
            var items = GameDataManager.Instance._calRandom.GetRandom(GameDataManager.Instance.LineRates[i]);
            SlotLines[i].StartSpin(i, items);

            for (int a = 0; a < items.Count; a++)
            {
                selected.Add(items[a]);
            }
        }

        _matchList = MatchChecker.Instance.CheckMatch(selected);
        // Debug.LogError("Match = " + _matchList.Count);
        // for (int i = 0; i < _matchList.Count; i++)
        // {
        //     //Debug.LogError(matchList[i].ToString());
        //     MatchLines[(int)_matchList[i]].SetLineVisible(true);
        // }
    }

    public void SpinEndChecker()
    {
        _spinCount += 1;
        Debug.LogError("_spinCount = " + _spinCount);
        if (_spinCount >= 5)
        {
            Debug.LogError("SpinEnd");
            for (int i = 0; i < _matchList.Count; i++)
            {
                //Debug.LogError(matchList[i].ToString());
                MatchLines[(int)_matchList[i]].SetLineVisible(true);
            }
            _spinCount = 0;
        }
    }
}

