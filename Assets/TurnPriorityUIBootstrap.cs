using UnityEngine;

public class TurnPriorityUIBootstrap : MonoBehaviour
{
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    static void Initialize()
    {
        // Create a game object for our UI manager
        GameObject uiManagerObject = new GameObject("TurnPriorityUIManager");
        
        // Add the UI manager component
        uiManagerObject.AddComponent<TurnPriorityUIManager>();
        
        // This object will persist across scene loads
        DontDestroyOnLoad(uiManagerObject);
        
        Debug.Log("Turn Priority UI System initialized!");
    }
} 