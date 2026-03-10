using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using ZapasPractica.Data;
using ZapasPractica.Models;

namespace ZapasPractica.Repositories
{
    public class RepositoryZapatillas
    {
        private ZapatillasContext context;

        public RepositoryZapatillas(ZapatillasContext context)
        {
            this.context = context;
        }

        public async Task<List<Zapatilla>> GetZapatillasAsync()
        {
            var consulta = from datos in context.Zapatillas
                           select datos;
            return await consulta.ToListAsync();
        }

        public async Task<Zapatilla> FindZapatillaAsync(int idZapa)
        {
            var consulta = from datos in context.Zapatillas
                           where datos.IdProducto == idZapa
                           select datos;
            return await consulta.FirstOrDefaultAsync();
        }

        public async Task<int> GetCountImagenesZapaAsync(int idzapa)
        {
            return await context.Imagenes.Where(z => z.IdProducto == idzapa).CountAsync();
        }

        public async Task<ImagenZapatilla> GetImagenZapaAsync(int idzapa, long? posicion)
        {
            string sql = "SP_IMAGENES_ZAPATILLAS @IDPRODUCTO, @POSICION";
            SqlParameter pamId = new SqlParameter("@IDPRODUCTO", idzapa);
            SqlParameter pamPos = new SqlParameter("@POSICION", posicion);
            var consulta = context.Imagenes.FromSqlRaw(sql, pamId, pamPos);
            return consulta.AsEnumerable().FirstOrDefault();
        }
    }
}
