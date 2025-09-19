using UnityEngine;

public class SpiritModeManagerSimple : MonoBehaviour
{
    [Header("Refs")]
    public GameObject body;               // Player (beden)
    public MonoBehaviour bodyController;  // Player hareket script'in (PlayerSimple)
    public GameObject spirit;             // Spirit objesi (başta kapalı)
    public SpiritControllerSimple spiritCtrl; // Spirit'in script'i
    public Transform cameraAnchor;        // HeroRoot/CameraAnchor
    public KeyCode toggleKey = KeyCode.X;

    [Header("Merge Rules")]
    public bool requireNearToMerge = true;
    public float mergeDistance = 1.2f;

    // Envanter
    public int bodyKeys = 0;    // bedenin anahtar sayısı
    int spiritPocketKeys = 0;   // ruh cebindeki

    bool inSpirit;

    void Start()
    {
        if (spirit) spirit.SetActive(false);
        if (bodyController) bodyController.enabled = true;
        if (spiritCtrl) spiritCtrl.body = body.transform;

        cameraAnchor.SetParent(body.transform);
        cameraAnchor.localPosition = Vector3.zero;
    }

    void Update()
    {
        if (Input.GetKeyDown(toggleKey))
        {
            if (!inSpirit) EnterSpirit();
            else TryReturnToBody();
        }
    }

    void EnterSpirit()
    {
        if (!body || !spirit || !cameraAnchor) return;

        var rb = body.GetComponent<Rigidbody2D>();
        if (rb) rb.linearVelocity = new Vector2(0f, rb.linearVelocity.y);
        if (bodyController) bodyController.enabled = false;

        spirit.transform.position = body.transform.position;
        spirit.SetActive(true);

        cameraAnchor.SetParent(spirit.transform);
        cameraAnchor.localPosition = Vector3.zero;

        inSpirit = true;
    }

    void TryReturnToBody()
    {
        if (requireNearToMerge &&
            Vector2.Distance(spirit.transform.position, body.transform.position) > mergeDistance)
            return;

        spirit.SetActive(false);
        if (bodyController) bodyController.enabled = true;

        cameraAnchor.SetParent(body.transform);
        cameraAnchor.localPosition = Vector3.zero;

        bodyKeys += spiritPocketKeys;
        spiritPocketKeys = 0;

        inSpirit = false;
    }

    public void SpiritPickedKey(int amount = 1)
    {
        spiritPocketKeys += amount;
    }
}
