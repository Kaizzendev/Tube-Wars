using TMPro;
using UnityEngine;

public class ActionUI : MonoBehaviour
{
    public TextMeshProUGUI actionNameText;
    public TextMeshProUGUI actionScoreText;

    public void Setup(string actionName, float actionScore)
    {
        actionNameText.text = actionName;
        actionScoreText.text =  actionScore.ToString();
    }
}
