using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;

public class TurnPriorityUI : MonoBehaviour
{
    public GameManager gm;
    public GameObject uiPanel;
    public TextMeshProUGUI turnText;
    public TextMeshProUGUI priorityText;
    
    // Visual elements for improved UX
    private Image panelImage;
    private Image turnHighlight;
    private Image priorityHighlight;
    
    // Color settings for different states
    private Color playerTurnColor = new Color(0.2f, 0.6f, 1f, 0.9f);    // Blue
    private Color aiTurnColor = new Color(1f, 0.4f, 0.4f, 0.9f);        // Red
    private Color playerPriorityColor = new Color(0.2f, 0.8f, 0.2f, 0.9f);  // Green
    private Color aiPriorityColor = new Color(1f, 0.7f, 0.2f, 0.9f);    // Orange
    
    // Track the current states for transition effects
    private int lastTurn = 0;
    private int lastPriority = 0;
    
    void Start()
    {
        // Find the GameManager if not assigned
        if (gm == null)
        {
            gm = GameObject.FindObjectOfType<GameManager>();
        }
        
        // If UI panel doesn't exist, create it
        if (uiPanel == null)
        {
            CreateUIPanel();
        }
        
        // Assign references to GameManager
        if (gm != null)
        {
            gm.turnDisplay = turnText;
            gm.priorityDisplay = priorityText;
        }
        else
        {
            Debug.LogError("GameManager not found. Turn and priority display will not work.");
        }
    }
    
    void Update()
    {
        if (gm != null)
        {
            // Check if turn changed and play transition
            if (gm.turn != lastTurn && lastTurn != 0)
            {
                PlayTurnTransition(gm.turn);
            }
            
            // Check if priority changed and play transition
            if (gm.priority != lastPriority && lastPriority != 0)
            {
                PlayPriorityTransition(gm.priority);
            }
            
            // Update the state tracking variables
            lastTurn = gm.turn;
            lastPriority = gm.priority;
        }
    }
    
    void CreateUIPanel()
    {
        // Create a panel for our UI elements
        uiPanel = new GameObject("Turn_Priority_Panel");
        uiPanel.transform.SetParent(transform);
        
        RectTransform panelRect = uiPanel.AddComponent<RectTransform>();
        panelRect.anchorMin = new Vector2(0.5f, 1f);
        panelRect.anchorMax = new Vector2(0.5f, 1f);
        panelRect.pivot = new Vector2(0.5f, 1f);
        panelRect.anchoredPosition = new Vector2(0, -20);
        panelRect.sizeDelta = new Vector2(300, 110);
        
        // Add a background image
        panelImage = uiPanel.AddComponent<Image>();
        panelImage.color = new Color(0, 0, 0, 0.7f);
        
        // Add panel border
        Image border = CreateBorder(uiPanel.transform);
        
        // Create Turn Section
        GameObject turnSection = new GameObject("Turn_Section");
        turnSection.transform.SetParent(uiPanel.transform);
        
        RectTransform turnSectionRect = turnSection.AddComponent<RectTransform>();
        turnSectionRect.anchorMin = new Vector2(0, 1);
        turnSectionRect.anchorMax = new Vector2(1, 1);
        turnSectionRect.pivot = new Vector2(0.5f, 1);
        turnSectionRect.anchoredPosition = new Vector2(0, -5);
        turnSectionRect.sizeDelta = new Vector2(-20, 45);
        
        // Add highlight background for turn
        turnHighlight = turnSection.AddComponent<Image>();
        turnHighlight.color = playerTurnColor;
        
        // Create Turn Text
        GameObject turnObj = new GameObject("Turn_Text");
        turnObj.transform.SetParent(turnSection.transform);
        
        RectTransform turnRect = turnObj.AddComponent<RectTransform>();
        turnRect.anchorMin = new Vector2(0, 0);
        turnRect.anchorMax = new Vector2(1, 1);
        turnRect.offsetMin = new Vector2(5, 5);
        turnRect.offsetMax = new Vector2(-5, -5);
        
        turnText = turnObj.AddComponent<TextMeshProUGUI>();
        turnText.alignment = TextAlignmentOptions.Center;
        turnText.fontSize = 22;
        turnText.fontStyle = FontStyles.Bold;
        turnText.color = Color.white;
        turnText.text = "Current Turn: Player";
        
        // Create Priority Section
        GameObject prioritySection = new GameObject("Priority_Section");
        prioritySection.transform.SetParent(uiPanel.transform);
        
        RectTransform prioritySectionRect = prioritySection.AddComponent<RectTransform>();
        prioritySectionRect.anchorMin = new Vector2(0, 1);
        prioritySectionRect.anchorMax = new Vector2(1, 1);
        prioritySectionRect.pivot = new Vector2(0.5f, 1);
        prioritySectionRect.anchoredPosition = new Vector2(0, -55);
        prioritySectionRect.sizeDelta = new Vector2(-20, 45);
        
        // Add highlight background for priority
        priorityHighlight = prioritySection.AddComponent<Image>();
        priorityHighlight.color = playerPriorityColor;
        
        // Create Priority Text
        GameObject priorityObj = new GameObject("Priority_Text");
        priorityObj.transform.SetParent(prioritySection.transform);
        
        RectTransform priorityRect = priorityObj.AddComponent<RectTransform>();
        priorityRect.anchorMin = new Vector2(0, 0);
        priorityRect.anchorMax = new Vector2(1, 1);
        priorityRect.offsetMin = new Vector2(5, 5);
        priorityRect.offsetMax = new Vector2(-5, -5);
        
        priorityText = priorityObj.AddComponent<TextMeshProUGUI>();
        priorityText.alignment = TextAlignmentOptions.Center;
        priorityText.fontSize = 22;
        priorityText.fontStyle = FontStyles.Bold;
        priorityText.color = Color.white;
        priorityText.text = "Priority: Player";
    }
    
    private Image CreateBorder(Transform parent)
    {
        GameObject border = new GameObject("Border");
        border.transform.SetParent(parent);
        
        RectTransform borderRect = border.AddComponent<RectTransform>();
        borderRect.anchorMin = Vector2.zero;
        borderRect.anchorMax = Vector2.one;
        borderRect.offsetMin = new Vector2(-2, -2);
        borderRect.offsetMax = new Vector2(2, 2);
        
        Image borderImage = border.AddComponent<Image>();
        borderImage.color = new Color(0.8f, 0.8f, 0.8f, 0.5f);
        
        return borderImage;
    }
    
    private void PlayTurnTransition(int newTurn)
    {
        // Start transition animation
        StartCoroutine(TransitionTurnColor(newTurn));
    }
    
    private void PlayPriorityTransition(int newPriority)
    {
        // Start transition animation
        StartCoroutine(TransitionPriorityColor(newPriority));
    }
    
    private IEnumerator TransitionTurnColor(int newTurn)
    {
        Color targetColor = (newTurn == 1) ? playerTurnColor : aiTurnColor;
        Color startColor = turnHighlight.color;
        
        // Flash effect
        turnHighlight.color = Color.white;
        yield return new WaitForSeconds(0.1f);
        
        // Fade to target color
        float duration = 0.3f;
        float elapsed = 0;
        
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / duration;
            turnHighlight.color = Color.Lerp(Color.white, targetColor, t);
            yield return null;
        }
        
        turnHighlight.color = targetColor;
    }
    
    private IEnumerator TransitionPriorityColor(int newPriority)
    {
        Color targetColor = (newPriority == 1) ? playerPriorityColor : aiPriorityColor;
        Color startColor = priorityHighlight.color;
        
        // Flash effect
        priorityHighlight.color = Color.white;
        yield return new WaitForSeconds(0.1f);
        
        // Fade to target color
        float duration = 0.3f;
        float elapsed = 0;
        
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / duration;
            priorityHighlight.color = Color.Lerp(Color.white, targetColor, t);
            yield return null;
        }
        
        priorityHighlight.color = targetColor;
    }
} 