using Unity.Netcode;
using UnityEngine;

public class PlayerMovement : NetworkBehaviour
{
    void Update()
    {
        if (IsOwner)
        {
            Vector3 move = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
            transform.position += move * Time.deltaTime;

            // ����� ����������� RPC ��� ���������� ������� �� �������
            UpdatePositionServerRpc(transform.position);

        }
    }

    [ServerRpc(Delivery = RpcDelivery.Unreliable)]
    void UpdatePositionServerRpc(Vector3 newPosition)
    {
        // ���������� ������� ������ �� �������
        transform.position = newPosition;

        // ����� ����� ���������� ������� ���� ��������
        UpdatePositionClientRpc(newPosition);
    }

    [ClientRpc(Delivery = RpcDelivery.Unreliable)]
    void UpdatePositionClientRpc(Vector3 newPosition)
    {
        // ��������� ������� �� �������
        transform.position = newPosition;
    }
}
