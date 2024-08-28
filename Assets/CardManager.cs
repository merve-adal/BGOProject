using UnityEngine;
using UnityEngine.UI;

public class CardManager : MonoBehaviour
{
    // UI Referanslarý
    public Text storyText;
    public Button leftButton;
    public Button rightButton;

    // Kategori Barlarý ve Textleri
    public Image powerBar;
    public Text powerText;
    public Image technologyBar;
    public Text technologyText;
    public Image moneyBar;
    public Text moneyText;
    public Image successBar;
    public Text successText;

    // Kategori Deðerleri
    private int power = 50;
    private int technology = 50;
    private int money = 50;
    private int success = 50;

    // Senaryo ve Seçim Verileri
    private string[] scenarios = {
        "Bir hacker sana gizli bir veritabaný sunuyor. Bu veritabaný, mega þirketlerin en üst düzey yöneticilerinin karanlýk sýrlarýný içeriyor.",
        "Karanlýk bir sokakta bir bilim insaný seni durduruyor ve sana yasa dýþý bir teknoloji satmak istiyor.",
        "Bir mega þirket, sana büyük bir rüþvet teklif ediyor, ancak bu teklifi kabul etmek itibarýni sarsabilir.",
        "Bir teknoloji konferansýna davet edildin. Orada konuþmacý olman, sektörle ilgili itibarýný artýrabilir.",
        "Siber bir saldýrý ile mega bir þirketin güvenlik sistemine sýzma fýrsatýn var."
    };

    private string[][] choices = {
        new string[] {"Reddet", "Kabul Et"},
        new string[] {"Reddet", "Satýn Al"},
        new string[] {"Reddet", "Kabul Et"},
        new string[] {"Gitme", "Konuþ"},
        new string[] {"Geri Çekil", "Sýz"}
    };

    // Her seçim için kategori deðiþiklikleri: Reddet (Ýlk 4), Kabul Et (Son 4)
    private int[][] outcomes = {
        new int[] {0, -20, -10, 0, 0, 20, 10, 0},  // Senaryo 1
        new int[] {0, -20, 0, 0, 0, 30, -10, 0},  // Senaryo 2
        new int[] {-20, 0, 0, -20, 0, 0, 30, 10},  // Senaryo 3
        new int[] {0, 0, 0, -10, 0, 0, 0, 20},  // Senaryo 4
        new int[] {0, 10, 0, -10, 10, 20, 0, -10}  // Senaryo 5
    };

    // Þu anki senaryo indeksi
    private int currentScenario = 0;

    void Start()
    {
        UpdateUI();

        // Butonlara týklama olaylarýný ekle
        leftButton.onClick.AddListener(() => MakeChoice(0));
        rightButton.onClick.AddListener(() => MakeChoice(1));
    }

    void MakeChoice(int choiceIndex)
    {
        // Seçim sonucu deðiþecek deðerler
        int powerChange = 0;
        int technologyChange = 0;
        int moneyChange = 0;
        int successChange = 0;

        // Seçime göre deðerleri belirle
        if (choiceIndex == 0) // Sol seçim (Reddi Et)
        {
            powerChange = outcomes[currentScenario][0];
            technologyChange = outcomes[currentScenario][1];
            moneyChange = outcomes[currentScenario][2];
            successChange = outcomes[currentScenario][3];
        }
        else if (choiceIndex == 1) // Sað seçim (Kabul Et)
        {
            powerChange = outcomes[currentScenario][4];
            technologyChange = outcomes[currentScenario][5];
            moneyChange = outcomes[currentScenario][6];
            successChange = outcomes[currentScenario][7];
        }

        // Deðerleri güncelle
        power += powerChange;
        technology += technologyChange;
        money += moneyChange;
        success += successChange;

        // Deðerleri sýnýrla
        power = Mathf.Clamp(power, 0, 100);
        technology = Mathf.Clamp(technology, 0, 100);
        money = Mathf.Clamp(money, 0, 100);
        success = Mathf.Clamp(success, 0, 100);

        // Konsolda hangi deðerin nasýl deðiþtiðini kontrol et
        Debug.Log($"Güç: {powerChange}, Teknoloji: {technologyChange}, Para: {moneyChange}, Baþarý: {successChange}");
        Debug.Log($"Güncel Deðerler -> Güç: {power}, Teknoloji: {technology}, Para: {money}, Baþarý: {success}");

        // UI'yý güncelle
        UpdateUI();

        // Bir sonraki senaryoya geç
        currentScenario = (currentScenario + 1) % scenarios.Length;
    }

    void UpdateUI()
    {
        // Senaryoyu ve seçimleri güncelle
        storyText.text = scenarios[currentScenario];
        leftButton.GetComponentInChildren<Text>().text = choices[currentScenario][0];
        rightButton.GetComponentInChildren<Text>().text = choices[currentScenario][1];

        // Barlarý ve textleri güncelle
        UpdateBarAndText(moneyBar, moneyText, money, "");
        UpdateBarAndText(powerBar, powerText, power, "");
        UpdateBarAndText(technologyBar, technologyText, technology, "");
        UpdateBarAndText(successBar, successText, success, "");
    }

    void UpdateBarAndText(Image bar, Text text, int value, string categoryName)
    {
        bar.fillAmount = value / 100f;
        text.text = $"{categoryName} {value}";
    }
}
