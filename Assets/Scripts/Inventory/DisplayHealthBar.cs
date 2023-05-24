using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using TMPro;

public class DisplayHealthBar : MonoBehaviour
{
    public TextMeshProUGUI text;
    public Image fill;

    private void UpdateBar()
    {
        Player player = InventoryManager.Instance.player;
        float currentHealth = player.GetAttributeValue(AttributeType.HP);
        fill.fillAmount = currentHealth / player.maxHP;
        text.text = ($"HP {currentHealth} / {player.maxHP}");
    }

    private void Update()
    {
        UpdateBar();
    }
}
