using TMPro;
using UnityEngine;

public class TextModifier : MonoBehaviour
{
    //The text which should be modified
    public TextMeshProUGUI textMeshPro;

    //updating the text based on a normal string
    public void UpdateText(string newText)
    {
        //when the text exist
        if (textMeshPro != null)
        {
            //we set the parameter text as the new text
            textMeshPro.text = newText;
        }
        else
        {
            Debug.LogWarning("TextMeshPro component not assigned.");
        }
    }

    //update method overloader for the use of the goal statment
    public void UpdateText(int x , int y)
    {
        //formating the appropriate sting
        string temp = x + "/" + y + "\n Enemy Alive";
        //calling the normal funktion
        UpdateText(temp);
    }
}
