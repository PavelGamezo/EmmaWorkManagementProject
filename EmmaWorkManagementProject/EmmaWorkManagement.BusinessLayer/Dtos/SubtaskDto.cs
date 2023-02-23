using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmmaWorkManagement.BusinessLayer.Dtos
{
    public class SubtaskDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Comment { get; set; }
        public bool isActive { get; set; }

        public int UserTaskId { get; set; }
    }
}
