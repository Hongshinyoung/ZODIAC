using UnityEngine;
using UnityEngine.UI;

public class UIPopupOpenStage : UIBase
{
    public Button buttons;
    private ClearData clearData;
    private int curStar;
    private int curStage;

    private void Awake()
    {
        clearData = GameManager.Instance.clearData;

        if (clearData.stageCleared[0, 5] == true)
        {
            buttons.image.color = new Color(1, 1, 1, 1);
            buttons.interactable = true;
        }
        else
        {
            buttons.image.color = new Color(1, 1, 1, 0.3f);
            buttons.interactable = false;
        }
    }

    public void EneterDungeon(string stageId)
    {
        SoundManager.Instance.PlaySound("ChoiceSFX", 0.2f);
        GameManager.Instance.EnterDungeon(stageId, curStar, curStage);
    }

    public void StageData(int stageIdx)
    {
        curStage = stageIdx;
    }

    public void StarData(int starIdx)
    {
        curStar = starIdx;
    }
}
