using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace SqlHistoryViewer
{
    public class ScriptHistoryData
    {
        public string DeployVersion { get; set; }
        public string FileName { get; set; }
        public string QueryData { get; set; }
        public DateTime DateCreated { get; set; }

        public override string ToString()
        {

            var stringBuilder = new StringBuilder();
            stringBuilder.AppendLine($"{nameof(DeployVersion)}        : {DeployVersion}");
            stringBuilder.AppendLine($"{nameof(FileName)}             : {FileName}");
            stringBuilder.AppendLine($"{nameof(QueryData)}            : {QueryData}");

            return stringBuilder.ToString();
        }

        public ScriptHistoryData Clone()
        {
            ScriptHistoryData result = null;
            using (MemoryStream ms = new MemoryStream())
            {
                IFormatter bf = new BinaryFormatter();

                bf.Serialize(ms, this);
                ms.Position = 0;

                result = (ScriptHistoryData)bf.Deserialize(ms);
            }


            return result;
        }
    }
}
