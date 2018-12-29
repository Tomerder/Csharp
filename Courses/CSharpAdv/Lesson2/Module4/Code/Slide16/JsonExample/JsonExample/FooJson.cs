﻿using Newtonsoft.Json;

namespace JsonExample
{
    public class FooJson
    {
        #region Id

        private int _id;

        [JsonProperty(PropertyName = "id")]
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

        [JsonIgnore()]
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
