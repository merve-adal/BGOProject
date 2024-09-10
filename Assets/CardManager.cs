using UnityEngine;
using UnityEngine.UI;

public class ChoiceSystem : MonoBehaviour
{
    public Text scenarioText;
    public Button acceptButton; // Kabul butonu
    public Button rejectButton; // Reddetme butonu
    public Image powerCircle;
    public Image techCircle;
    public Image moneyCircle;
    public Image successCircle;

    public Text powerText;
    public Text techText;
    public Text moneyText;
    public Text successText;

    public Image cardImage; // Kartýn image bileþeni
    public Text cardText;   // Kart üzerindeki metin

    private int powerValue = 50;
    private int techValue = 50;
    private int moneyValue = 50;
    private int successValue = 50;

    private int currentScenario = 0;

    private string[] scenarios = {
        "Bir hacker grubunun þehirdeki güvenlik sistemlerini devre dýþý býraktýðýný öðrendiniz. Onlarý durdurmak için müdahale etmeli misiniz?",
        "Büyük bir þirket size þehirdeki en yüksek binayý kontrol etme teklifinde bulunuyor. Teklifi kabul edecek misiniz?",
        "Þehirdeki eski bir teknoloji uzmaný, size gizli bir buluþ teklif ediyor. Bu buluþu kullanmalý mýsýnýz?",
        "Bir suç lordu, size büyük miktarda para karþýlýðýnda þehirdeki bir bölgeyi kontrol etmeyi teklif ediyor. Kabul edecek misiniz?",
        "Bir grup asi, þehirde bir ayaklanma baþlatmayý planlýyor. Onlara yardým etmeli misiniz?"
    };

    // Kart görselleri için Sprite kaynaklarý
    public Sprite[] cardImages; // Her senaryo için farklý bir kart görseli olabilir

    // Animator bileþenleri için referanslar
    public Animator powerAnimator;
    public Animator techAnimator;
    public Animator moneyAnimator;
    public Animator successAnimator;


    private Vector3 initialPosition;   // Kartýn baþlangýç pozisyonu
    private bool isDragging = false;   // Sürüklenip sürüklenmediðini kontrol eder
    private Vector2 startTouchPosition; // Baþlangýç dokunma pozisyonu
    private Vector2 currentTouchPosition; // Þu anki dokunma pozisyonu
    private float swipeResistanceX = 50f; // Minimum sürükleme mesafesi (direnci)

    public Animator cardAnimator; // Kartýn Animator bileþeni

    void Start()
    {
        UpdateCircleValues();
        DisplayScenario();

        // Kartýn baþlangýç pozisyonunu kaydet
        initialPosition = cardImage.transform.position;

        // Butonlara týklama olaylarý atanýyor
        acceptButton.onClick.AddListener(() => MakeChoice(true));  // Kabul butonu
        rejectButton.onClick.AddListener(() => MakeChoice(false)); // Reddetme butonu
    }

    void Update()
    {
        // Fare veya dokunma hareketini kontrol et
        if (Input.GetMouseButtonDown(0))
        {
            isDragging = true;
            startTouchPosition = Input.mousePosition;
        }

        if (Input.GetMouseButtonUp(0))
        {
            isDragging = false;
            CheckSwipeDirection();
        }

        if (isDragging)
        {
            currentTouchPosition = Input.mousePosition;
            Vector3 offset = currentTouchPosition - startTouchPosition;
            cardImage.transform.position = initialPosition + offset;
        }
    }

    void CheckSwipeDirection()
    {
        float deltaX = currentTouchPosition.x - startTouchPosition.x;

        // Eðer sürükleme hareketi belli bir mesafeden uzunsa, hareket yönünü kontrol et
        if (Mathf.Abs(deltaX) > swipeResistanceX)
        {
            if (deltaX > 0)
            {
                // Sað kaydýrma hareketi
                cardAnimator.SetTrigger("OpenRightTrigger");
                MakeChoice(true);  // Kabul etme seçeneðini tetikle
            }
            else
            {
                // Sol kaydýrma hareketi
                cardAnimator.SetTrigger("OpenLeftTrigger");
                MakeChoice(false); // Reddetme seçeneðini tetikle
            }
        }
        else
        {
            // Eðer kaydýrma hareketi yetersizse kartý geri pozisyonuna çek
            cardImage.transform.position = initialPosition;
        }
    }

    void DisplayScenario()
    {
        // Kart üzerindeki metni ayarla
        if (cardText != null && scenarioText != null)
        {
            cardText.text = scenarios[currentScenario];
            scenarioText.text = scenarios[currentScenario];
        }
        else
        {
            Debug.LogError("cardText veya scenarioText atanmadý!");
        }

        // Kart görselini güncelle
        if (cardImage != null && cardImages != null && cardImages.Length > currentScenario)
        {
            cardImage.sprite = cardImages[currentScenario]; // Sadece kartýn görselini deðiþtir
        }
        else
        {
            Debug.LogError("cardImage veya cardImages atanmadý ya da cardImages dizisi boþ veya yetersiz!");
        }
    }

    void MakeChoice(bool accepted)
    {
        // Senaryoya göre seçim sonuçlarýný iþle
        switch (currentScenario)
        {
            case 0:
                if (accepted)
                {
                    ModifyCategories(ref powerValue, powerCircle, powerText, 10, powerAnimator);
                    ModifyCategories(ref techValue, techCircle, techText, -10, techAnimator);
                }
                else
                {
                    ModifyCategories(ref techValue, techCircle, techText, 10, techAnimator);
                    ModifyCategories(ref powerValue, powerCircle, powerText, -10, powerAnimator);
                }
                break;

            case 1:
                if (accepted)
                {
                    ModifyCategories(ref moneyValue, moneyCircle, moneyText, 10, moneyAnimator);
                    ModifyCategories(ref powerValue, powerCircle, powerText, -10, powerAnimator);
                }
                else
                {
                    ModifyCategories(ref successValue, successCircle, successText, 10, successAnimator);
                    ModifyCategories(ref moneyValue, moneyCircle, moneyText, -10, moneyAnimator);
                }
                break;

            case 2:
                if (accepted)
                {
                    ModifyCategories(ref techValue, techCircle, techText, 10, techAnimator);
                    ModifyCategories(ref successValue, successCircle, successText, -10, successAnimator);
                }
                else
                {
                    ModifyCategories(ref powerValue, powerCircle, powerText, 10, powerAnimator);
                    ModifyCategories(ref techValue, techCircle, techText, -10, techAnimator);
                }
                break;

            case 3:
                if (accepted)
                {
                    ModifyCategories(ref moneyValue, moneyCircle, moneyText, 10, moneyAnimator);
                    ModifyCategories(ref successValue, successCircle, successText, -10, successAnimator);
                }
                else
                {
                    ModifyCategories(ref powerValue, powerCircle, powerText, 10, powerAnimator);
                    ModifyCategories(ref moneyValue, moneyCircle, moneyText, -10, moneyAnimator);
                }
                break;

            case 4:
                if (accepted)
                {
                    ModifyCategories(ref successValue, successCircle, successText, 10, successAnimator);
                    ModifyCategories(ref powerValue, powerCircle, powerText, -10, powerAnimator);
                }
                else
                {
                    ModifyCategories(ref techValue, techCircle, techText, 10, techAnimator);
                    ModifyCategories(ref successValue, successCircle, successText, -10, successAnimator);
                }
                break;
        }

        currentScenario++;
        if (currentScenario < scenarios.Length)
        {
            DisplayScenario();
        }
        else
        {
            scenarioText.text = "Demo Bitti!";
            acceptButton.gameObject.SetActive(false);
            rejectButton.gameObject.SetActive(false);
        }
    }

    void ModifyCategories(ref int categoryValue, Image categoryCircle, Text categoryText, int amount, Animator animator)
    {
        int oldValue = categoryValue; // Önceki deðeri sakla
        categoryValue = Mathf.Clamp(categoryValue + amount, 0, 100);
        categoryCircle.fillAmount = categoryValue / 100f;
        categoryText.text = categoryValue.ToString();

        // Sadece büyüme animasyonunu tetikle
        if (categoryValue > oldValue)
        {
            animator.SetTrigger("Grow");
        }
    }

    void UpdateCircleValues()
    {
        powerText.text = powerValue.ToString();
        techText.text = techValue.ToString();
        moneyText.text = moneyValue.ToString();
        successText.text = successValue.ToString();

        powerCircle.fillAmount = powerValue / 100f;
        techCircle.fillAmount = techValue / 100f;
        moneyCircle.fillAmount = moneyValue / 100f;
        successCircle.fillAmount = successValue / 100f;
    }
}
