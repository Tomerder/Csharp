using System;
using System.Runtime.Serialization;

namespace CustomSerialization
{
    /// <summary>
    /// The final version of the user class, incorporating custom serialization
    /// to "securely" store the password using XOR-encryption and to restore all
    /// volatile fields after the object is deserialized.
    /// 
    /// Note that even though this class implements ISerializable to control its
    /// serialization, it must still be marked with the [Serializable] attribute,
    /// or an exception will be thrown at runtime when attempting to serializing
    /// an instance of this class.
    /// </summary>
    [Serializable]
    class User4 : ISerializable
    {
        private string _name;
        private string _password;
        private DateTime _loginTime;
        private int _reputation;

        /// <summary>
        /// Performs simple XOR encryption on the specified string using the
        /// specified character key.  Note that this is not strong encryption
        /// and it is demonstrated here for demo purposes only.
        /// </summary>
        /// <param name="str">The string to encrypt or decrypt.</param>
        /// <param name="key">The encryption key.</param>
        /// <returns>The encrypted or decrypted string.</returns>
        private static string EncryptDecrypt(string str, char key)
        {
            string result = String.Empty;
            for (int i = 0; i < str.Length; ++i)
                result += (char)(str[i] ^ key);
            return result;
        }

        /// <summary>
        /// Called by the serialization runtime when the object is being serialized.
        /// Use the 'info' parameter to record all data that needs to be serialized.
        /// </summary>
        void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Name", _name);
            info.AddValue("Password", EncryptDecrypt(_password, 'X'));
        }

        /// <summary>
        /// Called by the serialization runtime when the object is being deserialized.
        /// You cannot rely on the standard constructor to be called - but this constructor
        /// will be called.  Note that this constructor could also be made private because
        /// the serialization runtime is not constrained by accessibility modifiers.
        /// </summary>
        protected User4(SerializationInfo info, StreamingContext context)
        {
            _name = info.GetString("Name");
            _password = EncryptDecrypt(info.GetString("Password"), 'X');
            InitData();
        }

        public User4(string name, string password)
        {
            _name = name;
            _password = password;
            InitData();
        }

        private void InitData()
        {
            _loginTime = DateTime.Now;
            _reputation = ReputationDB.ReputationForUser(_name);
        }

        public string Name { get { return _name; } }
        public int Reputation { get { return _reputation; } }
        public DateTime LoginTime { get { return _loginTime; } }

        public override string ToString()
        {
            return String.Format("{0}: {1}, logged on since {2}, password: {3}",
                _name, _reputation, _loginTime, _password);
        }
    }
}
