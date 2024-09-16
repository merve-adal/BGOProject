using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement; // Sahne y�netimi i�in gerekli
using System.Collections;

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

    public Image cardImage;
    public Text cardText;

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
    private string[] powerScenarios = {
    "Bir rakip �etenin lideri, size kar�� bir meydan okumada bulunuyor. G�c�n�z� g�sterip meydan okumay� kabul edecek misiniz?",
    "�ehirdeki g�venlik birimleri, g�c�n�z�n artt���n� fark edip sizi g�zlemlemeye ba�lad�. Onlarla i�birli�i yapmal� m�s�n�z?",
    "Bir isyan ba�l�yor ve lider sizden destek istiyor. �syan� bast�rmak i�in yard�m etmeli misiniz?",
    "�ehrin en g��l� grubuyla ittifak kurma f�rsat�n�z var. Bu f�rsat� de�erlendirmeli misiniz?",
    "Halk, g�c�n�zden dolay� size kar�� �ekimser davranmaya ba�lad�. �maj�n�z� d�zeltmek i�in bir yard�m kampanyas� ba�latmal� m�s�n�z?"
    };
    private string[] techScenarios = {
    "Bir teknoloji firmas�, �ehri daha verimli y�netmenizi sa�layacak yeni bir yaz�l�m sunuyor. Kabul edecek misiniz?",
    "Yapay zekalar, kontrol�n�zden ��kmaya ba�lad�. Onlar� durdurmak i�in bir ��z�m aramal� m�s�n�z?",
    "�ehirdeki enerji kaynaklar� t�kenmek �zere. Yeni bir yenilenebilir enerji kayna�� geli�tirilmeli mi?",
    "Teknolojik cihazlar halk aras�nda h�zla yay�l�yor, ancak bu durumun baz� tehlikeleri var. Yeni g�venlik �nlemleri almal� m�s�n�z?",
    "�ehrin altyap�s�n� geli�tirmek i�in yeni bir inovasyon merkezi kurma f�rsat�n�z var. Bunu desteklemeli misiniz?"
    };

    private string[] moneyScenarios = {
    "Bir yat�r�mc� size �ehirde b�y�k bir ticaret merkezi kurma teklifiyle geldi. Kabul edecek misiniz?",
    "Bir su� lordu, size b�y�k miktarda para teklif ederek baz� b�lgelerde kontrol sa�lamak istiyor. Teklifi kabul edecek misiniz?",
    "Bir i� adam� �ehirde b�y�k bir ekonomik reform ba�latmak istiyor. Ona yard�m etmeli misiniz?",
    "Ekonomik krizle kar�� kar��yas�n�z. �ehirdeki varl�klar� sat�p ekonomiyi toparlamay� denemeli misiniz?",
    "Bir grup giri�imci, �ehirde b�y�k bir start-up ekosistemi kurmak istiyor. Onlara destek vermeli misiniz?"
    };
    private string[] successScenarios = {
    "Ba�ar�lar�n�z nedeniyle size bir �d�l teklif ediliyor. Kabul edip t�rende boy g�stermeli misiniz?",
    "Bir �nl�, ba�ar�lar�n�z� kutlamak i�in sizi bir etkinli�e davet ediyor. Bu etkinli�e kat�lmal� m�s�n�z?",
    "Ba�ar�n�z halk aras�nda b�y�k yank� buluyor, ancak bu durum baz� d��manlar kazanman�za sebep oldu. Onlarla ba�a ��kmak i�in bir strateji geli�tirmeli misiniz?",
    "Ba�ar�n�z� daha fazla b�y�tmek i�in �ehirdeki en g��l� ki�ilerle i�birli�i yapma f�rsat�n�z var. Bu i�birli�ini yapmal� m�s�n�z?",
    "Ba�ar�lar�n�z nedeniyle �ehrin en zengin adam� sizi ki�isel dan��man� yapmay� teklif ediyor. Bu teklifi kabul etmeli misiniz?"
    };

    public Sprite[] cardImages;

    private Quaternion targetRotation;
    private float rotationSpeed = 5f;
    private bool isRotating = false;

    private float scaleSpeed = 5f;
    private bool isScaling = false;
    private RectTransform currentRectTransform;
    private Vector3 targetScale;
    private Vector3 originalScale;

    void Start()
    {
        UpdateCircleValues();
        DisplayScenario();

        targetRotation = cardImage.transform.rotation;

        acceptButton.onClick.AddListener(() => MakeChoice(true));
        rejectButton.onClick.AddListener(() => MakeChoice(false));
    }

    void Update()
    {
        if (isRotating)
        {
            cardImage.transform.rotation = Quaternion.Slerp(cardImage.transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);

            if (Quaternion.Angle(cardImage.transform.rotation, targetRotation) < 0.1f)
            {
                cardImage.transform.rotation = targetRotation;
                isRotating = false;

                if (Mathf.Abs(targetRotation.eulerAngles.y) == 90f || Mathf.Abs(targetRotation.eulerAngles.y) == 270f)
                {
                    StartCoroutine(ShowNextCard());
                }
            }
        }
    }

    void DisplayScenario()
    {
        if (currentScenario < scenarios.Length)
        {
            if (cardText != null && scenarioText != null)
            {
                cardText.text = scenarios[currentScenario];
                scenarioText.text = scenarios[currentScenario];
            }
            else
            {
                Debug.LogError("cardText veya scenarioText atanmad�!");
            }

            if (cardImage != null && cardImages != null && cardImages.Length > currentScenario)
            {
                cardImage.sprite = cardImages[currentScenario];
            }
            else
            {
                Debug.LogError("cardImage veya cardImages atanmad� ya da cardImages dizisi bo� veya yetersiz!");
            }
        }
        else
        {
            Debug.LogError("currentScenario, senaryo uzunlu�unu a��yor!");
        }
    }


    void MakeChoice(bool accepted)
    {
        if (isRotating) return;

        if (accepted)
        {
            targetRotation = Quaternion.Euler(0, 90, 0);
        }
        else
        {
            targetRotation = Quaternion.Euler(0, -90, 0);
        }

        isRotating = true;

        switch (currentScenario)
        {
            case 0:
                if (accepted)
                {
                    ModifyCategories(ref powerValue, powerCircle, powerText, 10);
                    ModifyCategories(ref techValue, techCircle, techText, -10);
                }
                else
                {
                    ModifyCategories(ref techValue, techCircle, techText, 10);
                    ModifyCategories(ref powerValue, powerCircle, powerText, -10);
                }
                break;

            case 1:
                if (accepted)
                {
                    ModifyCategories(ref moneyValue, moneyCircle, moneyText, 10);
                    ModifyCategories(ref powerValue, powerCircle, powerText, -10);
                }
                else
                {
                    ModifyCategories(ref successValue, successCircle, successText, 10);
                    ModifyCategories(ref moneyValue, moneyCircle, moneyText, -10);
                }
                break;

            case 2:
                if (accepted)
                {
                    ModifyCategories(ref techValue, techCircle, techText, 10);
                    ModifyCategories(ref successValue, successCircle, successText, -10);
                }
                else
                {
                    ModifyCategories(ref powerValue, powerCircle, powerText, 10);
                    ModifyCategories(ref techValue, techCircle, techText, -10);
                }
                break;

            case 3:
                if (accepted)
                {
                    ModifyCategories(ref moneyValue, moneyCircle, moneyText, 10);
                    ModifyCategories(ref successValue, successCircle, successText, -10);
                }
                else
                {
                    ModifyCategories(ref powerValue, powerCircle, powerText, 10);
                    ModifyCategories(ref moneyValue, moneyCircle, moneyText, -10);
                }
                break;

            case 4:
                if (accepted)
                {
                    ModifyCategories(ref successValue, successCircle, successText, 10);
                    ModifyCategories(ref powerValue, powerCircle, powerText, -10);
                }
                else
                {
                    ModifyCategories(ref techValue, techCircle, techText, 10);
                    ModifyCategories(ref successValue, successCircle, successText, -10);
                }
                break;
        }
    }

    void ModifyCategories(ref int categoryValue, Image categoryCircle, Text categoryText, int amount)
    {
        int previousValue = categoryValue;
        categoryValue = Mathf.Clamp(categoryValue + amount, 0, 100);
        categoryText.text = categoryValue.ToString();

        float newScale = categoryValue / 50f; // �l�ek aral��� 0 ile 2 aras�nda
        float oldScale = previousValue / 50f;

        if (currentRectTransform != categoryCircle.rectTransform)
        {
            currentRectTransform = categoryCircle.rectTransform;
            originalScale = currentRectTransform.localScale;
        }

        targetScale = new Vector3(newScale, 1f, 1f);
        StartCoroutine(ScaleAndReset(categoryCircle.rectTransform, targetScale, originalScale, 0.5f)); // 0.5 saniyede geri d�n
    }

    IEnumerator ScaleAndReset(RectTransform rectTransform, Vector3 targetScale, Vector3 originalScale, float duration)
    {
        float elapsed = 0f;
        Vector3 initialScale = rectTransform.localScale;

        while (elapsed < duration)
        {
            rectTransform.localScale = Vector3.Lerp(initialScale, targetScale, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        rectTransform.localScale = targetScale;

        elapsed = 0f; // S�f�rlayarak geri d�n�� i�in kullan
        while (elapsed < duration)
        {
            rectTransform.localScale = Vector3.Lerp(targetScale, originalScale, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        rectTransform.localScale = originalScale; // �l�e�i orijinal haline d�nd�r
    }

    void UpdateCircleValues()
    {
        powerText.text = powerValue.ToString();
        techText.text = techValue.ToString();
        moneyText.text = moneyValue.ToString();
        successText.text = successValue.ToString();

        powerCircle.rectTransform.localScale = new Vector3(powerValue / 50f, 1f, 1f);
        techCircle.rectTransform.localScale = new Vector3(techValue / 50f, 1f, 1f);
        moneyCircle.rectTransform.localScale = new Vector3(moneyValue / 50f, 1f, 1f);
        successCircle.rectTransform.localScale = new Vector3(successValue / 50f, 1f, 1f);
    }

    IEnumerator ShowNextCard()
    {
        currentScenario++;

        if (currentScenario < scenarios.Length)
        {
            yield return new WaitForSeconds(1f);

            DisplayScenario(); // Yeni senaryoyu g�stermek i�in ekrana yazd�rma
            targetRotation = Quaternion.identity;
            isRotating = true;
        }
        else
        {
            int highestValue = Mathf.Max(powerValue, techValue, moneyValue, successValue);

            if (highestValue == powerValue)
            {
                currentScenario = 0;
                scenarios = powerScenarios; // G�� senaryolar�na ge�i�
                DisplayScenario(); // Yeni senaryoyu hemen ekrana yazd�r�n
                SceneManager.LoadScene(6); // Power ekran�
            }
            else if (highestValue == techValue)
            {
                currentScenario = 0;
                scenarios = techScenarios; // Teknoloji senaryolar�na ge�i�
                DisplayScenario(); // Yeni senaryoyu hemen ekrana yazd�r�n
                SceneManager.LoadScene(7); // Tech ekran�
            }
            else if (highestValue == moneyValue)
            {
                currentScenario = 0;
                scenarios = moneyScenarios; // Para senaryolar�na ge�i�
                DisplayScenario(); // Yeni senaryoyu hemen ekrana yazd�r�n
                SceneManager.LoadScene(8); // Money ekran�
            }
            else if (highestValue == successValue)
            {
                currentScenario = 0;
                scenarios = successScenarios; // Ba�ar� senaryolar�na ge�i�
                DisplayScenario(); // Yeni senaryoyu hemen ekrana yazd�r�n
                SceneManager.LoadScene(9); // Success ekran�
            }
        }
    }
}