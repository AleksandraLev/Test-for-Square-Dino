using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    public List<GameObject> waypoints = new List<GameObject>();
    private int index;
    public float speed = 2f;
    private Animator animator;
    private bool isMoving = false; // Флаг для отслеживания движения
    public bool continue1 = false;
    bool shooting = false;
    public float rotationSpeed = 2;
    public float rotationSpeedForMoving = 2;
    public GunController gunController;
    public GameObject tapToPlay;
    public GameObject tapToShootAndTurnAround;

    private void Start()
    {
        animator = GetComponent<Animator>();
        index = 0;
    }

    IEnumerator MyStart()
    {
        tapToPlay.SetActive(false);
        isMoving = true; // Устанавливаем флаг

        while (index < 2)
        {
            Vector3 destination = waypoints[index].transform.position;

            // Проигрываем анимацию движения
            //animator.SetTrigger("Running");
            animator.SetFloat("MotionSpeed", 2.4f);

            transform.rotation = Quaternion.Slerp(waypoints[index].transform.rotation, Quaternion.LookRotation(waypoints[index].transform.position - transform.position), rotationSpeedForMoving * Time.deltaTime);
            transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);

            while (Vector3.Distance(transform.position, destination) > 0.03f)
            {
                // Перемещение объекта к цели
                transform.position = Vector3.MoveTowards(transform.position, destination, speed * Time.deltaTime);

                // Подождать до следующего кадра
                yield return null;
            }

            index++;
        }

        // Завершение движения
        if (index == 2)
        {
            //animator.ResetTrigger("Running");
            animator.SetFloat("MotionSpeed", 0.0f);
        }

        isMoving = false; // Сбрасываем флаг после завершения движения
        shooting = true;
        gunController.shooting = shooting;
        tapToShootAndTurnAround.SetActive(true);
    }

    public IEnumerator Continue1()
    {
        shooting = false;
        gunController.shooting = shooting;
        isMoving = true; // Устанавливаем флаг

        while (index < 4)
        {
            Vector3 destination = waypoints[index].transform.position;

            // Проигрываем анимацию движения
            //animator.SetTrigger("Running");
            animator.SetFloat("MotionSpeed", 2.4f);

            transform.rotation = Quaternion.Slerp(waypoints[index].transform.rotation, Quaternion.LookRotation(waypoints[index].transform.position - transform.position), rotationSpeedForMoving * Time.deltaTime);
            transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);

            while (Vector3.Distance(transform.position, destination) > 0.03f)
            {
                // Перемещение объекта к цели
                transform.position = Vector3.MoveTowards(transform.position, destination, speed * Time.deltaTime);

                // Подождать до следующего кадра
                yield return null;
            }

            index++;
        }

        // Завершение движения
        if (index == 4)
        {
            //animator.ResetTrigger("Running");
            animator.SetFloat("MotionSpeed", 0.0f);
        }

        isMoving = false; // Сбрасываем флаг после завершения движения
        continue1 = false;
        shooting = true;
        gunController.shooting = shooting;
    }

    void Update()
    {
        // Запускаем корутину только если движение не начато
        if (Input.GetMouseButtonDown(0) && !isMoving && index < 2)
        {
            StartCoroutine(MyStart());
        }
        if (continue1 && !isMoving && index < 5)
        {
            StartCoroutine(Continue1());
        }
        // Проверка нажатия на экран
        if (Input.GetMouseButton(0) && !isMoving && shooting)
        {
            tapToShootAndTurnAround.SetActive(false);
            // Получаем координаты нажатия на экран
            Vector3 touchPosition = Input.mousePosition;

            // Переводим координаты экрана в мировые координаты (с учетом положения камеры)
            Ray ray = Camera.main.ScreenPointToRay(touchPosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                // Получаем позицию в мире на поверхности
                Vector3 targetPosition = hit.point;

                // Рассчитываем направление к точке нажатия
                Vector3 direction = targetPosition - transform.position;

                // Обнуляем направление по оси y
                direction.y = 0f;

                // Рассчитываем угол поворота по оси Y
                float angle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;

                // Поворачиваем объект только по оси Y
                Quaternion targetRotation = Quaternion.Euler(new Vector3(0, angle, 0));
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
            }
        }
    }
}
