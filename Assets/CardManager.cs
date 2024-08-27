using UnityEngine;
using UnityEngine.UI;

public class CardManager : MonoBehaviour
{
    // UI Referanslar�
    public Text storyText;
    public Button leftButton;
    public Button rightButton;

    // Kaynak De�erleri
    private int power = 50;
    private int success = 50;
    private int technology = 50;
    private int money = 50;

    // Mevcut Kart ve Karar
    private int currentCardIndex = 0;

    // Kartlar�n Verileri
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

    // Sol ve Sa� Se�imlerin Etkileri
    private int[,] choicesEffects = {
        { 10, -10, 0, 0 }, // Card 1: Sol se�im: G�� +10, Ba�ar� -10
        { -10, 0, 10, 0 }, // Card 1: Sa� se�im: Ba�ar� +10, G�� -10
        { 0, -10, 0, 10 }, // Card 2: Sol se�im: Ba�ar� -10, Para +10
        { 10, 0, -10, 0 }, // Card 2: Sa� se�im: G�� +10, Teknoloji -10
        { 0, 0, -10, 10 }, // Card 3: Sol se�im: Teknoloji -10, Para +10
        { -10, 0, 0, 10 }, // Card 3: Sa� se�im: G�� -10, Ba�ar� +10
        { 0, -10, 10, 0 }, // Card 4: Sol se�im: Ba�ar� -10, Teknoloji +10
        { 10, 0, 0, -10 }, // Card 4: Sa� se�im: G�� +10, Para -10
        { 10, 0, -10, 0 }, // Card 5: Sol se�im: G�� +10, Teknoloji -10
        { -10, 10, 0, 0 }, // Card 5: Sa� se�im: Ba�ar� +10, G�� -10
        { 0, -10, 10, 0 }, // Card 6: Sol se�im: Ba�ar� -10, Teknoloji +10
        { 10, 0, 0, -10 }, // Card 6: Sa� se�im: G�� +10, Para -10
        { -10, 0, 10, 0 }, // Card 7: Sol se�im: G�� -10, Teknoloji +10
        { 10, -10, 0, 0 }, // Card 7: Sa� se�im: G�� +10, Ba�ar� -10
        { 0, 10, 0, -10 }, // Card 8: Sol se�im: Ba�ar� +10, Para -10
        { -10, 0, 10, 0 }  // Card 8: Sa� se�im: G�� -10, Teknoloji +10
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

        Debug.Log($"G��: {power}, Ba�ar�: {success}, Teknoloji: {technology}, Para: {money}");

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
