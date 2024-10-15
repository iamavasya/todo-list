﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace todo_list.BusinessLogic.Dtos
{
    public class TaskDto
    {
        public string? Title { get; set; }
        public string? Description { get; set; }
        public DateTime DueDate { get; set; }
        public bool IsCompleted { get; set; }
        public int CategoryId { get; set; }
        public string? CategoryName { get; set; }
    }
}
