using System.Collections.Generic;

namespace SerializationCallbacks
{
    /// <summary>
    /// Maintains a reputation index for users.
    /// </summary>
    class ReputationDB
    {
        private static Dictionary<string, int> _reputation = new Dictionary<string, int>();

        static ReputationDB()
        {
            _reputation["Joe"] = 15;
            _reputation["Mike"] = 12;
        }

        public static void ChangeReputation(string user, int reputation)
        {
            _reputation[user] = reputation;
        }

        public static int ReputationForUser(string user)
        {
            return _reputation[user];
        }
    }
}
