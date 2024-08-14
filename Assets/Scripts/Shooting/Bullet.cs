using System.Collections;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private float activeTime = 0f;
    public float maxActiveTime = 60f;
    public int damageAmount = 100;

    private void OnEnable()
    {
        ResetActiveTime();
    }

    private void Update()
    {
        TrackActiveTime();
    }

    private void OnCollisionEnter(Collision collision)
    {
        HandleCollision();
    }

    private void OnTriggerEnter(Collider other)
    {
        HandleTriggerEnter(other);
    }

    private void ResetActiveTime()
    {
        activeTime = 0f;
    }

    private void TrackActiveTime()
    {
        activeTime += Time.deltaTime;

        if (activeTime >= maxActiveTime)
        {
            Deactivate();
        }
    }

    private void HandleCollision()
    {
        Deactivate();
    }

    private void HandleTriggerEnter(Collider other)
    {
        Deactivate();

        if (other.CompareTag("Enemy"))
        {
            if (other.TryGetComponent<Enemy>(out var enemy))
            {
                enemy.TakeDamage(damageAmount);
            }
        }
    }

    private void Deactivate()
    {
        gameObject.SetActive(false);
    }
}