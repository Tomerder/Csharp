using System;

namespace BinaryFormatterExample
{
    /// <summary>
    /// Represents a user logged on to the system.  The user class
    /// is marked with the [Serializable] attribute, indicating to the
    /// serialization runtime that instances of the User class can be
    /// serialized and deserialized.  (Remember that serialization is
    /// an opt-in mechanism.)
    /// </summary>
    [Serializable]
    class User
    {
        private string _name;
        private string _password;
        private DateTime _loginTime;
        private int _reputation;

        public User(string name, string password)
        {
            _name = name;
            _password = password;
            _loginTime = DateTime.Now;
            _reputation = ReputationDB.ReputationForUser(name);
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
