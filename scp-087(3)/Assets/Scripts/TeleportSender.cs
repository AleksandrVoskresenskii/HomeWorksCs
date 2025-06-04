using UnityEngine;

public class TeleportSender : MonoBehaviour
{
    public Transform teleportTarget; // Указываем зелёный куб (позицию выхода)

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            CharacterController cc = other.GetComponent<CharacterController>();

            if (cc != null)
            {
                // Временно отключаем контроллер, чтобы избежать бага с перемещением
                cc.enabled = false;
                other.transform.position = teleportTarget.position;
                cc.enabled = true;
            }
            else
            {
                // Без контроллера, просто телепортируем
                other.transform.position = new Vector3( other.transform.position.x, teleportTarget.transform.position.y, other.transform.position.z);
            }
        }
    }
}

