using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class EventArea : MonoBehaviour
{
    public UnityEvent enterEvents;
    public UnityEvent exitEvents;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            enterEvents.Invoke();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            exitEvents.Invoke();
        }
    }

    public void ChangeLevel(int levelIndex)
    {
        SceneManager.LoadScene(levelIndex);
    }
}
