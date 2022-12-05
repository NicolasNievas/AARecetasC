using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Final_Recetas_Prog2.Dominio
{
    public class Receta
    {
        public int RecetaNro { get; set; }
        public string Nombre { get; set; }
        public int TipoReceta { get; set; }
        public string Cheff { get; set; }
        public List<DetalleReceta> detalleRecetas { get; set; }
        public Receta()
        {
            detalleRecetas = new List<DetalleReceta>();
        }
        public void AgregaDetalle(DetalleReceta detalle)
        {
            detalleRecetas.Add(detalle);
        }
        public void QuitarDetalle(int i)
        {
            detalleRecetas.RemoveAt(i);
        }
    }
}
