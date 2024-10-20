using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class CanvasGroupChange : MonoBehaviour
{
    [SerializeField]private CanvasGroup _targetCanvasGroup;
    [SerializeField]private CanvasGroup _currentCanvasGroup;
    private Button _button;

    private void Start()
    {
        _button = GetComponent<Button>();
        
        _button.onClick.AddListener(ChangeCanvasGroup);
        
        if (_targetCanvasGroup == null || _currentCanvasGroup == null)
        {
            throw new Exception("missing CanvasGroup");
        }
    }

    private void ChangeCanvasGroup()
    {
        _currentCanvasGroup.alpha = 0;
        _currentCanvasGroup.interactable = false;
        _currentCanvasGroup.blocksRaycasts = false;

        _targetCanvasGroup.alpha = 1f;
        _targetCanvasGroup.interactable = true;
        _targetCanvasGroup.blocksRaycasts = true;
    }
}
