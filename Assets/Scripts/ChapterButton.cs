using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Unity.VisualScripting;
using UnityEditor.Localization.Editor;
using UnityEngine;
using UnityEngine.UI;

public class ChapterButton : MonoBehaviour
{
    [SerializeField] private Button button;
    [SerializeField] private Image canSelectIcon;
    [SerializeField] private LocalizationAsset localizationAsset;

    public Button Button => button;

    private Transform showTarget;
    public event Action<ChapterButton> OnClick;

    void OnDestroy() 
    {
        button.onClick.RemoveListener(ShowChapter);
    }

    public void Init(Transform chapter)
    {
        button.name = $"Button {chapter.name}";
        showTarget = chapter;
        button.onClick.AddListener(ShowChapter);
        localizationAsset.SetLocalizedString("", "");
    }

    public void ChangeSelectStatus(bool state) => canSelectIcon.enabled = state;

    private void ShowChapter()
    {
        OnClick.Invoke(this);
    }
}
