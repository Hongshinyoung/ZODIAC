using System.Threading.Tasks;
using UnityEngine;

public class UILoading : UIBase
{
    public CanvasGroup fadeCanvasGroup;
    private float fadeDuration = 1.0f;

    private void Awake()
    {
        fadeCanvasGroup = GetComponent<CanvasGroup>();
    }

    public override void Opened(params object[] param)
    {
        base.Opened(param);
    }

    public async Task FadeIn()
    {
        if (fadeCanvasGroup == null) return;
        fadeCanvasGroup.alpha = 0;

        float time = 0;
        while (time < fadeDuration)
        {
            time += Time.deltaTime;
            fadeCanvasGroup.alpha = Mathf.Lerp(0, 1, time / fadeDuration);
            await Task.Yield();
        }
        fadeCanvasGroup.alpha = 1;
    }
    public async Task FadeOut()
    {
        if (fadeCanvasGroup == null) return;
        fadeCanvasGroup.alpha = 1;

        float time = 0;
        while (time < fadeDuration)
        {
            time += Time.deltaTime;
            fadeCanvasGroup.alpha = Mathf.Lerp(1, 0, time / fadeDuration);
            await Task.Yield();
        }
        fadeCanvasGroup.alpha = 0;
    }

}