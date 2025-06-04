using UnityEngine;

public class TeleportSender : MonoBehaviour
{
    public Transform teleportTarget; // ��������� ������ ��� (������� ������)

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            CharacterController cc = other.GetComponent<CharacterController>();

            if (cc != null)
            {
                // �������� ��������� ����������, ����� �������� ���� � ������������
                cc.enabled = false;
                other.transform.position = teleportTarget.position;
                cc.enabled = true;
            }
            else
            {
                // ��� �����������, ������ �������������
                other.transform.position = new Vector3( other.transform.position.x, teleportTarget.transform.position.y, other.transform.position.z);
            }
        }
    }
}

