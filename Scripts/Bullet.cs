using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter
{
    public class Bullet : MonoBehaviour
    {
        [SerializeField] private float speed = 6f;
        [SerializeField] private float deactivateTimer = 3f;
        [HideInInspector] public bool isEnemyBullet = false;

        void Start()
        {
            if (isEnemyBullet)
            {
                speed *= -1f;
            }
            Invoke("DeactivateGameObject", deactivateTimer);
        }

        void Update()
        {
            Move();
        }

        void Move()
        {
            Vector3 position = transform.position;
            position.x += speed * Time.deltaTime;
            transform.position = position;
        }

        void DeactivateGameObject()
        {
            gameObject.SetActive(false);
        }

        private void OnTriggerEnter2D(Collider2D target)
        {
            // if (target.gameObject.CompareTag("Bullet") || target.gameObject.CompareTag("Enemy"))
            // {
            //     gameObject.SetActive(false);
            // }
            if (target.tag == "Bullet" || target.tag == "Enemy")
            {
                gameObject.SetActive(false);
            }
        }
    }
}