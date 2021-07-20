using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private float speed = 5f;
        [SerializeField] private float minY, maxY;
        [SerializeField] private GameObject playerBullet;
        [SerializeField] private Transform firePoint;
        [SerializeField] private float shootTimer = 0.35f;
        [SerializeField] private float currentShootTimer;
        [SerializeField] private GameObject gameOverPanel;
        private bool canShoot;
        private AudioSource laserAudio;
        private AudioSource explosionSound;
        private Animator animator;
        private void Awake()
        {
            laserAudio = GetComponent<AudioSource>();
            explosionSound = GetComponent<AudioSource>();
            animator = GetComponent<Animator>();
        }
        private void Start()
        {
            gameOverPanel.SetActive(false);
            currentShootTimer = shootTimer;
        }

        void Update()
        {
            float vertical = Input.GetAxisRaw("Vertical");
            MovePlayer(vertical);
            Shoot();
        }

        void MovePlayer(float vertical)
        {
            Vector3 position = transform.position;
            if (vertical > 0f)
            {
                position.y += speed * Time.deltaTime;
                if (position.y > maxY)
                {
                    position.y = maxY;
                }
            }
            else if (vertical < 0f)
            {
                position.y -= speed * Time.deltaTime;
                if (position.y < minY)
                {
                    position.y = minY;
                }
            }
            transform.position = position;
        }

        void Shoot()
        {
            shootTimer += Time.deltaTime;
            if (shootTimer > currentShootTimer)
            {
                canShoot = true;
            }
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                if (canShoot)
                {
                    canShoot = false;
                    shootTimer = 0f;
                    Instantiate(playerBullet, firePoint.position, Quaternion.identity);
                    laserAudio.Play();
                }
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.GetComponent<EnemyController>() != null || collision.gameObject.GetComponent<Bullet>() != null)
            {
                animator.Play("Destroy");
                explosionSound.Play();
                gameOverPanel.SetActive(true);
            }
        }
    }
}