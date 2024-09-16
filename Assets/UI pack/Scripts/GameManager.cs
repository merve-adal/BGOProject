using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public int powerValue = 50;
    public int techValue = 50;
    public int moneyValue = 50;
    public int successValue = 50;

    public bool[] screensVisited = new bool[4]; // Power, Tech, Money, Success

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
