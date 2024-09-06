using UnityEngine;
using UnityEngine.UI;

public class ChoiceSystem : MonoBehaviour
{
    public Text scenarioText;
    public Button acceptButton;
    public Button rejectButton;
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

    void Start()
    {
        UpdateCircleValues();
        DisplayScenario();
        acceptButton.onClick.AddListener(() => MakeChoice(true));
        rejectButton.onClick.AddListener(() => MakeChoice(false));
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
