using Shopping.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shoping.Model.Models
{
    public class User
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.Now;

        public string Password { get; set; }
        public RoleType Role { get; set; }
        //public virtual ICollection<CategoryEntity> Categories { get; } = new List<CategoryEntity>();
        //public virtual ICollection<ProductEntity> Products { get; } = new List<ProductEntity>();
        //public virtual ICollection<CommentEntity> Comments { get; } = new List<CommentEntity>();
        //public virtual ICollection<VoteEntity> Votes { get; } = new List<VoteEntity>();
        //public virtual ICollection<OrderEntity> Orders { get; } = new List<OrderEntity>();

    }
    public class SimpleUser
    {
        public int Id { get; set; }
        public string UserName { get; set; }
    }
}
