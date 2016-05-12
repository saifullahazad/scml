namespace SCML.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class SCMLModel : DbContext
    {
        public SCMLModel()
            : base("name=SCMLConnectionString")
        {
        }

        public virtual DbSet<Content> Contents { get; set; }
        public virtual DbSet<Type> Types { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Content>()
                .Property(e => e.title)
                .IsUnicode(false);

            modelBuilder.Entity<Content>()
                .Property(e => e.summary)
                .IsUnicode(false);

            modelBuilder.Entity<Content>()
                .Property(e => e.large_image_path)
                .IsUnicode(false);

            modelBuilder.Entity<Content>()
                .Property(e => e.thambnail_image_path)
                .IsUnicode(false);

            modelBuilder.Entity<Content>()
                .Property(e => e.content_file_path)
                .IsUnicode(false);

            modelBuilder.Entity<Content>()
              .Property(e => e.href)
              .IsUnicode(false);

            modelBuilder.Entity<Type>()
                .Property(e => e.name)
                .IsUnicode(false);

            modelBuilder.Entity<Type>()
                .HasMany(e => e.Contents)
                .WithRequired(e => e.Type)
                .HasForeignKey(e => e.type_id)
                .WillCascadeOnDelete(false);
        }
    }
}
