using Unity.Netcode;
using UnityEngine;

public class NetworkPlayerSpawner : MonoBehaviour
{
    public GameObject playerPrefab;  // ������ ������ ��� ������

    private void OnEnable()
    {
        // ������������� �� �������, ����� ������ ������������
        NetworkManager.Singleton.OnClientConnectedCallback += HandleClientConnected;
    }

    private void HandleClientConnected(ulong clientId)
    {
        Debug.Log("HandleClientConnected " + clientId);
        // ���������, ��� ��� ������, � ������� ������
        if (NetworkManager.Singleton.IsServer)
        {
            Debug.Log("Spawning player for client " + clientId);

            // ������� ������ � �������� ��� NetworkObject
            var playerObject = Instantiate(playerPrefab); // �������� ������� ������
            playerObject.GetComponent<NetworkObject>().SpawnAsPlayerObject(clientId); // ������� � ��������� � clientId
        }
    }

    private void OnDisable()
    {
        // ������� �������� �� �������
        if (NetworkManager.Singleton != null)
            NetworkManager.Singleton.OnClientConnectedCallback -= HandleClientConnected;

    }
}
