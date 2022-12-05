using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Final_Recetas_Prog2.Servicios.Interfaces;

namespace Final_Recetas_Prog2.Servicios
{
    abstract class AbstractFactoryService
    {
        public abstract IServicio crearServicio();
    }
}
