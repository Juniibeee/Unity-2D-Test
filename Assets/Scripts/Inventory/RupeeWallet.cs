using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RupeeWallet : MonoBehaviour
{
    public int currentRupees;
    
    [SerializeField] private TMP_Text rupeeText;

    private void Update() {
        UpdateRupeeText();
    }

    public void UpdateRupeeText() {
        rupeeText.text = currentRupees.ToString("D3");
    }

    public void IncreaseRupeeCount(int amount) {
        currentRupees += amount;
    }
}
