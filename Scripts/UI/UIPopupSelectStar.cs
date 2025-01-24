using UnityEngine;
using UnityEngine.UI;

public class UIPopupSelectStar : UIBase
{
    [SerializeField] private Button[] stageButtons;

    private void Start()
    {
        UIManager.Instance.Show<UIPopupSelectStar>();
    }

    public override void Opened(params object[] param)
    {
        base.Opened(param);


    }

    //시작 시 for문으로 먼저 인덱스 지정해주고 , 로드 시 그 인덱스 로드 되도록 1순위 

    public void LoadStageUI(string stageName)
    {
        SoundManager.Instance.PlaySound("ChoiceSFX", 0.2f);
        for(int i = 0; i < stageButtons.Length; i++)
        {
            if(stageButtons[i].name == stageName)
            {
                GameObject obj = ResourceManager.Instance.LoadAsset<GameObject>(stageName, eAssetType.UI, eCategoryType.Stage);
                Instantiate(obj,transform); // 스테이지 버튼들 생성해주었고,
            }
        }
    }
}
