using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SwitchScene : MonoBehaviour
{
    public Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator.SetTrigger("Fade");
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Player") && name.Equals("Building One Entrance"))
        {
            animator.SetTrigger("Idle");
            StartCoroutine(ChangeScene(1));
        }
    }

    IEnumerator ChangeScene (int id)
    {
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(id);
    }
}
