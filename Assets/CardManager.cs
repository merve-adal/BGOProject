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
        "Ana siber güvenlik merkezine ilk saldýrýnýzý planlýyorsunuz. Çalýþanlarýn çoðu gece vardiyasýnda olacak. Direkt bir saldýrý mý yapacaksýnýz, yoksa içeri sýzmak için bir güvenlik açýðý mý arayacaksýnýz?",
        "Siber güvenlik merkezi sizi fark etti ve alarmý devreye soktu. Þu anda geri çekilme veya alarmý devre dýþý býrakma arasýnda karar vermelisiniz.",
        "Bir grup direniþçi, megakorporasyonun önemli bir tesisine saldýrmak için destek istiyor. Ancak, bu size pahalýya patlayabilir. Destek verecek misiniz?",
        "Gizli bir bilgi kaynaðý, bir sonraki teknoloji sevkiyatýný kesmenin büyük bir avantaj saðlayacaðýný söylüyor. Ama bu tehlikeli bir operasyon olacak.",
        "Þehrin yeraltý siyasetinde önemli bir figür, direniþle iþ birliði yapmak istiyor. Ancak onunla çalýþmak imajýnýza zarar verebilir.",
        "Mega þirketin CEO'su size gizli bir anlaþma teklif ediyor: Belirli bir bölgeyi boþaltýrsanýz, size yüksek miktarda para verecek.",
        "Ana þirket binasýna nihai saldýrýyý planlýyorsunuz. Güçlü bir EMP cihazý kullanmak, düþmanýn tüm savunmalarýný devre dýþý býrakabilir, ancak bu direniþin tüm teknoloji kaynaklarýný tüketecektir.",
        "Son saldýrýnýzdan sonra, direniþ üyelerinizden bazýlarý þehri terk etmek ve güvenli bir yere gitmek istiyor. Onlarý tutacak mýsýnýz?"
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
        Debug.Log($"Güç: {power}, Baþarý: {success}, Teknoloji: {technology}, Para: {money}");

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
