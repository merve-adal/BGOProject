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
        "Bir teknoloji firmas�, �ehri daha verimli y�netmenizi sa�layacak yeni bir yaz�l�m sunuyor. Kabul edecek misiniz?",
        "Yapay zekalar, kontrol�n�zden ��kmaya ba�lad�. Onlar� durdurmak i�in bir ��z�m aramal� m�s�n�z?",
        "�ehirdeki enerji kaynaklar� t�kenmek �zere. Yeni bir yenilenebilir enerji kayna�� geli�tirilmeli mi?",
        "Bir teknoloji m�hendisi, sizin i�in �zel bir g�venlik sistemi tasarlamay� �neriyor. Bu teklifi kabul etmeli misiniz?",
        "Teknoloji alan�nda devrim yaratacak bir proje i�in b�y�k bir yat�r�m yapma f�rsat�n�z var. Bu f�rsat� de�erlendirmeli misiniz?"
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
            Debug.LogError("currentScenario, senaryo uzunlu�unu a��yor!");
        }
    }

    void MakeChoice(bool accepted)
    {
        // �rne�in, se�ime ba�l� olarak teknoloji de�erini g�ncelle
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
            // Senaryo biti�i i�lemleri
        }
    }
}
