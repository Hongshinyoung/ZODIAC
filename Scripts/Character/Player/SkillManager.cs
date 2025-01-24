using UnityEngine;

public class SkillManager : MonoBehaviour
{
    public SkillBase[] skills; // 2번: 매테오, 3번: 레인, 4번: 패시브

    private SkillBase activeSkill;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SetActiveSkill(0); // 매테오 스킬
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            SetActiveSkill(1); // 레인 스킬
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            ActivatePassiveSkill(); // 패시브 스킬
        }
        if (activeSkill != null)
        {
            HandleActiveSkillInput();
        }
    }

    private void SetActiveSkill(int index)
    {
        if (activeSkill != null)
        {
            activeSkill.HideRangeIndicator();

            if (activeSkill == skills[index])
            {
                activeSkill = null; 
                return;
            }
        }

        if (index < skills.Length)
        {
            activeSkill = skills[index];
        }
    }

    private void HandleActiveSkillInput()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            Vector3 targetPosition = hit.point;
            float distance = Vector3.Distance(transform.position, targetPosition);

            if (distance <= activeSkill.skillRange)
            {
                activeSkill.ShowRangeIndicator(targetPosition);

                if (Input.GetMouseButtonDown(0)) 
                {
                    activeSkill.ActivateSkill(targetPosition);
                    activeSkill.HideRangeIndicator();
                    activeSkill = null; 
                }
            }
            else
            {
                activeSkill.HideRangeIndicator();
            }
        }
    }

    private void ActivatePassiveSkill()
    {
        if (skills.Length >= 3 && skills[2] is HealingSkill passiveSkill)
        {
            passiveSkill.ActivateSkill(Vector3.zero); 
        }
    }
}
