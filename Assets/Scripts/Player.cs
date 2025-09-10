using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //[SerializeField] Camera mainCamera;

    [Header("Projectile")]
    [SerializeField] GameObject laserPrefab;
    [SerializeField] float projectileSpeed = 30f;
    [SerializeField] float projectileFireRate = 0.6f;

    [Header("Player")]
    [SerializeField] float moveSpeed = 10f;
    [SerializeField] float padding = 1f;
    [SerializeField] float health = 30f;

    [Header("Audio")]
    [SerializeField] AudioClip deathSFX;
    [SerializeField] [Range(0, 1)] float deathSoundVolume = 0.75f;
    [SerializeField] AudioClip shootSFX;
    [SerializeField] [Range(0, 1)] float shootSoundVolume = 0.1f;

    [Header("Death")]
    [SerializeField] GameObject deathVFX;
    [SerializeField] float explosionDuration = 1f;

    private Coroutine firingCoroutine;

    private float xMin;
    private float xMax;
    private float yMin;
    private float yMax;

    private void Start()
    {
        SetUpBoundaries();
    }

    private void SetUpBoundaries()
    {
        Camera camera = Camera.main;
        xMin = camera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x + padding;
        xMax = camera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x - padding;
        yMin = camera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y + padding;
        yMax = camera.ViewportToWorldPoint(new Vector3(0, 1, 0)).y - padding;

    }

    void Update()
    {
        Move();
        Attack();
    }

    private void Move()
    {
#if UNITY_STANDALONE || UNITY_EDITOR
        float deltaX = Input.GetAxis("Horizontal") * Time.deltaTime * moveSpeed;
        float deltaY = Input.GetAxis("Vertical") * Time.deltaTime * moveSpeed;

        var nextXPos = Mathf.Clamp(transform.position.x + deltaX, xMin, xMax);
        var nextYPos = Mathf.Clamp(transform.position.y + deltaY, yMin, yMax);

        transform.position = new Vector2(nextXPos, nextYPos);

#endif
#if UNITY_ANDROID
        if (Input.touchCount == 1)
        {
            Touch touch = Input.touches[0];
            Vector3 touchPosition = Camera.main.ScreenToWorldPoint(touch.position);
            touchPosition.z = transform.position.z;
            transform.position = Vector3.MoveTowards(transform.position, touchPosition, 30 * Time.deltaTime);

            if(touch.phase == TouchPhase.Began)
            {
                firingCoroutine = StartCoroutine(Fire());
            }
            if(touch.phase == TouchPhase.Ended)
            {
                StopCoroutine(firingCoroutine);
            }
        }
#endif
    }

    private void Attack()
    {
#if UNITY_STANDALONE || UNITY_EDITOR
        if (Input.GetButtonDown("Fire1"))
            firingCoroutine = StartCoroutine(Fire());
        if (Input.GetButtonUp("Fire1"))
            StopCoroutine(firingCoroutine);
#endif
    }

    IEnumerator Fire()
    {
        while (true)
        {
            GameObject laser = Instantiate(laserPrefab, transform.position, Quaternion.identity) as GameObject;
            laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, projectileSpeed);
            AudioSource.PlayClipAtPoint(shootSFX, Camera.main.transform.position, shootSoundVolume);

            yield return new WaitForSeconds(projectileFireRate);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        DamageDeal damageDeal = collision.gameObject.GetComponent<DamageDeal>();
        if(!damageDeal) { return; }
        HitEnter(damageDeal);
    }

    private void HitEnter(DamageDeal damageDeal)
    {
        health -= damageDeal.GetDamage();
        damageDeal.Hit();

        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        FindObjectOfType<Level>().LoadGameOver();
        Destroy(gameObject);
        GameObject explosion = Instantiate(deathVFX, transform.position, transform.rotation);
        Destroy(explosion, explosionDuration);
        AudioSource.PlayClipAtPoint(deathSFX, Camera.main.transform.position, deathSoundVolume);
    }

    public float GetHealth()
    { return health; }
}