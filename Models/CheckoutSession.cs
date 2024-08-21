using System.ComponentModel.DataAnnotations;

namespace API.Models
{
    public class CreateCheckoutSessionRequest
    {
        [Required]
        public string PriceId {get;set;}
    }

}