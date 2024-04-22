using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BookLendApi.Domain.Entities
{
    public class Books
{
    [Key]
    public int BookId { get; set; }
    public string Title { get; set; }
    public string Author { get; set; }
    public string Gender { get; set; }
     public int Stock { get; set; }
    public int PublicationYear { get; set; }
    [JsonIgnore]
    public bool Lend { get; set; } = false;
}

}