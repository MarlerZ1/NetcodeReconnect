using System.IO;
using UnityEngine;

public class PlayerPositionManager : MonoBehaviour
{
    private string filePath;

    private void Awake()
    {
        // Путь к файлу на сервере
        filePath = Path.Combine(Application.persistentDataPath, "playerPosition.json");
    }

    // Сохранение позиции на сервере
    public void SavePlayerPosition(Vector3 position, ulong clientId)
    {
        PlayerPositionData playerPositionData = new PlayerPositionData
        {
            x = position.x,
            y = position.y,
            z = position.z
        };

        string json = JsonUtility.ToJson(playerPositionData);
        File.WriteAllText(filePath, json);  // Сохраняем данные на сервере
        Debug.Log("Player position saved: " + json);
    }

    // Восстановление позиции на сервере
    public Vector3 LoadPlayerPosition(ulong clientId)
    {
        Debug.Log(filePath);
        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            PlayerPositionData playerPositionData = JsonUtility.FromJson<PlayerPositionData>(json);
            Debug.Log("Player position loaded: " + json);
            Debug.Log(playerPositionData.x + "\t" + playerPositionData.y + "\t" + playerPositionData.z);
            return new Vector3(playerPositionData.x, playerPositionData.y, playerPositionData.z);
        }
        Debug.Log("Zero vector");
        return Vector3.zero;  // Если файл не существует, возвращаем начальную позицию
    }
}
