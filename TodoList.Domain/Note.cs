using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TodoList.Domain
{
    public class Note
    {
        public int Id { get; set; }
        public string Heading { get; set; }
        public string Text { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime Deadline { get; set; }
        public bool Status { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
    }
}
