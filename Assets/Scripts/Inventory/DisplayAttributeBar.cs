using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using TMPro;

public class DisplayAttributeBar : MonoBehaviour
{
    public AttributeType typeToDisplay;
    public TextMeshProUGUI text;
    public Image fill;

    private void Update()
    {
        fill.fillAmount = InventoryManager.Instance.player.GetAttributeValue(typeToDisplay) / InventoryManager.Instance.player.maxHP;
        text.text = ($"{typeToDisplay.ToString()} : {InventoryManager.Instance.player.GetAttributeValue(typeToDisplay)} / {InventoryManager.Instance.player.maxHP} ");
    }
}
