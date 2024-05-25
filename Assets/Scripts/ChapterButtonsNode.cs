using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UIElements;

public class ChapterButtonsNode : MonoBehaviour
{
    [SerializeField] private Transform chaptersContainer;
    [SerializeField] private Transform buttonsContainer;
    [SerializeField] private ChapterButton buttonPrefab;
    [SerializeField] private ScrollView scrollView;

    private List<ChapterButton> _buttons;

    private void Start() 
    {
        chaptersContainer.GetChildren().ForEach(chapter => 
        {
            var chapterButton = Instantiate(buttonPrefab, buttonsContainer);
            chapterButton.Init(chapter);
            chapterButton.OnClick += SwitchCurrentChapter;
        });
    }

    private void SwitchCurrentChapter(ChapterButton currentButton)
    {
        _buttons.ForEach(b => b.ChangeSelectStatus(true));
        currentButton.ChangeSelectStatus(false);
        //scrollView
    }
}
