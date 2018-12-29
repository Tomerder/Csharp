using System;

namespace ControllingSerialization
{
    /// <summary>
    /// Revised version of the user class.  Does not serialize the password
    /// for security reasons, so that sensitive information is not persisted
    /// outside the program's memory.
    /// </summary>
    [Serializable]
    class User2
    {
        private string _name;
        [NonSerialized]
        private string _password;
        private DateTime _loginTime;
        private int _reputation;

        public User2(string name, string password)
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
