using System.Runtime.Serialization;

namespace DataContractExample
{
    [DataContract(Name = "Foo")]
    public class FooDataContract
    {
        #region Id

        private int _id;

        [DataMember(Name = "id")]
        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        #endregion // Id

        #region Name

        private string _name;

        [DataMember]
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        #endregion // Name

        #region Pass

        private string _pass;

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
