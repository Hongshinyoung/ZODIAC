using UnityEngine;

public abstract class SkillBase : MonoBehaviour
{
    public string skillName;
    public float skillRange;
    public GameObject rangeIndicatorPrefab;

    protected GameObject rangeIndicator;

    public abstract void ActivateSkill(Vector3 targetPosition);

    public virtual void ShowRangeIndicator(Vector3 position)
    {
        if (rangeIndicator == null)
            rangeIndicator = Instantiate(rangeIndicatorPrefab);

        rangeIndicator.transform.position = position;
        rangeIndicator.SetActive(true);
    }

    public virtual void HideRangeIndicator()
    {
        if (rangeIndicator != null)
            rangeIndicator.SetActive(false);
    }
}