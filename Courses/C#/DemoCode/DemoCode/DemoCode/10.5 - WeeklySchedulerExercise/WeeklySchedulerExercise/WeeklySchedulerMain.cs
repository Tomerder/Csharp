using System;

namespace WeeklySchedulerExercise
{
    class WeeklySchedulerMain
    {
        static void Main(string[] args)
        {
            WeeklyScheduler schedule = new WeeklyScheduler(4, 9);
            
            schedule[DayOfWeek.Sunday, 9] = "Mary";
            schedule[DayOfWeek.Sunday, 12] = "Diana";

            schedule.Print();
        }
    }
}
