using UnityEngine;

public class PanelManager : MonoBehaviour
{
    public GameObject[] panels;  // Panels to show/hide
    public GameObject[] buttonObjects; // 3D objects as buttons

    private int activePanelIndex = -1;

    void Start()
    {
        if (buttonObjects.Length != panels.Length)
        {
            Debug.LogError("Button objects and Panels arrays must have the same length!");
            return;
        }

        // Assign listeners to each 3D button
        for (int i = 0; i < buttonObjects.Length; i++)
        {
            int index = i;
            var buttonCollider = buttonObjects[i].GetComponent<Collider>();
            if (buttonCollider != null)
            {
                buttonCollider.gameObject.AddComponent<TouchableButton>().Initialize(() => TogglePanel(index));
            }
        }
    }

    void TogglePanel(int index)
    {
        if (activePanelIndex == index)
        {
            panels[index].SetActive(false);
            activePanelIndex = -1;
        }
        else
        {
            CloseAllPanels();
            panels[index].SetActive(true);
            activePanelIndex = index;
        }
    }

    void CloseAllPanels()
    {
        foreach (GameObject panel in panels)
        {
            panel.SetActive(false);
        }
        activePanelIndex = -1;
    }

    public void CloseCurrentPanel()
    {
        if (activePanelIndex != -1)
        {
            panels[activePanelIndex].SetActive(false);
            activePanelIndex = -1;
        }
    }
}
