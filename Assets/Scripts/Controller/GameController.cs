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
        Debug.LogError("클릭");
        for (int i = 0; i < SlotLines.Length; i++)
        {
            SlotLines[i].StartSpin(i);
        }
    }
}

