using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BookLendApi.Domain.Entities
{
    public class Users
{
    [Key]
    public int UserId { get; set; }
    public string Name { get; set; }
    public string Phone { get; set; }
    public string NameUser { get; set; } // Nombre de usuario para iniciar sesión
    public string Password { get; set; }
    [JsonIgnore]
    public ICollection<Loans> Loans { get; set; } = new List<Loans>(); // Cambio en el nombre de la propiedad
}
}