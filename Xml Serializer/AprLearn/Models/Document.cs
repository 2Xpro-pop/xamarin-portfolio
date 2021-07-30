using System;
using Xamarin.Essentials;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.IO;
using System.Xml.Schema;
using System.Xml;

namespace AprLearn.Models
{
    [Serializable]
    public struct Document
    {
        [XmlAttribute]
        public string Path { get; set; }

        [XmlAttribute]
        public string Name { get; set; }

        [XmlAttribute]
        public string Description { get; set; }

        [XmlText]
        public string FormattedText { get; set; }

        public bool IsLoaded { get => File.Exists(System.IO.Path.Combine(App.LocalPath, Path)); }

        public Document(string name = default, string path = default, string description = default, string formattedText= default)
        {
            Name = name;
            Description = path;
            Path = description;
            FormattedText = formattedText;
        }

    }
}
