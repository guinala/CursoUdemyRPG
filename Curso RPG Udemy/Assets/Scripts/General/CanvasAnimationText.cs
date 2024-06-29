using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CanvasAnimationText : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI damageText;

    public void SetText(float text, Color color)
    {
        damageText.text = text.ToString();
        damageText.color = color;
    }
}
