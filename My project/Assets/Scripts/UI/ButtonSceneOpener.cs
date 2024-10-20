using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonSceneOpener : MonoBehaviour
{
    [SerializeField] private string _targetSceneName;
    private Button _button;
    void Start()
    {
        _button = GetComponent<Button>();
        _button.onClick.AddListener(OpenTargetScene);
    }
    
    void Update()
    {
        
    }

    private void OpenTargetScene()
    {
        SceneManager.LoadScene(_targetSceneName);
    }
}
