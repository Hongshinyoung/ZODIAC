using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldManager : Singleton<GoldManager>
{
    private int gold;
    public int Gold
    {
        get => gold;
        set
        {
            gold = Mathf.Max(0, value);
            OnGoldChanged?.Invoke(gold);
        }
    }
    public event Action<int> OnGoldChanged;

    public void SpendGold(int amount)
    {
        if (Gold >= amount)
        {
            Gold -= amount;
        }
        else
        {
            Debug.Log("돈 부족함");
        }
    }
}
