using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public int HP = 100;
    public Slider healtheBar;

    private void Update()
    {
        healtheBar.value = HP;
    }

    public void TakeDamage(int damageAmount)
    {
        HP -= damageAmount;
        if (HP <= 0)
        {
            //GetComponent<Collider>().enabled = false;
            healtheBar.gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Проверяем, если объект столкновения имеет тег "Bullet"
        if (other.CompareTag("Bullet"))
        {
            // Получаем компонент Animator на текущем объекте
            Animator animator = GetComponent<Animator>();

            // Если компонент найден, выключаем его
            if (animator != null && animator.enabled)
            {
                animator.enabled = false;
                GameManager.enemyCount++;
            }
        }
    }
}
