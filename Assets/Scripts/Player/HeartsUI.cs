using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Updates the UI on our canvas 
public class HeartsUI : MonoBehaviour
{
    #region Private Variables

    [SerializeField] private Image[] hearts;
    [SerializeField] private Sprite fullHeart;
    [SerializeField] private Sprite emptyHeart;
    private PlayerHealth player;

    #endregion

    #region Unity Methods

    private void Awake() {
    }

    private void Start() {
        SetHeartsUI();
    }

    private void Update() {
        UpdateHearthUI();

        if (player == null) {
            player = FindObjectOfType<PlayerHealth>();
        }
    }

    #endregion

    #region Private Methods    

    private void SetHeartsUI() {
        List<Image> allHearts = new List<Image>();

        foreach (Transform child in transform)
        {
            allHearts.Add(child.gameObject.GetComponent<Image>());
        }

        hearts = allHearts.ToArray();
    }
    

    private void UpdateHearthUI() {
        if (player == null) { return; }

        if (player.currentHealth > player.maxHealth){
            player.currentHealth = player.maxHealth;
        }

        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < player.currentHealth) {
                hearts[i].sprite = fullHeart;
            } else {
                hearts[i].sprite = emptyHeart;
            }

            if(i < player.maxHealth) {
                hearts[i].enabled = true;
            } else {
                hearts[i].enabled = false;
            }
        }
    }

    #endregion
}
