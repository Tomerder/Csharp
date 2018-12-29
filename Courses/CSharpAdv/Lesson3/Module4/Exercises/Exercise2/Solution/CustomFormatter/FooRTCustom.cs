using System;
using System.Runtime.Serialization;
using System.Security.Cryptography;
using System.Text;

namespace SoapFormatterExample
{
    [Serializable]
    public class FooRTCustom : ISerializable
    {
        #region Ctor

        public FooRTCustom() { }

        public FooRTCustom(SerializationInfo info, StreamingContext context)
        {
            _id = info.GetInt32("id_");
            _name = info.GetString("name_");
            byte[] cipher = info.GetValue("pass_", typeof(byte[])) as byte[];
            byte[] buffer = ProtectedData.Unprotect(cipher, new byte[0], DataProtectionScope.CurrentUser);
            _pass = Encoding.UTF8.GetString(buffer);
        }

        #endregion // Ctor

        #region GetObjectData

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("id_", _id);
            info.AddValue("name_", _name);
            var buffer = Encoding.UTF8.GetBytes(_pass);
            byte[] cipher = ProtectedData.Protect(buffer, new byte[0], DataProtectionScope.CurrentUser);
            info.AddValue("pass_", cipher);
        }

        #endregion // GetObjectData

        #region Id

        private int _id;

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
