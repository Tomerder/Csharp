using System;
using System.Runtime.Serialization;

namespace SerializationCallbacks
{
    /// <summary>
    /// Another revised version of the user class, which demonstrates the
    /// use of serialization callbacks for controlling what happens after
    /// serialization.  Only the [OnDeserialized] callback is actually used -
    /// the rest of the callbacks are provided for demonstration purposes.
    /// </summary>
    [Serializable]
    class User3
    {
        /// <summary>
        /// Only the user name is serialized.  The rest of the fields
        /// are dynamically restored when deserialization completes.
        /// The password is not serialized at all for security reasons.
        /// </summary>
        private string _name;
        [NonSerialized]
        private string _password;
        [NonSerialized]
        private DateTime _loginTime;
        [NonSerialized]
        private int _reputation;

        /// <summary>
        /// This method is called immediately after deserialization
        /// completes, and ensures that the user's reputation and
        /// login time are refreshed to reflect the present.
        /// </summary>
        [OnDeserialized]
        private void OnDeserialized(StreamingContext context)
        {
            Console.WriteLine("[OnDeserialized] callback: User3 object has been deserialized");
            InitData();
        }

        [OnDeserializing]
        private void OnDeserializing(StreamingContext context)
        {
            Console.WriteLine("[OnDeserializing] callback: User3 object is deserializing");
        }

        [OnSerialized]
        private void OnSerialized(StreamingContext context)
        {
            Console.WriteLine("[OnSerialized] callback: User3 object has been serialized");
        }

        [OnSerializing]
        private void OnSerializing(StreamingContext context)
        {
            Console.WriteLine("[OnSerializing] callback: User3 object is serializing");
        }

        public User3(string name, string password)
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
