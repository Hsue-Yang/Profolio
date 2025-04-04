using Microsoft.EntityFrameworkCore;
using Profolio.Server.Models.Entities;

namespace Profolio.Server.Models;

public partial class ProfolioContext : DbContext
{
    public ProfolioContext() { }
    public ProfolioContext(DbContextOptions<ProfolioContext> options) : base(options) { }
    public virtual DbSet<ArticleView> ArticleViews { get; set; }
    public virtual DbSet<HackMDNote> HackMdnotes { get; set; }
    public virtual DbSet<HackMDNoteTag> HackMdnoteTags { get; set; }
    public virtual DbSet<Subscriber> Subscribers { get; set; }
    public virtual DbSet<Tag> Tags { get; set; }
    public virtual DbSet<TagTree> TagTrees { get; set; }
    public virtual DbSet<TechImage> TechImages { get; set; }
    public virtual DbSet<Timeline> Timelines { get; set; }
    public virtual DbSet<UserIntroCard> UserIntroCards { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ArticleView>(entity =>
        {
            entity.ToTable("ArticleView");

            entity.Property(e => e.ID).HasColumnName("ID");
            entity.Property(e => e.NoteID).HasColumnName("NoteID");
            entity.Property(e => e.UserID).HasColumnName("UserID");
            entity.Property(e => e.ViewedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
        });

        modelBuilder.Entity<HackMDNote>(entity =>
        {
            entity.ToTable("HackMDNote");

            entity.Property(e => e.ID).HasColumnName("ID");
            entity.Property(e => e.Author).HasMaxLength(255);
            entity.Property(e => e.BlobUrl).HasMaxLength(500);
            entity.Property(e => e.Content).HasMaxLength(200);
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.NoteID)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("NoteID");
            entity.Property(e => e.PublishLink).HasMaxLength(100);
            entity.Property(e => e.PublishedAt).HasColumnType("datetime");
            entity.Property(e => e.Tags).HasMaxLength(255);
            entity.Property(e => e.Title).HasMaxLength(255);
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
        });

        modelBuilder.Entity<HackMDNoteTag>(entity =>
        {
            entity.HasKey(e => new { e.NoteID, e.TagID });

            entity.ToTable("HackMDNoteTag");
            entity.Property(e => e.NoteID)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("NoteID");
            entity.Property(e => e.TagID).HasColumnName("TagID");
        });

        modelBuilder.Entity<Subscriber>(entity =>
        {
            entity.ToTable("Subscriber");

            entity.Property(e => e.ID).HasColumnName("ID");
            entity.Property(e => e.Email).HasMaxLength(255);
            entity.Property(e => e.IsSubscribed).HasDefaultValue(true);
            entity.Property(e => e.SubscribedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.UnsubscribedAt).HasColumnType("datetime");
        });

        modelBuilder.Entity<Tag>(entity =>
        {
            entity.ToTable("Tag");

            entity.Property(e => e.ID).HasColumnName("ID");
            entity.Property(e => e.Name).HasMaxLength(255);
            entity.Property(e => e.ParentID).HasColumnName("ParentID");
        });

        modelBuilder.Entity<TagTree>(entity =>
        {
            entity.ToTable("TagTree");

            entity.Property(e => e.ID).HasColumnName("ID");
            entity.Property(e => e.TagID).HasColumnName("TagID");
            entity.Property(e => e.Url).HasMaxLength(500);
        });

        modelBuilder.Entity<TechImage>(entity =>
        {
            entity.ToTable("TechImage");

            entity.HasIndex(e => e.TagId, "UQ__TechImag__657CFA4DC7648676").IsUnique();

            entity.Property(e => e.ID).HasColumnName("ID");
            entity.Property(e => e.PhotoUrl).HasMaxLength(500);
            entity.Property(e => e.TagId)
                .HasMaxLength(255)
                .HasColumnName("TagID");
        });

        modelBuilder.Entity<Timeline>(entity =>
        {
            entity.ToTable("Timeline");

            entity.Property(e => e.ID).HasColumnName("ID");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.ImageUrl).HasMaxLength(50);
            entity.Property(e => e.TimePoint).HasColumnType("datetime");
            entity.Property(e => e.Title).HasMaxLength(255);
        });

        modelBuilder.Entity<UserIntroCard>(entity =>
        {
            entity.ToTable("UserIntroCard");

            entity.Property(e => e.ID).HasColumnName("ID");
            entity.Property(e => e.Category).HasMaxLength(100);
            entity.Property(e => e.Description).HasMaxLength(500);
            entity.Property(e => e.IconUrl).HasMaxLength(150);
            entity.Property(e => e.ImageUrl).HasMaxLength(150);
            entity.Property(e => e.Title).HasMaxLength(150);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}