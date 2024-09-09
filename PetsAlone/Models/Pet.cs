using PetsAlone.Models.Enums;
using System.Text.Json.Serialization;

namespace PetsAlone.Models
{
    public class Pet
    {
        public int Id { get; set; }
        public string Name { get; set; }
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public PetType PetType { get; set; }
        public string? Description { get; set; }
        public bool IsMissing { get; set; } = true;
        public DateTime DateMissing { get; set; }
    }
}
