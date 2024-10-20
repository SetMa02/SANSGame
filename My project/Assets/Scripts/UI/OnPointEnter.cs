using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;

[RequireComponent(typeof(EventTrigger))]
public class OnPointEnter : MonoBehaviour
{
    [SerializeField] private Color _hoverColor;
    [SerializeField] private float _scale;
    [SerializeField] private float _transitionTime;
    private TMP_Text _text;
    
    private Color _originalColor;

    private void Start()
    {
        _text = GetComponentInChildren<TMP_Text>();
        _originalColor = _text.color;
    }

    public void OnPointerEnter()
    {
        _text.DOColor(_hoverColor,_transitionTime).SetEase(Ease.OutBounce);
        _text.transform.DOScale(_scale,_transitionTime).SetEase(Ease.OutBounce);
    }

    public void OnPointerExit()
    {
        _text.DOKill();
        _text.DOColor(_originalColor,_transitionTime).SetEase(Ease.OutBounce);
        _text.transform.DOScale(1,_transitionTime).SetEase(Ease.OutBounce);
    }
}
