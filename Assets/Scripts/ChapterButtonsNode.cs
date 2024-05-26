using System.Collections;
using System.Collections.Generic;
using System.Text;
using DG.Tweening;
using UnityEditor.Localization.Plugins.XLIFF.V20;
using UnityEngine;
using UnityEngine.Localization.Settings;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class ChapterButtonsNode : MonoBehaviour
{
    [SerializeField] private Transform chaptersContainer;
    [SerializeField] private Transform buttonsContainer;
    [SerializeField] private ChapterButton buttonPrefab;
    [SerializeField] private ScrollView scrollView;
    [SerializeField] private Scrollbar scrollbar;

    private List<ChapterButton> _buttons = new();
    private int _previousChapterNumber = 1;
    private const float DurationAutoScroll = 0.25f;

    private void Start() 
    {
        chaptersContainer.GetChildren().ForEach(chapter => 
        {
            var chapterButton = Instantiate(buttonPrefab, buttonsContainer);
            chapterButton.Init(chapter);
            chapterButton.OnClick += SwitchCurrentChapter;
            _buttons.Add(chapterButton);
        });
        SwitchCurrentChapter(_buttons[0]);
    }

    private void SwitchCurrentChapter(ChapterButton selectedButton)
    {
        _buttons.ForEach(b => b.ChangeSelectStatus(true));
        selectedButton.ChangeSelectStatus(false);
 
        var targetChapterId = _buttons.IndexOf(selectedButton);
        var targetChapterNumber = targetChapterId + 1;

        var startValue = scrollbar.value;
        var targetValue = 1f - targetChapterId / (_buttons.Count - 1f);
        var delta = Mathf.Abs(targetChapterNumber - _previousChapterNumber);
        DOVirtual.Float(startValue, targetValue, DurationAutoScroll * delta, (value) => scrollbar.value = value);

        _previousChapterNumber = targetChapterNumber;
    }

    private void ChangeLanguage(int i) => LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[i];
}
