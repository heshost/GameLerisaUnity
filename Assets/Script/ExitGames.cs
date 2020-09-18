using UnityEngine;

namespace lerisa
{
    public class ExitGames : MonoBehaviour
    {
        public AudioSource ButtonSound;
        public void ExitDariGame()
        {
            AudioSource buttonSound = ButtonSound.GetComponent<AudioSource>();  //sound exit
            buttonSound.PlayOneShot(buttonSound.clip);

            Debug.Log("Quit Sukses!");
            Application.Quit();
        }
    }
}
