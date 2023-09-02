using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    #region Private Variables 

    [SerializeField] private int startingHealth = 3;
    [SerializeField] private int currentHealth;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Material matWhiteFlash;
    [SerializeField] private GameObject deathVFX;
    [SerializeField] private float setDefaultMatRestorefloat = .1f;
    private Material matDefault;
    private SpriteRenderer spriteRenderer;

    #endregion

    private void Awake() {
        spriteRenderer = GetComponent<SpriteRenderer>();
        matDefault = spriteRenderer.material;
    }

    private void Start() {
        currentHealth = startingHealth;
    }

    private void Update() { 
        DetectDeath();
    }

    public void TakeDamage(int damage) {
        currentHealth -= damage;
        spriteRenderer.material = matWhiteFlash;
        StartCoroutine(SetDefaultMatRoutine(setDefaultMatRestorefloat));
    }

    private void DetectDeath() {
        if (currentHealth <= 0) {
            Instantiate(deathVFX, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }

    private IEnumerator SetDefaultMatRoutine(float whiteFlashTime) {
        yield return new WaitForSeconds(whiteFlashTime);
        spriteRenderer.material = matDefault;
    }

}
