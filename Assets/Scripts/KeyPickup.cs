using UnityEngine;

public class KeyPickup : MonoBehaviour
{
    [Tooltip("Kapıyla eşleşecek anahtar kimliği")]
    public string keyId = "DoorA";

    [Tooltip("Sadece hayalet alabilsin mi?")]
    public bool onlyGhostCanPick = true;

    public AudioClip pickupSfx;

    void OnTriggerEnter2D(Collider2D other)
    {
        // Sadece hayalet alsın istiyorsan
        if (onlyGhostCanPick && !other.CompareTag("Ghost")) return;

        // Ortak envantere ekle
        Inventory.Instance.AddKey(keyId);

        // SFX (opsiyonel)
        if (pickupSfx) AudioSource.PlayClipAtPoint(pickupSfx, transform.position);

        // İstersen önce bir toplama animasyonu oynatıp sonra yok et
        Destroy(gameObject);
    }
}
