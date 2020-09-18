using System;
using System.Collections.Generic;

[Serializable]
public class JsonSaya
{
    public List<listSoal> data;
}

[Serializable]
public class listSoal
{
    public string id_soal;
    public string poin;
    public List<listanswer> jawaban;
}

[Serializable]
public class listanswer
{
    public string id_objektif;
    public string id_jawab;
}