using UnityEngine;
using UnityEngine.UI;

public class UIPopupSelectStage : UIBase
{
    public Button[] buttons;
    private ClearData clearData;
    private int curStar;
    private int curStage;

    private void Awake()
    {
        clearData = GameManager.Instance.clearData;
        int buttonIndex = 0;
        for (int i = 0; i < clearData.stageCleared.GetLength(0); i++) // 별자리 갯수 만큼 12번 돌리기
        {
            for (int j = 0; j < clearData.stageCleared.GetLength(1); j++) // 스테이지 갯수 만큼 6번 돌리기 
            {
                Debug.Log(clearData.stageCleared[i, j]);
                if (buttonIndex >= buttons.Length) // 버튼 수 초과하면 반복 종
                {
                    break;
                }
                if (i == 0 && j == 0)
                {
                    buttons[buttonIndex].image.color = new Color(1, 1, 1, 1);
                    buttons[buttonIndex].interactable = true;
                }

                else if (clearData.stageCleared[i, j] == false)
                {
                    buttons[buttonIndex].image.color = new Color(1, 1, 1, 0.3f);
                    buttons[buttonIndex].interactable = false;
                }
                else
                {
                    buttons[buttonIndex].image.color = new Color(1, 1, 1, 1);
                    buttons[buttonIndex].interactable = true;
                }
                buttonIndex++;
            }
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
