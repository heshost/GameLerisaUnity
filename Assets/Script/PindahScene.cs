using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PindahScene : MonoBehaviour
{
    // pindahscene parameter tidak boleh sama dengan class

    public AudioSource ButtonSound;   //untuk sound pindahscene
    public string namaScene;          //pindah scene dengan memanggil string nama
    public void PindahAntarScene()
    {
        AudioSource buttonSound = ButtonSound.GetComponent<AudioSource>();  //referense audiosource dan memanggil buttonsound
        buttonSound.PlayOneShot(buttonSound.clip);                          //eksekusi buttonsound dengan play one shot 

        Scene sceneIniAktif = SceneManager.GetActiveScene();                //cek scene yang sedang aktif
        if (sceneIniAktif.name != namaScene)                                // jika scene aktif tidak sama dengan nama scene (yang dicek nama) maka bisa pindah panel
            SceneManager.LoadScene (namaScene);                             // tidak terjadi pindah scene sesama aktif
    }
}
