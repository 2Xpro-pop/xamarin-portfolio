using System;
using System.IO;
using Xamarin.Forms;
using System.Text;
using System.ComponentModel;
using System.Windows.Input;
using System.Xml.Serialization;
using System.Xml;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace AprLearn.ViewModels
{
    public class DocumentSerializer : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private Models.Document document;

        public ICommand SerializeObject { get; private set;}
        public ICommand DeserializeEditorText { get; private set; }

        private IExceptionVisualize visualHandler;

        public string Description
        {
            get => document.Description;
            set 
            {
                
                document.Description = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Description)));
            }
        }

        public string Name
        {
            get => document.Name;
            set
            {
                
                document.Name = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Name)));
            }
        }

        public string Path
        {
            get => document.Path;
            set
            {
                
                document.Path = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Path)));
            }
        }

        public string Text
        {
            get => document.FormattedText;
            set
            {
                document.FormattedText = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Text)));
            }
        }

        public string EditorText
        {
            get => editorText;
            set
            {
                editorText = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(EditorText)));
            }
        }
        private string editorText;

        public DocumentSerializer(IExceptionVisualize visualHandler)
        {
            document = new Models.Document("arnold", "/hey.ad", "something");
            document.FormattedText = "asdasdasdd";
            SerializeObject = new Command(ObjectToEditorText);
            DeserializeEditorText = new Command(EditorTextToObject);
            SerializeObject.Execute(null);
            this.visualHandler = visualHandler;
        }

        private void ObjectToEditorText()
        {
            XmlSerializer serializer = new XmlSerializer(document.GetType());
 
            using (var stream = new MemoryStream())
            {
                serializer.Serialize(stream, document);
                var reader = new StreamReader(stream);

                string data = Encoding.UTF8.GetString(stream.ToArray());
                string _byteOrderMarkUtf8 = Encoding.UTF8.GetString(Encoding.UTF8.GetPreamble());
                if (data.StartsWith(_byteOrderMarkUtf8))
                {
                    data = data.Remove(0, _byteOrderMarkUtf8.Length);
                }

                /// Frendly xml formatting 
                var element = System.Xml.Linq.XElement.Parse(data);
                var frendlyXml = new StringBuilder() ;
                var settings = new XmlWriterSettings
                {
                    Indent = true,
                    NewLineOnAttributes = true,
                    IndentChars = "    ",
                };

                using (var writer = XmlWriter.Create(frendlyXml, settings))
                {
                    element.Save(writer);
                }

                EditorText = frendlyXml.ToString();
            }
        }

        private void EditorTextToObject()
        {
            XmlSerializer serializer = new XmlSerializer(document.GetType(), new XmlRootAttribute("Document"));
            using (var stream = new StringReader(EditorText))
            {
                try
                {
                    document = (Models.Document)serializer.Deserialize(stream);
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Description)));
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Path)));
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Name)));
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Text)));
                }
                catch (Exception exc)
                {
                    Console.WriteLine(exc);
                    visualHandler.Visualize(ViewModelExceptionOn.DocumentDeserelizing);
                }
            }
        }

    }

    public interface IExceptionVisualize
    {
        void Visualize(ViewModelExceptionOn exceptionOn);
    }

    public enum ViewModelExceptionOn
    {
        DocumentDeserelizing,
        DocumentSerializing,
    }
}
