using TMPro;
using UnityEngine;

public class TextModifier : MonoBehaviour
{
    public TextMeshProUGUI textMeshPro;

    public void UpdateText(string newText)
    {
        if (textMeshPro != null)
        {
            textMeshPro.text = newText;
        }
        else
        {
            Debug.LogWarning("TextMeshPro component not assigned.");
        }
    }

    public void UpdateText(int x , int y)
    {
        string temp = x + "/" + y + "\n Enemy Alive";
        UpdateText(temp);
    }
}
