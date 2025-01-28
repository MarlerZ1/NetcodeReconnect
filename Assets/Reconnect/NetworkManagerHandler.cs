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



    // ����� ������ �����������
    private void HandleClientDisconnected(ulong clientId)
    {
        GameObject playerObject = FindPlayerObjectByClientId(clientId);  // ����� ������ ������
        if (playerObject != null)
           positionManager.SavePlayerPosition(playerObject.transform.position, clientId);  // ���������� ������� �� �������

        
    }

    private GameObject FindPlayerObjectByClientId(ulong clientId)
    {
        foreach (var player in NetworkManager.Singleton.SpawnManager.SpawnedObjects.Values)
        {
            // ���������, �������� �� ������� ������ �������� ������
            if (player.IsOwner && player.OwnerClientId == clientId)
            {
                return player.gameObject;
            }
        }

        return null;
    }

}
