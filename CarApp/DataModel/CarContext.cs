using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;



namespace CarApp.DataModel
{
    class CarContext : DbContext
    {
        public CarContext()
            : base("DbConnection")
        { }

        public DbSet<Inspection> Inspections { get; set; }
        public DbSet<Car> Cars { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<Student>()
        //        .HasOptional<Standard>(s => s.Standard)
        //        .WithMany()
        //        .WillCascadeOnDelete(false);

        //}
        {
            modelBuilder.Entity<Inspection>()
                .HasOptional<Car>(s => s.Car)
                .WithMany()
                .WillCascadeOnDelete(true);

        }

    }
}
