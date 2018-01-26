

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAppPizza.Models
{
    public class Command
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IDCommand { get; set; }        

        public DateTime CommandDate { get; set; }

        public decimal Total { get; set; }

        public ICollection<DetailCommand> DetailCommands { get; set; }

        [NotMapped]
        public string Description { get; set; }

        public Command()
        {
            //Liste ordonnée
            DetailCommands = new HashSet<DetailCommand>();
        }
    }
}