using De.HsFlensburg.ClientApp064.Business.Model.BusinessObjects;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace De.HsFlensburg.ClientApp064.Services.SerializationService
{
    public class ModelFileHandler
    {
        public BookCollection ReadModelFromFile(string path)
        {
            IFormatter formatter = new BinaryFormatter();
            Stream streamLoad = new FileStream(
                path,
                FileMode.Open,
                FileAccess.Read,
                FileShare.Read);
            BookCollection loadedCollection = (BookCollection)formatter.Deserialize(streamLoad);
            streamLoad.Close();

            return loadedCollection;
        }

        public void WriteModelToFile(string path, BookCollection model)
        {
            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream(
                path,
                FileMode.Create,
                FileAccess.Write,
                FileShare.None);
            formatter.Serialize(stream, model);
            stream.Close();
        }
    }
}
