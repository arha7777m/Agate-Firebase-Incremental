using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        gameObject.transform.localScale = Vector3.LerpUnclamped(gameObject.transform.localScale, Vector3.one, 0.15f);
        gameObject.transform.Rotate(0f, 0f, Time.deltaTime * -100f);
    }

    public void NextScene()
    {
        SceneManager.LoadScene(1);
    }
}
