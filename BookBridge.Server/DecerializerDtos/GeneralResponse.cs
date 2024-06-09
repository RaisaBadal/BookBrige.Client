using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookBridge.Server.DecerializerDtos
{
    public class GeneralResponse<T>where T : class
    {
        public bool succeeded { get; set; }
        public object hasViewPermission { get; set; }
        public List<T> data { get; set; }
        public List<object> errors { get; set; }
        public List<object> messages { get; set; }
    }
}
