using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter
{
    public class EnemyController : MonoBehaviour
    {
        [SerializeField] private float speed = 3f;
        [SerializeField] private float rotateSpeed = 50f;
        [SerializeField] private float boundX = -11f;
        [SerializeField] private bool canShoot;
        [SerializeField] private bool canRotate;
        [SerializeField] private Transform firePoint;
        [SerializeField] private GameObject bulletPrefab;
        private bool canMove = true;
        private Animator animator;
        private AudioSource explosionSound;
        void Awake()
        {
            animator = GetComponent<Animator>();
            explosionSound = GetComponent<AudioSource>();
        }

        private void Start()
        {
            if (canRotate)
            {
                if (Random.Range(0, 2) > 0)
                {
                    rotateSpeed = Random.Range(rotateSpeed, rotateSpeed + 20f);
                    rotateSpeed *= -1f;
                }
                else
                {
                    rotateSpeed = Random.Range(rotateSpeed, rotateSpeed + 20f);
                }
            }
            if (canShoot)
            {
                Invoke("StartShooting", Random.Range(1f, 3f));
            }
        }

        void Update()
        {
            Move();
            RotateEnemy();
        }

        void Move()
        {
            if (canMove)
            {
                Vector3 position = transform.position;
                position.x -= speed * Time.deltaTime;
                transform.position = position;

                if (position.x < boundX)
                {
                    gameObject.SetActive(false);
                }
            }
        }

        void RotateEnemy()
        {
            if (canRotate)
            {
                transform.Rotate(new Vector3(0f, 0f, rotateSpeed * Time.deltaTime), Space.World);
            }
        }

        void StartShooting()
        {
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
            bullet.GetComponent<Bullet>().isEnemyBullet = true;
            if (canShoot)
            {
                Invoke("StartShooting", Random.Range(1f, 3f));
            }
        }

        void TurnOffGameObject()
        {
            gameObject.SetActive(false);
        }

        private void OnTriggerEnter2D(Collider2D target)
        {
            // if (target.gameObject.CompareTag("Bullet"))
            // {
            //     canMove = false;
            //     if (canShoot)
            //     {
            //         canShoot = false;
            //         CancelInvoke("StartShooting");
            //     }
            // }
            if (target.tag == "Bullet")
            {
                canMove = false;
                if (canShoot)
                {
                    canShoot = false;
                    CancelInvoke("StartShooting");
                }
            }
            Invoke("TurnOffGameObject", 3f);
            explosionSound.Play();
            animator.Play("Destroy");
        }
    }
}