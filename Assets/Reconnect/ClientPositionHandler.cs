using Unity.Netcode;
using UnityEngine;

public class ClientPositionHandler : MonoBehaviour
{
    private PlayerPositionManager positionManager;

    private void Awake()
    {
        positionManager = GetComponent<PlayerPositionManager>();
    }

    private void Start()
    {
        if (NetworkManager.Singleton.IsClient)
        {
            Vector3 restoredPosition = positionManager.LoadPlayerPosition(NetworkManager.Singleton.LocalClientId);
            transform.position = restoredPosition;
        }
    }
}
