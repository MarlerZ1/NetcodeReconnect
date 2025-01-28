using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

public class NetworkUIManager : MonoBehaviour
{
    public Button hostButton;
    public Button clientButton;

    void Start()
    {
        // ����������� �������� � �������
        hostButton.onClick.AddListener(StartHost);
        clientButton.onClick.AddListener(StartClient);
    }

    // ������ �����
    void StartHost()
    {
        NetworkManager.Singleton.StartHost();
        Debug.Log("Host Started");
    }

    // ������ �������
    void StartClient()
    {
        NetworkManager.Singleton.StartClient();
        Debug.Log("Client Started");
    }
}
