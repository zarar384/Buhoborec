using Buhoborec.Domain.Entities;
using System.Collections.Generic;

namespace Buhoborec.Infrastructure.Extensions
{
    public class InitialData
    {
        public static IEnumerable<WorkLog> WorkLogs =>
            new List<WorkLog>
            {
            };

        public static IEnumerable<TaskItem> TaskItems =>
           new List<TaskItem>
           {
           };

        public static IEnumerable<Absence> Absences
        {
            get
            {
                return new List<Absence> {  };
            }
        }
    }
}
