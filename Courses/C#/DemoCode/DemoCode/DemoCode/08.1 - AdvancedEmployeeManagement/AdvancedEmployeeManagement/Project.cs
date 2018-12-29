using System;

namespace AdvancedEmployeeManagement
{
    public class Project
    {
        // Note that we're declaring a nested enum.  To refer to it,
        //  we will have to use the following notation:
        //      Project.ProjectStatus.BeforeSchedule
        //
        public enum ProjectStatus
        {
            BeforeSchedule,
            OnSchedule,
            AfterSchedule,
            Failed
        }

        private string _name;
        private ProjectStatus _status;

        public Project(string name)
        {
            _name = name;
        }

        public ProjectStatus GetStatus() { return _status; }
        public void SetStatus(ProjectStatus status) { _status = status; }
        //public ProjectStatus Status
        //{
        //    get { return _status; }
        //    set { _status = value; }
        //}
    }
}
