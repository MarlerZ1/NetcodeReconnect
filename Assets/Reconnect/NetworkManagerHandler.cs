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
        GameObject playerObject = FindPlayerObjectByClientId(clientId);  // Найти объект игрока
        if (playerObject != null)
           positionManager.SavePlayerPosition(playerObject.transform.position, clientId);  // Сохранение позиции на сервере

        
    }

    private GameObject FindPlayerObjectByClientId(ulong clientId)
    {
        foreach (var player in NetworkManager.Singleton.SpawnManager.SpawnedObjects.Values)
        {
            // Проверяем, является ли текущий объект объектом игрока
            if (player.IsOwner && player.OwnerClientId == clientId)
            {
                return player.gameObject;
            }
        }

        return null;
    }

}
