using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealthSystem : MonoBehaviour
{
    public int max_health;
    public int current_health;

    public int first_aid_count;
    public int Max_first_aid;

    UI_controller hp_bar;
    // Start is called before the first frame update
    void Start()
    {
        current_health = max_health;
        hp_bar = FindObjectOfType<UI_controller>();
        hp_bar.SetMaxHealth(max_health);
        hp_bar.first_aid_count.SetText(first_aid_count.ToString());
    }

    // Update is called once per frame
    void Update()
    {
        recover();
    }


    private void recover()
    {
        if(Input.GetKeyDown(KeyCode.T) && first_aid_count > 0)
        {
            if(current_health > 0)
            {
                current_health += max_health / 2;
                first_aid_count--;
                if(current_health > max_health)
                {
                    current_health = max_health;
                }
                hp_bar.SetHealth(current_health);
                hp_bar.first_aid_count.SetText(first_aid_count.ToString());
            }            
        }
    }

    public void TakingDamange(int damage)
    {
        current_health -= damage;

        hp_bar.SetHealth(current_health);

        if(current_health <= 0)
        {
            
            gameObject.SetActive(false);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}
