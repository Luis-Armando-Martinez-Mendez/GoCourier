using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoCourier.Api.Dtos
{
    public class EnvioDto
    {
        public int Id { get; set; }
        public int UsuarioId { get; set; }
        public string Direccion { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;
        public string Estado { get; set; } = string.Empty;
        public DateTime Fecha { get; set; } = DateTime.Now;
        public UsuarioDto? Usuario { get; set; }
    }
}

