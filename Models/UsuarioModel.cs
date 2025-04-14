namespace TicketsWebMVC.Models
{
    public class UsuarioModel
    {
        public int us_identificador { get; set; }
        public string us_nombre_completo { get; set; }
        public string us_correo { get; set; }
        public string us_clave { get; set; }
        public int us_ro_identificador { get; set; }
        public string us_estado { get; set; }
        public DateTime us_fecha_adicion { get; set; }
        public string us_adicionado_por { get; set; }
        public DateTime? us_fecha_modificacion { get; set; }
        public string? us_modificado_por { get; set; }
    }
}
