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
    private bool isMoving = false;
    public bool myContinue = false;
    private bool shooting = false;
    public bool finish = false;
    public float rotationSpeed = 2f;
    public float rotationSpeedForMoving = 2f;
    public GunController gunController;
    public GameObject tapToPlay;
    public GameObject tapToShootAndTurnAround;

    private void Start()
    {
        animator = GetComponent<Animator>();
        index = 0;
    }

    private IEnumerator MoveToWaypoint(int targetIndex)
    {
        tapToPlay.SetActive(false);
        isMoving = true;
        animator.SetFloat("MotionSpeed", 2.0f);

        while (index < targetIndex)
        {
            Vector3 destination = waypoints[index].transform.position;

            RotateTowards(waypoints[index].transform.position, rotationSpeedForMoving);

            while (Vector3.Distance(transform.position, destination) > 0.03f)
            {
                RotateTowards(destination, rotationSpeedForMoving);
                ResetRotationY();
                transform.position = Vector3.MoveTowards(transform.position, destination, speed * Time.deltaTime);
                yield return null;
            }

            index++;
        }

        StopMovement();
    }

    private void RotateTowards(Vector3 targetPosition, float rotationSpeed)
    {
        // Рассчитываем направление к точке нажатия
        Vector3 direction = targetPosition - transform.position;

        // Обнуляем направление по оси y
        direction.y = 0f;

        // Рассчитываем угол поворота по оси Y
        float angle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;

        if (angle < -45f)
            angle = -45f;
        else if (angle > 45)
            angle = 45f;

        Quaternion targetRotation = Quaternion.LookRotation(targetPosition - transform.position);
        targetRotation = Quaternion.Euler(0, angle, 0);
        if (Input.GetMouseButton(0) && shooting && !isMoving)
            animator.SetFloat("MotionSpeed", 10.0f);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }

    private void ResetRotationY()
    {
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, 0, transform.eulerAngles.z);
    }

    private void StopMovement()
    {
        tapToShootAndTurnAround.SetActive(true);
        animator.SetFloat("MotionSpeed", 0.0f);
        isMoving = false;
        shooting = true;
        gunController.shooting = shooting;
    }

    public IEnumerator ContinueMovement(int finishPoint)
    {
        yield return MoveToWaypoint(finishPoint);
        myContinue = false;
        tapToShootAndTurnAround.SetActive(false);
    }

    private void HandleTouchRotation()
    {
        if (Input.GetMouseButton(0) && shooting && !isMoving)
        {
            tapToShootAndTurnAround.SetActive(false);

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                RotateTowards(hit.point, rotationSpeed);
            }
        }
        else if (shooting && !isMoving)
        {
            animator.SetFloat("MotionSpeed", 0.0f);
        }
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && !isMoving && index == 0)
        {
            StartCoroutine(MoveToWaypoint(2));
        }
        else if (myContinue && !isMoving && index == 2)
        {
            StartCoroutine(ContinueMovement(4));
        }
        else if (myContinue && !isMoving && index == 4)
        {
            StartCoroutine(ContinueMovement(6));
        }
        else if (index == waypoints.Count)
        {
            finish = true;
        }
        else if (Input.GetMouseButton(0) && shooting && !isMoving)
        {
            HandleTouchRotation();
        }
        
    }
}
