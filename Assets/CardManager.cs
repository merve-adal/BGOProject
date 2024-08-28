using UnityEngine;
using UnityEngine.UI;

public class CardManager : MonoBehaviour
{
    // UI References
    public Text storyText;
    public Button leftButton;
    public Button rightButton;

    // Progress Bars for Parameters
    public ProgressBar powerBar;
    public ProgressBar successBar;
    public ProgressBar technologyBar;
    public ProgressBar moneyBar;

    // Resource Values
    private int power = 50;
    private int success = 50;
    private int technology = 50;
    private int money = 50;

    // Current Card and Decision
    private int currentCardIndex = 0;

    // Story Card Data
    private string[] storyCards = {
        "Yüksek riskli bir uyuþturucu sevkiyatý önerisi aldýn. Kabul edersen, çetenin gücünü artýrabilirsin ama maliyeti yüksek olacak. Kabul ediyor musun?",
        "Çetene, düþmanlarý alt etmek için son teknoloji bir savaþ dronu sunuluyor. Bu dronu almak büyük bir teknoloji sýçramasý yapmaný saðlayacak, ancak bakým ve iþletim maliyetleri yüksek. Kabul ediyor musun?",
       "Çetene bir siber güvenlik sistemi kurman öneriliyor. Bu sistemle rakip hackerlardan korunabilir, ancak kurulum ve sürekli güncellemeler için büyük bir yatýrým yapman gerekecek. Kabul ediyor musun?",
        "Yeraltý dünyasýnýn en prestijli mühendislerinden biri, sana özel olarak geliþtirilmiþ, yüksek teknolojili bir aracý tanýtýyor . Satýn almayý kabul ediyor musun?",
        "Geliþmiþ bir izleme sistemi satýn alarak rakip çetelerin her adýmýný izleyebilir, böylece operasyonel üstünlük saðlayabilirsin. Ancak bu teknolojinin kurulumu ve bakýmý zor ve pahalý. Kabul ediyor musun?" ,
        "Rakip çetenin liderine karþý teke tek bir düello daveti aldýn. Bu, seni çete içinde bir efsane yapabilir, ama kaybedersen çetenin itibarý zarar görebilir. Kabul ediyor musun?",
        "Çete lideri, kritik bir operasyon için seni görevlendirdi. Bu görevde çetenin gücünü önemli ölçüde artýracak stratejik bir hamle yapman gerekiyor. Ancak, bu görevde liderin talimatlarýný harfiyen uygulamak zorundasýn, bu da senin bireysel olarak parlamaný engelleyecek. Görevi kabul edersen, çete güç kazanacak ama senin kendi baþarýlarýn gölgede kalacak. Kabul ediyor musun?",
        "Rakip bir çeteye karþý büyük bir saldýrý düzenleyebilirsin. Bu saldýrý, çetene büyük bir güç kazandýracak, fakat operasyon oldukça maliyetli. Kabul ediyor musun?"
    };

    // Arrays to categorize scenarios
    private int[] techMoneyList = { 0 }; // Teknoloji ve Para
    private int[] techPowerList = { 1 }; // Teknoloji ve Güç
    private int[] techSuccessList = { 2 }; // Teknoloji ve Baþarý
    private int[] moneyPowerList = {3 }; // Para ve Güç
    private int[] moneySuccessList = { 4 }; // Para ve Baþarý
    private int[] powerSuccessList = { 5, 6, 7 }; // Güç ve Baþarý

    void Start()
    {
        // Set initial values for progress bars
        UpdateProgressBars();

        // Add listeners to buttons
        leftButton.onClick.AddListener(() =>
        {
            int choiceIndex = 0;
            MakeChoice(choiceIndex);
        });
        rightButton.onClick.AddListener(() =>
        {
            int choiceIndex = 1;
        MakeChoice(choiceIndex);
        });

        // Display the first card
        UpdateCard();
    }

    void UpdateCard()
    {
        if (currentCardIndex >= storyCards.Length)
        {
            EndGame();
            return;
        }

        storyText.text = storyCards[currentCardIndex];
    }

    public void MakeChoice(int choiceIndex)
    {
        // Check which list the current card belongs to
        if (IsInList(currentCardIndex, techMoneyList))
        {
            if (choiceIndex == 0)
            {
                technology -= 10;
                money += 10;
            }
            else
            {
                technology += 10;
                money -= 10;
            }
        }
        else if (IsInList(currentCardIndex, techPowerList))
        {
            if (choiceIndex == 0)
            {
                technology -= 10;
                power += 10;
            }
            else
            {
                technology += 10;
                power -= 10;
            }
        }
        else if (IsInList(currentCardIndex, techSuccessList))
        {
            if (choiceIndex == 0)
            {
                technology -= 10;
                success += 10;
            }
            else
            {
                technology += 10;
                success -= 10;
            }
        }
        else if (IsInList(currentCardIndex, moneyPowerList))
        {
            if (choiceIndex == 0)
            {
                money -= 10;
                power += 10;
            }
            else
            {
                money += 10;
                power -= 10;
            }
        }
        else if (IsInList(currentCardIndex, moneySuccessList))
        {
            if (choiceIndex == 0)
            {
                money -= 10;
                success += 10;
            }
            else
            {
                money += 10;
                success -= 10;
            }
        }
        else if (IsInList(currentCardIndex, powerSuccessList))
        {
            if (choiceIndex == 0)
            {
                power -= 10;
                success += 10;
            }
            else
            {
                power += 10;
                success -= 10;
            }
        }

        // Log the updated values
        Debug.Log($"Güç: {power}, Baþarý: {success}, Teknoloji: {technology}, Para: {money}");

        // Update progress bars
        UpdateProgressBars();

        // Move to the next card
        currentCardIndex++;
        UpdateCard();
    }


    bool IsInList(int index, int[] list)
    {
        foreach (int i in list)
        {
            if (i == index)
                return true;
        }
        return false;
    }

    void UpdateProgressBars()
    {
        // Update progress bars with the current values of the parameters
        powerBar.BarValue = power;
        successBar.BarValue = success;
        technologyBar.BarValue = technology;
        moneyBar.BarValue = money;
    }

    void EndGame()
    {
        storyText.text = "Oyun Bitti!";
        leftButton.gameObject.SetActive(false);
        rightButton.gameObject.SetActive(false);
    }
}
