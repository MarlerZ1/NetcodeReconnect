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

            // Вызов ненадежного RPC для обновления позиции на сервере
            UpdatePositionServerRpc(transform.position);

        }
    }

    [ServerRpc(Delivery = RpcDelivery.Unreliable)]
    void UpdatePositionServerRpc(Vector3 newPosition)
    {
        // Обновление позиции игрока на сервере
        transform.position = newPosition;

        // После этого отправляем позицию всем клиентам
        UpdatePositionClientRpc(newPosition);
    }

    [ClientRpc(Delivery = RpcDelivery.Unreliable)]
    void UpdatePositionClientRpc(Vector3 newPosition)
    {
        // Обновляем позицию на клиенте
        transform.position = newPosition;
    }
}
