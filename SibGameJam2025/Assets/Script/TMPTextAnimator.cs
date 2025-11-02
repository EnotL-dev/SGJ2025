using System.Collections;
using TMPro;
using UnityEngine;

public class TMPTextAnimator : MonoBehaviour
{
    [SerializeField] private int timer = 1; //seconds
    private TextMeshProUGUI textMesh;


    private string myText = "";
    private string animText = "";
    private void Start()
    {
        if (!GetComponent<TextMeshProUGUI>())
            return;

        textMesh = GetComponent<TextMeshProUGUI>();

        if (textMesh.text != "")
        {
            myText = textMesh.text;
            textMesh.text = "";
            StartCoroutine(FillText());
        }
    }

    IEnumerator FillText()
    {
        int let = 0;
        while(myText != animText)
        {
            animText += myText[let];
            let++;

            textMesh.text = animText;
            yield return new WaitForSeconds(timer/myText.Length);
        }

        yield return null;
    }
}
