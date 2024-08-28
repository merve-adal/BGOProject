using UnityEngine;
using UnityEngine.UI;

public class CardManager : MonoBehaviour
{
    // UI Referanslar�
    public Text storyText;
    public Button leftButton;
    public Button rightButton;

    // Kategori Barlar� ve Textleri
    public Image powerBar;
    public Text powerText;
    public Image technologyBar;
    public Text technologyText;
    public Image moneyBar;
    public Text moneyText;
    public Image successBar;
    public Text successText;

    // Kategori De�erleri
    private int power = 50;
    private int technology = 50;
    private int money = 50;
    private int success = 50;

    // Senaryo ve Se�im Verileri
    private string[] scenarios = {
        "Bir hacker sana gizli bir veritaban� sunuyor. Bu veritaban�, mega �irketlerin en �st d�zey y�neticilerinin karanl�k s�rlar�n� i�eriyor.",
        "Karanl�k bir sokakta bir bilim insan� seni durduruyor ve sana yasa d��� bir teknoloji satmak istiyor.",
        "Bir mega �irket, sana b�y�k bir r��vet teklif ediyor, ancak bu teklifi kabul etmek itibar�ni sarsabilir.",
        "Bir teknoloji konferans�na davet edildin. Orada konu�mac� olman, sekt�rle ilgili itibar�n� art�rabilir.",
        "Siber bir sald�r� ile mega bir �irketin g�venlik sistemine s�zma f�rsat�n var."
    };

    private string[][] choices = {
        new string[] {"Reddet", "Kabul Et"},
        new string[] {"Reddet", "Sat�n Al"},
        new string[] {"Reddet", "Kabul Et"},
        new string[] {"Gitme", "Konu�"},
        new string[] {"Geri �ekil", "S�z"}
    };

    // Her se�im i�in kategori de�i�iklikleri: Reddet (�lk 4), Kabul Et (Son 4)
    private int[][] outcomes = {
        new int[] {0, -20, -10, 0, 0, 20, 10, 0},  // Senaryo 1
        new int[] {0, -20, 0, 0, 0, 30, -10, 0},  // Senaryo 2
        new int[] {-20, 0, 0, -20, 0, 0, 30, 10},  // Senaryo 3
        new int[] {0, 0, 0, -10, 0, 0, 0, 20},  // Senaryo 4
        new int[] {0, 10, 0, -10, 10, 20, 0, -10}  // Senaryo 5
    };

    // �u anki senaryo indeksi
    private int currentScenario = 0;

    void Start()
    {
        UpdateUI();

        // Butonlara t�klama olaylar�n� ekle
        leftButton.onClick.AddListener(() => MakeChoice(0));
        rightButton.onClick.AddListener(() => MakeChoice(1));
    }

    void MakeChoice(int choiceIndex)
    {
        // Se�im sonucu de�i�ecek de�erler
        int powerChange = 0;
        int technologyChange = 0;
        int moneyChange = 0;
        int successChange = 0;

        // Se�ime g�re de�erleri belirle
        if (choiceIndex == 0) // Sol se�im (Reddi Et)
        {
            powerChange = outcomes[currentScenario][0];
            technologyChange = outcomes[currentScenario][1];
            moneyChange = outcomes[currentScenario][2];
            successChange = outcomes[currentScenario][3];
        }
        else if (choiceIndex == 1) // Sa� se�im (Kabul Et)
        {
            powerChange = outcomes[currentScenario][4];
            technologyChange = outcomes[currentScenario][5];
            moneyChange = outcomes[currentScenario][6];
            successChange = outcomes[currentScenario][7];
        }

        // De�erleri g�ncelle
        power += powerChange;
        technology += technologyChange;
        money += moneyChange;
        success += successChange;

        // De�erleri s�n�rla
        power = Mathf.Clamp(power, 0, 100);
        technology = Mathf.Clamp(technology, 0, 100);
        money = Mathf.Clamp(money, 0, 100);
        success = Mathf.Clamp(success, 0, 100);

        // Konsolda hangi de�erin nas�l de�i�ti�ini kontrol et
        Debug.Log($"G��: {powerChange}, Teknoloji: {technologyChange}, Para: {moneyChange}, Ba�ar�: {successChange}");
        Debug.Log($"G�ncel De�erler -> G��: {power}, Teknoloji: {technology}, Para: {money}, Ba�ar�: {success}");

        // UI'y� g�ncelle
        UpdateUI();

        // Bir sonraki senaryoya ge�
        currentScenario = (currentScenario + 1) % scenarios.Length;
    }

    void UpdateUI()
    {
        // Senaryoyu ve se�imleri g�ncelle
        storyText.text = scenarios[currentScenario];
        leftButton.GetComponentInChildren<Text>().text = choices[currentScenario][0];
        rightButton.GetComponentInChildren<Text>().text = choices[currentScenario][1];

        // Barlar� ve textleri g�ncelle
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
