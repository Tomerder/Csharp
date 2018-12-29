using System;

namespace AdvancedEmployeeManagement
{
    // Deriving a class from another class is as simple as
    //  using the : notation.  There is no such thing as
    //  deriving only implementation (protected/private derivation),
    //  as in C++.  In addition, there is no multiple inheritance,
    //  i.e. a class can have only one base class.
    //
    public class Programmer : Employee
    {
        // _knowsCSharp is implicitly initialized to false, and
        //  _bonus is implicitly initialized to 0.
        //
        private bool _knowsCSharp;
        private int _bonus;

        public Programmer(string name, int salary, bool knowsCSharp)
            // Note that we're calling the base class' constructor here.
            //  There can be no possible ambiguity between various base
            //  classes, because a .NET type may only have one base.
            //
            : base(name, salary)
        {
            this._knowsCSharp = knowsCSharp;
        }

        public void SetBonus(int bonus)
        {
            this._bonus = bonus;
        }
        //public int Bonus { set { _bonus = bonus; } }

        // This is what programmers are supposed to do:
        //
        public void Program() { }

        // Note that when we override Employee.CalculateSalary,
        //  we can still call the base class version by using
        //  the base.CalculateSalary notation.  Again, there
        //  can be only one base, so there is no room for ambiguity.
        //
        public override void CalculateSalary()
        {
            if (_bonus > 0)
            {
                _salary += _bonus;
                _bonus = 0;
            }
            base.CalculateSalary();
        }
    }
}
