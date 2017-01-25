using System;
using System.Collections.Generic;
using System.Linq; 
using System.Text;
using System.Threading.Tasks;

namespace WebAddressbookTests
{
    class Square : Figura
    {
        private int size;
        
        public Square(int size)
        {
            this.size = size;
        }

        public int Size
        {
            get
            {
                return size;
            }
            set
            {
                size = value;
            }
        }
               

        public int squarePerimetr()
        {
            return size * 4;
        }

        public int squareValue()
        {
            return size*size;
        }

    }
}
