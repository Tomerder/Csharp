using System;

namespace Delegates
{
    class Car
    {
        private string _name;
        private int _licenseId;
        private bool _isDirty;
        private int _distanceTravelled;

        public string Name
        {
            get { return _name; }
        }

        public int LicenseId
        {
            get { return _licenseId; }
        }

        public bool IsDirty
        {
            get { return _isDirty; }
            set { _isDirty = value; }
        }

        public int DistanceTravelled
        {
            get { return _distanceTravelled; }
        }

        public void Travel(int distanceKm)
        {
            _distanceTravelled += distanceKm;
        }

        public Car(string name, int licenseId)
        {
            _name = name;
            _licenseId = licenseId;
        }
    }
}
