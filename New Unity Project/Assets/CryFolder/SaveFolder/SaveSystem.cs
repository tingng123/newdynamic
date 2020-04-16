using System.IO;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
public class SaveSystem
{
    public static void saveplayer(PlayerSave player)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/player.save";
        FileStream filestream = new FileStream(path, FileMode.Create);

        //PlayerSave data = new PlayerSave();
    }
}
