using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    // Configuration Parameters
    [Header("Player")] 
    float moveSpeed = 8f;
    int health = 200;
    float padding_x_left = 1f;
    float padding_x_right = 2f;
    float padding_y_bottom = 2f;
    float padding_y_top = 3.5f;

    [Header("Projectile")]
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] float projectileSpeed = 20f;
    [SerializeField] float projectfileFiringPeriod = 0.2f;


    [Header("Effects")]
    [SerializeField] AudioClip deathSFX;
    [SerializeField] [Range(0, 1)] float deathSFXVolume = 0.7f;
    [SerializeField] AudioClip hitSFX;
    [SerializeField] [Range(0,1)] float hitSFXVolume = 0.7f;
    [SerializeField] AudioClip shootSound;
    [SerializeField] [Range(0, 1)] float shootSoundVolume = 0.25f;

    Coroutine firingCoroutine; 
    float xMin;
    float xMax;
    float yMin;
    float yMax;

    // Use this for initialization

    private void Awake()
    {
        health = FindObjectOfType<Level>().GetPlayerHealth();
    }
    void Start () {
        SetUpMoveBoundaries();
	}

    // Update is called once per frame
    void Update () {
		Move();
        Fire();
	}

    private void OnTriggerEnter2D(Collider2D other)
    {
        //game object that bumped into me
        DamageDealer damageDealer = other.gameObject.GetComponent<DamageDealer>();
        if (!damageDealer) { return; }
        ProcessHit(damageDealer);
    }

    private void ProcessHit(DamageDealer damageDealer)
    {
        health -= damageDealer.GetDamage();
        damageDealer.Hit();
        AudioSource.PlayClipAtPoint(hitSFX, Camera.main.transform.position, hitSFXVolume);
        if (health <= 0)
        {
            Die();
        }
    }

    private void Die() {
        FindObjectOfType<Level>().LoadGameLost();
        Destroy(gameObject);
        AudioSource.PlayClipAtPoint(deathSFX, Camera.main.transform.position, deathSFXVolume);
    }

    public int GetHealth(){
        return health;
    }

    private void Fire()
    {
        if(Input.GetButtonDown("Fire1")) {
            firingCoroutine = StartCoroutine(FireContinuously());

        }
        if(Input.GetButtonUp("Fire1")) {
            StopCoroutine(firingCoroutine);
        }
    }

    IEnumerator FireContinuously() {
        while (true)
        {
            Vector2 position = new Vector2(transform.position.x + 1.5f, transform.position.y+0.2f);
            GameObject bullet = Instantiate(
                    bulletPrefab,
                    position,
                    Quaternion.identity) as GameObject;
            bullet.GetComponent<Rigidbody2D>().velocity = new Vector2(projectileSpeed, 0);
            AudioSource.PlayClipAtPoint(shootSound, Camera.main.transform.position, shootSoundVolume);
            yield return new WaitForSeconds(projectfileFiringPeriod);
        }
    }

    private void SetUpMoveBoundaries()
    {
        Camera gameCamera = Camera.main;
        xMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x + padding_x_left;
        xMax = gameCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x - padding_x_right;

        yMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y + padding_y_bottom;
        yMax = gameCamera.ViewportToWorldPoint(new Vector3(0, 1, 0)).y - padding_y_top;

    }

    private void Move() {
        var deltaX = Input.GetAxis("Horizontal") * Time.deltaTime * moveSpeed;
        var deltaY = Input.GetAxis("Vertical") * Time.deltaTime * moveSpeed;

        var newXPos = Mathf.Clamp(transform.position.x + deltaX, xMin, xMax);
        var newYPos = Mathf.Clamp(transform.position.y + deltaY, yMin, yMax);

        transform.position = new Vector2(newXPos, newYPos);
	}
   
}
