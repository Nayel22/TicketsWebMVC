namespace TicketsWebMVC.Models
{
    public class RolModel
    {
        public int ro_identificador { get; set; }
        public string ro_descripcion { get; set; }
        public DateTime ro_fecha_adicion { get; set; }
        public string ro_adicionado_por { get; set; }
        public DateTime? ro_fecha_modificacion { get; set; }
        public string? ro_modificado_por { get; set; }
    }
}
