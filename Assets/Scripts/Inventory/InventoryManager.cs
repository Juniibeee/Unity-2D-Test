using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// Updates UI based on event system's current selected object. 
public class InventoryManager : Singleton<InventoryManager>
{
    #region Public Variables

    public enum CurrentEquippedItem { Boomerang, Bomb };
    public CurrentEquippedItem currentEquippedItem;
    public GameObject itemEquippedInv;
    public GameObject currentSelectedItem;
    public GameObject inventoryContainer;

    #endregion

    #region Private Variables 

    [SerializeField] private GameObject selectionBorder;
    [SerializeField] private EventSystem eventSystem;
    [SerializeField] private Image activeSpriteUI;
    private PlayerControls playerControls;
    private const string boomerangString = "Boomerang";
    private const string bombString = "Bomb";

    #endregion

    #region Unity Methods

    protected override void Awake() {
        base.Awake();
        playerControls = new PlayerControls();
    }

    private void Start() {
        playerControls.Inventory.OpenInventoryContainer.performed += _ => OpenInventoryContainer();
    }

    private void OnEnable() {
        playerControls.Enable();
    }

    private void OnDisable() {
        if (playerControls != null) {
            playerControls.Disable();
        }
    }

    private void Update() {
        UpdateDetectIfItemChange();
    }

    #endregion

    #region Public Methods


    // see EventSystemSpawner.cs
    public void SetEventSystem(EventSystem newEventSystem) {
        eventSystem = newEventSystem;
    }

    #endregion

    #region Private Methods

    private void OpenInventoryContainer() {
        if (inventoryContainer.gameObject.activeInHierarchy == false) {
            inventoryContainer.gameObject.SetActive(true);
            PlayerController.Instance.PauseGame();
        }

        else if (inventoryContainer.gameObject.activeInHierarchy == true) {
            inventoryContainer.gameObject.SetActive(false);
            PlayerController.Instance.UnpauseGame();
        }
    }

    private void UpdateDetectIfItemChange(){
        if (currentSelectedItem != eventSystem.currentSelectedGameObject || currentSelectedItem == null) {
            currentSelectedItem = eventSystem.currentSelectedGameObject;
            selectionBorder.transform.position = currentSelectedItem.transform.position;
            activeSpriteUI.sprite = currentSelectedItem.GetComponent<Image>().sprite;
            ChangeCurrentEquippedItem();
        }
    }

    private void updateSelectionBorder() {
        selectionBorder.transform.position = currentSelectedItem.transform.position;
    }

    private void ChangeCurrentEquippedItem() { 
        ItemDisplay thisItem = currentSelectedItem.GetComponent<ItemDisplay>();
        
        if (thisItem) { 
            if (thisItem.item.itemType == boomerangString) {
                currentEquippedItem = CurrentEquippedItem.Boomerang;
            } else if (thisItem.item.itemType == bombString) {
                currentEquippedItem = CurrentEquippedItem.Bomb;
            } 

            itemEquippedInv = thisItem.item.useItemPrefab;
        } else {
            itemEquippedInv = null;
        }
    }

    #endregion
}
