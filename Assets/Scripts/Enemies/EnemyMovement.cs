using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    #region Public Variables 

    public bool canMove = true;
    public float enemyKnockBackThrust = 15f;
    public int damageDoneToHero;

    #endregion

    #region Private Variables

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float moveSpeed;
    [SerializeField] private SpriteRenderer spriteRenderer;
    // Used in PlayerHealth collision with Enemies
    private int xDir;
    private int yDir;
    private float randomNum;
    private Vector2 movement;

    #endregion

    private void Start() {
        StartCoroutine(ChangeDirectionRoutine());
    }

    private void Update() {
        Move();
    }

    private void Move() {
        if (!canMove) { return; }

        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);

        // can flip sprite to reduce amount of sprites & animations
        if (movement.x == -1) {
            spriteRenderer.flipX = true;
        }
        else {
            spriteRenderer.flipX = false;
        }
    }

    // random movement around map
    private IEnumerator ChangeDirectionRoutine() {
        while (true) {
            randomNum = Random.Range(-5, 5);
            movement.x = Random.Range(-1, 2);
            movement.y = Random.Range(-1, 2);
            yield return new WaitForSeconds(randomNum);
        }
    }

}
