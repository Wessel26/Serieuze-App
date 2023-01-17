using System.Collections;
using UnityEngine;
using TMPro;

public class Dialogue : MonoBehaviour
{

    [System.Serializable]
    public class DialogueSegment
    {
        public string SubjectText;

        [TextArea]
        public string DialogueToPrint;
        public bool Skippable;

        [Range(1f, 25f)]
        public float LettersPerSecond;
    }
    [SerializeField] private DialogueSegment[] DialogueSegments;
    [Space]
    [SerializeField] private TMP_Text SubjectText;
    [SerializeField] private TMP_Text BodyText;

    private int DialogueIndex;
    private bool PlayingDialogue;
    private bool Skip;
   
    void Start()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (DialogueIndex == DialogueSegment.Lenght)
            {
                enabled = false;
                return;
            }

            if (!PlayingDialogue)
            {
                StartCoroutine(PlayingDialogue(DialogueSegments[DialogueIndex]));
            }
            else
            {
                if (DialogueSegments[DialogueIndex].Skippable)
                {
                    Skip = true;
                }
            }
        }
    }

    private IEnumerator PlayDialogue(DialogueSegment segment)
    {
        PlayingDialogue = true;

        BodyText.SetText(string.Empty);
        SubjectText.SetText(segment.SubjectText);

        float delay = 1f / segment.LettersPerSecond;
        for (int i = 0; i < segment.DialogueToPrint.Length; i++) 
        {
            if (Skip)
            {
                BodyText.SetText(segment.DialogueToPrint);
                Skip = false;
                break;
            }

            string chunkToAdd = string.Empty;
            chunkToAdd += segment.DialogueToPrint[i];
            if (segment.DialogueToPrint[i] == '' && i < segment.DialogueToPrint.Length -1)
            {
                chunkToAdd = segment.DialogueToPrint.Substring(if, 2);
                i++;
            }

            BodyText.text += chunkToAdd;
            yield return new WaitForSeconds(delay);
        }

        PlayingDialogue = false;
        DialogueIndex++;

    }
}
