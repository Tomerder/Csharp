using System;
using System.Collections.Generic;
using System.Text;

namespace AdvancedEmployeeManagement
{
    public class ProjectManager : Employee
    {
        private int _numProjectsManaged;
        // Reminder: Project is a reference type, and therefore the
        //  _currentProject field is initialized to null.  We must
        //  set it (using the SetProject method) before we use it,
        //  or we will get a NullReferenceException.
        private Project _currentProject;

        public ProjectManager(string name, int salary, int numProjectsManaged)
            : base(name, salary)
        {
            _numProjectsManaged = numProjectsManaged;
        }

        public void SetProject(Project project)
        {
            _currentProject = project;
        }
        //public Project Project { set { _currentProject = value; } }

        public override void CalculateSalary()
        {
            switch (_currentProject.GetStatus())
            {
                case Project.ProjectStatus.BeforeSchedule:
                    _salary += (int)(_salary * 0.1);
                    // Note that every case statement inside a C# switch
                    //  must have a break statement at its end, unless
                    //  it's an empty statement.
                    break;
                case Project.ProjectStatus.OnSchedule:
                    _salary += (int)(_salary * 0.05);
                    break;
                default:
                    break;
            }
            base.CalculateSalary();
        }
    }
}
