using UnityEngine;

public class SpiritControllerSimple : MonoBehaviour
{
    [Header("Move")]
    public float moveSpeed = 8f;
    public Transform body;     // beden (Player)
    public float maxRange = 6f;

    [Header("Animation")]
    public Animator anim;            // Inspector'da atayabilirsin
    public SpriteRenderer sr;        // Inspector'da atayabilirsin

    void Awake()
    {
        if (!anim) anim = GetComponent<Animator>();
        if (!sr)   sr   = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        // Giriş
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");
        Vector2 input = new Vector2(x, y);
        Vector2 dir   = input.normalized;

        // Hedef pozisyon (menzil kısıtlı)
        Vector3 next = transform.position + (Vector3)(dir * moveSpeed * Time.deltaTime);
        if (body != null)
        {
            Vector3 center = body.position;
            Vector3 delta  = next - center;
            if (delta.magnitude > maxRange)
                next = center + delta.normalized * maxRange;
        }
        transform.position = next;

        // Yön aynalama (tek animasyonu iki yönde kullanmak için)
        if (sr)
        {
            if (x > 0.01f)      sr.flipX = false;   // sağ
            else if (x < -0.01f) sr.flipX = true;   // sol
        }

        // Animator parametreleri
        if (anim)
        {
            float speed01 = Mathf.Clamp01(input.magnitude); // 0 dur, 1 hareket
            anim.SetFloat("Speed", speed01);                // Idle<->Move koşulu için
            //anim.SetBool("IsMoving", speed01 > 0.01f);      // istersen bool da kullan
            // Eğer solda/sağda ayrı state'lerin varsa:
            // anim.SetFloat("DirX", x);
        }
    }

    void OnDrawGizmosSelected()
    {
        if (!body) return;
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(body.position, maxRange);
    }
}
