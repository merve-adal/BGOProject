using UnityEngine;
using UnityEngine.UI;

public class CardManager : MonoBehaviour
{
    // UI Referanslarý
    public Text storyText;
    public Button leftButton;
    public Button rightButton;

    // Kaynak Deðerleri
    private int power = 50;
    private int success = 50;
    private int technology = 50;
    private int money = 50;

    // Mevcut Kart ve Karar
    private int currentCardIndex = 0;

    // Kartlarýn Verileri
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

    // Sol ve Sað Seçimlerin Etkileri
    private int[,] choicesEffects = {
        { 10, -10, 0, 0 }, // Card 1: Sol seçim: Güç +10, Baþarý -10
        { -10, 0, 10, 0 }, // Card 1: Sað seçim: Baþarý +10, Güç -10
        { 0, -10, 0, 10 }, // Card 2: Sol seçim: Baþarý -10, Para +10
        { 10, 0, -10, 0 }, // Card 2: Sað seçim: Güç +10, Teknoloji -10
        { 0, 0, -10, 10 }, // Card 3: Sol seçim: Teknoloji -10, Para +10
        { -10, 0, 0, 10 }, // Card 3: Sað seçim: Güç -10, Baþarý +10
        { 0, -10, 10, 0 }, // Card 4: Sol seçim: Baþarý -10, Teknoloji +10
        { 10, 0, 0, -10 }, // Card 4: Sað seçim: Güç +10, Para -10
        { 10, 0, -10, 0 }, // Card 5: Sol seçim: Güç +10, Teknoloji -10
        { -10, 10, 0, 0 }, // Card 5: Sað seçim: Baþarý +10, Güç -10
        { 0, -10, 10, 0 }, // Card 6: Sol seçim: Baþarý -10, Teknoloji +10
        { 10, 0, 0, -10 }, // Card 6: Sað seçim: Güç +10, Para -10
        { -10, 0, 10, 0 }, // Card 7: Sol seçim: Güç -10, Teknoloji +10
        { 10, -10, 0, 0 }, // Card 7: Sað seçim: Güç +10, Baþarý -10
        { 0, 10, 0, -10 }, // Card 8: Sol seçim: Baþarý +10, Para -10
        { -10, 0, 10, 0 }  // Card 8: Sað seçim: Güç -10, Teknoloji +10
    };

    void Start()
    {
        UpdateCard();
        leftButton.onClick.AddListener(() => MakeChoice(0));
        rightButton.onClick.AddListener(() => MakeChoice(1));
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
        power += choicesEffects[currentCardIndex * 2 + choiceIndex, 0];
        success += choicesEffects[currentCardIndex * 2 + choiceIndex, 1];
        technology += choicesEffects[currentCardIndex * 2 + choiceIndex, 2];
        money += choicesEffects[currentCardIndex * 2 + choiceIndex, 3];

        Debug.Log($"Güç: {power}, Baþarý: {success}, Teknoloji: {technology}, Para: {money}");

        currentCardIndex++;
        UpdateCard();
    }

    void EndGame()
    {
        storyText.text = "Oyun Bitti!";
        leftButton.gameObject.SetActive(false);
        rightButton.gameObject.SetActive(false);
    }
}
