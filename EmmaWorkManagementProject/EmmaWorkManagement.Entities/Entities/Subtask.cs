using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmmaWorkManagement.Entities.Entities
{
    public class Subtask
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Comment { get; set; }
        public bool isActive { get; set; }

        public int UserTaskId { get; set; }
        public UserTask UserTask { get; set; }
    }
}
