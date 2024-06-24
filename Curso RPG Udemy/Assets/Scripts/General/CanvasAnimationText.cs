using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CanvasAnimationText : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI damageText;

    public void SetText(float text)
    {
        damageText.text = text.ToString();
    }
}
