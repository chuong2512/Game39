using UnityEngine;

public class SkinSelector : MonoBehaviour
{
    private PlayerData playerData; //todo delete
    private GameDataManager gameData; 
    public int currentSkin;
    public SkinItem[] skinItems;

    void Start()
    {
        gameData = GameDataManager.Instance;
        playerData = gameData.playerData;
        
        currentSkin = playerData.currentSkin;

        skinItems[currentSkin].Choose();
    }
    
    public void ChooseSkin(int index)
    {
        if (currentSkin == index)
        {
            return;
        }

        skinItems[currentSkin].UnChoose();
        skinItems[index].Choose();
        currentSkin = index;
        playerData.currentSkin = currentSkin;
        //todo add Playerdata
    }

    public void UnlockSkin(int index)
    {
    }

}
