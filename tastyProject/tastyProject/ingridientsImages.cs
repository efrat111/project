using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tastyProject
{
    public class ingridientsImages
    {
        public string Code { get; set; }
        public string Image { get; set; }

        public ingridientsImages(string code, string image)
        {
            Code = code;
            Image = image;
        }
    }
}
