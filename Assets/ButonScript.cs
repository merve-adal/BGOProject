using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardController : MonoBehaviour
{
    public List<GameObject> cards; // Kartlarýnýzýn bulunduðu liste
    private int currentCardIndex = 0;

    public int money = 50;
    public int technology = 50;
    public int gangPower = 50;
    public int individualSuccess = 50;

    public Text moneyText;
    public Text technologyText;
    public Text gangPowerText;
    public Text individualSuccessText;

    public Camera mainCamera; // Ana kamera referansý
    public float cameraMoveSpeed = 2.0f; // Kamera hareket hýzý

    void Start()
    {
        ShowCurrentCard();
        MoveCameraToCurrentCard(); // Baþlangýçta kamera doðru konumda olsun
    }

    public void OnAccept()
    {
        // Parametreleri güncelle
        money += 10;
        if (money > 100) money = 100;

        // Diðer parametreleri güncelleyin (örneðin technology, gangPower, individualSuccess)

        // Parametreleri güncel olarak ekranda göster
        UpdateUI();

        // Bir sonraki karta geç
        currentCardIndex++;
        if (currentCardIndex < cards.Count)
        {
            ShowCurrentCard();
            MoveCameraToCurrentCard(); // Kamerayý yeni karta odakla
        }
        else
        {
            Debug.Log("Oyun Bitti!");
            // Oyun bitiþ ekraný veya diðer iþlemler
        }
    }

    public void OnReject()
    {
        // Parametreleri güncelle
        money -= 10;
        if (money < 0) money = 0;

        // Diðer parametreleri güncelleyin (örneðin technology, gangPower, individualSuccess)

        // Parametreleri güncel olarak ekranda göster
        UpdateUI();

        // Bir sonraki karta geç
        currentCardIndex++;
        if (currentCardIndex < cards.Count)
        {
            ShowCurrentCard();
            MoveCameraToCurrentCard(); // Kamerayý yeni karta odakla
        }
        else
        {
            Debug.Log("Oyun Bitti!");
            // Oyun bitiþ ekraný veya diðer iþlemler
        }
    }

    private void ShowCurrentCard()
    {
        // Tüm kartlarý gizle
        foreach (var card in cards)
        {
            card.SetActive(false);
        }

        // Mevcut kartý göster
        cards[currentCardIndex].SetActive(true);
    }

    private void UpdateUI()
    {
        moneyText.text = "Money: " + money;
        technologyText.text = "Technology: " + technology;
        gangPowerText.text = "Gang Power: " + gangPower;
        individualSuccessText.text = "Individual Success: " + individualSuccess;
    }

    private void MoveCameraToCurrentCard()
    {
        Vector3 targetPosition = new Vector3(cards[currentCardIndex].transform.position.x, cards[currentCardIndex].transform.position.y, mainCamera.transform.position.z);
        StartCoroutine(SmoothMoveCamera(targetPosition));
    }

    private IEnumerator SmoothMoveCamera(Vector3 targetPosition)
    {
        while (Vector3.Distance(mainCamera.transform.position, targetPosition) > 0.01f)
        {
            mainCamera.transform.position = Vector3.Lerp(mainCamera.transform.position, targetPosition, cameraMoveSpeed * Time.deltaTime);
            yield return null;
        }
        mainCamera.transform.position = targetPosition;
    }
}
