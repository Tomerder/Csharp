// Based on IExecuteWithObject.cs
// http://www.galasoft.ch

namespace Toolkit
{
    public interface IWeakActionWithParam1
    {
        object Target
        {
            get;
        }

        void ExecuteWithObject(object parameter);

        void MarkForDeletion();
    }

    public interface IWeakActionWithParam2 : IWeakActionWithParam1
    {
        void ExecuteWithObject(object param1, object param2);
    }
}