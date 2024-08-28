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
        "Y�ksek riskli bir uyu�turucu sevkiyat� �nerisi ald�n. Kabul edersen, �etenin g�c�n� art�rabilirsin ama maliyeti y�ksek olacak. Kabul ediyor musun?",
        "�etene, d��manlar� alt etmek i�in son teknoloji bir sava� dronu sunuluyor. Bu dronu almak b�y�k bir teknoloji s��ramas� yapman� sa�layacak, ancak bak�m ve i�letim maliyetleri y�ksek. Kabul ediyor musun?",
       "�etene bir siber g�venlik sistemi kurman �neriliyor. Bu sistemle rakip hackerlardan korunabilir, ancak kurulum ve s�rekli g�ncellemeler i�in b�y�k bir yat�r�m yapman gerekecek. Kabul ediyor musun?",
        "Yeralt� d�nyas�n�n en prestijli m�hendislerinden biri, sana �zel olarak geli�tirilmi�, y�ksek teknolojili bir arac� tan�t�yor . Sat�n almay� kabul ediyor musun?",
        "Geli�mi� bir izleme sistemi sat�n alarak rakip �etelerin her ad�m�n� izleyebilir, b�ylece operasyonel �st�nl�k sa�layabilirsin. Ancak bu teknolojinin kurulumu ve bak�m� zor ve pahal�. Kabul ediyor musun?" ,
        "Rakip �etenin liderine kar�� teke tek bir d�ello daveti ald�n. Bu, seni �ete i�inde bir efsane yapabilir, ama kaybedersen �etenin itibar� zarar g�rebilir. Kabul ediyor musun?",
        "�ete lideri, kritik bir operasyon i�in seni g�revlendirdi. Bu g�revde �etenin g�c�n� �nemli �l��de art�racak stratejik bir hamle yapman gerekiyor. Ancak, bu g�revde liderin talimatlar�n� harfiyen uygulamak zorundas�n, bu da senin bireysel olarak parlaman� engelleyecek. G�revi kabul edersen, �ete g�� kazanacak ama senin kendi ba�ar�lar�n g�lgede kalacak. Kabul ediyor musun?",
        "Rakip bir �eteye kar�� b�y�k bir sald�r� d�zenleyebilirsin. Bu sald�r�, �etene b�y�k bir g�� kazand�racak, fakat operasyon olduk�a maliyetli. Kabul ediyor musun?"
    };

    // Arrays to categorize scenarios
    private int[] techMoneyList = { 0 }; // Teknoloji ve Para
    private int[] techPowerList = { 1 }; // Teknoloji ve G��
    private int[] techSuccessList = { 2 }; // Teknoloji ve Ba�ar�
    private int[] moneyPowerList = {3 }; // Para ve G��
    private int[] moneySuccessList = { 4 }; // Para ve Ba�ar�
    private int[] powerSuccessList = { 5, 6, 7 }; // G�� ve Ba�ar�

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
        Debug.Log($"G��: {power}, Ba�ar�: {success}, Teknoloji: {technology}, Para: {money}");

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
