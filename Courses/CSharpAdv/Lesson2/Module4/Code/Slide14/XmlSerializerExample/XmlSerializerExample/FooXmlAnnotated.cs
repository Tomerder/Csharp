using System.Xml.Serialization;

namespace XmlSerializerExample
{
    [XmlRoot("Foo", Namespace = "urn:sela.samples")]
    public class FooXmlAnnotated
    {
        #region Id

        private int _id;

        [XmlAttribute("ID")]
        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        #endregion // Id

        #region Name

        private string _name;

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        #endregion // Name

        #region Pass

        private string _pass;

        [XmlIgnore]
        public string Pass
        {
            get { return _pass; }
            set { _pass = value; }
        }

        #endregion // Pass

        #region ToString

        public override string ToString()
        {
            return string.Format("Id = {0}, Name = {1}, Pass = {2}", Id, Name, Pass);
        }

        #endregion // ToString
    }
}
