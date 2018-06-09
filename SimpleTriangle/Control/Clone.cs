using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using SimpleTriangle.Model;

namespace SimpleTriangle.Control
{
    class CloneTool
    {
        public static T Clone<T>(T real)
        {
            using (Stream objectStream=new MemoryStream())
            {
                IFormatter formatter = new BinaryFormatter();
                formatter.Serialize(objectStream,real);
                objectStream.Seek(0, SeekOrigin.Begin);
                return (T) formatter.Deserialize(objectStream);
            }
        }

        public static List<Pointdata> CloneList(List<Pointdata> real)
        {
            List<Pointdata> list=new List<Pointdata>();
            foreach (var pointdata in real)
            {
                list.Add(new Pointdata(pointdata.x,pointdata.y));
            }

            return list;
        }
    }
}
