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
        // ���������, ���� ������ ������������ ����� ��� "Bullet"
        if (other.CompareTag("Bullet"))
        {
            // �������� ��������� Animator �� ������� �������
            Animator animator = GetComponent<Animator>();

            // ���� ��������� ������, ��������� ���
            if (animator != null && animator.enabled)
            {
                animator.enabled = false;
                GameManager.enemyCount++;
            }
        }
    }
}
