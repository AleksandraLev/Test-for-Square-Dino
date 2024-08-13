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
    private bool isMoving = false; // ���� ��� ������������ ��������
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
        isMoving = true; // ������������� ����

        while (index < 2)
        {
            Vector3 destination = waypoints[index].transform.position;

            // ����������� �������� ��������
            //animator.SetTrigger("Running");
            animator.SetFloat("MotionSpeed", 2.4f);

            transform.rotation = Quaternion.Slerp(waypoints[index].transform.rotation, Quaternion.LookRotation(waypoints[index].transform.position - transform.position), rotationSpeedForMoving * Time.deltaTime);
            transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);

            while (Vector3.Distance(transform.position, destination) > 0.03f)
            {
                // ����������� ������� � ����
                transform.position = Vector3.MoveTowards(transform.position, destination, speed * Time.deltaTime);

                // ��������� �� ���������� �����
                yield return null;
            }

            index++;
        }

        // ���������� ��������
        if (index == 2)
        {
            //animator.ResetTrigger("Running");
            animator.SetFloat("MotionSpeed", 0.0f);
        }

        isMoving = false; // ���������� ���� ����� ���������� ��������
        shooting = true;
        gunController.shooting = shooting;
        tapToShootAndTurnAround.SetActive(true);
    }

    public IEnumerator Continue1()
    {
        shooting = false;
        gunController.shooting = shooting;
        isMoving = true; // ������������� ����

        while (index < 4)
        {
            Vector3 destination = waypoints[index].transform.position;

            // ����������� �������� ��������
            //animator.SetTrigger("Running");
            animator.SetFloat("MotionSpeed", 2.4f);

            transform.rotation = Quaternion.Slerp(waypoints[index].transform.rotation, Quaternion.LookRotation(waypoints[index].transform.position - transform.position), rotationSpeedForMoving * Time.deltaTime);
            transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);

            while (Vector3.Distance(transform.position, destination) > 0.03f)
            {
                // ����������� ������� � ����
                transform.position = Vector3.MoveTowards(transform.position, destination, speed * Time.deltaTime);

                // ��������� �� ���������� �����
                yield return null;
            }

            index++;
        }

        // ���������� ��������
        if (index == 4)
        {
            //animator.ResetTrigger("Running");
            animator.SetFloat("MotionSpeed", 0.0f);
        }

        isMoving = false; // ���������� ���� ����� ���������� ��������
        continue1 = false;
        shooting = true;
        gunController.shooting = shooting;
    }

    void Update()
    {
        // ��������� �������� ������ ���� �������� �� ������
        if (Input.GetMouseButtonDown(0) && !isMoving && index < 2)
        {
            StartCoroutine(MyStart());
        }
        if (continue1 && !isMoving && index < 5)
        {
            StartCoroutine(Continue1());
        }
        // �������� ������� �� �����
        if (Input.GetMouseButton(0) && !isMoving && shooting)
        {
            tapToShootAndTurnAround.SetActive(false);
            // �������� ���������� ������� �� �����
            Vector3 touchPosition = Input.mousePosition;

            // ��������� ���������� ������ � ������� ���������� (� ������ ��������� ������)
            Ray ray = Camera.main.ScreenPointToRay(touchPosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                // �������� ������� � ���� �� �����������
                Vector3 targetPosition = hit.point;

                // ������������ ����������� � ����� �������
                Vector3 direction = targetPosition - transform.position;

                // �������� ����������� �� ��� y
                direction.y = 0f;

                // ������������ ���� �������� �� ��� Y
                float angle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;

                // ������������ ������ ������ �� ��� Y
                Quaternion targetRotation = Quaternion.Euler(new Vector3(0, angle, 0));
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
            }
        }
    }
}
