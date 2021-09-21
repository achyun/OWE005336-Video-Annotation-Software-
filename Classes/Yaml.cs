using LabellingDB;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YamlDotNet.Core;
using YamlDotNet.Core.Events;
using YamlDotNet.Serialization;

namespace OWE005336__Video_Annotation_Software_.Classes
{
    internal sealed class LabelNodeYamlTypeConverter : IYamlTypeConverter
    {
        public bool Accepts(Type type) => type == typeof(LabelNode);

        public object ReadYaml(IParser parser, Type type)
        {
            if (parser.TryConsume<Scalar>(out Scalar ev))
            {
                string textID = ev.Value;
                return Program.ImageDatabase.LabelTree_LoadByTextID(textID);
            }
            throw new InvalidDataException("Invalid value for LabelNode type");
        }

        public void WriteYaml(IEmitter emitter, object value, Type type)
        {
            emitter.Emit(new Scalar((value as LabelNode).TextID));
        }
    }
}
