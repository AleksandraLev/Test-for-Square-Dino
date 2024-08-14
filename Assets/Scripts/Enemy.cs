using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    [SerializeField] private int maxHP = 100;
    [SerializeField] private Slider healthBar;

    private int currentHP;

    private void Start()
    {
        currentHP = maxHP;
        UpdateHealthBar();
    }

    public void TakeDamage(int damageAmount)
    {
        currentHP -= damageAmount;
        UpdateHealthBar();

        if (currentHP <= 0)
        {
            Die();
        }
    }

    private void UpdateHealthBar()
    {
        if (healthBar != null)
        {
            healthBar.value = currentHP;
        }
    }

    private void Die()
    {
        if (healthBar != null)
        {
            healthBar.gameObject.SetActive(false);
        }

        // Отключаем анимацию, если есть Animator
        Animator animator = GetComponent<Animator>();
        if (animator != null && animator.enabled)
        {
            animator.enabled = false;
            GameManager.enemyCount++;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Проверяем, если объект столкновения имеет тег "Bullet"
        if (other.CompareTag("Bullet"))
        {
            // Обрабатываем урон от пули
            Bullet bullet = other.GetComponent<Bullet>();
            if (bullet != null)
            {
                TakeDamage(bullet.damageAmount);
            }
            //if (currentHP <= 0)
            //{
            //    // Добавляем к счётчику врагов в GameManager
            //    GameManager.enemyCount++;
            //}
        }
    }
}
