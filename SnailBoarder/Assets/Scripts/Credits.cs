using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Credits : MonoBehaviour
{
    public float textTime = 5f;

    public TextMeshProUGUI creditsText, topText, thanksText;
    public Image pictureImage, hifiImage;

    [TextArea]
    public string[] credits, thanks;
    public Sprite[] pictures;

    private Animator textAnimator, pictureAnimator, thanksAnimator;
    
    private void Start()
    {
        textAnimator = creditsText.GetComponent<Animator>();
        pictureAnimator = pictureImage.GetComponent<Animator>();
        thanksAnimator = thanksText.GetComponent<Animator>();
        StartCoroutine(FadeTopText());
        StartCoroutine(StartCredits());
    }

    IEnumerator FadeTopText()
    {
        topText.GetComponent<Animator>().Play("fadein");
        yield return new WaitForSeconds(textTime);
        topText.GetComponent<Animator>().Play("fadeout");
    }

    IEnumerator FadeText(int index) 
    {
        creditsText.text = credits[index];
        pictureImage.sprite = pictures[index];
        textAnimator.Play("fadein");
        pictureAnimator.Play("fadepicturein");
        yield return new WaitForSeconds(textTime);
        textAnimator.Play("fadeout");
        pictureAnimator.Play("fadepictureout");
        yield return new WaitForSeconds(1f);
    }

    IEnumerator SpecialThanks(int index)
    {
        thanksText.text = thanks[index];
        thanksAnimator.enabled = true;
        thanksAnimator.Play("fadein");
        hifiImage.GetComponent<Animator>().enabled = true;
        hifiImage.GetComponent<Animator>().Play("fadepicturein");
        yield return new WaitForSeconds(textTime);
        thanksAnimator.Play("fadeout");
        hifiImage.GetComponent<Animator>().Play("fadepictureout");
        yield return new WaitForSeconds(1f);
    }

    IEnumerator StartCredits()
    {
        for (int i = 0; i < credits.Length; i++)
        {
            yield return FadeText(i);
        }
        for (int i = 0; i < thanks.Length; i++)
        {
            yield return SpecialThanks(i);
        }
        yield return new WaitForSeconds(2f);

        SceneLoader.instance.LoadScene("title");
    }
}
