using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shopping.Utility;

namespace Shopping.Data.Entities
{
    public class UserEntity
    {
        public int Id { get; set; }
        public string UserName { get; set; } 
        public string Email { get; set; } 

        public DateTime CreatedDate { get; set; } = DateTime.Now;

        public string Password { get; set; } 
        public RoleType Role { get; set; }

        public virtual ICollection<CartEntity> Carts { get; } = new List<CartEntity>();
        public virtual ICollection<CategoryEntity> Categories { get; } = new List<CategoryEntity>();
        public virtual ICollection<ProductEntity> Products { get; } = new List<ProductEntity>();
        public virtual ICollection<CommentEntity> Comments { get; } = new List<CommentEntity>();
        public virtual ICollection<VoteEntity> Votes { get; } = new List<VoteEntity>();
        public virtual ICollection<OrderEntity> Orders { get; } = new List<OrderEntity>();

    }
}
