using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
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

    private void SwitchCurrentChapter(ChapterButton currentButton)
    {
        _buttons.ForEach(b => b.ChangeSelectStatus(true));
        currentButton.ChangeSelectStatus(false);
        scrollbar.value = 1f - _buttons.IndexOf(currentButton) / (_buttons.Count-1f);
    }
}
