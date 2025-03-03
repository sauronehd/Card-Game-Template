using UnityEngine;
using UnityEngine.UI;

public class TurnPriorityUIManager : MonoBehaviour
{
    private static TurnPriorityUIManager _instance;
    public static TurnPriorityUIManager Instance { get { return _instance; } }
    
    private Canvas mainCanvas;
    private TurnPriorityUI turnPriorityUI;
    
    private void Awake()
    {
        // Singleton pattern to ensure only one instance exists
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        
        _instance = this;
        DontDestroyOnLoad(this.gameObject);
        
        // Find or create a canvas
        InitializeCanvas();
        
        // Create the turn priority UI
        CreateTurnPriorityUI();
    }
    
    private void InitializeCanvas()
    {
        // Try to find an existing canvas
        mainCanvas = FindObjectOfType<Canvas>();
        
        // If no canvas exists, create one
        if (mainCanvas == null)
        {
            GameObject canvasObj = new GameObject("UI_Canvas");
            mainCanvas = canvasObj.AddComponent<Canvas>();
            mainCanvas.renderMode = RenderMode.ScreenSpaceOverlay;
            
            // Add required components for UI
            canvasObj.AddComponent<CanvasScaler>();
            canvasObj.AddComponent<GraphicRaycaster>();
        }
    }
    
    private void CreateTurnPriorityUI()
    {
        // Create UI container
        GameObject turnPriorityObj = new GameObject("Turn_Priority_UI");
        turnPriorityObj.transform.SetParent(mainCanvas.transform, false);
        
        // Position it at the top center of the screen
        RectTransform rectTransform = turnPriorityObj.AddComponent<RectTransform>();
        rectTransform.anchorMin = new Vector2(0.5f, 1);
        rectTransform.anchorMax = new Vector2(0.5f, 1);
        rectTransform.pivot = new Vector2(0.5f, 1);
        rectTransform.anchoredPosition = Vector2.zero;
        rectTransform.sizeDelta = new Vector2(300, 100);
        
        // Add the turn priority UI component
        turnPriorityUI = turnPriorityObj.AddComponent<TurnPriorityUI>();
    }
    
    // Method to get reference to the UI if needed elsewhere
    public TurnPriorityUI GetTurnPriorityUI()
    {
        return turnPriorityUI;
    }
} 