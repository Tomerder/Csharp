using System;

namespace AdvancedEmployeeManagement
{
    class EmployeeMain
    {
        static void Main(string[] args)
        {
            ProjectManager mary = new ProjectManager("Mary White", 10000, 3);
            Programmer jack = new Programmer("Jack Smith", 7000, true);

            Payroll payroll = new Payroll(100);
            payroll.AddEmployee(mary);
            payroll.AddEmployee(jack);

            payroll.PrintReport();

            Project project1 = new Project("Infrastructure");
            mary.SetProject(project1);

            // Programmers worked hard on the infrastructure project,
            //  and finished it before schedule.
            //
            project1.SetStatus(Project.ProjectStatus.BeforeSchedule);
            jack.SetBonus(500);

            payroll.CalculateSalaries();
            payroll.PrintReport();
        }
    }
}
