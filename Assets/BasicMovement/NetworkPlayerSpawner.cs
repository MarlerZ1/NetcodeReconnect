using Unity.Netcode;
using UnityEngine;

public class NetworkPlayerSpawner : MonoBehaviour
{
    public GameObject playerPrefab;  // Префаб игрока для спауна

    private void OnEnable()
    {
        // Подписываемся на событие, когда клиент подключается
        NetworkManager.Singleton.OnClientConnectedCallback += HandleClientConnected;
    }

    private void HandleClientConnected(ulong clientId)
    {
        Debug.Log("HandleClientConnected " + clientId);
        // Проверяем, что это сервер, и создаем игрока
        if (NetworkManager.Singleton.IsServer)
        {
            Debug.Log("Spawning player for client " + clientId);

            // Создаем игрока и передаем его NetworkObject
            var playerObject = Instantiate(playerPrefab); // Создание объекта игрока
            playerObject.GetComponent<NetworkObject>().SpawnAsPlayerObject(clientId); // Спауним с привязкой к clientId
        }
    }

    private void OnDisable()
    {
        // Очищаем подписку на событие
        if (NetworkManager.Singleton != null)
            NetworkManager.Singleton.OnClientConnectedCallback -= HandleClientConnected;

    }
}
