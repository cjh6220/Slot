using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class MatchChecker : SingletonBase<MatchChecker>
{
    public List<e_Match> CheckMatch(List<int> datas)
    {
        var matchList = new List<e_Match>();

        //case_1
        var case_1 = new int[5] { datas[1], datas[4], datas[7], datas[10], datas[13] };
        if (MatchCheck(case_1))
        {
            matchList.Add(e_Match.Case_1);
        }

        //case_2
        var case_2 = new int[5] { datas[2], datas[5], datas[8], datas[11], datas[14] };
        if (MatchCheck(case_2))
        {
            matchList.Add(e_Match.Case_2);
        }

        //case_3
        var case_3 = new int[5] { datas[0], datas[3], datas[6], datas[9], datas[12] };
        if (MatchCheck(case_3))
        {
            matchList.Add(e_Match.Case_3);
        }

        //case_4
        var case_4 = new int[5] { datas[2], datas[4], datas[6], datas[10], datas[14] };
        if (MatchCheck(case_4))
        {
            matchList.Add(e_Match.Case_4);
        }

        //case_5
        var case_5 = new int[5] { datas[0], datas[4], datas[8], datas[10], datas[12] };
        if (MatchCheck(case_5))
        {
            matchList.Add(e_Match.Case_5);
        }

        //case_6
        var case_6 = new int[5] { datas[1], datas[5], datas[8], datas[11], datas[13] };
        if (MatchCheck(case_6))
        {
            matchList.Add(e_Match.Case_6);
        }

        //case_7
        var case_7 = new int[5] { datas[1], datas[3], datas[6], datas[9], datas[13] };
        if (MatchCheck(case_7))
        {
            matchList.Add(e_Match.Case_7);
        }

        //case_8
        var case_8 = new int[5] { datas[2], datas[5], datas[7], datas[9], datas[12] };
        if (MatchCheck(case_8))
        {
            matchList.Add(e_Match.Case_8);
        }

        //case_9
        var case_9 = new int[5] { datas[0], datas[3], datas[7], datas[11], datas[14] };
        if (MatchCheck(case_9))
        {
            matchList.Add(e_Match.Case_9);
        }

        //case_10
        var case_10 = new int[5] { datas[1], datas[5], datas[7], datas[9], datas[13] };
        if (MatchCheck(case_10))
        {
            matchList.Add(e_Match.Case_10);
        }

        //case_11
        var case_11 = new int[5] { datas[1], datas[3], datas[7], datas[11], datas[13] };
        if (MatchCheck(case_11))
        {
            matchList.Add(e_Match.Case_11);
        }

        //case_12
        var case_12 = new int[5] { datas[2], datas[4], datas[7], datas[10], datas[14] };
        if (MatchCheck(case_12))
        {
            matchList.Add(e_Match.Case_12);
        }

        //case_13
        var case_13 = new int[5] { datas[0], datas[4], datas[7], datas[10], datas[12] };
        if (MatchCheck(case_13))
        {
            matchList.Add(e_Match.Case_13);
        }

        //case_14
        var case_14 = new int[5] { datas[2], datas[4], datas[8], datas[10], datas[14] };
        if (MatchCheck(case_14))
        {
            matchList.Add(e_Match.Case_14);
        }

        //case_15
        var case_15 = new int[5] { datas[0], datas[4], datas[6], datas[10], datas[12] };
        if (MatchCheck(case_15))
        {
            matchList.Add(e_Match.Case_15);
        }

        //case_16
        var case_16 = new int[5] { datas[1], datas[4], datas[8], datas[10], datas[13] };
        if (MatchCheck(case_16))
        {
            matchList.Add(e_Match.Case_16);
        }

        //case_17
        var case_17 = new int[5] { datas[1], datas[4], datas[6], datas[10], datas[13] };
        if (MatchCheck(case_17))
        {
            matchList.Add(e_Match.Case_17);
        }

        //case_18
        var case_18 = new int[5] { datas[2], datas[5], datas[6], datas[11], datas[14] };
        if (MatchCheck(case_18))
        {
            matchList.Add(e_Match.Case_18);
        }

        //case_19
        var case_19 = new int[5] { datas[0], datas[3], datas[8], datas[9], datas[12] };
        if (MatchCheck(case_19))
        {
            matchList.Add(e_Match.Case_19);
        }

        //case_20
        var case_20 = new int[5] { datas[2], datas[3], datas[6], datas[9], datas[14] };
        if (MatchCheck(case_20))
        {
            matchList.Add(e_Match.Case_20);
        }

        return matchList;
    }

    // static bool HasThreeOrMoreEqual(int[] numbers)
    // {
    //     int count = 0;
    //     for (int i = 1; i < numbers.Length; i++)
    //     {
    //         if (numbers[i] != numbers[0])
    //         {
    //             return false;
    //         }
    //     }
    //     return true;
    // }

    static bool MatchCheck(int[] numbers)
    {
        int count = 0;

        for (int i = 1; i < numbers.Length; i++)
        {
            if (numbers[0] == numbers[i] || numbers[i] == 7)
            {
                count += 1;
            }
        }

        if (count >= 3)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
