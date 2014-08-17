using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alfursan.Domain
{
    public class EntityResponder<T> : Responder
    {
        public T Data { get; set; }
    }
}
