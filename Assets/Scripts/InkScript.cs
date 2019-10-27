using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ink.Runtime;
using TMPro;

public class InkScript : MonoBehaviour
{
    public TextAsset[] inkJSON;
    private Story story;

    public Canvas dialogueBox;

    public TextMeshProUGUI dialogueText;
    public TextMeshProUGUI playerChoiceText;

    // Start is called before the first frame update
    void Start()
    {
        story = new Story(inkJSON[0].text);

        dialogueText.text = LoadStoryChunk();

        foreach(Choice c in story.currentChoices)
            playerChoiceText.text = c.text;
    }

    private string LoadStoryChunk()
    {
        if(story.canContinue)
            return story.ContinueMaximally();
        else
            return string.Empty;
    }

    public void Next()
    {
        if(story.currentChoices.Count == 0)
        {
            dialogueBox.enabled = false;
            GameManager.Instance.StartGame();
            return;
        }

        story.ChooseChoiceIndex(0);

        dialogueText.text = LoadStoryChunk();
        
        foreach(Choice c in story.currentChoices)
            playerChoiceText.text = c.text;
    }

    public void NewDialogue()
    {
        dialogueBox.enabled = true;
        story = new Story(inkJSON[1].text);
        dialogueText.text = LoadStoryChunk();

        foreach(Choice c in story.currentChoices)
            playerChoiceText.text = c.text;
    }
}
