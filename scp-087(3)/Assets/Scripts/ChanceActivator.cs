using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ChanceActivator : MonoBehaviour
{
    public GameObject targetObject;      // Объект, который может появиться
    public GameObject player;            // Игрок
    public MonoBehaviour playerController; // Скрипт управления игроком (отключаем его)
    public Transform cameraTransform;    // Камера игрока
    public Transform focusTarget;        // Точка, куда камера должна повернуться (можно использовать сам targetObject)
    public GameObject uiPanel;           // Панель, которую показываем через 1 секунду

    public float cameraRotateSpeed = 2f;

    private bool rotatingCamera = false;

    void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            print(123);
            int chance = PlayerPrefs.GetInt("chance", 0);
            int roll = Random.Range(0, 100);

            print(chance + " " + roll);

            if (roll < chance)
            {
                Cursor.lockState = CursorLockMode.None;
                targetObject.SetActive(true);
                playerController.enabled = false;
                rotatingCamera = true;
                StartCoroutine(ShowUIPanelAfterDelay(2f));
            }
        }
    }

    void Update()
    {
        if (rotatingCamera)
        {
            Vector3 dir = (focusTarget.position - cameraTransform.position).normalized;
            Quaternion lookRot = Quaternion.LookRotation(dir);
            cameraTransform.rotation = Quaternion.Slerp(cameraTransform.rotation, lookRot, Time.deltaTime * cameraRotateSpeed);
        }
    }

    IEnumerator ShowUIPanelAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        uiPanel.SetActive(true);
        rotatingCamera = false;
    }
}
