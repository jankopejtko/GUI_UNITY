using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerInventory : MonoBehaviour
{
    [Header("Nastavení skóre")]
    public int coins = 0;
    public Text scoreText;

    //todo add sound effect when collecting coins
    [Header("Efekty")]
    public AudioClip collectCoinSound;
    public AudioClip collectPowerCoinSound;
    private AudioSource audioSource;
    //todo add sound effect when collecting coins

    void Start()
    {
        //todo add sound effect when collecting coins
        audioSource = GetComponent<AudioSource>();
        //todo add sound effect when collecting coins
        UpdateUI();
    }

    private void OnTriggerEnter(Collider other)
    {
        //level 4 lava
        if (other.gameObject.CompareTag("Lava"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        //level 4 lava

        if (other.gameObject.CompareTag("Coin"))
        {
            coins++;

            UpdateUI();

            //todo add sound effect when collecting coins
            if (audioSource && collectCoinSound)
            {
                audioSource.PlayOneShot(collectCoinSound);
            }
            //todo add sound effect when collecting coins

            Destroy(other.gameObject);

            // todo add vicory condition
            if ((GameObject.FindGameObjectsWithTag("Coin").Length - 1) <= 0)
            {
                Debug.Log("Všechny mince posbírány! Vítězství!");
                scoreText.color = Color.green;
                // level 5 return to menu
                SceneManager.LoadScene("MainMenuExample");
                // level 5 return to menu
            }
            // todo add vicory condition
        }

        //todo add "red coin" power coin that gives 5 coins
        if (other.gameObject.CompareTag("Power_coin")) 
        {
            coins = coins + 5;
            UpdateUI();
            if (audioSource && collectPowerCoinSound)
            {
                audioSource.PlayOneShot(collectPowerCoinSound);
            }
            Destroy(other.gameObject);
        }
        //todo add "red coin" power coin that gives 5 coins
    }

    void UpdateUI()
    {
        if (scoreText != null)
        {
            scoreText.text = "Mince: " + coins;
        }
    }
}
