using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CreditsHelper : MonoBehaviour
{
    [SerializeField] private Button _defaultOption;
    [SerializeField] private GameObject _defaultOptionPanel;
    
    private void OnEnable()
    {
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(_defaultOption.gameObject);

        _defaultOptionPanel.SetActive(true);
    }

}
