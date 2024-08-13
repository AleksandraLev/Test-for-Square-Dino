using System.Collections;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    // ������ ��� ������������ �������, ����� ������ �������
    private float activeTime = 0f;
    public float maxActiveTime = 60f; // ����� � ��������, ����� �������� ������ ��������������
    public int damageAmount = 100;

    void OnEnable()
    {
        // ����� ������� ��� ��������� �������
        activeTime = 0f;
    }

    void Update()
    {
        // ����������� ������ �� �����, ��������� � ���������� �����
        activeTime += Time.deltaTime;

        // ���������, ������ �� ������ 20 ������
        if (activeTime >= maxActiveTime)
        {
            Deactivate();
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        //Debug.Log("Collision detected with: " + collision.gameObject.name);
        // ����� ������ ������������ � ������ �����������, ������������ ���
        Deactivate();
    }

    void OnTriggerEnter(Collider other)
    {
        //Debug.Log("Trigger Collision detected with: " + other.gameObject.name);
        // ����� ������ �������� � �������, ������������ ���
        Deactivate();
        if (other.tag == "Enemy")
        {
            //transform.parent = other.transform;
            other.GetComponent<Enemy>().TakeDamage(damageAmount);
        }
    }

    void Deactivate()
    {
        // ����������� �������
        gameObject.SetActive(false);
        //GunController.anyInactive = true;
    }
}