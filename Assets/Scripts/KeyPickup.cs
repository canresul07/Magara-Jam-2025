using UnityEngine;

public class KeyPickup : MonoBehaviour
{
    [Tooltip("Kapıyla eşleşecek anahtar kimliği")]
    public string keyId = "K1";

    [Tooltip("Sadece hayalet alabilsin mi?")]
    public bool onlyGhostCanPick = true;

    public AudioClip pickupSfx;

    void OnTriggerEnter2D(Collider2D other)
    {
        // Sadece hayalet alsın istiyorsan
        if (onlyGhostCanPick && !other.CompareTag("Ghost")) return;

        // Ortak envantere ekle
        Inventory.Instance.AddKey(keyId);

        // İstersen önce bir toplama animasyonu oynatıp sonra yok et
        Destroy(gameObject);
    }
}
