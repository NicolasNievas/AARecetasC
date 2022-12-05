using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Final_Recetas_Prog2.Servicios.Interfaces;
using Final_Recetas_Prog2.Servicios.Implementaciones;

namespace Final_Recetas_Prog2.Servicios
{
    class ServiceFactoryImp : AbstractFactoryService
    {
        public override IServicio crearServicio()
        {
            return new ServicioReceta();
        }
    }
}
