using UnityEngine;
using UnityEngine.UI;
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
        "Bir hacker grubunun þehirdeki güvenlik sistemlerini devre dýþý býraktýðýný öðrendiniz. Onlarý durdurmak için müdahale etmeli misiniz?",
        "Büyük bir þirket size þehirdeki en yüksek binayý kontrol etme teklifinde bulunuyor. Teklifi kabul edecek misiniz?",
        "Þehirdeki eski bir teknoloji uzmaný, size gizli bir buluþ teklif ediyor. Bu buluþu kullanmalý mýsýnýz?",
        "Bir suç lordu, size büyük miktarda para karþýlýðýnda þehirdeki bir bölgeyi kontrol etmeyi teklif ediyor. Kabul edecek misiniz?",
        "Bir grup asi, þehirde bir ayaklanma baþlatmayý planlýyor. Onlara yardým etmeli misiniz?"
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

        if (isScaling && currentRectTransform != null)
        {
            currentRectTransform.localScale = Vector3.Lerp(currentRectTransform.localScale, targetScale, Time.deltaTime * scaleSpeed);

            if (Vector3.Distance(currentRectTransform.localScale, targetScale) < 0.01f)
            {
                currentRectTransform.localScale = targetScale;
                StartCoroutine(ResetScale(currentRectTransform, originalScale, 1f));
                isScaling = false;
                Debug.Log("Scaling complete. Final scale: " + targetScale);
            }
        }
    }

    void DisplayScenario()
    {
        if (cardText != null && scenarioText != null)
        {
            cardText.text = scenarios[currentScenario];
            scenarioText.text = scenarios[currentScenario];
        }
        else
        {
            Debug.LogError("cardText veya scenarioText atanmadý!");
        }

        if (cardImage != null && cardImages != null && cardImages.Length > currentScenario)
        {
            cardImage.sprite = cardImages[currentScenario];
        }
        else
        {
            Debug.LogError("cardImage veya cardImages atanmadý ya da cardImages dizisi boþ veya yetersiz!");
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
        // Geçerli deðeri sakla
        int previousValue = categoryValue;

        // Deðeri güncelle
        categoryValue = Mathf.Clamp(categoryValue + amount, 0, 100);
        categoryText.text = categoryValue.ToString();

        // Yeni ve eski ölçekleri hesapla
        float newScale = categoryValue / 50f; // Ölçek aralýðý 0 ile 2 arasýnda
        float oldScale = previousValue / 50f;

        // Ölçekleme yönünü belirle
        if (currentRectTransform != categoryCircle.rectTransform)
        {
            currentRectTransform = categoryCircle.rectTransform;
            originalScale = currentRectTransform.localScale; // Eski ölçeði sakla
        }

        targetScale = new Vector3(newScale, 1f, 1f);

        // Ölçeklemeyi baþlat
        isScaling = true;
    }

    IEnumerator ResetScale(RectTransform rectTransform, Vector3 originalScale, float duration)
    {
        float elapsed = 0f;
        Vector3 initialScale = rectTransform.localScale;

        while (elapsed < duration)
        {
            rectTransform.localScale = Vector3.Lerp(initialScale, originalScale, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }
        rectTransform.localScale = originalScale;
    }

    void UpdateCircleValues()
    {
        powerText.text = powerValue.ToString();
        techText.text = techValue.ToString();
        moneyText.text = moneyValue.ToString();
        successText.text = successValue.ToString();

        // Ölçekleri doðrudan güncelle
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

            DisplayScenario();
            targetRotation = Quaternion.identity;
            isRotating = true;
        }
    }
}
