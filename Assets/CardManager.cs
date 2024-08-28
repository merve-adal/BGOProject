using UnityEngine;
using UnityEngine.UI;

public class ChoiceSystem : MonoBehaviour
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

    private int powerValue = 50;
    private int techValue = 50;
    private int moneyValue = 50;
    private int successValue = 50;

    private int currentScenario = 0;

    private string[] scenarios = {
        "Bir hacker grubunun �ehirdeki g�venlik sistemlerini devre d��� b�rakt���n� ��rendiniz. Onlar� durdurmak i�in m�dahale etmeli misiniz?",
        "B�y�k bir �irket size �ehirdeki en y�ksek binay� kontrol etme teklifinde bulunuyor. Teklifi kabul edecek misiniz?",
        "�ehirdeki eski bir teknoloji uzman�, size gizli bir bulu� teklif ediyor. Bu bulu�u kullanmal� m�s�n�z?",
        "Bir su� lordu, size b�y�k miktarda para kar��l���nda �ehirdeki bir b�lgeyi kontrol etmeyi teklif ediyor. Kabul edecek misiniz?",
        "Bir grup asi, �ehirde bir ayaklanma ba�latmay� planl�yor. Onlara yard�m etmeli misiniz?"
    };

    void Start()
    {
        UpdateCircleValues();
        DisplayScenario();
        acceptButton.onClick.AddListener(() => MakeChoice(true));
        rejectButton.onClick.AddListener(() => MakeChoice(false));
    }

    void DisplayScenario()
    {
        scenarioText.text = scenarios[currentScenario];
    }

    void MakeChoice(bool accepted)
    {
        switch (currentScenario)
        {
            case 0:
                if (accepted)
                {
                    ModifyCategories(ref powerValue, powerCircle, powerText, 10);
                    ModifyCategories(ref techValue, techCircle, techText, -10);
                }
                else
                {
                    ModifyCategories(ref techValue, techCircle, techText, 10);
                    ModifyCategories(ref powerValue, powerCircle, powerText, -10);
                }
                break;

            case 1:
                if (accepted)
                {
                    ModifyCategories(ref moneyValue, moneyCircle, moneyText, 10);
                    ModifyCategories(ref powerValue, powerCircle, powerText, -10);
                }
                else
                {
                    ModifyCategories(ref successValue, successCircle, successText, 10);
                    ModifyCategories(ref moneyValue, moneyCircle, moneyText, -10);
                }
                break;

            case 2:
                if (accepted)
                {
                    ModifyCategories(ref techValue, techCircle, techText, 10);
                    ModifyCategories(ref successValue, successCircle, successText, -10);
                }
                else
                {
                    ModifyCategories(ref powerValue, powerCircle, powerText, 10);
                    ModifyCategories(ref techValue, techCircle, techText, -10);
                }
                break;

            case 3:
                if (accepted)
                {
                    ModifyCategories(ref moneyValue, moneyCircle, moneyText, 10);
                    ModifyCategories(ref successValue, successCircle, successText, -10);
                }
                else
                {
                    ModifyCategories(ref powerValue, powerCircle, powerText, 10);
                    ModifyCategories(ref moneyValue, moneyCircle, moneyText, -10);
                }
                break;

            case 4:
                if (accepted)
                {
                    ModifyCategories(ref successValue, successCircle, successText, 10);
                    ModifyCategories(ref powerValue, powerCircle, powerText, -10);
                }
                else
                {
                    ModifyCategories(ref techValue, techCircle, techText, 10);
                    ModifyCategories(ref successValue, successCircle, successText, -10);
                }
                break;
        }

        currentScenario++;
        if (currentScenario < scenarios.Length)
        {
            DisplayScenario();
        }
        else
        {
            scenarioText.text = "Demo Bitti!";
            acceptButton.gameObject.SetActive(false);
            rejectButton.gameObject.SetActive(false);
        }
    }

    void ModifyCategories(ref int categoryValue, Image categoryCircle, Text categoryText, int amount)
    {
        categoryValue = Mathf.Clamp(categoryValue + amount, 0, 100);
        categoryCircle.fillAmount = categoryValue / 100f;
        categoryText.text = categoryValue.ToString();
    }

    void UpdateCircleValues()
    {
        powerText.text = powerValue.ToString();
        techText.text = techValue.ToString();
        moneyText.text = moneyValue.ToString();
        successText.text = successValue.ToString();

        powerCircle.fillAmount = powerValue / 100f;
        techCircle.fillAmount = techValue / 100f;
        moneyCircle.fillAmount = moneyValue / 100f;
        successCircle.fillAmount = successValue / 100f;
    }
}
