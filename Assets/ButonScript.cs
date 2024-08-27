using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardController : MonoBehaviour
{
    public List<GameObject> cards; // Kartlar�n�z�n bulundu�u liste
    private int currentCardIndex = 0;

    public int money = 50;
    public int technology = 50;
    public int gangPower = 50;
    public int individualSuccess = 50;

    public Text moneyText;
    public Text technologyText;
    public Text gangPowerText;
    public Text individualSuccessText;

    public Camera mainCamera; // Ana kamera referans�
    public float cameraMoveSpeed = 2.0f; // Kamera hareket h�z�

    void Start()
    {
        ShowCurrentCard();
        MoveCameraToCurrentCard(); // Ba�lang��ta kamera do�ru konumda olsun
    }

    public void OnAccept()
    {
        // Parametreleri g�ncelle
        money += 10;
        if (money > 100) money = 100;

        // Di�er parametreleri g�ncelleyin (�rne�in technology, gangPower, individualSuccess)

        // Parametreleri g�ncel olarak ekranda g�ster
        UpdateUI();

        // Bir sonraki karta ge�
        currentCardIndex++;
        if (currentCardIndex < cards.Count)
        {
            ShowCurrentCard();
            MoveCameraToCurrentCard(); // Kameray� yeni karta odakla
        }
        else
        {
            Debug.Log("Oyun Bitti!");
            // Oyun biti� ekran� veya di�er i�lemler
        }
    }

    public void OnReject()
    {
        // Parametreleri g�ncelle
        money -= 10;
        if (money < 0) money = 0;

        // Di�er parametreleri g�ncelleyin (�rne�in technology, gangPower, individualSuccess)

        // Parametreleri g�ncel olarak ekranda g�ster
        UpdateUI();

        // Bir sonraki karta ge�
        currentCardIndex++;
        if (currentCardIndex < cards.Count)
        {
            ShowCurrentCard();
            MoveCameraToCurrentCard(); // Kameray� yeni karta odakla
        }
        else
        {
            Debug.Log("Oyun Bitti!");
            // Oyun biti� ekran� veya di�er i�lemler
        }
    }

    private void ShowCurrentCard()
    {
        // T�m kartlar� gizle
        foreach (var card in cards)
        {
            card.SetActive(false);
        }

        // Mevcut kart� g�ster
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
