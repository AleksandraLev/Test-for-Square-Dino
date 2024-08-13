using System.Collections;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    // Таймер для отслеживания времени, когда объект активен
    private float activeTime = 0f;
    public float maxActiveTime = 60f; // Время в секундах, после которого объект деактивируется
    public int damageAmount = 100;

    void OnEnable()
    {
        // Сброс таймера при активации объекта
        activeTime = 0f;
    }

    void Update()
    {
        // Увеличиваем таймер на время, прошедшее с последнего кадра
        activeTime += Time.deltaTime;

        // Проверяем, прошло ли больше 20 секунд
        if (activeTime >= maxActiveTime)
        {
            Deactivate();
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        //Debug.Log("Collision detected with: " + collision.gameObject.name);
        // Когда объект сталкивается с другим коллайдером, деактивируем его
        Deactivate();
    }

    void OnTriggerEnter(Collider other)
    {
        //Debug.Log("Trigger Collision detected with: " + other.gameObject.name);
        // Когда объект попадает в триггер, деактивируем его
        Deactivate();
        if (other.tag == "Enemy")
        {
            //transform.parent = other.transform;
            other.GetComponent<Enemy>().TakeDamage(damageAmount);
        }
    }

    void Deactivate()
    {
        // Деактивация объекта
        gameObject.SetActive(false);
        //GunController.anyInactive = true;
    }
}