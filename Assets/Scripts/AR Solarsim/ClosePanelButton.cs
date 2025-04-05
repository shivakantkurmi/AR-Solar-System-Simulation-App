using UnityEngine;
using UnityEngine.UI;

public class ClosePanelButton : MonoBehaviour
{
    private PanelManager panelManager; // Reference to PanelManager

    void Start()
    {
        // Try to find PanelManager in the scene
        panelManager = FindFirstObjectByType<PanelManager>();

        if (panelManager == null)
        {
            Debug.LogError("PanelManager not found in the scene!");
            return;
        }

        // Add the CloseCurrentPanel function to this button's click event
        GetComponent<Button>().onClick.AddListener(() => panelManager.CloseCurrentPanel());
    }
}
