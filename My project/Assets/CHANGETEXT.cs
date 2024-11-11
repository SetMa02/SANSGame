using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CHANGETEXT : MonoBehaviour
{
   [SerializeField] private string _text;
   [SerializeField] private Button _button;
   [SerializeField]private Text _textComponent;

   private void Start()
   {
      _button = _button.GetComponent<Button>();
      
      _button.onClick.AddListener(ChangeText);
   }

   private void ChangeText()
   {
      _textComponent.text = _text;
   }
}
