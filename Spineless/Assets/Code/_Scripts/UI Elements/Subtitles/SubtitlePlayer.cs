using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SubtitlePlayer : MonoBehaviour
{
    [SerializeField] private Subtitle[] subtitles;
    [SerializeField] private TextMeshProUGUI subText;

    public void PlaySubtitles()
    {
        StartCoroutine("SubtitlesCoroutine");
    }
    public void StopSubtitles()
    {
        StopAllCoroutines();
        gameObject.SetActive(false);
    }

    private IEnumerator SubtitlesCoroutine()
    {
        for (int i = 0; i < subtitles.Length; i++)
        {
            subText.SetText(subtitles[i].text);
            yield return new WaitForSeconds(subtitles[i].duration);
        }
        gameObject.SetActive(false);
    }
}
