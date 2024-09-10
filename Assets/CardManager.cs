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

    public Image cardImage; // Kart�n image bile�eni
    public Text cardText;   // Kart �zerindeki metin

    private int powerValue = 50;
    private int techValue = 50;
    private int moneyValue = 50;
    private int successValue = 50;

    private int currentScenario = 0;

    private string[] scenarios = {
        "Bir hacker grubunun �ehirdeki g�venlik sistemlerini devre d��� b�rakt���n� ��rendiniz. Onlar� durdurmak i�in m�dahale etmeli misiniz?",
        "B�y�k bir �irket size �ehirdeki en y�ksek binay� kontrol etme teklifinde bulunuyor. Teklifi kabul edecek misiniz?",
        "�ehirdeki eski bir teknoloji uzman�, size gizli bir bulu� teklif ediyor. Bu bulu�u kullanmal� m�s�n�z?",
        "Bir su� lordu, size b�y�k miktarda para kar��l���nda �ehirdeki bir b�lgeyi kontrol etmeyi teklif ediyor. Kabul edecek misiniz?",
        "Bir grup asi, �ehirde bir ayaklanma ba�latmay� planl�yor. Onlara yard�m etmeli misiniz?"
    };

    // Kart g�rselleri i�in Sprite kaynaklar�
    public Sprite[] cardImages; // Her senaryo i�in farkl� bir kart g�rseli olabilir

    // Animator bile�enleri i�in referanslar
    public Animator powerAnimator;
    public Animator techAnimator;
    public Animator moneyAnimator;
    public Animator successAnimator;


    private Vector3 initialPosition;   // Kart�n ba�lang�� pozisyonu
    private bool isDragging = false;   // S�r�klenip s�r�klenmedi�ini kontrol eder
    private Vector2 startTouchPosition; // Ba�lang�� dokunma pozisyonu
    private Vector2 currentTouchPosition; // �u anki dokunma pozisyonu
    private float swipeResistanceX = 50f; // Minimum s�r�kleme mesafesi (direnci)

    public Animator cardAnimator; // Kart�n Animator bile�eni

    void Start()
    {
        UpdateCircleValues();
        DisplayScenario();

        // Kart�n ba�lang�� pozisyonunu kaydet
        initialPosition = cardImage.transform.position;

        // Butonlara t�klama olaylar� atan�yor
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

        // E�er s�r�kleme hareketi belli bir mesafeden uzunsa, hareket y�n�n� kontrol et
        if (Mathf.Abs(deltaX) > swipeResistanceX)
        {
            if (deltaX > 0)
            {
                // Sa� kayd�rma hareketi
                cardAnimator.SetTrigger("OpenRightTrigger");
                MakeChoice(true);  // Kabul etme se�ene�ini tetikle
            }
            else
            {
                // Sol kayd�rma hareketi
                cardAnimator.SetTrigger("OpenLeftTrigger");
                MakeChoice(false); // Reddetme se�ene�ini tetikle
            }
        }
        else
        {
            // E�er kayd�rma hareketi yetersizse kart� geri pozisyonuna �ek
            cardImage.transform.position = initialPosition;
        }
    }

    void DisplayScenario()
    {
        // Kart �zerindeki metni ayarla
        if (cardText != null && scenarioText != null)
        {
            cardText.text = scenarios[currentScenario];
            scenarioText.text = scenarios[currentScenario];
        }
        else
        {
            Debug.LogError("cardText veya scenarioText atanmad�!");
        }

        // Kart g�rselini g�ncelle
        if (cardImage != null && cardImages != null && cardImages.Length > currentScenario)
        {
            cardImage.sprite = cardImages[currentScenario]; // Sadece kart�n g�rselini de�i�tir
        }
        else
        {
            Debug.LogError("cardImage veya cardImages atanmad� ya da cardImages dizisi bo� veya yetersiz!");
        }
    }

    void MakeChoice(bool accepted)
    {
        // Senaryoya g�re se�im sonu�lar�n� i�le
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
        int oldValue = categoryValue; // �nceki de�eri sakla
        categoryValue = Mathf.Clamp(categoryValue + amount, 0, 100);
        categoryCircle.fillAmount = categoryValue / 100f;
        categoryText.text = categoryValue.ToString();

        // Sadece b�y�me animasyonunu tetikle
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
