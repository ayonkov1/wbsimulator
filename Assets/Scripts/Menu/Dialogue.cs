using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Dialogue : MonoBehaviour
{
    public TextMeshProUGUI textComponent;
    public string[] lines;
    public float textSpeed;
    public GameObject[] videos;
    public GameObject VideoScreen;

    bool DialogueReady = false;

    public GameObject[] selectorArr;
    public GameObject selector; //selected in the editor

    private int index;
    private Camera mainCamera;

    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;
        textComponent.text = string.Empty;
        VideoScreen.SetActive(false);
        SetDialogueFalse();
        StartDialogue();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (textComponent.text == lines[index])
            {  
                NextLine();
                NextVideo(index);

                if (DialogueReady)
                {
                    EndDialogue();
                }
                
            }
            else
            {
                StopAllCoroutines();
                textComponent.text = lines[index];
            }
        }
    }

    void StartDialogue()
    {
        index = 0;
        textComponent.text = lines[index];
        mainCamera.GetComponent<CameraFollow>().enabled = false;
    }

    void EndDialogue()
    {
        SetDialogueTrue();
        mainCamera.GetComponent<CameraFollow>().enabled = true;
    }

    IEnumerator TypeLine()
    {
        foreach (char c in lines[index].ToCharArray())
        {
            textComponent.text += c;
            yield return new WaitForSeconds(textSpeed);

        }
    }

    void NextLine()
    {
        if (index < lines.Length - 1)
        {
            index++;
            textComponent.text = string.Empty;
            StartCoroutine(TypeLine());
        }
        else
        {
            DialogueReady = true;
            gameObject.SetActive(false);
        }
    }

    void NextVideo(int index)
    {
        if (videos[index] == null)
        {
            VideoScreen.SetActive(false);
        }
        else
        {
            VideoScreen.SetActive(true);
            videos[index].SetActive(true);
        }
    }

    void SetDialogueTrue()
    {
        foreach (GameObject gameObject in selectorArr)
        {
            gameObject.SetActive(true);
        }
    }

    void SetDialogueFalse()
    {
        foreach (GameObject gameObject in selectorArr)
        {
            gameObject.SetActive(false);
        }
    }

   
}
