using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.Localization.Settings;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Button = UnityEngine.UI.Button;

public class ChapterButtonsNode : MonoBehaviour
{
    [SerializeField] private Transform chaptersContainer;
    [SerializeField] private Transform buttonsContainer;
    [SerializeField] private ChapterButton buttonPrefab;
    [SerializeField] private ScrollView scrollView;
    [SerializeField] private Scrollbar scrollbar;
    [SerializeField] private Button languageButton;
    [SerializeField] private TextMeshProUGUI languageButtonLabel;
    [SerializeField] private Button exitButton;

    private List<ChapterButton> _buttons = new();
    private int _previousChapterNumber = 1;
    private bool _isEnglish;

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

        languageButton.onClick.AddListener(OnChangeLanguage);
        exitButton.onClick.AddListener(OnExit);
    }

    private void OnDestroy() 
    {
        languageButton.onClick.RemoveListener(OnChangeLanguage);
        exitButton.onClick.RemoveListener(OnExit);
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

    private void OnChangeLanguage()
    {
        _isEnglish = !_isEnglish;
        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[_isEnglish ? 1 : 0];
        languageButtonLabel.text = _isEnglish ? "Eng" : "Ru";
    }

    private void OnExit() => Application.Quit();
}
