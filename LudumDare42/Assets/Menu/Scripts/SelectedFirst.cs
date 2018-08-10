using UnityEngine;
using UnityEngine.EventSystems;

public class SelectedFirst : MonoBehaviour
{
    EventSystem eventSystem;
    bool isSelected;

    void Start()
    {
        eventSystem = FindObjectOfType<EventSystem>();
    }

    void Update()
    {
        if (!isSelected)
        {
            eventSystem.SetSelectedGameObject(gameObject);
            isSelected = true;
        }
    }

    private void OnDisable()
    {
        isSelected = false;
    }
}

