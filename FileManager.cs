// Decompiled with JetBrains decompiler
// Type: FileManager
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8E6852E2-1B68-460A-A222-84B5FB6C5C57
// Assembly location: C:\Users\m\Downloads\Solgryns-Pokemon-On-Twitch-App-V2.2.6\Solgryns Pokemon On Twitch_Data\Managed\Assembly-CSharp.dll

using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

#pragma warning disable 612, 618
public class FileManager
{
    // public static string SWITCH_MOUNT_NAME = Application.productName + "Save";

    public static object LoadData(string path)
    {
        if (File.Exists(path))
        {
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            FileStream serializationStream = File.Open(path, FileMode.Open);
            try
            {
                object obj = binaryFormatter.Deserialize((Stream)serializationStream);
                serializationStream.Close();
                return obj;
            }
            catch (SerializationException ex)
            {
                // Debug.LogError((object)("The file is not the correct format. (" + path + ")" + ex.ToString()));
            }
            // Debug.Log((object)("Loaded " + path));
            serializationStream.Close();
        }
        return (object)null;
    }

    public static void SaveData(string path, object dataToSave)
    {
        BinaryFormatter binaryFormatter = new BinaryFormatter();
        FileStream fileStream = File.Exists(path) ? File.Open(path, FileMode.Open) : File.Open(path, FileMode.Create);
        FileStream serializationStream = fileStream;
        object graph = dataToSave;
        binaryFormatter.Serialize((Stream)serializationStream, graph);
        fileStream.Close();
    }

    public static void DeleteData(string path) => File.Delete(path);
}
