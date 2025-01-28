using Unity.Netcode;
using UnityEngine;

public class NetworkManagerHandler : MonoBehaviour
{
    private PlayerPositionManager positionManager;

    private void Awake()
    {
        positionManager = GetComponent<PlayerPositionManager>();
        NetworkManager.Singleton.OnClientDisconnectCallback += HandleClientDisconnected;
    }



    // Когда клиент отключается
    private void HandleClientDisconnected(ulong clientId)
    {
        GameObject playerObject = this.gameObject;  // Найти объект игрока
        if (playerObject != null)
           positionManager.SavePlayerPosition(playerObject.transform.position, clientId);  // Сохранение позиции на сервере

        
    }

}
