using System.Collections.Generic;
using UnityEngine;

public class TutorialController : MonoBehaviour
{
    [SerializeField] private List<TutorialBase> tutorials;
    [SerializeField] private string nextSceneName = "InGameTownScene";
    private TutorialBase curTutorial = null;
    public int curIndex { get; set; } = -1;

    private void Start()
    {
        SetNextTutorial();
    }

    private void Update()
    {
        if(curTutorial != null)
        {
            curTutorial.Execute(this);
        }
    }

    public void SetNextTutorial()
    {
        if( curTutorial != null)
        {
            curTutorial.Exit();
        }

        if(curIndex >= tutorials.Count - 1)
        {
            CompletedAllTutorials();
            return;
        }

        curIndex++;
        curTutorial = tutorials[curIndex];

        curTutorial.Enter();
    }

    private void CompletedAllTutorials()
    {
        curTutorial = null;
    }
}
