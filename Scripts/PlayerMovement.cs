using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public static PlayerMovement Instance;

    void Awake()
    {
        Instance = this;
    }

    // Kamera rotasyonu
    public float mouseSensX = 100f; // Saga - Sola
    public float mouseSensY = 100f; // Yukarı - Aşağı
    public float rotationSmoothTime = 1f; // Kamera dönüşü yumuşatma süresi
    private float xRotation = 0f;
    private float yRotation = 0f;
    private float currentXRotation;
    private float currentYRotation;
    private float xRotationVelocity;
    private float yRotationVelocity;

    // Karakter hareketi
    public float moveSpeed = 10f; // Hareket hızı
    public float runSpeedMultiplier = 2f; // Koşma hızı çarpanı
    public float acceleration = 15f; // Hızlanma
    public float deceleration = 15f; // Yavaşlama
    private Vector3 moveDirection; // Hareket yönü

    // Engel kontrolü
    public float distanceObject = 3f;

    // Koşma durumu
    private bool run = false;

    // Başlangıç pozisyonu ve kontrol
    public GameObject spawnPos;
    public bool canMove = true;

    public AudioSource walkSound;

    void Start()
    {
        // Başlangıç pozisyonunu ayarla
        transform.position = spawnPos.transform.position;
        transform.rotation = spawnPos.transform.rotation;

        Cursor.lockState = CursorLockMode.Locked; // İmleci gizle
    }

    void FixedUpdate()
    {
        if (canMove)
        {
            RotateCam(); // Kamera açısını ayarla
            Move(); // W-A-S-D ile hareket et
            HandleRunInput(); // Koşma tuşu kontrolü
        }
    }

    void RotateCam()
    {
        // Fare hareketini al
        float mouseX = Input.GetAxis("Mouse X") * mouseSensX * Time.fixedDeltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensY * Time.fixedDeltaTime;

        // X eksenindeki dönüş (yukarı-aşağı)
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f); // Döndürmeyi sınırlama

        // Y eksenindeki dönüş (sağa-sola)
        yRotation += mouseX;
        

        // Yumuşatma ile dönüş
        currentXRotation = Mathf.SmoothDamp(currentXRotation, xRotation, ref xRotationVelocity, rotationSmoothTime);
        currentYRotation = Mathf.SmoothDamp(currentYRotation, yRotation, ref yRotationVelocity, rotationSmoothTime);

        // Kamera dönüşü
        transform.localRotation = Quaternion.Euler(currentXRotation, currentYRotation, 0f);
    }

    void Move()
    {
        // Hareket için yön vektörlerini ayarla
        Vector3 forward = new Vector3(transform.forward.x, 0, transform.forward.z).normalized;
        Vector3 right = new Vector3(transform.right.x, 0, transform.right.z).normalized;

        Vector3 targetDirection = Vector3.zero;

        // Kullanıcı girişine göre hareket yönünü belirle
        if (Input.GetKey("w")) targetDirection += forward;
        if (Input.GetKey("s")) targetDirection -= forward;
        if (Input.GetKey("a")) targetDirection -= right;
        if (Input.GetKey("d")) targetDirection += right;

        targetDirection = targetDirection.normalized * (run ? moveSpeed * runSpeedMultiplier : moveSpeed); // Koşma durumuna göre hız

        // Hareket yönünü yumuşak bir şekilde uygula
        if (targetDirection != Vector3.zero && !Physics.Raycast(transform.position, targetDirection.normalized, distanceObject, 1 << 6))
        {
            StartPlay();
            moveDirection = Vector3.Lerp(moveDirection, targetDirection, acceleration * Time.fixedDeltaTime);
        }
        else if(targetDirection != Vector3.zero && Physics.Raycast(transform.position, targetDirection.normalized, distanceObject, 1 << 6))
        {
            moveDirection = Vector3.zero;
        }
        else if(targetDirection == Vector3.zero && !Physics.Raycast(transform.position, targetDirection.normalized, distanceObject, 1 << 6))
        {
            // Hareket durduğunda yavaşlama uygula
            EndPlay();
            moveDirection = Vector3.Lerp(moveDirection, Vector3.zero, deceleration * Time.fixedDeltaTime);
        }

        // Hareketi uygula
        transform.position += moveDirection * Time.fixedDeltaTime;
    }


    //Ses
    void StartPlay()
    {
        if (!walkSound.isPlaying)
        {
            walkSound.Play();
        }
    }
    void EndPlay()
    {
        walkSound.Stop();
    }


    // Koşma tuşu kontrolü
    void HandleRunInput()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift)) run = true; // LeftShift'e basıldığında koşmaya başla
        if (Input.GetKeyUp(KeyCode.LeftShift)) run = false; // LeftShift bırakıldığında koşmayı durdur
    }
}
