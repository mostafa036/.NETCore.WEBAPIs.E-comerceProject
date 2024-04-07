using System.Collections;
using System.Collections.Generic;

namespace Talabat.APIs.Errors
{
    public class ApiValidtionErrorResponse : ApiResponese
    {
        public IEnumerable<string> Errors  { get; set; }

        public ApiValidtionErrorResponse():base(400)
        {
            
        }

    }
}
