using UnityEngine;
using UnityEngine.SceneManagement;


public class EndLevel : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            //collision.gameObject.transform.position = new Vector3(-4f, -1.5f, 0f);
            int indexCurrentScene = SceneManager.GetActiveScene().buildIndex;

            if (indexCurrentScene == 1) SceneManager.LoadScene(indexCurrentScene + 1);
            if (indexCurrentScene == 2) SceneManager.LoadScene(0);
            
        }
    }
}
