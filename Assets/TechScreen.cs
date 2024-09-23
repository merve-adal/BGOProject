using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TechScreen : MonoBehaviour
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

    public Sprite[] techCardImages;
    public Image cardImage;
    public Text cardText;

    private int powerValue = 50;
    private int techValue = 50;
    private int moneyValue = 50;
    private int successValue = 50;

    private int currentScenario = 0;
    private string[] techScenarios = {
        "Bir teknoloji firmasý, þehri daha verimli yönetmenizi saðlayacak yeni bir yazýlým sunuyor. Kabul edecek misiniz?",
        "Yapay zekalar, kontrolünüzden çýkmaya baþladý. Onlarý durdurmak için bir çözüm aramalý mýsýnýz?",
        "Þehirdeki enerji kaynaklarý tükenmek üzere. Yeni bir yenilenebilir enerji kaynaðý geliþtirilmeli mi?",
        "Bir teknoloji mühendisi, sizin için özel bir güvenlik sistemi tasarlamayý öneriyor. Bu teklifi kabul etmeli misiniz?",
        "Teknoloji alanýnda devrim yaratacak bir proje için büyük bir yatýrým yapma fýrsatýnýz var. Bu fýrsatý deðerlendirmeli misiniz?"
    };

    void Start()
    {
        DisplayScenario();
        acceptButton.onClick.AddListener(() => MakeChoice(true));
        rejectButton.onClick.AddListener(() => MakeChoice(false));
    }

    void DisplayScenario()
    {
        if (currentScenario < techScenarios.Length)
        {
            scenarioText.text = techScenarios[currentScenario];
            cardText.text = techScenarios[currentScenario];
            cardImage.sprite = techCardImages[currentScenario];
        }
        else
        {
            Debug.LogError("currentScenario, senaryo uzunluðunu aþýyor!");
        }
    }

    void MakeChoice(bool accepted)
    {
        // Örneðin, seçime baðlý olarak teknoloji deðerini güncelle
        // Burada `ModifyCategories` metodunu da dahil edebilirsiniz
        // ...

        StartCoroutine(ShowNextCard());
    }

    IEnumerator ShowNextCard()
    {
        currentScenario++;
        if (currentScenario < techScenarios.Length)
        {
            yield return new WaitForSeconds(1f);
            DisplayScenario();
        }
        else
        {
            // Senaryo bitiþi iþlemleri
        }
    }
}
