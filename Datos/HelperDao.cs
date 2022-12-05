using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using Final_Recetas_Prog2.Dominio;

namespace Final_Recetas_Prog2.Datos
{
    public class HelperDao
    {
        private SqlConnection cnn;
        private SqlCommand cmd;
        private static HelperDao instancia;

        public HelperDao()
        {
            cnn = new SqlConnection(@"Data Source=DESKTOP-HJBQUI6\SQLEXPRESS;Initial Catalog=recetas_db;Integrated Security=True");
            cmd = new SqlCommand();
        }
        public static HelperDao ObtenerInstancia() //Singleton
        {
            if(instancia == null)
            {
                instancia = new HelperDao();
            }
            return instancia;
        }
        public int ObtenerProximoID(string nombreSP, string nombreParametro)
        {
            cnn.Open();
            cmd.Connection = cnn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = nombreSP;
            SqlParameter pOut = new SqlParameter();
            pOut.ParameterName = nombreParametro;
            pOut.DbType = DbType.Int32;
            pOut.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(pOut);
            cmd.ExecuteNonQuery();
            cmd.Parameters.Clear();
            cnn.Close();
            return (int)pOut.Value;
        }
        public DataTable cargarCombo(string nombreSP)
        {
            DataTable dt = new DataTable();
            cnn.Open();
            cmd.Connection = cnn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = nombreSP;
            cmd.Parameters.Clear();
            dt.Load(cmd.ExecuteReader());
            cnn.Close();
            return dt;
        }
        public bool GrabarReceta(Receta oReceta, string MaestroSP, string DetalleSP) //primero se inserta el maestro y despues el detalle
        {
            bool ok = false;
            SqlTransaction sqlTransaction = null;
            try
            {
                cnn.Open();
                sqlTransaction = cnn.BeginTransaction();
                SqlCommand cmdMaestro = new SqlCommand(MaestroSP,cnn,sqlTransaction); //Maestro
                cmdMaestro.CommandType = CommandType.StoredProcedure;
                cmdMaestro.Parameters.AddWithValue("@nombre",oReceta.Nombre);
                cmdMaestro.Parameters.AddWithValue("@cheff", oReceta.Cheff);
                cmdMaestro.Parameters.AddWithValue("@tipo_receta", oReceta.TipoReceta);
                SqlParameter pOut = new SqlParameter();
                pOut.ParameterName = "@id";
                pOut.DbType = DbType.Int32;
                pOut.Direction = ParameterDirection.Output;
                cmdMaestro.Parameters.Add(pOut);
                cmdMaestro.ExecuteNonQuery();
                int nroReceta = (int)pOut.Value;
                SqlCommand cmdDetalle = null;
                foreach(DetalleReceta det in oReceta.detalleRecetas) //Detalle
                {
                    cmdDetalle = new SqlCommand(DetalleSP, cnn, sqlTransaction);
                    cmdDetalle.Parameters.AddWithValue("@id_receta", nroReceta);
                    cmdDetalle.Parameters.AddWithValue("@id_ingrediente", det.ingrediente.IngredienteID);
                    cmdDetalle.Parameters.AddWithValue("@cantidad", det.cantidad);
                }
                sqlTransaction.Commit();
                ok = true;
            }
            catch (Exception)
            {
                sqlTransaction.Rollback();
                ok = false;
            }
            finally
            {
                if(cnn.State == ConnectionState.Open)
                {
                    cnn.Close();
                }
            }
            return ok;
        }
    }
}
