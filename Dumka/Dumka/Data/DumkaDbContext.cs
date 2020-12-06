using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Dumka
{
    public partial class DumkaDbContext : DbContext
    {
        public DumkaDbContext()
        {
        }

        public DumkaDbContext(DbContextOptions<DumkaDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<CommentLike> CommentLikes { get; set; }
        public virtual DbSet<CommentReport> CommentReports { get; set; }
        public virtual DbSet<Comment> Comments { get; set; }
        public virtual DbSet<FeedbackType> FeedbackTypes { get; set; }
        public virtual DbSet<ProposalDeadlineType> ProposalDeadlineTypes { get; set; }
        public virtual DbSet<ProposalLike> ProposalLikes { get; set; }
        public virtual DbSet<ProposalReport> ProposalReports { get; set; }
        public virtual DbSet<ProposalStageTypes> ProposalStageTypes { get; set; }
        public virtual DbSet<Proposal> Proposals { get; set; }
        public virtual DbSet<ReportCategory> ReportCategories { get; set; }
        public virtual DbSet<School> Schools { get; set; }
        public virtual DbSet<UserType> UserTypes { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Database=DumkaDb;Integrated Security=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.6-servicing-10079");

            modelBuilder.Entity<CommentLike>(entity =>
            {
                entity.ToTable("comment_likes");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CommentId).HasColumnName("comment_id");

                entity.Property(e => e.DateCreated)
                    .HasColumnName("date_created")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.DateModified)
                    .HasColumnName("date_modified")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.FeedbackId).HasColumnName("feedback_id");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.HasOne(d => d.Comment)
                    .WithMany(p => p.CommentLikes)
                    .HasForeignKey(d => d.CommentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("comment_likes_comments_id_fk");

                entity.HasOne(d => d.Feedback)
                    .WithMany(p => p.CommentLikes)
                    .HasForeignKey(d => d.FeedbackId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("comment_likes_feedbacks_id_fk");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.CommentLikes)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("comment_likes_users_id_fk");
            });

            modelBuilder.Entity<CommentReport>(entity =>
            {
                entity.ToTable("comment_reports");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Comment)
                    .HasColumnName("comment")
                    .HasColumnType("text");

                entity.Property(e => e.CommentId).HasColumnName("comment_id");

                entity.Property(e => e.DateCreated)
                    .HasColumnName("date_created")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.DateModified)
                    .HasColumnName("date_modified")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.ReportCategory).HasColumnName("report_category");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.HasOne(d => d.CommentNavigation)
                    .WithMany(p => p.CommentReports)
                    .HasForeignKey(d => d.CommentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("comment_reports_comments_id_fk");

                entity.HasOne(d => d.ReportCategoryNavigation)
                    .WithMany(p => p.CommentReports)
                    .HasForeignKey(d => d.ReportCategory)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("comment_reports_report_categories_id_fk");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.CommentReports)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("comment_reports_users_id_fk");
            });

            modelBuilder.Entity<Comment>(entity =>
            {
                entity.ToTable("comments");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Anonymous).HasColumnName("anonymous");

                entity.Property(e => e.CommentStr)
                    .IsRequired()
                    .HasColumnName("comment")
                    .HasColumnType("text");

                entity.Property(e => e.DateCreated)
                    .HasColumnName("date_created")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.DateModified)
                    .HasColumnName("date_modified")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.ProposalId).HasColumnName("proposal_id");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.HasOne(d => d.Proposal)
                    .WithMany(p => p.Comments)
                    .HasForeignKey(d => d.ProposalId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("comments_proposals_id_fk");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Comments)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("comments_users_id_fk");
            });

            modelBuilder.Entity<FeedbackType>(entity =>
            {
                entity.ToTable("feedback_types");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(7)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<ProposalDeadlineType>(entity =>
            {
                entity.ToTable("proposal_deadline_types");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(5)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<ProposalLike>(entity =>
            {
                entity.ToTable("proposal_likes");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.DateCreated)
                    .HasColumnName("date_created")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.DateModified)
                    .HasColumnName("date_modified")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.FeedbackId).HasColumnName("feedback_id");

                entity.Property(e => e.ProposalId).HasColumnName("proposal_id");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.HasOne(d => d.Feedback)
                    .WithMany(p => p.ProposalLikes)
                    .HasForeignKey(d => d.FeedbackId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("proposal_likes_feedbacks_id_fk");

                entity.HasOne(d => d.Proposal)
                    .WithMany(p => p.ProposalLikes)
                    .HasForeignKey(d => d.ProposalId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("proposal_likes_proposals_id_fk");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.ProposalLikes)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("proposal_likes_users_id_fk");
            });

            modelBuilder.Entity<ProposalReport>(entity =>
            {
                entity.ToTable("proposal_reports");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Comment)
                    .HasColumnName("comment")
                    .HasColumnType("text");

                entity.Property(e => e.DateCreated)
                    .HasColumnName("date_created")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.DateModified)
                    .HasColumnName("date_modified")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.ProposalId).HasColumnName("proposal_id");

                entity.Property(e => e.ReportCategory).HasColumnName("report_category");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.HasOne(d => d.Proposal)
                    .WithMany(p => p.ProposalReports)
                    .HasForeignKey(d => d.ProposalId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("proposal_reports_proposals_id_fk");

                entity.HasOne(d => d.ReportCategoryNavigation)
                    .WithMany(p => p.ProposalReports)
                    .HasForeignKey(d => d.ReportCategory)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("proposal_reports_report_categories_id_fk");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.ProposalReports)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("proposal_reports_users_id_fk");
            });

            modelBuilder.Entity<ProposalStageTypes>(entity =>
            {
                entity.ToTable("proposal_stage_types");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(11)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Proposal>(entity =>
            {
                entity.ToTable("proposals");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Anonymous).HasColumnName("anonymous");

                entity.Property(e => e.DateCreated)
                    .HasColumnName("date_created")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.DateModified)
                    .HasColumnName("date_modified")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.DeadlineId).HasColumnName("deadline_id");

                entity.Property(e => e.StageId)
                    .HasColumnName("stage_id")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Text)
                    .IsRequired()
                    .HasColumnName("text")
                    .HasColumnType("text");

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasColumnName("title")
                    .HasMaxLength(512)
                    .IsUnicode(false);

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.HasOne(d => d.Deadline)
                    .WithMany(p => p.Proposals)
                    .HasForeignKey(d => d.DeadlineId)
                    .HasConstraintName("proposals_deadlines_id_fk");

                entity.HasOne(d => d.Stage)
                    .WithMany(p => p.Proposals)
                    .HasForeignKey(d => d.StageId)
                    .HasConstraintName("proposals_stages_id_fk");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Proposals)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("proposals_users_id_fk");
            });

            modelBuilder.Entity<ReportCategory>(entity =>
            {
                entity.ToTable("report_categories");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.DateCreated)
                    .HasColumnName("date_created")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.DateModified)
                    .HasColumnName("date_modified")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(256)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<School>(entity =>
            {
                entity.ToTable("schools");

                entity.HasIndex(e => e.Name)
                    .HasName("schools_name_uindex")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.DateCreated)
                    .HasColumnName("date_created")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.DateModified)
                    .HasColumnName("date_modified")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Display).HasColumnName("display");

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasMaxLength(256)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<UserType>(entity =>
            {
                entity.ToTable("user_types");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(10)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("users");

                entity.HasIndex(e => e.Nickname)
                    .HasName("users_nickname_uindex")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.BannedTo)
                    .HasColumnName("banned_to")
                    .HasColumnType("datetime");

                entity.Property(e => e.Code)
                    .HasColumnName("code")
                    .HasDefaultValueSql("(floor(rand()*(900000)+(100000)))");

                entity.Property(e => e.CodeGenerated)
                    .HasColumnName("code_generated")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.DateCreated)
                    .HasColumnName("date_created")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.DateModified)
                    .HasColumnName("date_modified")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.Nickname)
                    .IsRequired()
                    .HasColumnName("nickname")
                    .HasMaxLength(32)
                    .IsUnicode(false);

                entity.Property(e => e.SchoolId).HasColumnName("school_id");

                entity.Property(e => e.UserTypeId).HasColumnName("user_type_id");

                entity.Property(e => e.Verified)
                    .HasColumnName("verified")
                    .HasDefaultValueSql("((0))");

                entity.HasOne(d => d.School)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.SchoolId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("users_schools_id_fk");

                entity.HasOne(d => d.UserType)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.UserTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("users_types_id_fk");
            });
        }
    }
}
