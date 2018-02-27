using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace BlueMine.Redmine
{
    
    
    public partial class RedmineContext : DbContext
    {
        public virtual DbSet<attachments> attachments { get; set; }
        public virtual DbSet<auth_sources> auth_sources { get; set; }
        public virtual DbSet<boards> boards { get; set; }
        public virtual DbSet<changes> changes { get; set; }
        public virtual DbSet<changesets> changesets { get; set; }
        public virtual DbSet<comments> comments { get; set; }
        public virtual DbSet<custom_field_enumerations> custom_field_enumerations { get; set; }
        public virtual DbSet<custom_fields> custom_fields { get; set; }
        public virtual DbSet<custom_values> custom_values { get; set; }
        public virtual DbSet<documents> documents { get; set; }
        public virtual DbSet<email_addresses> email_addresses { get; set; }
        public virtual DbSet<enabled_modules> enabled_modules { get; set; }
        public virtual DbSet<enumerations> enumerations { get; set; }
        public virtual DbSet<import_items> import_items { get; set; }
        public virtual DbSet<imports> imports { get; set; }
        public virtual DbSet<issue_categories> issue_categories { get; set; }
        public virtual DbSet<issue_relations> issue_relations { get; set; }
        public virtual DbSet<issue_statuses> issue_statuses { get; set; }
        public virtual DbSet<issues> issues { get; set; }
        public virtual DbSet<journal_details> journal_details { get; set; }
        public virtual DbSet<journals> journals { get; set; }
        public virtual DbSet<member_roles> member_roles { get; set; }
        public virtual DbSet<members> members { get; set; }
        public virtual DbSet<messages> messages { get; set; }
        public virtual DbSet<news> news { get; set; }
        public virtual DbSet<open_id_authentication_associations> open_id_authentication_associations { get; set; }
        public virtual DbSet<open_id_authentication_nonces> open_id_authentication_nonces { get; set; }
        public virtual DbSet<projects> projects { get; set; }
        public virtual DbSet<queries> queries { get; set; }
        public virtual DbSet<repositories> repositories { get; set; }
        public virtual DbSet<roles> roles { get; set; }
        public virtual DbSet<settings> settings { get; set; }
        public virtual DbSet<time_entries> time_entries { get; set; }
        public virtual DbSet<tokens> tokens { get; set; }
        public virtual DbSet<trackers> trackers { get; set; }
        public virtual DbSet<user_preferences> user_preferences { get; set; }
        public virtual DbSet<users> users { get; set; }
        public virtual DbSet<versions> versions { get; set; }
        public virtual DbSet<watchers> watchers { get; set; }
        public virtual DbSet<wiki_content_versions> wiki_content_versions { get; set; }
        public virtual DbSet<wiki_contents> wiki_contents { get; set; }
        public virtual DbSet<wiki_pages> wiki_pages { get; set; }
        public virtual DbSet<wiki_redirects> wiki_redirects { get; set; }
        public virtual DbSet<wikis> wikis { get; set; }
        public virtual DbSet<workflows> workflows { get; set; }

        // Unable to generate entity type for table 'dbo.groups_users'. Please see the warning messages.
        // Unable to generate entity type for table 'dbo.changeset_parents'. Please see the warning messages.
        // Unable to generate entity type for table 'dbo.schema_migrations'. Please see the warning messages.
        // Unable to generate entity type for table 'dbo.queries_roles'. Please see the warning messages.
        // Unable to generate entity type for table 'dbo.custom_fields_roles'. Please see the warning messages.
        // Unable to generate entity type for table 'dbo.roles_managed_roles'. Please see the warning messages.
        // Unable to generate entity type for table 'dbo.custom_fields_projects'. Please see the warning messages.
        // Unable to generate entity type for table 'dbo.custom_fields_trackers'. Please see the warning messages.
        // Unable to generate entity type for table 'dbo.issues_history'. Please see the warning messages.
        // Unable to generate entity type for table 'dbo.changesets_issues'. Please see the warning messages.
        // Unable to generate entity type for table 'dbo.projects_trackers'. Please see the warning messages.
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(
                    $@"Server={System.Environment.MachineName}\SQLEXPRESS;Database=Redmine;Integrated Security=true;");
            }
        }
        
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<attachments>(entity =>
            {
                entity.HasIndex(e => e.AuthorId)
                    .HasName("index_attachments_on_author_id");

                entity.HasIndex(e => e.CreatedOn)
                    .HasName("index_attachments_on_created_on");

                entity.HasIndex(e => new { e.ContainerId, e.ContainerType })
                    .HasName("index_attachments_on_container_id_and_container_type");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.AuthorId)
                    .HasColumnName("author_id")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.ContainerId).HasColumnName("container_id");

                entity.Property(e => e.ContainerType)
                    .HasColumnName("container_type")
                    .HasMaxLength(30);

                entity.Property(e => e.ContentType)
                    .HasColumnName("content_type")
                    .HasMaxLength(4000)
                    .HasDefaultValueSql("(N'')");

                entity.Property(e => e.CreatedOn)
                    .HasColumnName("created_on")
                    .HasColumnType("datetime");

                entity.Property(e => e.Description)
                    .HasColumnName("description")
                    .HasMaxLength(4000);

                entity.Property(e => e.Digest)
                    .IsRequired()
                    .HasColumnName("digest")
                    .HasMaxLength(40)
                    .HasDefaultValueSql("(N'')");

                entity.Property(e => e.DiskDirectory)
                    .HasColumnName("disk_directory")
                    .HasMaxLength(4000);

                entity.Property(e => e.DiskFilename)
                    .IsRequired()
                    .HasColumnName("disk_filename")
                    .HasMaxLength(4000)
                    .HasDefaultValueSql("(N'')");

                entity.Property(e => e.Downloads)
                    .HasColumnName("downloads")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Filename)
                    .IsRequired()
                    .HasColumnName("filename")
                    .HasMaxLength(4000)
                    .HasDefaultValueSql("(N'')");

                entity.Property(e => e.Filesize)
                    .HasColumnName("filesize")
                    .HasDefaultValueSql("((0))");
            });

            modelBuilder.Entity<auth_sources>(entity =>
            {
                entity.HasIndex(e => new { e.Id, e.Type })
                    .HasName("index_auth_sources_on_id_and_type");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Account)
                    .HasColumnName("account")
                    .HasMaxLength(4000);

                entity.Property(e => e.AccountPassword)
                    .HasColumnName("account_password")
                    .HasMaxLength(4000)
                    .HasDefaultValueSql("(N'')");

                entity.Property(e => e.AttrFirstname)
                    .HasColumnName("attr_firstname")
                    .HasMaxLength(30);

                entity.Property(e => e.AttrLastname)
                    .HasColumnName("attr_lastname")
                    .HasMaxLength(30);

                entity.Property(e => e.AttrLogin)
                    .HasColumnName("attr_login")
                    .HasMaxLength(30);

                entity.Property(e => e.AttrMail)
                    .HasColumnName("attr_mail")
                    .HasMaxLength(30);

                entity.Property(e => e.BaseDn)
                    .HasColumnName("base_dn")
                    .HasMaxLength(255);

                entity.Property(e => e.Filter).HasColumnName("filter");

                entity.Property(e => e.Host)
                    .HasColumnName("host")
                    .HasMaxLength(60);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(60)
                    .HasDefaultValueSql("(N'')");

                entity.Property(e => e.OntheflyRegister).HasColumnName("onthefly_register");

                entity.Property(e => e.Port).HasColumnName("port");

                entity.Property(e => e.Timeout).HasColumnName("timeout");

                entity.Property(e => e.Tls).HasColumnName("tls");

                entity.Property(e => e.Type)
                    .IsRequired()
                    .HasColumnName("type")
                    .HasMaxLength(30)
                    .HasDefaultValueSql("(N'')");
            });

            modelBuilder.Entity<boards>(entity =>
            {
                entity.HasIndex(e => e.LastMessageId)
                    .HasName("index_boards_on_last_message_id");

                entity.HasIndex(e => e.ProjectId)
                    .HasName("boards_project_id");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Description)
                    .HasColumnName("description")
                    .HasMaxLength(4000);

                entity.Property(e => e.LastMessageId).HasColumnName("last_message_id");

                entity.Property(e => e.MessagesCount)
                    .HasColumnName("messages_count")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(4000)
                    .HasDefaultValueSql("(N'')");

                entity.Property(e => e.ParentId).HasColumnName("parent_id");

                entity.Property(e => e.Position)
                    .HasColumnName("position")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.ProjectId).HasColumnName("project_id");

                entity.Property(e => e.TopicsCount)
                    .HasColumnName("topics_count")
                    .HasDefaultValueSql("((0))");
            });

            modelBuilder.Entity<changes>(entity =>
            {
                entity.HasIndex(e => e.ChangesetId)
                    .HasName("changesets_changeset_id");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Action)
                    .IsRequired()
                    .HasColumnName("action")
                    .HasMaxLength(1)
                    .HasDefaultValueSql("(N'')");

                entity.Property(e => e.Branch)
                    .HasColumnName("branch")
                    .HasMaxLength(4000);

                entity.Property(e => e.ChangesetId).HasColumnName("changeset_id");

                entity.Property(e => e.FromPath).HasColumnName("from_path");

                entity.Property(e => e.FromRevision)
                    .HasColumnName("from_revision")
                    .HasMaxLength(4000);

                entity.Property(e => e.Path)
                    .IsRequired()
                    .HasColumnName("path");

                entity.Property(e => e.Revision)
                    .HasColumnName("revision")
                    .HasMaxLength(4000);
            });

            modelBuilder.Entity<changesets>(entity =>
            {
                entity.HasIndex(e => e.CommittedOn)
                    .HasName("index_changesets_on_committed_on");

                entity.HasIndex(e => e.RepositoryId)
                    .HasName("index_changesets_on_repository_id");

                entity.HasIndex(e => e.UserId)
                    .HasName("index_changesets_on_user_id");

                entity.HasIndex(e => new { e.RepositoryId, e.Revision })
                    .HasName("changesets_repos_rev")
                    .IsUnique();

                entity.HasIndex(e => new { e.RepositoryId, e.Scmid })
                    .HasName("changesets_repos_scmid");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Comments).HasColumnName("comments");

                entity.Property(e => e.CommitDate)
                    .HasColumnName("commit_date")
                    .HasColumnType("date");

                entity.Property(e => e.CommittedOn)
                    .HasColumnName("committed_on")
                    .HasColumnType("datetime");

                entity.Property(e => e.Committer)
                    .HasColumnName("committer")
                    .HasMaxLength(4000);

                entity.Property(e => e.RepositoryId).HasColumnName("repository_id");

                entity.Property(e => e.Revision)
                    .IsRequired()
                    .HasColumnName("revision")
                    .HasMaxLength(4000);

                entity.Property(e => e.Scmid)
                    .HasColumnName("scmid")
                    .HasMaxLength(4000);

                entity.Property(e => e.UserId).HasColumnName("user_id");
            });

            modelBuilder.Entity<comments>(entity =>
            {
                entity.HasIndex(e => e.AuthorId)
                    .HasName("index_comments_on_author_id");

                entity.HasIndex(e => new { e.CommentedId, e.CommentedType })
                    .HasName("index_comments_on_commented_id_and_commented_type");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.AuthorId)
                    .HasColumnName("author_id")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.CommentedId)
                    .HasColumnName("commented_id")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.CommentedType)
                    .IsRequired()
                    .HasColumnName("commented_type")
                    .HasMaxLength(30)
                    .HasDefaultValueSql("(N'')");

                entity.Property(e => e.Comments).HasColumnName("comments");

                entity.Property(e => e.CreatedOn)
                    .HasColumnName("created_on")
                    .HasColumnType("datetime");

                entity.Property(e => e.UpdatedOn)
                    .HasColumnName("updated_on")
                    .HasColumnType("datetime");
            });

            modelBuilder.Entity<custom_field_enumerations>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Active)
                    .HasColumnName("active")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.CustomFieldId).HasColumnName("custom_field_id");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(4000);

                entity.Property(e => e.Position)
                    .HasColumnName("position")
                    .HasDefaultValueSql("((1))");
            });

            modelBuilder.Entity<custom_fields>(entity =>
            {
                entity.HasIndex(e => new { e.Id, e.Type })
                    .HasName("index_custom_fields_on_id_and_type");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.DefaultValue).HasColumnName("default_value");

                entity.Property(e => e.Description).HasColumnName("description");

                entity.Property(e => e.Editable)
                    .HasColumnName("editable")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.FieldFormat)
                    .IsRequired()
                    .HasColumnName("field_format")
                    .HasMaxLength(30)
                    .HasDefaultValueSql("(N'')");

                entity.Property(e => e.FormatStore).HasColumnName("format_store");

                entity.Property(e => e.IsFilter).HasColumnName("is_filter");

                entity.Property(e => e.IsForAll).HasColumnName("is_for_all");

                entity.Property(e => e.IsRequired).HasColumnName("is_required");

                entity.Property(e => e.MaxLength).HasColumnName("max_length");

                entity.Property(e => e.MinLength).HasColumnName("min_length");

                entity.Property(e => e.Multiple).HasColumnName("multiple");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(30)
                    .HasDefaultValueSql("(N'')");

                entity.Property(e => e.Position)
                    .HasColumnName("position")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.PossibleValues).HasColumnName("possible_values");

                entity.Property(e => e.Regexp)
                    .HasColumnName("regexp")
                    .HasMaxLength(4000)
                    .HasDefaultValueSql("(N'')");

                entity.Property(e => e.Searchable).HasColumnName("searchable");

                entity.Property(e => e.Type)
                    .IsRequired()
                    .HasColumnName("type")
                    .HasMaxLength(30)
                    .HasDefaultValueSql("(N'')");

                entity.Property(e => e.Visible)
                    .HasColumnName("visible")
                    .HasDefaultValueSql("((1))");
            });

            modelBuilder.Entity<custom_values>(entity =>
            {
                entity.HasIndex(e => e.CustomFieldId)
                    .HasName("index_custom_values_on_custom_field_id");

                entity.HasIndex(e => new { e.CustomizedType, e.CustomizedId })
                    .HasName("custom_values_customized");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CustomFieldId)
                    .HasColumnName("custom_field_id")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.CustomizedId)
                    .HasColumnName("customized_id")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.CustomizedType)
                    .IsRequired()
                    .HasColumnName("customized_type")
                    .HasMaxLength(30)
                    .HasDefaultValueSql("(N'')");

                entity.Property(e => e.Value).HasColumnName("value");
            });

            modelBuilder.Entity<documents>(entity =>
            {
                entity.HasIndex(e => e.CategoryId)
                    .HasName("index_documents_on_category_id");

                entity.HasIndex(e => e.CreatedOn)
                    .HasName("index_documents_on_created_on");

                entity.HasIndex(e => e.ProjectId)
                    .HasName("documents_project_id");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CategoryId)
                    .HasColumnName("category_id")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.CreatedOn)
                    .HasColumnName("created_on")
                    .HasColumnType("datetime");

                entity.Property(e => e.Description).HasColumnName("description");

                entity.Property(e => e.ProjectId)
                    .HasColumnName("project_id")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasColumnName("title")
                    .HasMaxLength(4000)
                    .HasDefaultValueSql("(N'')");
            });

            modelBuilder.Entity<email_addresses>(entity =>
            {
                entity.HasIndex(e => e.UserId)
                    .HasName("index_email_addresses_on_user_id");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Address)
                    .IsRequired()
                    .HasColumnName("address")
                    .HasMaxLength(4000);

                entity.Property(e => e.CreatedOn)
                    .HasColumnName("created_on")
                    .HasColumnType("datetime");

                entity.Property(e => e.IsDefault).HasColumnName("is_default");

                entity.Property(e => e.Notify)
                    .HasColumnName("notify")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.UpdatedOn)
                    .HasColumnName("updated_on")
                    .HasColumnType("datetime");

                entity.Property(e => e.UserId).HasColumnName("user_id");
            });

            modelBuilder.Entity<enabled_modules>(entity =>
            {
                entity.HasIndex(e => e.ProjectId)
                    .HasName("enabled_modules_project_id");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(4000);

                entity.Property(e => e.ProjectId).HasColumnName("project_id");
            });

            modelBuilder.Entity<enumerations>(entity =>
            {
                entity.HasIndex(e => e.ProjectId)
                    .HasName("index_enumerations_on_project_id");

                entity.HasIndex(e => new { e.Id, e.Type })
                    .HasName("index_enumerations_on_id_and_type");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Active)
                    .HasColumnName("active")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.IsDefault).HasColumnName("is_default");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(30)
                    .HasDefaultValueSql("(N'')");

                entity.Property(e => e.ParentId).HasColumnName("parent_id");

                entity.Property(e => e.Position)
                    .HasColumnName("position")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.PositionName)
                    .HasColumnName("position_name")
                    .HasMaxLength(30);

                entity.Property(e => e.ProjectId).HasColumnName("project_id");

                entity.Property(e => e.Type)
                    .HasColumnName("type")
                    .HasMaxLength(4000);
            });

            modelBuilder.Entity<import_items>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.ImportId).HasColumnName("import_id");

                entity.Property(e => e.Message).HasColumnName("message");

                entity.Property(e => e.ObjId).HasColumnName("obj_id");

                entity.Property(e => e.Position).HasColumnName("position");
            });

            modelBuilder.Entity<imports>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CreatedAt)
                    .HasColumnName("created_at")
                    .HasColumnType("datetime");

                entity.Property(e => e.Filename)
                    .HasColumnName("filename")
                    .HasMaxLength(4000);

                entity.Property(e => e.Finished).HasColumnName("finished");

                entity.Property(e => e.Settings).HasColumnName("settings");

                entity.Property(e => e.TotalItems).HasColumnName("total_items");

                entity.Property(e => e.Type)
                    .HasColumnName("type")
                    .HasMaxLength(4000);

                entity.Property(e => e.UpdatedAt)
                    .HasColumnName("updated_at")
                    .HasColumnType("datetime");

                entity.Property(e => e.UserId).HasColumnName("user_id");
            });

            modelBuilder.Entity<issue_categories>(entity =>
            {
                entity.HasIndex(e => e.AssignedToId)
                    .HasName("index_issue_categories_on_assigned_to_id");

                entity.HasIndex(e => e.ProjectId)
                    .HasName("issue_categories_project_id");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.AssignedToId).HasColumnName("assigned_to_id");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(60)
                    .HasDefaultValueSql("(N'')");

                entity.Property(e => e.ProjectId)
                    .HasColumnName("project_id")
                    .HasDefaultValueSql("((0))");
            });

            modelBuilder.Entity<issue_relations>(entity =>
            {
                entity.HasIndex(e => e.IssueFromId)
                    .HasName("index_issue_relations_on_issue_from_id");

                entity.HasIndex(e => e.IssueToId)
                    .HasName("index_issue_relations_on_issue_to_id");

                entity.HasIndex(e => new { e.IssueFromId, e.IssueToId })
                    .HasName("index_issue_relations_on_issue_from_id_and_issue_to_id")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Delay).HasColumnName("delay");

                entity.Property(e => e.IssueFromId).HasColumnName("issue_from_id");

                entity.Property(e => e.IssueToId).HasColumnName("issue_to_id");

                entity.Property(e => e.RelationType)
                    .IsRequired()
                    .HasColumnName("relation_type")
                    .HasMaxLength(4000)
                    .HasDefaultValueSql("(N'')");
            });

            modelBuilder.Entity<issue_statuses>(entity =>
            {
                entity.HasIndex(e => e.IsClosed)
                    .HasName("index_issue_statuses_on_is_closed");

                entity.HasIndex(e => e.Position)
                    .HasName("index_issue_statuses_on_position");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.DefaultDoneRatio).HasColumnName("default_done_ratio");

                entity.Property(e => e.IsClosed).HasColumnName("is_closed");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(30)
                    .HasDefaultValueSql("(N'')");

                entity.Property(e => e.Position)
                    .HasColumnName("position")
                    .HasDefaultValueSql("((1))");
            });

            modelBuilder.Entity<issues>(entity =>
            {
                entity.HasIndex(e => e.AssignedToId)
                    .HasName("index_issues_on_assigned_to_id");

                entity.HasIndex(e => e.AuthorId)
                    .HasName("index_issues_on_author_id");

                entity.HasIndex(e => e.CategoryId)
                    .HasName("index_issues_on_category_id");

                entity.HasIndex(e => e.CreatedOn)
                    .HasName("index_issues_on_created_on");

                entity.HasIndex(e => e.FixedVersionId)
                    .HasName("index_issues_on_fixed_version_id");

                entity.HasIndex(e => e.PriorityId)
                    .HasName("index_issues_on_priority_id");

                entity.HasIndex(e => e.ProjectId)
                    .HasName("issues_project_id");

                entity.HasIndex(e => e.StatusId)
                    .HasName("index_issues_on_status_id");

                entity.HasIndex(e => e.TrackerId)
                    .HasName("index_issues_on_tracker_id");

                entity.HasIndex(e => new { e.RootId, e.Lft, e.Rgt })
                    .HasName("index_issues_on_root_id_and_lft_and_rgt");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.AssignedToId).HasColumnName("assigned_to_id");

                entity.Property(e => e.AuthorId).HasColumnName("author_id");

                entity.Property(e => e.CategoryId).HasColumnName("category_id");

                entity.Property(e => e.ClosedOn)
                    .HasColumnName("closed_on")
                    .HasColumnType("datetime");

                entity.Property(e => e.CreatedOn)
                    .HasColumnName("created_on")
                    .HasColumnType("datetime");

                entity.Property(e => e.Description).HasColumnName("description");

                entity.Property(e => e.DoneRatio)
                    .HasColumnName("done_ratio")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.DueDate)
                    .HasColumnName("due_date")
                    .HasColumnType("date");

                entity.Property(e => e.EstimatedHours).HasColumnName("estimated_hours");

                entity.Property(e => e.FixedVersionId).HasColumnName("fixed_version_id");

                entity.Property(e => e.IsPrivate).HasColumnName("is_private");

                entity.Property(e => e.Lft).HasColumnName("lft");

                entity.Property(e => e.LockVersion)
                    .HasColumnName("lock_version")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.ParentId).HasColumnName("parent_id");

                entity.Property(e => e.PriorityId).HasColumnName("priority_id");

                entity.Property(e => e.ProjectId).HasColumnName("project_id");

                entity.Property(e => e.Rgt).HasColumnName("rgt");

                entity.Property(e => e.RootId).HasColumnName("root_id");

                entity.Property(e => e.StartDate)
                    .HasColumnName("start_date")
                    .HasColumnType("date");

                entity.Property(e => e.StatusId).HasColumnName("status_id");

                entity.Property(e => e.Subject)
                    .IsRequired()
                    .HasColumnName("subject")
                    .HasMaxLength(4000)
                    .HasDefaultValueSql("(N'')");

                entity.Property(e => e.TrackerId).HasColumnName("tracker_id");

                entity.Property(e => e.UpdatedOn)
                    .HasColumnName("updated_on")
                    .HasColumnType("datetime");
            });

            modelBuilder.Entity<journal_details>(entity =>
            {
                entity.HasIndex(e => e.JournalId)
                    .HasName("journal_details_journal_id");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.JournalId)
                    .HasColumnName("journal_id")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.OldValue).HasColumnName("old_value");

                entity.Property(e => e.PropKey)
                    .IsRequired()
                    .HasColumnName("prop_key")
                    .HasMaxLength(30)
                    .HasDefaultValueSql("(N'')");

                entity.Property(e => e.Property)
                    .IsRequired()
                    .HasColumnName("property")
                    .HasMaxLength(30)
                    .HasDefaultValueSql("(N'')");

                entity.Property(e => e.Value).HasColumnName("value");
            });

            modelBuilder.Entity<journals>(entity =>
            {
                entity.HasIndex(e => e.CreatedOn)
                    .HasName("index_journals_on_created_on");

                entity.HasIndex(e => e.JournalizedId)
                    .HasName("index_journals_on_journalized_id");

                entity.HasIndex(e => e.UserId)
                    .HasName("index_journals_on_user_id");

                entity.HasIndex(e => new { e.JournalizedId, e.JournalizedType })
                    .HasName("journals_journalized_id");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CreatedOn)
                    .HasColumnName("created_on")
                    .HasColumnType("datetime");

                entity.Property(e => e.JournalizedId)
                    .HasColumnName("journalized_id")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.JournalizedType)
                    .IsRequired()
                    .HasColumnName("journalized_type")
                    .HasMaxLength(30)
                    .HasDefaultValueSql("(N'')");

                entity.Property(e => e.Notes).HasColumnName("notes");

                entity.Property(e => e.PrivateNotes).HasColumnName("private_notes");

                entity.Property(e => e.UserId)
                    .HasColumnName("user_id")
                    .HasDefaultValueSql("((0))");
            });

            modelBuilder.Entity<member_roles>(entity =>
            {
                entity.HasIndex(e => e.MemberId)
                    .HasName("index_member_roles_on_member_id");

                entity.HasIndex(e => e.RoleId)
                    .HasName("index_member_roles_on_role_id");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.InheritedFrom).HasColumnName("inherited_from");

                entity.Property(e => e.MemberId).HasColumnName("member_id");

                entity.Property(e => e.RoleId).HasColumnName("role_id");
            });

            modelBuilder.Entity<members>(entity =>
            {
                entity.HasIndex(e => e.ProjectId)
                    .HasName("index_members_on_project_id");

                entity.HasIndex(e => e.UserId)
                    .HasName("index_members_on_user_id");

                entity.HasIndex(e => new { e.UserId, e.ProjectId })
                    .HasName("index_members_on_user_id_and_project_id")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CreatedOn)
                    .HasColumnName("created_on")
                    .HasColumnType("datetime");

                entity.Property(e => e.MailNotification).HasColumnName("mail_notification");

                entity.Property(e => e.ProjectId)
                    .HasColumnName("project_id")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.UserId)
                    .HasColumnName("user_id")
                    .HasDefaultValueSql("((0))");
            });

            modelBuilder.Entity<messages>(entity =>
            {
                entity.HasIndex(e => e.AuthorId)
                    .HasName("index_messages_on_author_id");

                entity.HasIndex(e => e.BoardId)
                    .HasName("messages_board_id");

                entity.HasIndex(e => e.CreatedOn)
                    .HasName("index_messages_on_created_on");

                entity.HasIndex(e => e.LastReplyId)
                    .HasName("index_messages_on_last_reply_id");

                entity.HasIndex(e => e.ParentId)
                    .HasName("messages_parent_id");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.AuthorId).HasColumnName("author_id");

                entity.Property(e => e.BoardId).HasColumnName("board_id");

                entity.Property(e => e.Content).HasColumnName("content");

                entity.Property(e => e.CreatedOn)
                    .HasColumnName("created_on")
                    .HasColumnType("datetime");

                entity.Property(e => e.LastReplyId).HasColumnName("last_reply_id");

                entity.Property(e => e.Locked).HasColumnName("locked");

                entity.Property(e => e.ParentId).HasColumnName("parent_id");

                entity.Property(e => e.RepliesCount)
                    .HasColumnName("replies_count")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Sticky)
                    .HasColumnName("sticky")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Subject)
                    .IsRequired()
                    .HasColumnName("subject")
                    .HasMaxLength(4000)
                    .HasDefaultValueSql("(N'')");

                entity.Property(e => e.UpdatedOn)
                    .HasColumnName("updated_on")
                    .HasColumnType("datetime");
            });

            modelBuilder.Entity<news>(entity =>
            {
                entity.HasIndex(e => e.AuthorId)
                    .HasName("index_news_on_author_id");

                entity.HasIndex(e => e.CreatedOn)
                    .HasName("index_news_on_created_on");

                entity.HasIndex(e => e.ProjectId)
                    .HasName("news_project_id");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.AuthorId)
                    .HasColumnName("author_id")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.CommentsCount)
                    .HasColumnName("comments_count")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.CreatedOn)
                    .HasColumnName("created_on")
                    .HasColumnType("datetime");

                entity.Property(e => e.Description).HasColumnName("description");

                entity.Property(e => e.ProjectId).HasColumnName("project_id");

                entity.Property(e => e.Summary)
                    .HasColumnName("summary")
                    .HasMaxLength(255)
                    .HasDefaultValueSql("(N'')");

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasColumnName("title")
                    .HasMaxLength(60)
                    .HasDefaultValueSql("(N'')");
            });

            modelBuilder.Entity<open_id_authentication_associations>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.AssocType)
                    .HasColumnName("assoc_type")
                    .HasMaxLength(4000);

                entity.Property(e => e.Handle)
                    .HasColumnName("handle")
                    .HasMaxLength(4000);

                entity.Property(e => e.Issued).HasColumnName("issued");

                entity.Property(e => e.Lifetime).HasColumnName("lifetime");

                entity.Property(e => e.Secret).HasColumnName("secret");

                entity.Property(e => e.ServerUrl).HasColumnName("server_url");
            });

            modelBuilder.Entity<open_id_authentication_nonces>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Salt)
                    .IsRequired()
                    .HasColumnName("salt")
                    .HasMaxLength(4000);

                entity.Property(e => e.ServerUrl)
                    .HasColumnName("server_url")
                    .HasMaxLength(4000);

                entity.Property(e => e.Timestamp).HasColumnName("timestamp");
            });

            modelBuilder.Entity<projects>(entity =>
            {
                entity.HasIndex(e => e.Lft)
                    .HasName("index_projects_on_lft");

                entity.HasIndex(e => e.Rgt)
                    .HasName("index_projects_on_rgt");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CreatedOn)
                    .HasColumnName("created_on")
                    .HasColumnType("datetime");

                entity.Property(e => e.DefaultVersionId).HasColumnName("default_version_id");

                entity.Property(e => e.Description).HasColumnName("description");

                entity.Property(e => e.Homepage)
                    .HasColumnName("homepage")
                    .HasMaxLength(4000)
                    .HasDefaultValueSql("(N'')");

                entity.Property(e => e.Identifier)
                    .HasColumnName("identifier")
                    .HasMaxLength(4000);

                entity.Property(e => e.InheritMembers).HasColumnName("inherit_members");

                entity.Property(e => e.IsPublic)
                    .HasColumnName("is_public")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Lft).HasColumnName("lft");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(4000)
                    .HasDefaultValueSql("(N'')");

                entity.Property(e => e.ParentId).HasColumnName("parent_id");

                entity.Property(e => e.Rgt).HasColumnName("rgt");

                entity.Property(e => e.Status)
                    .HasColumnName("status")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.UpdatedOn)
                    .HasColumnName("updated_on")
                    .HasColumnType("datetime");
            });

            modelBuilder.Entity<queries>(entity =>
            {
                entity.HasIndex(e => e.ProjectId)
                    .HasName("index_queries_on_project_id");

                entity.HasIndex(e => e.UserId)
                    .HasName("index_queries_on_user_id");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.ColumnNames).HasColumnName("column_names");

                entity.Property(e => e.Filters).HasColumnName("filters");

                entity.Property(e => e.GroupBy)
                    .HasColumnName("group_by")
                    .HasMaxLength(4000);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(4000)
                    .HasDefaultValueSql("(N'')");

                entity.Property(e => e.Options).HasColumnName("options");

                entity.Property(e => e.ProjectId).HasColumnName("project_id");

                entity.Property(e => e.SortCriteria).HasColumnName("sort_criteria");

                entity.Property(e => e.Type)
                    .HasColumnName("type")
                    .HasMaxLength(4000);

                entity.Property(e => e.UserId)
                    .HasColumnName("user_id")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Visibility)
                    .HasColumnName("visibility")
                    .HasDefaultValueSql("((0))");
            });

            modelBuilder.Entity<repositories>(entity =>
            {
                entity.HasIndex(e => e.ProjectId)
                    .HasName("index_repositories_on_project_id");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CreatedOn)
                    .HasColumnName("created_on")
                    .HasColumnType("datetime");

                entity.Property(e => e.ExtraInfo).HasColumnName("extra_info");

                entity.Property(e => e.Identifier)
                    .HasColumnName("identifier")
                    .HasMaxLength(4000);

                entity.Property(e => e.IsDefault).HasColumnName("is_default");

                entity.Property(e => e.LogEncoding)
                    .HasColumnName("log_encoding")
                    .HasMaxLength(64);

                entity.Property(e => e.Login)
                    .HasColumnName("login")
                    .HasMaxLength(60)
                    .HasDefaultValueSql("(N'')");

                entity.Property(e => e.Password)
                    .HasColumnName("password")
                    .HasMaxLength(4000)
                    .HasDefaultValueSql("(N'')");

                entity.Property(e => e.PathEncoding)
                    .HasColumnName("path_encoding")
                    .HasMaxLength(64);

                entity.Property(e => e.ProjectId)
                    .HasColumnName("project_id")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.RootUrl)
                    .HasColumnName("root_url")
                    .HasMaxLength(255)
                    .HasDefaultValueSql("(N'')");

                entity.Property(e => e.Type)
                    .HasColumnName("type")
                    .HasMaxLength(4000);

                entity.Property(e => e.Url)
                    .IsRequired()
                    .HasColumnName("url")
                    .HasMaxLength(4000)
                    .HasDefaultValueSql("(N'')");
            });

            modelBuilder.Entity<roles>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.AllRolesManaged)
                    .HasColumnName("all_roles_managed")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Assignable)
                    .HasColumnName("assignable")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Builtin)
                    .HasColumnName("builtin")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.IssuesVisibility)
                    .IsRequired()
                    .HasColumnName("issues_visibility")
                    .HasMaxLength(30)
                    .HasDefaultValueSql("(N'default')");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(30)
                    .HasDefaultValueSql("(N'')");

                entity.Property(e => e.Permissions).HasColumnName("permissions");

                entity.Property(e => e.Position)
                    .HasColumnName("position")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.TimeEntriesVisibility)
                    .IsRequired()
                    .HasColumnName("time_entries_visibility")
                    .HasMaxLength(30)
                    .HasDefaultValueSql("(N'all')");

                entity.Property(e => e.UsersVisibility)
                    .IsRequired()
                    .HasColumnName("users_visibility")
                    .HasMaxLength(30)
                    .HasDefaultValueSql("(N'all')");
            });

            modelBuilder.Entity<settings>(entity =>
            {
                entity.HasIndex(e => e.Name)
                    .HasName("index_settings_on_name");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(255)
                    .HasDefaultValueSql("(N'')");

                entity.Property(e => e.UpdatedOn)
                    .HasColumnName("updated_on")
                    .HasColumnType("datetime");

                entity.Property(e => e.Value).HasColumnName("value");
            });

            modelBuilder.Entity<time_entries>(entity =>
            {
                entity.HasIndex(e => e.ActivityId)
                    .HasName("index_time_entries_on_activity_id");

                entity.HasIndex(e => e.CreatedOn)
                    .HasName("index_time_entries_on_created_on");

                entity.HasIndex(e => e.IssueId)
                    .HasName("time_entries_issue_id");

                entity.HasIndex(e => e.ProjectId)
                    .HasName("time_entries_project_id");

                entity.HasIndex(e => e.UserId)
                    .HasName("index_time_entries_on_user_id");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.ActivityId).HasColumnName("activity_id");

                entity.Property(e => e.Comments)
                    .HasColumnName("comments")
                    .HasMaxLength(1024);

                entity.Property(e => e.CreatedOn)
                    .HasColumnName("created_on")
                    .HasColumnType("datetime");

                entity.Property(e => e.Hours).HasColumnName("hours");

                entity.Property(e => e.IssueId).HasColumnName("issue_id");

                entity.Property(e => e.ProjectId).HasColumnName("project_id");

                entity.Property(e => e.SpentOn)
                    .HasColumnName("spent_on")
                    .HasColumnType("date");

                entity.Property(e => e.Tmonth).HasColumnName("tmonth");

                entity.Property(e => e.Tweek).HasColumnName("tweek");

                entity.Property(e => e.Tyear).HasColumnName("tyear");

                entity.Property(e => e.UpdatedOn)
                    .HasColumnName("updated_on")
                    .HasColumnType("datetime");

                entity.Property(e => e.UserId).HasColumnName("user_id");
            });

            modelBuilder.Entity<tokens>(entity =>
            {
                entity.HasIndex(e => e.UserId)
                    .HasName("index_tokens_on_user_id");

                entity.HasIndex(e => e.Value)
                    .HasName("tokens_value")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Action)
                    .IsRequired()
                    .HasColumnName("action")
                    .HasMaxLength(30)
                    .HasDefaultValueSql("(N'')");

                entity.Property(e => e.CreatedOn)
                    .HasColumnName("created_on")
                    .HasColumnType("datetime");

                entity.Property(e => e.UpdatedOn)
                    .HasColumnName("updated_on")
                    .HasColumnType("datetime");

                entity.Property(e => e.UserId)
                    .HasColumnName("user_id")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Value)
                    .IsRequired()
                    .HasColumnName("value")
                    .HasMaxLength(40)
                    .HasDefaultValueSql("(N'')");
            });

            modelBuilder.Entity<trackers>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.DefaultStatusId).HasColumnName("default_status_id");

                entity.Property(e => e.FieldsBits)
                    .HasColumnName("fields_bits")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.IsInChlog).HasColumnName("is_in_chlog");

                entity.Property(e => e.IsInRoadmap)
                    .HasColumnName("is_in_roadmap")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(30)
                    .HasDefaultValueSql("(N'')");

                entity.Property(e => e.Position)
                    .HasColumnName("position")
                    .HasDefaultValueSql("((1))");
            });

            modelBuilder.Entity<user_preferences>(entity =>
            {
                entity.HasIndex(e => e.UserId)
                    .HasName("index_user_preferences_on_user_id");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.HideMail)
                    .HasColumnName("hide_mail")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Others).HasColumnName("others");

                entity.Property(e => e.TimeZone)
                    .HasColumnName("time_zone")
                    .HasMaxLength(4000);

                entity.Property(e => e.UserId)
                    .HasColumnName("user_id")
                    .HasDefaultValueSql("((0))");
            });

            modelBuilder.Entity<users>(entity =>
            {
                entity.HasIndex(e => e.AuthSourceId)
                    .HasName("index_users_on_auth_source_id");

                entity.HasIndex(e => e.Type)
                    .HasName("index_users_on_type");

                entity.HasIndex(e => new { e.Id, e.Type })
                    .HasName("index_users_on_id_and_type");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Admin).HasColumnName("admin");

                entity.Property(e => e.AuthSourceId).HasColumnName("auth_source_id");

                entity.Property(e => e.CreatedOn)
                    .HasColumnName("created_on")
                    .HasColumnType("datetime");

                entity.Property(e => e.Firstname)
                    .IsRequired()
                    .HasColumnName("firstname")
                    .HasMaxLength(30)
                    .HasDefaultValueSql("(N'')");

                entity.Property(e => e.HashedPassword)
                    .IsRequired()
                    .HasColumnName("hashed_password")
                    .HasMaxLength(40)
                    .HasDefaultValueSql("(N'')");

                entity.Property(e => e.IdentityUrl)
                    .HasColumnName("identity_url")
                    .HasMaxLength(4000);

                entity.Property(e => e.Language)
                    .HasColumnName("language")
                    .HasMaxLength(5)
                    .HasDefaultValueSql("(N'')");

                entity.Property(e => e.LastLoginOn)
                    .HasColumnName("last_login_on")
                    .HasColumnType("datetime");

                entity.Property(e => e.Lastname)
                    .IsRequired()
                    .HasColumnName("lastname")
                    .HasMaxLength(255)
                    .HasDefaultValueSql("(N'')");

                entity.Property(e => e.Login)
                    .IsRequired()
                    .HasColumnName("login")
                    .HasMaxLength(4000)
                    .HasDefaultValueSql("(N'')");

                entity.Property(e => e.MailNotification)
                    .IsRequired()
                    .HasColumnName("mail_notification")
                    .HasMaxLength(4000)
                    .HasDefaultValueSql("(N'')");

                entity.Property(e => e.MustChangePasswd).HasColumnName("must_change_passwd");

                entity.Property(e => e.PasswdChangedOn)
                    .HasColumnName("passwd_changed_on")
                    .HasColumnType("datetime");

                entity.Property(e => e.Salt)
                    .HasColumnName("salt")
                    .HasMaxLength(64);

                entity.Property(e => e.Status)
                    .HasColumnName("status")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Type)
                    .HasColumnName("type")
                    .HasMaxLength(4000);

                entity.Property(e => e.UpdatedOn)
                    .HasColumnName("updated_on")
                    .HasColumnType("datetime");
            });

            modelBuilder.Entity<versions>(entity =>
            {
                entity.HasIndex(e => e.ProjectId)
                    .HasName("versions_project_id");

                entity.HasIndex(e => e.Sharing)
                    .HasName("index_versions_on_sharing");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CreatedOn)
                    .HasColumnName("created_on")
                    .HasColumnType("datetime");

                entity.Property(e => e.Description)
                    .HasColumnName("description")
                    .HasMaxLength(4000)
                    .HasDefaultValueSql("(N'')");

                entity.Property(e => e.EffectiveDate)
                    .HasColumnName("effective_date")
                    .HasColumnType("date");

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasMaxLength(4000)
                    .HasDefaultValueSql("(N'')");

                entity.Property(e => e.ProjectId)
                    .HasColumnName("project_id")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Sharing)
                    .IsRequired()
                    .HasColumnName("sharing")
                    .HasMaxLength(4000)
                    .HasDefaultValueSql("(N'none')");

                entity.Property(e => e.Status)
                    .HasColumnName("status")
                    .HasMaxLength(4000)
                    .HasDefaultValueSql("(N'open')");

                entity.Property(e => e.UpdatedOn)
                    .HasColumnName("updated_on")
                    .HasColumnType("datetime");

                entity.Property(e => e.WikiPageTitle)
                    .HasColumnName("wiki_page_title")
                    .HasMaxLength(4000);
            });

            modelBuilder.Entity<watchers>(entity =>
            {
                entity.HasIndex(e => e.UserId)
                    .HasName("index_watchers_on_user_id");

                entity.HasIndex(e => new { e.UserId, e.WatchableType })
                    .HasName("watchers_user_id_type");

                entity.HasIndex(e => new { e.WatchableId, e.WatchableType })
                    .HasName("index_watchers_on_watchable_id_and_watchable_type");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.Property(e => e.WatchableId)
                    .HasColumnName("watchable_id")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.WatchableType)
                    .IsRequired()
                    .HasColumnName("watchable_type")
                    .HasMaxLength(4000)
                    .HasDefaultValueSql("(N'')");
            });

            modelBuilder.Entity<wiki_content_versions>(entity =>
            {
                entity.HasIndex(e => e.UpdatedOn)
                    .HasName("index_wiki_content_versions_on_updated_on");

                entity.HasIndex(e => e.WikiContentId)
                    .HasName("wiki_content_versions_wcid");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.AuthorId).HasColumnName("author_id");

                entity.Property(e => e.Comments)
                    .HasColumnName("comments")
                    .HasMaxLength(1024)
                    .HasDefaultValueSql("(N'')");

                entity.Property(e => e.Compression)
                    .HasColumnName("compression")
                    .HasMaxLength(6)
                    .HasDefaultValueSql("(N'')");

                entity.Property(e => e.Data).HasColumnName("data");

                entity.Property(e => e.PageId).HasColumnName("page_id");

                entity.Property(e => e.UpdatedOn)
                    .HasColumnName("updated_on")
                    .HasColumnType("datetime");

                entity.Property(e => e.Version).HasColumnName("version");

                entity.Property(e => e.WikiContentId).HasColumnName("wiki_content_id");
            });

            modelBuilder.Entity<wiki_contents>(entity =>
            {
                entity.HasIndex(e => e.AuthorId)
                    .HasName("index_wiki_contents_on_author_id");

                entity.HasIndex(e => e.PageId)
                    .HasName("wiki_contents_page_id");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.AuthorId).HasColumnName("author_id");

                entity.Property(e => e.Comments)
                    .HasColumnName("comments")
                    .HasMaxLength(1024)
                    .HasDefaultValueSql("(N'')");

                entity.Property(e => e.PageId).HasColumnName("page_id");

                entity.Property(e => e.Text).HasColumnName("text");

                entity.Property(e => e.UpdatedOn)
                    .HasColumnName("updated_on")
                    .HasColumnType("datetime");

                entity.Property(e => e.Version).HasColumnName("version");
            });

            modelBuilder.Entity<wiki_pages>(entity =>
            {
                entity.HasIndex(e => e.ParentId)
                    .HasName("index_wiki_pages_on_parent_id");

                entity.HasIndex(e => e.WikiId)
                    .HasName("index_wiki_pages_on_wiki_id");

                entity.HasIndex(e => new { e.WikiId, e.Title })
                    .HasName("wiki_pages_wiki_id_title");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CreatedOn)
                    .HasColumnName("created_on")
                    .HasColumnType("datetime");

                entity.Property(e => e.ParentId).HasColumnName("parent_id");

                entity.Property(e => e.Protected).HasColumnName("protected");

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasColumnName("title")
                    .HasMaxLength(255);

                entity.Property(e => e.WikiId).HasColumnName("wiki_id");
            });

            modelBuilder.Entity<wiki_redirects>(entity =>
            {
                entity.HasIndex(e => e.WikiId)
                    .HasName("index_wiki_redirects_on_wiki_id");

                entity.HasIndex(e => new { e.WikiId, e.Title })
                    .HasName("wiki_redirects_wiki_id_title");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CreatedOn)
                    .HasColumnName("created_on")
                    .HasColumnType("datetime");

                entity.Property(e => e.RedirectsTo)
                    .HasColumnName("redirects_to")
                    .HasMaxLength(4000);

                entity.Property(e => e.RedirectsToWikiId).HasColumnName("redirects_to_wiki_id");

                entity.Property(e => e.Title)
                    .HasColumnName("title")
                    .HasMaxLength(4000);

                entity.Property(e => e.WikiId).HasColumnName("wiki_id");
            });

            modelBuilder.Entity<wikis>(entity =>
            {
                entity.HasIndex(e => e.ProjectId)
                    .HasName("wikis_project_id");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.ProjectId).HasColumnName("project_id");

                entity.Property(e => e.StartPage)
                    .IsRequired()
                    .HasColumnName("start_page")
                    .HasMaxLength(255);

                entity.Property(e => e.Status)
                    .HasColumnName("status")
                    .HasDefaultValueSql("((1))");
            });

            modelBuilder.Entity<workflows>(entity =>
            {
                entity.HasIndex(e => e.NewStatusId)
                    .HasName("index_workflows_on_new_status_id");

                entity.HasIndex(e => e.OldStatusId)
                    .HasName("index_workflows_on_old_status_id");

                entity.HasIndex(e => e.RoleId)
                    .HasName("index_workflows_on_role_id");

                entity.HasIndex(e => new { e.RoleId, e.TrackerId, e.OldStatusId })
                    .HasName("wkfs_role_tracker_old_status");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Assignee).HasColumnName("assignee");

                entity.Property(e => e.Author).HasColumnName("author");

                entity.Property(e => e.FieldName)
                    .HasColumnName("field_name")
                    .HasMaxLength(30);

                entity.Property(e => e.NewStatusId)
                    .HasColumnName("new_status_id")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.OldStatusId)
                    .HasColumnName("old_status_id")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.RoleId)
                    .HasColumnName("role_id")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Rule)
                    .HasColumnName("rule")
                    .HasMaxLength(30);

                entity.Property(e => e.TrackerId)
                    .HasColumnName("tracker_id")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Type)
                    .HasColumnName("type")
                    .HasMaxLength(30);
            });
        }
    }
}
