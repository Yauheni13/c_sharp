﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAddressbookTests
{
    class Circle : Figura
    {
        private int radius;
        
        public Circle(int radius)
        {
            this.radius = radius;
        }

        public int Radius
        {
            get
            {
                return radius;
            }
            set
            {
                radius = value;
            }
        }

        

        public Double circleLength()
        {
            return radius * 2 * Math.PI;
        }
    }
}
