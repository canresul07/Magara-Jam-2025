using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class DoorFinish : MonoBehaviour
{
    [Header("Rule")]
    public int requiredKeys = 3;
    public bool requireBody = true;            // sadece beden açsın

    [Header("Transition")]
    public string nextSceneName = "EndScene";  // Build Settings'e ekle
    public SimpleFader fader;
    public float fadeDuration = 1.0f;

    bool inRange;
    Collider2D who;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (requireBody && !other.CompareTag("Body")) return;
        inRange = true; who = other;
        // burada "E - Gir" UI yazısı gösterebilirsin (opsiyonel)
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other == who) { inRange = false; who = null; }
        // UI yazısını gizle (opsiyonel)
    }

    void Update()
    {
        if (!inRange) return;
        if (Input.GetKeyDown(KeyCode.E)) TryFinish();
    }

    void TryFinish()
    {
        if (Inventory.Instance != null && Inventory.Instance.HasAtLeast(requiredKeys))
            StartCoroutine(FinishSequence());
        else
        {
            // kilitli: ses/ekran uyarısı verebilirsin
            // Debug.Log("3 anahtar yok!");
        }
    }

    IEnumerator FinishSequence()
    {
        // Kontrol kilitleme vb. varsa burada yap (opsiyonel)
        if (fader) yield return StartCoroutine(fader.FadeTo(1f, fadeDuration));
        if (!string.IsNullOrEmpty(nextSceneName))
            SceneManager.LoadScene(nextSceneName);
    }
}
