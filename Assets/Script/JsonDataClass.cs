using System;

[System.Serializable]
public class JsonDataClass
{
    public soal[] soalbanyak;
}

[System.Serializable]
public class soal
{
    public string id_soal;
    public string poin;
    public listJawabanku[] jawaban;
}

[System.Serializable]
public class listJawabanku
{
    public string id_objektif;
    public string id_jawab;

}

