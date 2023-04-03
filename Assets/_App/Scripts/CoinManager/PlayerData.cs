using System;
using UnityEngine;

public class Constant
{
    public static string DataKey_PlayerData = "player_data";
    public static int countSong = 3;
    public static int priceUnlockSkin = 100;
}

public class PlayerData : BaseData
{
    public int intDiamond;
    public int currentSkin;

    public int score = 0;
    public int currentScore = 0;

    public Action<int> onChangeDiamond;

    public override void Init()
    {
        prefString = Constant.DataKey_PlayerData;
        if (PlayerPrefs.GetString(prefString).Equals(""))
        {
            ResetData();
        }

        base.Init();
    }


    public void SetScore(int high)
    {
        if (high >= score)
        {
            score = high;
        }
        
        Save();
    }

    public override void ResetData()
    {
        intDiamond = 0;
        currentSkin = 0;

        Save();
    }

    public bool CheckLock(int id)
    {
        return true;
    }

    public void Unlock(int id)
    {

        Save();
    }

    public void AddDiamond(int a)
    {
        intDiamond += a;

        onChangeDiamond?.Invoke(intDiamond);
        
        Save();
    }

    public bool CheckCanUnlock()
    {
        return intDiamond >= Constant.priceUnlockSkin;
    }

    public void SubDiamond(int a)
    {
        intDiamond -= a;

        if (intDiamond < 0)
        {
            intDiamond = 0;
        }

        onChangeDiamond?.Invoke(intDiamond);
        
        Save();
    }

    public void ChooseSong(int i)
    {
        currentSkin = i;
        Save();
    }
}