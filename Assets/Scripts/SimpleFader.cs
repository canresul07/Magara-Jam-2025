using UnityEngine;
using System.Collections;

public class SimpleFader : MonoBehaviour
{
    public CanvasGroup cg;

    void Awake() { if (!cg) cg = GetComponent<CanvasGroup>(); }

    public IEnumerator FadeTo(float target, float dur)
    {
        if (!cg) yield break;
        float start = cg.alpha, t = 0f;
        while (t < dur)
        {
            t += Time.unscaledDeltaTime;
            cg.alpha = Mathf.Lerp(start, target, t / dur);
            yield return null;
        }
        cg.alpha = target;
    }
}
