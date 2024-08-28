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
        "Ana siber g�venlik merkezine ilk sald�r�n�z� planl�yorsunuz. �al��anlar�n �o�u gece vardiyas�nda olacak. Direkt bir sald�r� m� yapacaks�n�z, yoksa i�eri s�zmak i�in bir g�venlik a���� m� arayacaks�n�z?",
        "Siber g�venlik merkezi sizi fark etti ve alarm� devreye soktu. �u anda geri �ekilme veya alarm� devre d��� b�rakma aras�nda karar vermelisiniz.",
        "Bir grup direni��i, megakorporasyonun �nemli bir tesisine sald�rmak i�in destek istiyor. Ancak, bu size pahal�ya patlayabilir. Destek verecek misiniz?",
        "Gizli bir bilgi kayna��, bir sonraki teknoloji sevkiyat�n� kesmenin b�y�k bir avantaj sa�layaca��n� s�yl�yor. Ama bu tehlikeli bir operasyon olacak.",
        "�ehrin yeralt� siyasetinde �nemli bir fig�r, direni�le i� birli�i yapmak istiyor. Ancak onunla �al��mak imaj�n�za zarar verebilir.",
        "Mega �irketin CEO'su size gizli bir anla�ma teklif ediyor: Belirli bir b�lgeyi bo�alt�rsan�z, size y�ksek miktarda para verecek.",
        "Ana �irket binas�na nihai sald�r�y� planl�yorsunuz. G��l� bir EMP cihaz� kullanmak, d��man�n t�m savunmalar�n� devre d��� b�rakabilir, ancak bu direni�in t�m teknoloji kaynaklar�n� t�ketecektir.",
        "Son sald�r�n�zdan sonra, direni� �yelerinizden baz�lar� �ehri terk etmek ve g�venli bir yere gitmek istiyor. Onlar� tutacak m�s�n�z?"
    };

    // Left and Right Choices Effects
    private int[,] choicesEffects = {
        { 10, -10, 0, 0 }, // Card 1: Left Choice: Power +10, Success -10
        { -10, 0, 10, 0 }, // Card 1: Right Choice: Success +10, Power -10
        { 0, -10, 0, 10 }, // Card 2: Left Choice: Success -10, Money +10
        { 10, 0, -10, 0 }, // Card 2: Right Choice: Power +10, Technology -10
        { 0, 0, -10, 10 }, // Card 3: Left Choice: Technology -10, Money +10
        { -10, 0, 0, 10 }, // Card 3: Right Choice: Power -10, Success +10
        { 0, -10, 10, 0 }, // Card 4: Left Choice: Success -10, Technology +10
        { 10, 0, 0, -10 }, // Card 4: Right Choice: Power +10, Money -10
        { 10, 0, -10, 0 }, // Card 5: Left Choice: Power +10, Technology -10
        { -10, 10, 0, 0 }, // Card 5: Right Choice: Success +10, Power -10
        { 0, -10, 10, 0 }, // Card 6: Left Choice: Success -10, Technology +10
        { 10, 0, 0, -10 }, // Card 6: Right Choice: Power +10, Money -10
        { -10, 0, 10, 0 }, // Card 7: Left Choice: Power -10, Technology +10
        { 10, -10, 0, 0 }, // Card 7: Right Choice: Power +10, Success -10
        { 0, 10, 0, -10 }, // Card 8: Left Choice: Success +10, Money -10
        { -10, 0, 10, 0 }  // Card 8: Right Choice: Power -10, Technology +10
    };

    void Start()
    {
        // Set initial values for progress bars
        UpdateProgressBars();

        // Add listeners to buttons
        leftButton.onClick.AddListener(() => MakeChoice(0));
        rightButton.onClick.AddListener(() => MakeChoice(1));

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
        // Update parameter values based on choice
        power += choicesEffects[currentCardIndex * 2 + choiceIndex, 0];
        success += choicesEffects[currentCardIndex * 2 + choiceIndex, 1];
        technology += choicesEffects[currentCardIndex * 2 + choiceIndex, 2];
        money += choicesEffects[currentCardIndex * 2 + choiceIndex, 3];

        // Log the updated values
        Debug.Log($"G��: {power}, Ba�ar�: {success}, Teknoloji: {technology}, Para: {money}");

        // Update progress bars
        UpdateProgressBars();

        // Move to the next card
        currentCardIndex++;
        UpdateCard();
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
