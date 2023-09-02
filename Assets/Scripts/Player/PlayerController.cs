using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : Singleton<PlayerController>
{
    #region Public Variables

    // For use of AreaExit and AreaEntrance scripts
    public string areaTransitionName;
    public GameObject itemEquipped;
    public bool canAttack = true;
    public bool canMove = true;
    public bool itemInUse = false;

    #endregion

    #region Private Variables

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float runSpeed = 4f;
    [SerializeField] private Animator myAnimator;
    // The hitboxes of our 4 different sword directions
    [SerializeField] private GameObject hitBox_Top;
    [SerializeField] private GameObject hitBox_Bottom;
    [SerializeField] private GameObject hitBox_Left;
    [SerializeField] private GameObject hitBox_Right;

    // GameState used to handle when Hero can take certain actions.  Could expand on this depending on if you wanted to add different player game handling states.
    private enum GameState { Playing, Paused};
    private GameState currentGameState;
    private PlayerControls playerControls;
    private Vector2 movement;

    #endregion

    #region Player Controls / Input Actions

    protected override void Awake() {
        // base.Awake() is called to apply our singleton inherited class, as well as anything else we want to set up in Awake()
        base.Awake();
        playerControls = new PlayerControls();
    }

    #endregion

    #region Unity Methods 

    private void Start()
    {
        playerControls.Movement.Run.performed += _ => StartRun();
        playerControls.Movement.Run.canceled += _ => StopRun();
        playerControls.Combat.Attack.performed += _ => Attack();
        playerControls.RightClick.Use.performed += _ => UseItem();

        currentGameState = GameState.Playing;
    }

    private void OnEnable() {
        playerControls.Enable();
    }

    private void OnDisable() {
        if (playerControls != null) {
            playerControls.Disable();
        }
    }

    private void Update()
    {
        PlayerInput();
    }

    private void FixedUpdate() {
        Move();   
    }

    #endregion

    #region  Public Methods

    // SpawnItem() is called in our animator
    public void SpawnItem() {
        itemEquipped = InventoryManager.Instance.itemEquippedInv;

        if (itemInUse || !itemEquipped) { return; }

        itemInUse = true;
        ItemDisplay itemDisplay = InventoryManager.Instance.currentSelectedItem.GetComponent<ItemDisplay>();

        if (itemDisplay) {
            if (itemDisplay.item.itemType == "Bomb") {
                itemInUse = false;
            }

            if (myAnimator.GetFloat("lastMoveX") == 1) {
                Instantiate(itemEquipped, new Vector2(transform.position.x + 1, transform.position.y + 0.5f), transform.rotation);
            }
            else if (myAnimator.GetFloat("lastMoveX") == -1) {
                Instantiate(itemEquipped, new Vector2(transform.position.x - 1, transform.position.y + 0.5f), transform.rotation);
            }
            else if (myAnimator.GetFloat("lastMoveY") == 1) {
                Instantiate(itemEquipped, new Vector2(transform.position.x, transform.position.y + 1.5f), transform.rotation);
            }
            else if (myAnimator.GetFloat("lastMoveY") == -1) {
                Instantiate(itemEquipped, new Vector2(transform.position.x, transform.position.y - 1f), transform.rotation);
            }
        }
    }

    public void ActivateCollider() {
        if (myAnimator.GetFloat("lastMoveX") == -1) {
            hitBox_Left.SetActive(true);
        }
        if (myAnimator.GetFloat("lastMoveX") == 1) {
            hitBox_Right.SetActive(true);
        }
        if (myAnimator.GetFloat("lastMoveY") == -1) {
            hitBox_Bottom.SetActive(true);
        }
        if (myAnimator.GetFloat("lastMoveY") == 1) {
            hitBox_Top.SetActive(true);
        }
    }

    public void toggleGameState() {
        if (currentGameState == GameState.Paused) {
            currentGameState = GameState.Playing;
        } else {
            myAnimator.SetFloat("moveX", 0f);
            myAnimator.SetFloat("moveY", 0f);
            currentGameState = GameState.Paused; 
        }
    }

    public void PauseGame() {
        currentGameState = GameState.Paused;
        Time.timeScale = 0f;
    }

    public void UnpauseGame() {
        currentGameState = GameState.Playing;
        Time.timeScale = 1f;
    }

    public void DialogueStopMove() {
        myAnimator.SetFloat("moveX", 0f);
        myAnimator.SetFloat("moveY", 0f);
    }

    #endregion
    
    #region Private Methods
    
    private void PlayerInput() {
        if (!canMove || currentGameState == GameState.Paused) { return; }

        movement = playerControls.Movement.Move.ReadValue<Vector2>();

        myAnimator.SetFloat("moveX", movement.x);
        myAnimator.SetFloat("moveY", movement.y);

        if(movement.x == 1 || movement.x == -1 || movement.y == 1 || movement.y == -1) {
            if (canMove) {
                myAnimator.SetFloat("lastMoveX", movement.x);
                myAnimator.SetFloat("lastMoveY", movement.y);
            }
        }
    }

    private void Move() {
        if (!canMove) { return; }

        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }

    private void StartRun() {
        myAnimator.SetBool("isRunning", true);
        moveSpeed += runSpeed;
    }

    private void StopRun() {
        myAnimator.SetBool("isRunning", false);
        moveSpeed -= runSpeed;
    }

    private void Attack() {
        if (canAttack && currentGameState != GameState.Paused) {
            rb.velocity = Vector2.zero;
            canMove = false;
            myAnimator.SetTrigger("attack");
        }
    }

    public void canMoveFunction() {
        canMove = true;

        hitBox_Left.SetActive(false);
        hitBox_Right.SetActive(false);
        hitBox_Bottom.SetActive(false);
        hitBox_Top.SetActive(false);
    }

    private void UseItem() {
        if (canAttack && currentGameState != GameState.Paused) {
            rb.velocity = Vector2.zero;
            canMove = false;
            myAnimator.SetTrigger("useItem");
        }
    }

    


    #endregion
}
