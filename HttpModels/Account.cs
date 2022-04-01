using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpModels
{
    public class Account : IEntity
    {
        public Guid Id { get; set; }
        public Guid CartId { get; set; }
        public string Login { get; set; }
        public string PasswordHash { get; set; }
        public string Email { get; set; }
    }
}
