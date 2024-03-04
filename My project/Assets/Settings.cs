using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

public class Settings : MonoBehaviour
{
    public static float sensitivity = 1f;

    // Start is called before the first frame update
    void Start()
    {
        FileStream stream;
        if (!File.Exists(Application.persistentDataPath + "/settings.save"))
        {
            stream = new FileStream(Application.persistentDataPath + "/settings.save", FileMode.Create);
            StreamWriter writer = new StreamWriter(stream);
            writer.WriteLine(sensitivity);
        }
        else stream = new FileStream(Application.persistentDataPath + "/settings.save", FileMode.Open);

        StreamReader reader = new StreamReader(stream);
        if (float.TryParse(reader.ReadLine(), out float l))
        {
            sensitivity = l;
        }

        stream.Close();
    }

    private void OnApplicationQuit()
    {
        FileStream stream;
        if (!File.Exists(Application.persistentDataPath + "/settings.save"))
        {
            stream = new FileStream(Application.persistentDataPath + "/settings.save", FileMode.Create);
        }
        else stream = new FileStream(Application.persistentDataPath + "/settings.save", FileMode.Open);

        StreamWriter writer = new StreamWriter(stream);

        writer.WriteLine(sensitivity);

        stream.Close();
    }
}
