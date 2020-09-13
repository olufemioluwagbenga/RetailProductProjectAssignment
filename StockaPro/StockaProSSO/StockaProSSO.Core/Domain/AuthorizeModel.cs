using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace StockaProSSO.Core.Domain
{
    public class AuthorizeModel
    {
        public AuthorizeModel()
        {
            Enabled = true;
            CreatedDate = DateTime.Now; // AppSettings.LocalTime;
        }
        public int Id { get; set; }
        [Required]
        public string Userid { get; set; }
        [Required]
        public List<int> ClientKeys { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public bool Enabled { get; set; }
    }

    public class AdminAuthorizeModel
    {
        public int ClearanceId { get; set; }
        public int ClientKey { get; set; }
        public string ClientName { get; set; }
        public string ClientId { get; set; }
        public bool IsEnabled { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? LastUpdate { get; set; }
    }

    public class ToggleAuthorizeModel
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Userid { get; set; }
        [Required]
        public int ClientKey { get; set; }
        public bool Enabled { get; set; }
    }
}
