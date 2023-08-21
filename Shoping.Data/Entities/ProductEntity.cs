using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shopping.Data.Entities
{
    public class ProductEntity
    {

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public float Price { get; set; }
        public string FirstImage { get; set; } = "";
        public string SecondImage { get; set; } = "";
        public string ThirdImage { get; set; } = "";
        public string LastImage { get; set; } = "";
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public int CategoryId { get; set; }
        public CategoryEntity Category { get; set; } = null!;

        public int CreatedById { get; set; } 
        public UserEntity CreatedBy { get; set; } = null!;

        public virtual ICollection<CommentEntity> Comments { get; } = new List<CommentEntity>();
        public virtual ICollection<VoteEntity> Votes { get; } = new List<VoteEntity>();
        public virtual ICollection<CartEntity> Carts { get; } = new List<CartEntity>();
        public List<OrderProductEntity> OrderProducts { get; } = new();


    }
}
