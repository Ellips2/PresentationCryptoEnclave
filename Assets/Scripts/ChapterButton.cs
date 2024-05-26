using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Unity.VisualScripting;
using UnityEditor.Localization.Editor;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Components;
using UnityEngine.UI;

public class ChapterButton : MonoBehaviour
{
    [SerializeField] private Button button;
    [SerializeField] private Image canSelectIcon;
    [SerializeField] private LocalizeStringEvent localizedStringEvent;


    public Button Button => button;
    public event Action<ChapterButton> OnClick;


    void OnDestroy() 
    {
        button.onClick.RemoveListener(ShowChapter);
    }

    public void Init(Transform chapter)
    {
        button.name = $"Button {chapter.name}";
        button.onClick.AddListener(ShowChapter);

        localizedStringEvent.StringReference.SetReference("UI", chapter.name.ToLower());
        localizedStringEvent.RefreshString();
    }

    public void ChangeSelectStatus(bool state) => canSelectIcon.enabled = state;

    private void ShowChapter()
    {
        OnClick.Invoke(this);
    }
}
