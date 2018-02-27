
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace BlueMine.Db
{
    
    
    public partial class BlueMineContext : DbContext
    {
        
        public virtual DbSet<T_attachments> attachments { get; set; } 
        public virtual DbSet<T_auth_sources> auth_sources { get; set; } 
        public virtual DbSet<T_boards> boards { get; set; } 
        public virtual DbSet<T_changes> changes { get; set; } 
        public virtual DbSet<T_changeset_parents> changeset_parents { get; set; } 
        public virtual DbSet<T_changesets> changesets { get; set; } 
        public virtual DbSet<T_changesets_issues> changesets_issues { get; set; } 
        public virtual DbSet<T_comments> comments { get; set; } 
        public virtual DbSet<T_custom_field_enumerations> custom_field_enumerations { get; set; } 
        public virtual DbSet<T_custom_fields> custom_fields { get; set; } 
        public virtual DbSet<T_custom_fields_projects> custom_fields_projects { get; set; } 
        public virtual DbSet<T_custom_fields_roles> custom_fields_roles { get; set; } 
        public virtual DbSet<T_custom_fields_trackers> custom_fields_trackers { get; set; } 
        public virtual DbSet<T_custom_values> custom_values { get; set; } 
        public virtual DbSet<T_documents> documents { get; set; } 
        public virtual DbSet<T_email_addresses> email_addresses { get; set; } 
        public virtual DbSet<T_enabled_modules> enabled_modules { get; set; } 
        public virtual DbSet<T_enumerations> enumerations { get; set; } 
        public virtual DbSet<T_groups_users> groups_users { get; set; } 
        public virtual DbSet<T_import_items> import_items { get; set; } 
        public virtual DbSet<T_imports> imports { get; set; } 
        public virtual DbSet<T_issue_categories> issue_categories { get; set; } 
        public virtual DbSet<T_issue_relations> issue_relations { get; set; } 
        public virtual DbSet<T_issue_statuses> issue_statuses { get; set; } 
        public virtual DbSet<T_issues> issues { get; set; } 
        public virtual DbSet<T_issues_history> issues_history { get; set; } 
        public virtual DbSet<T_journal_details> journal_details { get; set; } 
        public virtual DbSet<T_journals> journals { get; set; } 
        public virtual DbSet<T_member_roles> member_roles { get; set; } 
        public virtual DbSet<T_members> members { get; set; } 
        public virtual DbSet<T_messages> messages { get; set; } 
        public virtual DbSet<T_news> news { get; set; } 
        public virtual DbSet<T_open_id_authentication_associations> open_id_authentication_associations { get; set; } 
        public virtual DbSet<T_open_id_authentication_nonces> open_id_authentication_nonces { get; set; } 
        public virtual DbSet<T_projects> projects { get; set; } 
        public virtual DbSet<T_projects_trackers> projects_trackers { get; set; } 
        public virtual DbSet<T_queries> queries { get; set; } 
        public virtual DbSet<T_queries_roles> queries_roles { get; set; } 
        public virtual DbSet<T_repositories> repositories { get; set; } 
        public virtual DbSet<T_roles> roles { get; set; } 
        public virtual DbSet<T_roles_managed_roles> roles_managed_roles { get; set; } 
        public virtual DbSet<T_schema_migrations> schema_migrations { get; set; } 
        public virtual DbSet<T_settings> settings { get; set; } 
        public virtual DbSet<T_time_entries> time_entries { get; set; } 
        public virtual DbSet<T_tokens> tokens { get; set; } 
        public virtual DbSet<T_trackers> trackers { get; set; } 
        public virtual DbSet<T_user_preferences> user_preferences { get; set; } 
        public virtual DbSet<T_users> users { get; set; } 
        public virtual DbSet<T_versions> versions { get; set; } 
        public virtual DbSet<T_watchers> watchers { get; set; } 
        public virtual DbSet<T_wiki_content_versions> wiki_content_versions { get; set; } 
        public virtual DbSet<T_wiki_contents> wiki_contents { get; set; } 
        public virtual DbSet<T_wiki_pages> wiki_pages { get; set; } 
        public virtual DbSet<T_wiki_redirects> wiki_redirects { get; set; } 
        public virtual DbSet<T_wikis> wikis { get; set; } 
        public virtual DbSet<T_workflows> workflows { get; set; } 



        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(
                    $@"Server ={ System.Environment.MachineName}\SQLEXPRESS; Database = Redmine; Integrated Security = true; ");
            }
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<T_attachments>(entity =>
            {
                 
                    entity.HasKey(e => e.id)
                        .HasName("PK__attachme__3213E83FCFA30206");

                    entity.HasIndex(e => new {  e.container_id , e.container_type } )
                        .HasName("index_attachments_on_container_id_and_container_type");

                    entity.HasIndex(e =>  e.author_id)
                        .HasName("index_attachments_on_author_id");

                    entity.HasIndex(e =>  e.created_on)
                        .HasName("index_attachments_on_created_on");
;


                    entity.Property(e => e.id)
                        .IsRequired()
                        .HasColumnName("id");


                    entity.Property(e => e.container_id)
                        .HasColumnName("container_id");


                    entity.Property(e => e.container_type)
                        .HasColumnName("container_type")
                        .HasMaxLength(30);


                    entity.Property(e => e.filename)
                        .IsRequired()
                        .HasColumnName("filename")
                        .HasMaxLength(4000)
                        .HasDefaultValueSql("(N'')");


                    entity.Property(e => e.disk_filename)
                        .IsRequired()
                        .HasColumnName("disk_filename")
                        .HasMaxLength(4000)
                        .HasDefaultValueSql("(N'')");


                    entity.Property(e => e.filesize)
                        .IsRequired()
                        .HasColumnName("filesize")
                        .HasDefaultValueSql("((0))");


                    entity.Property(e => e.content_type)
                        .HasColumnName("content_type")
                        .HasMaxLength(4000)
                        .HasDefaultValueSql("(N'')");


                    entity.Property(e => e.digest)
                        .IsRequired()
                        .HasColumnName("digest")
                        .HasMaxLength(40)
                        .HasDefaultValueSql("(N'')");


                    entity.Property(e => e.downloads)
                        .IsRequired()
                        .HasColumnName("downloads")
                        .HasDefaultValueSql("((0))");


                    entity.Property(e => e.author_id)
                        .IsRequired()
                        .HasColumnName("author_id")
                        .HasDefaultValueSql("((0))");


                    entity.Property(e => e.created_on)
                        .HasColumnName("created_on")
                        .HasColumnType("datetime");


                    entity.Property(e => e.description)
                        .HasColumnName("description")
                        .HasMaxLength(4000);


                    entity.Property(e => e.disk_directory)
                        .HasColumnName("disk_directory")
                        .HasMaxLength(4000);


            }); 
            modelBuilder.Entity<T_auth_sources>(entity =>
            {
                 
                    entity.HasKey(e => e.id)
                        .HasName("PK__auth_sou__3213E83F4DC0AA6F");

                    entity.HasIndex(e => new {  e.id , e.type } )
                        .HasName("index_auth_sources_on_id_and_type");
;


                    entity.Property(e => e.id)
                        .IsRequired()
                        .HasColumnName("id");


                    entity.Property(e => e.type)
                        .IsRequired()
                        .HasColumnName("type")
                        .HasMaxLength(30)
                        .HasDefaultValueSql("(N'')");


                    entity.Property(e => e.name)
                        .IsRequired()
                        .HasColumnName("name")
                        .HasMaxLength(60)
                        .HasDefaultValueSql("(N'')");


                    entity.Property(e => e.host)
                        .HasColumnName("host")
                        .HasMaxLength(60);


                    entity.Property(e => e.port)
                        .HasColumnName("port");


                    entity.Property(e => e.account)
                        .HasColumnName("account")
                        .HasMaxLength(4000);


                    entity.Property(e => e.account_password)
                        .HasColumnName("account_password")
                        .HasMaxLength(4000)
                        .HasDefaultValueSql("(N'')");


                    entity.Property(e => e.base_dn)
                        .HasColumnName("base_dn")
                        .HasMaxLength(255);


                    entity.Property(e => e.attr_login)
                        .HasColumnName("attr_login")
                        .HasMaxLength(30);


                    entity.Property(e => e.attr_firstname)
                        .HasColumnName("attr_firstname")
                        .HasMaxLength(30);


                    entity.Property(e => e.attr_lastname)
                        .HasColumnName("attr_lastname")
                        .HasMaxLength(30);


                    entity.Property(e => e.attr_mail)
                        .HasColumnName("attr_mail")
                        .HasMaxLength(30);


                    entity.Property(e => e.onthefly_register)
                        .IsRequired()
                        .HasColumnName("onthefly_register")
                        .HasDefaultValueSql("((0))");


                    entity.Property(e => e.tls)
                        .IsRequired()
                        .HasColumnName("tls")
                        .HasDefaultValueSql("((0))");


                    entity.Property(e => e.filter)
                        .HasColumnName("filter");


                    entity.Property(e => e.timeout)
                        .HasColumnName("timeout");


            }); 
            modelBuilder.Entity<T_boards>(entity =>
            {
                 
                    entity.HasKey(e => e.id)
                        .HasName("PK__boards__3213E83F9BF5EC98");

                    entity.HasIndex(e =>  e.project_id)
                        .HasName("boards_project_id");

                    entity.HasIndex(e =>  e.last_message_id)
                        .HasName("index_boards_on_last_message_id");
;


                    entity.Property(e => e.id)
                        .IsRequired()
                        .HasColumnName("id");


                    entity.Property(e => e.project_id)
                        .IsRequired()
                        .HasColumnName("project_id");


                    entity.Property(e => e.name)
                        .IsRequired()
                        .HasColumnName("name")
                        .HasMaxLength(4000)
                        .HasDefaultValueSql("(N'')");


                    entity.Property(e => e.description)
                        .HasColumnName("description")
                        .HasMaxLength(4000);


                    entity.Property(e => e.position)
                        .HasColumnName("position")
                        .HasDefaultValueSql("((1))");


                    entity.Property(e => e.topics_count)
                        .IsRequired()
                        .HasColumnName("topics_count")
                        .HasDefaultValueSql("((0))");


                    entity.Property(e => e.messages_count)
                        .IsRequired()
                        .HasColumnName("messages_count")
                        .HasDefaultValueSql("((0))");


                    entity.Property(e => e.last_message_id)
                        .HasColumnName("last_message_id");


                    entity.Property(e => e.parent_id)
                        .HasColumnName("parent_id");


            }); 
            modelBuilder.Entity<T_changes>(entity =>
            {
                 
                    entity.HasKey(e => e.id)
                        .HasName("PK__changes__3213E83FBA032E05");

                    entity.HasIndex(e =>  e.changeset_id)
                        .HasName("changesets_changeset_id");
;


                    entity.Property(e => e.id)
                        .IsRequired()
                        .HasColumnName("id");


                    entity.Property(e => e.changeset_id)
                        .IsRequired()
                        .HasColumnName("changeset_id");


                    entity.Property(e => e.action)
                        .IsRequired()
                        .HasColumnName("action")
                        .HasMaxLength(1)
                        .HasDefaultValueSql("(N'')");


                    entity.Property(e => e.path)
                        .IsRequired()
                        .HasColumnName("path");


                    entity.Property(e => e.from_path)
                        .HasColumnName("from_path");


                    entity.Property(e => e.from_revision)
                        .HasColumnName("from_revision")
                        .HasMaxLength(4000);


                    entity.Property(e => e.revision)
                        .HasColumnName("revision")
                        .HasMaxLength(4000);


                    entity.Property(e => e.branch)
                        .HasColumnName("branch")
                        .HasMaxLength(4000);


            }); 
            modelBuilder.Entity<T_changeset_parents>(entity =>
            {
                 
                    entity.HasKey(e => new { e.changeset_id, e.parent_id } )
                        .HasName("PK_changeset_parents");

                    entity.HasIndex(e =>  e.changeset_id)
                        .HasName("changeset_parents_changeset_ids");

                    entity.HasIndex(e =>  e.parent_id)
                        .HasName("changeset_parents_parent_ids");
;


                    entity.Property(e => e.changeset_id)
                        .IsRequired()
                        .HasColumnName("changeset_id");


                    entity.Property(e => e.parent_id)
                        .IsRequired()
                        .HasColumnName("parent_id");


            }); 
            modelBuilder.Entity<T_changesets>(entity =>
            {
                 
                    entity.HasKey(e => new { e.id } )
                        .HasName("PK__changese__3213E83FD8574402");

                    entity.HasIndex(e => new {  e.repository_id , e.revision } )
                        .HasName("changesets_repos_rev");

                    entity.HasIndex(e =>  e.user_id)
                        .HasName("index_changesets_on_user_id");

                    entity.HasIndex(e =>  e.repository_id)
                        .HasName("index_changesets_on_repository_id");

                    entity.HasIndex(e =>  e.committed_on)
                        .HasName("index_changesets_on_committed_on");

                    entity.HasIndex(e => new {  e.repository_id , e.scmid } )
                        .HasName("changesets_repos_scmid");
;


                    entity.Property(e => e.id)
                        .IsRequired()
                        .HasColumnName("id");


                    entity.Property(e => e.repository_id)
                        .IsRequired()
                        .HasColumnName("repository_id");


                    entity.Property(e => e.revision)
                        .IsRequired()
                        .HasColumnName("revision")
                        .HasMaxLength(4000);


                    entity.Property(e => e.committer)
                        .HasColumnName("committer")
                        .HasMaxLength(4000);


                    entity.Property(e => e.committed_on)
                        .IsRequired()
                        .HasColumnName("committed_on")
                        .HasColumnType("datetime");


                    entity.Property(e => e.comments)
                        .HasColumnName("comments");


                    entity.Property(e => e.commit_date)
                        .HasColumnName("commit_date")
                        .HasColumnType("date");


                    entity.Property(e => e.scmid)
                        .HasColumnName("scmid")
                        .HasMaxLength(4000);


                    entity.Property(e => e.user_id)
                        .HasColumnName("user_id");


            }); 
            modelBuilder.Entity<T_changesets_issues>(entity =>
            {
                 
                    entity.HasKey(e => new {  e.changeset_id , e.issue_id } )
                        .HasName("changesets_issues_ids");

                    entity.HasIndex(e => new {  e.changeset_id , e.issue_id } )
                        .HasName("changesets_issues_ids");
;


                    entity.Property(e => e.changeset_id)
                        .IsRequired()
                        .HasColumnName("changeset_id");


                    entity.Property(e => e.issue_id)
                        .IsRequired()
                        .HasColumnName("issue_id");


            }); 
            modelBuilder.Entity<T_comments>(entity =>
            {
                 
                    entity.HasKey(e => e.id)
                        .HasName("PK__comments__3213E83FB11E3082");

                    entity.HasIndex(e => new {  e.commented_id , e.commented_type } )
                        .HasName("index_comments_on_commented_id_and_commented_type");

                    entity.HasIndex(e =>  e.author_id)
                        .HasName("index_comments_on_author_id");
;


                    entity.Property(e => e.id)
                        .IsRequired()
                        .HasColumnName("id");


                    entity.Property(e => e.commented_type)
                        .IsRequired()
                        .HasColumnName("commented_type")
                        .HasMaxLength(30)
                        .HasDefaultValueSql("(N'')");


                    entity.Property(e => e.commented_id)
                        .IsRequired()
                        .HasColumnName("commented_id")
                        .HasDefaultValueSql("((0))");


                    entity.Property(e => e.author_id)
                        .IsRequired()
                        .HasColumnName("author_id")
                        .HasDefaultValueSql("((0))");


                    entity.Property(e => e.comments)
                        .HasColumnName("comments");


                    entity.Property(e => e.created_on)
                        .IsRequired()
                        .HasColumnName("created_on")
                        .HasColumnType("datetime");


                    entity.Property(e => e.updated_on)
                        .IsRequired()
                        .HasColumnName("updated_on")
                        .HasColumnType("datetime");


            }); 
            modelBuilder.Entity<T_custom_field_enumerations>(entity =>
            {
                 
                    entity.HasKey(e => e.id)
                        .HasName("PK__custom_f__3213E83FE5070D88");
;


                    entity.Property(e => e.id)
                        .IsRequired()
                        .HasColumnName("id");


                    entity.Property(e => e.custom_field_id)
                        .IsRequired()
                        .HasColumnName("custom_field_id");


                    entity.Property(e => e.name)
                        .IsRequired()
                        .HasColumnName("name")
                        .HasMaxLength(4000);


                    entity.Property(e => e.active)
                        .IsRequired()
                        .HasColumnName("active")
                        .HasDefaultValueSql("((1))");


                    entity.Property(e => e.position)
                        .IsRequired()
                        .HasColumnName("position")
                        .HasDefaultValueSql("((1))");


            }); 
            modelBuilder.Entity<T_custom_fields>(entity =>
            {
                 
                    entity.HasKey(e => e.id)
                        .HasName("PK__custom_f__3213E83FDF06BB30");

                    entity.HasIndex(e => new {  e.id , e.type } )
                        .HasName("index_custom_fields_on_id_and_type");
;


                    entity.Property(e => e.id)
                        .IsRequired()
                        .HasColumnName("id");


                    entity.Property(e => e.type)
                        .IsRequired()
                        .HasColumnName("type")
                        .HasMaxLength(30)
                        .HasDefaultValueSql("(N'')");


                    entity.Property(e => e.name)
                        .IsRequired()
                        .HasColumnName("name")
                        .HasMaxLength(30)
                        .HasDefaultValueSql("(N'')");


                    entity.Property(e => e.field_format)
                        .IsRequired()
                        .HasColumnName("field_format")
                        .HasMaxLength(30)
                        .HasDefaultValueSql("(N'')");


                    entity.Property(e => e.possible_values)
                        .HasColumnName("possible_values");


                    entity.Property(e => e.regexp)
                        .HasColumnName("regexp")
                        .HasMaxLength(4000)
                        .HasDefaultValueSql("(N'')");


                    entity.Property(e => e.min_length)
                        .HasColumnName("min_length");


                    entity.Property(e => e.max_length)
                        .HasColumnName("max_length");


                    entity.Property(e => e.is_required)
                        .IsRequired()
                        .HasColumnName("is_required")
                        .HasDefaultValueSql("((0))");


                    entity.Property(e => e.is_for_all)
                        .IsRequired()
                        .HasColumnName("is_for_all")
                        .HasDefaultValueSql("((0))");


                    entity.Property(e => e.is_filter)
                        .IsRequired()
                        .HasColumnName("is_filter")
                        .HasDefaultValueSql("((0))");


                    entity.Property(e => e.position)
                        .HasColumnName("position")
                        .HasDefaultValueSql("((1))");


                    entity.Property(e => e.searchable)
                        .HasColumnName("searchable")
                        .HasDefaultValueSql("((0))");


                    entity.Property(e => e.default_value)
                        .HasColumnName("default_value");


                    entity.Property(e => e.editable)
                        .HasColumnName("editable")
                        .HasDefaultValueSql("((1))");


                    entity.Property(e => e.visible)
                        .IsRequired()
                        .HasColumnName("visible")
                        .HasDefaultValueSql("((1))");


                    entity.Property(e => e.multiple)
                        .HasColumnName("multiple")
                        .HasDefaultValueSql("((0))");


                    entity.Property(e => e.format_store)
                        .HasColumnName("format_store");


                    entity.Property(e => e.description)
                        .HasColumnName("description");


            }); 
            modelBuilder.Entity<T_custom_fields_projects>(entity =>
            {
                 
                    entity.HasKey(e => new {  e.custom_field_id , e.project_id } )
                        .HasName("index_custom_fields_projects_on_custom_field_id_and_project_id");

                    entity.HasIndex(e => new {  e.custom_field_id , e.project_id } )
                        .HasName("index_custom_fields_projects_on_custom_field_id_and_project_id");
;


                    entity.Property(e => e.custom_field_id)
                        .IsRequired()
                        .HasColumnName("custom_field_id")
                        .HasDefaultValueSql("((0))");


                    entity.Property(e => e.project_id)
                        .IsRequired()
                        .HasColumnName("project_id")
                        .HasDefaultValueSql("((0))");


            }); 
            modelBuilder.Entity<T_custom_fields_roles>(entity =>
            {
                 
                    entity.HasKey(e => new {  e.custom_field_id , e.role_id } )
                        .HasName("custom_fields_roles_ids");

                    entity.HasIndex(e => new {  e.custom_field_id , e.role_id } )
                        .HasName("custom_fields_roles_ids");
;


                    entity.Property(e => e.custom_field_id)
                        .IsRequired()
                        .HasColumnName("custom_field_id");


                    entity.Property(e => e.role_id)
                        .IsRequired()
                        .HasColumnName("role_id");


            }); 
            modelBuilder.Entity<T_custom_fields_trackers>(entity =>
            {
                 
                    entity.HasKey(e => new {  e.custom_field_id , e.tracker_id } )
                        .HasName("index_custom_fields_trackers_on_custom_field_id_and_tracker_id");

                    entity.HasIndex(e => new {  e.custom_field_id , e.tracker_id } )
                        .HasName("index_custom_fields_trackers_on_custom_field_id_and_tracker_id");
;


                    entity.Property(e => e.custom_field_id)
                        .IsRequired()
                        .HasColumnName("custom_field_id")
                        .HasDefaultValueSql("((0))");


                    entity.Property(e => e.tracker_id)
                        .IsRequired()
                        .HasColumnName("tracker_id")
                        .HasDefaultValueSql("((0))");


            }); 
            modelBuilder.Entity<T_custom_values>(entity =>
            {
                 
                    entity.HasKey(e => e.id)
                        .HasName("PK__custom_v__3213E83F383B4A01");

                    entity.HasIndex(e => new {  e.customized_type , e.customized_id } )
                        .HasName("custom_values_customized");

                    entity.HasIndex(e =>  e.custom_field_id)
                        .HasName("index_custom_values_on_custom_field_id");
;


                    entity.Property(e => e.id)
                        .IsRequired()
                        .HasColumnName("id");


                    entity.Property(e => e.customized_type)
                        .IsRequired()
                        .HasColumnName("customized_type")
                        .HasMaxLength(30)
                        .HasDefaultValueSql("(N'')");


                    entity.Property(e => e.customized_id)
                        .IsRequired()
                        .HasColumnName("customized_id")
                        .HasDefaultValueSql("((0))");


                    entity.Property(e => e.custom_field_id)
                        .IsRequired()
                        .HasColumnName("custom_field_id")
                        .HasDefaultValueSql("((0))");


                    entity.Property(e => e.value)
                        .HasColumnName("value");


            }); 
            modelBuilder.Entity<T_documents>(entity =>
            {
                 
                    entity.HasKey(e => e.id)
                        .HasName("PK__document__3213E83F2969C58A");

                    entity.HasIndex(e =>  e.project_id)
                        .HasName("documents_project_id");

                    entity.HasIndex(e =>  e.category_id)
                        .HasName("index_documents_on_category_id");

                    entity.HasIndex(e =>  e.created_on)
                        .HasName("index_documents_on_created_on");
;


                    entity.Property(e => e.id)
                        .IsRequired()
                        .HasColumnName("id");


                    entity.Property(e => e.project_id)
                        .IsRequired()
                        .HasColumnName("project_id")
                        .HasDefaultValueSql("((0))");


                    entity.Property(e => e.category_id)
                        .IsRequired()
                        .HasColumnName("category_id")
                        .HasDefaultValueSql("((0))");


                    entity.Property(e => e.title)
                        .IsRequired()
                        .HasColumnName("title")
                        .HasMaxLength(4000)
                        .HasDefaultValueSql("(N'')");


                    entity.Property(e => e.description)
                        .HasColumnName("description");


                    entity.Property(e => e.created_on)
                        .HasColumnName("created_on")
                        .HasColumnType("datetime");


            }); 
            modelBuilder.Entity<T_email_addresses>(entity =>
            {
                 
                    entity.HasKey(e => e.id)
                        .HasName("PK__email_ad__3213E83F3D822A6B");

                    entity.HasIndex(e =>  e.user_id)
                        .HasName("index_email_addresses_on_user_id");
;


                    entity.Property(e => e.id)
                        .IsRequired()
                        .HasColumnName("id");


                    entity.Property(e => e.user_id)
                        .IsRequired()
                        .HasColumnName("user_id");


                    entity.Property(e => e.address)
                        .IsRequired()
                        .HasColumnName("address")
                        .HasMaxLength(4000);


                    entity.Property(e => e.is_default)
                        .IsRequired()
                        .HasColumnName("is_default")
                        .HasDefaultValueSql("((0))");


                    entity.Property(e => e.notify)
                        .IsRequired()
                        .HasColumnName("notify")
                        .HasDefaultValueSql("((1))");


                    entity.Property(e => e.created_on)
                        .IsRequired()
                        .HasColumnName("created_on")
                        .HasColumnType("datetime");


                    entity.Property(e => e.updated_on)
                        .IsRequired()
                        .HasColumnName("updated_on")
                        .HasColumnType("datetime");


            }); 
            modelBuilder.Entity<T_enabled_modules>(entity =>
            {
                 
                    entity.HasKey(e => e.id)
                        .HasName("PK__enabled___3213E83FB66CA7CA");

                    entity.HasIndex(e =>  e.project_id)
                        .HasName("enabled_modules_project_id");
;


                    entity.Property(e => e.id)
                        .IsRequired()
                        .HasColumnName("id");


                    entity.Property(e => e.project_id)
                        .HasColumnName("project_id");


                    entity.Property(e => e.name)
                        .IsRequired()
                        .HasColumnName("name")
                        .HasMaxLength(4000);


            }); 
            modelBuilder.Entity<T_enumerations>(entity =>
            {
                 
                    entity.HasKey(e => e.id)
                        .HasName("PK__enumerat__3213E83FFD0AA589");

                    entity.HasIndex(e =>  e.project_id)
                        .HasName("index_enumerations_on_project_id");

                    entity.HasIndex(e => new {  e.id , e.type } )
                        .HasName("index_enumerations_on_id_and_type");
;


                    entity.Property(e => e.id)
                        .IsRequired()
                        .HasColumnName("id");


                    entity.Property(e => e.name)
                        .IsRequired()
                        .HasColumnName("name")
                        .HasMaxLength(30)
                        .HasDefaultValueSql("(N'')");


                    entity.Property(e => e.position)
                        .HasColumnName("position")
                        .HasDefaultValueSql("((1))");


                    entity.Property(e => e.is_default)
                        .IsRequired()
                        .HasColumnName("is_default")
                        .HasDefaultValueSql("((0))");


                    entity.Property(e => e.type)
                        .HasColumnName("type")
                        .HasMaxLength(4000);


                    entity.Property(e => e.active)
                        .IsRequired()
                        .HasColumnName("active")
                        .HasDefaultValueSql("((1))");


                    entity.Property(e => e.project_id)
                        .HasColumnName("project_id");


                    entity.Property(e => e.parent_id)
                        .HasColumnName("parent_id");


                    entity.Property(e => e.position_name)
                        .HasColumnName("position_name")
                        .HasMaxLength(30);


            }); 
            modelBuilder.Entity<T_groups_users>(entity =>
            {
                 
                    entity.HasKey(e => new {  e.group_id , e.user_id } )
                        .HasName("groups_users_ids");

                    entity.HasIndex(e => new {  e.group_id , e.user_id } )
                        .HasName("groups_users_ids");
;


                    entity.Property(e => e.group_id)
                        .IsRequired()
                        .HasColumnName("group_id");


                    entity.Property(e => e.user_id)
                        .IsRequired()
                        .HasColumnName("user_id");


            }); 
            modelBuilder.Entity<T_import_items>(entity =>
            {
                 
                    entity.HasKey(e => e.id)
                        .HasName("PK__import_i__3213E83F3F6CADD5");
;


                    entity.Property(e => e.id)
                        .IsRequired()
                        .HasColumnName("id");


                    entity.Property(e => e.import_id)
                        .IsRequired()
                        .HasColumnName("import_id");


                    entity.Property(e => e.position)
                        .IsRequired()
                        .HasColumnName("position");


                    entity.Property(e => e.obj_id)
                        .HasColumnName("obj_id");


                    entity.Property(e => e.message)
                        .HasColumnName("message");


            }); 
            modelBuilder.Entity<T_imports>(entity =>
            {
                 
                    entity.HasKey(e => e.id)
                        .HasName("PK__imports__3213E83F7C2B6290");
;


                    entity.Property(e => e.id)
                        .IsRequired()
                        .HasColumnName("id");


                    entity.Property(e => e.type)
                        .HasColumnName("type")
                        .HasMaxLength(4000);


                    entity.Property(e => e.user_id)
                        .IsRequired()
                        .HasColumnName("user_id");


                    entity.Property(e => e.filename)
                        .HasColumnName("filename")
                        .HasMaxLength(4000);


                    entity.Property(e => e.settings)
                        .HasColumnName("settings");


                    entity.Property(e => e.total_items)
                        .HasColumnName("total_items");


                    entity.Property(e => e.finished)
                        .IsRequired()
                        .HasColumnName("finished")
                        .HasDefaultValueSql("((0))");


                    entity.Property(e => e.created_at)
                        .IsRequired()
                        .HasColumnName("created_at")
                        .HasColumnType("datetime");


                    entity.Property(e => e.updated_at)
                        .IsRequired()
                        .HasColumnName("updated_at")
                        .HasColumnType("datetime");


            }); 
            modelBuilder.Entity<T_issue_categories>(entity =>
            {
                 
                    entity.HasKey(e => e.id)
                        .HasName("PK__issue_ca__3213E83F0676F14B");

                    entity.HasIndex(e =>  e.project_id)
                        .HasName("issue_categories_project_id");

                    entity.HasIndex(e =>  e.assigned_to_id)
                        .HasName("index_issue_categories_on_assigned_to_id");
;


                    entity.Property(e => e.id)
                        .IsRequired()
                        .HasColumnName("id");


                    entity.Property(e => e.project_id)
                        .IsRequired()
                        .HasColumnName("project_id")
                        .HasDefaultValueSql("((0))");


                    entity.Property(e => e.name)
                        .IsRequired()
                        .HasColumnName("name")
                        .HasMaxLength(60)
                        .HasDefaultValueSql("(N'')");


                    entity.Property(e => e.assigned_to_id)
                        .HasColumnName("assigned_to_id");


            }); 
            modelBuilder.Entity<T_issue_relations>(entity =>
            {
                 
                    entity.HasKey(e => new { e.id } )
                        .HasName("PK__issue_re__3213E83FECD88315");

                    entity.HasIndex(e =>  e.issue_from_id)
                        .HasName("index_issue_relations_on_issue_from_id");

                    entity.HasIndex(e =>  e.issue_to_id)
                        .HasName("index_issue_relations_on_issue_to_id");

                    entity.HasIndex(e => new {  e.issue_from_id , e.issue_to_id } )
                        .HasName("index_issue_relations_on_issue_from_id_and_issue_to_id");
;


                    entity.Property(e => e.id)
                        .IsRequired()
                        .HasColumnName("id");


                    entity.Property(e => e.issue_from_id)
                        .IsRequired()
                        .HasColumnName("issue_from_id");


                    entity.Property(e => e.issue_to_id)
                        .IsRequired()
                        .HasColumnName("issue_to_id");


                    entity.Property(e => e.relation_type)
                        .IsRequired()
                        .HasColumnName("relation_type")
                        .HasMaxLength(4000)
                        .HasDefaultValueSql("(N'')");


                    entity.Property(e => e.delay)
                        .HasColumnName("delay");


            }); 
            modelBuilder.Entity<T_issue_statuses>(entity =>
            {
                 
                    entity.HasKey(e => e.id)
                        .HasName("PK__issue_st__3213E83F1223BCAE");

                    entity.HasIndex(e =>  e.position)
                        .HasName("index_issue_statuses_on_position");

                    entity.HasIndex(e =>  e.is_closed)
                        .HasName("index_issue_statuses_on_is_closed");
;


                    entity.Property(e => e.id)
                        .IsRequired()
                        .HasColumnName("id");


                    entity.Property(e => e.name)
                        .IsRequired()
                        .HasColumnName("name")
                        .HasMaxLength(30)
                        .HasDefaultValueSql("(N'')");


                    entity.Property(e => e.is_closed)
                        .IsRequired()
                        .HasColumnName("is_closed")
                        .HasDefaultValueSql("((0))");


                    entity.Property(e => e.position)
                        .HasColumnName("position")
                        .HasDefaultValueSql("((1))");


                    entity.Property(e => e.default_done_ratio)
                        .HasColumnName("default_done_ratio");


            }); 
            modelBuilder.Entity<T_issues>(entity =>
            {
                 
                    entity.HasKey(e => e.id)
                        .HasName("PK__issues__3213E83FA8B08604");

                    entity.HasIndex(e =>  e.project_id)
                        .HasName("issues_project_id");

                    entity.HasIndex(e =>  e.status_id)
                        .HasName("index_issues_on_status_id");

                    entity.HasIndex(e =>  e.category_id)
                        .HasName("index_issues_on_category_id");

                    entity.HasIndex(e =>  e.assigned_to_id)
                        .HasName("index_issues_on_assigned_to_id");

                    entity.HasIndex(e =>  e.fixed_version_id)
                        .HasName("index_issues_on_fixed_version_id");

                    entity.HasIndex(e =>  e.tracker_id)
                        .HasName("index_issues_on_tracker_id");

                    entity.HasIndex(e =>  e.priority_id)
                        .HasName("index_issues_on_priority_id");

                    entity.HasIndex(e =>  e.author_id)
                        .HasName("index_issues_on_author_id");

                    entity.HasIndex(e =>  e.created_on)
                        .HasName("index_issues_on_created_on");

                    entity.HasIndex(e => new {  e.root_id , e.lft , e.rgt } )
                        .HasName("index_issues_on_root_id_and_lft_and_rgt");
;


                    entity.Property(e => e.id)
                        .IsRequired()
                        .HasColumnName("id");


                    entity.Property(e => e.tracker_id)
                        .IsRequired()
                        .HasColumnName("tracker_id");


                    entity.Property(e => e.project_id)
                        .IsRequired()
                        .HasColumnName("project_id");


                    entity.Property(e => e.subject)
                        .IsRequired()
                        .HasColumnName("subject")
                        .HasMaxLength(4000)
                        .HasDefaultValueSql("(N'')");


                    entity.Property(e => e.description)
                        .HasColumnName("description");


                    entity.Property(e => e.due_date)
                        .HasColumnName("due_date")
                        .HasColumnType("date");


                    entity.Property(e => e.category_id)
                        .HasColumnName("category_id");


                    entity.Property(e => e.status_id)
                        .IsRequired()
                        .HasColumnName("status_id");


                    entity.Property(e => e.assigned_to_id)
                        .HasColumnName("assigned_to_id");


                    entity.Property(e => e.priority_id)
                        .IsRequired()
                        .HasColumnName("priority_id");


                    entity.Property(e => e.fixed_version_id)
                        .HasColumnName("fixed_version_id");


                    entity.Property(e => e.author_id)
                        .IsRequired()
                        .HasColumnName("author_id");


                    entity.Property(e => e.lock_version)
                        .IsRequired()
                        .HasColumnName("lock_version")
                        .HasDefaultValueSql("((0))");


                    entity.Property(e => e.created_on)
                        .HasColumnName("created_on")
                        .HasColumnType("datetime");


                    entity.Property(e => e.updated_on)
                        .HasColumnName("updated_on")
                        .HasColumnType("datetime");


                    entity.Property(e => e.start_date)
                        .HasColumnName("start_date")
                        .HasColumnType("date");


                    entity.Property(e => e.done_ratio)
                        .IsRequired()
                        .HasColumnName("done_ratio")
                        .HasDefaultValueSql("((0))");


                    entity.Property(e => e.estimated_hours)
                        .HasColumnName("estimated_hours");


                    entity.Property(e => e.parent_id)
                        .HasColumnName("parent_id");


                    entity.Property(e => e.root_id)
                        .HasColumnName("root_id");


                    entity.Property(e => e.lft)
                        .HasColumnName("lft");


                    entity.Property(e => e.rgt)
                        .HasColumnName("rgt");


                    entity.Property(e => e.is_private)
                        .IsRequired()
                        .HasColumnName("is_private")
                        .HasDefaultValueSql("((0))");


                    entity.Property(e => e.closed_on)
                        .HasColumnName("closed_on")
                        .HasColumnType("datetime");


            }); 
            modelBuilder.Entity<T_issues_history>(entity =>
            {
                 
                    entity.HasKey(e => e.isshist_uid).HasName("PK_issues_history");
;


                    entity.Property(e => e.operation_dbuser)
                        .HasColumnName("operation_dbuser")
                        .HasMaxLength(128);


                    entity.Property(e => e.operation_name)
                        .HasColumnName("operation_name")
                        .HasMaxLength(128);


                    entity.Property(e => e.operation_time)
                        .HasColumnName("operation_time")
                        .HasMaxLength(128);


                    entity.Property(e => e.id)
                        .IsRequired()
                        .HasColumnName("id");


                    entity.Property(e => e.tracker_id)
                        .IsRequired()
                        .HasColumnName("tracker_id");


                    entity.Property(e => e.project_id)
                        .IsRequired()
                        .HasColumnName("project_id");


                    entity.Property(e => e.subject)
                        .IsRequired()
                        .HasColumnName("subject")
                        .HasMaxLength(4000);


                    entity.Property(e => e.description)
                        .HasColumnName("description");


                    entity.Property(e => e.due_date)
                        .HasColumnName("due_date")
                        .HasColumnType("date");


                    entity.Property(e => e.category_id)
                        .HasColumnName("category_id");


                    entity.Property(e => e.status_id)
                        .IsRequired()
                        .HasColumnName("status_id");


                    entity.Property(e => e.assigned_to_id)
                        .HasColumnName("assigned_to_id");


                    entity.Property(e => e.priority_id)
                        .IsRequired()
                        .HasColumnName("priority_id");


                    entity.Property(e => e.fixed_version_id)
                        .HasColumnName("fixed_version_id");


                    entity.Property(e => e.author_id)
                        .IsRequired()
                        .HasColumnName("author_id");


                    entity.Property(e => e.lock_version)
                        .IsRequired()
                        .HasColumnName("lock_version");


                    entity.Property(e => e.created_on)
                        .HasColumnName("created_on")
                        .HasColumnType("datetime");


                    entity.Property(e => e.updated_on)
                        .HasColumnName("updated_on")
                        .HasColumnType("datetime");


                    entity.Property(e => e.start_date)
                        .HasColumnName("start_date")
                        .HasColumnType("date");


                    entity.Property(e => e.done_ratio)
                        .IsRequired()
                        .HasColumnName("done_ratio");


                    entity.Property(e => e.estimated_hours)
                        .HasColumnName("estimated_hours");


                    entity.Property(e => e.parent_id)
                        .HasColumnName("parent_id");


                    entity.Property(e => e.root_id)
                        .HasColumnName("root_id");


                    entity.Property(e => e.lft)
                        .HasColumnName("lft");


                    entity.Property(e => e.rgt)
                        .HasColumnName("rgt");


                    entity.Property(e => e.is_private)
                        .IsRequired()
                        .HasColumnName("is_private");


                    entity.Property(e => e.closed_on)
                        .HasColumnName("closed_on")
                        .HasColumnType("datetime");


                    entity.Property(e => e.isshist_uid)
                        .IsRequired()
                        .HasColumnName("isshist_uid")
                        .HasDefaultValueSql("(newid())");


            }); 
            modelBuilder.Entity<T_journal_details>(entity =>
            {
                 
                    entity.HasKey(e => e.id)
                        .HasName("PK__journal___3213E83F63485CE8");

                    entity.HasIndex(e =>  e.journal_id)
                        .HasName("journal_details_journal_id");
;


                    entity.Property(e => e.id)
                        .IsRequired()
                        .HasColumnName("id");


                    entity.Property(e => e.journal_id)
                        .IsRequired()
                        .HasColumnName("journal_id")
                        .HasDefaultValueSql("((0))");


                    entity.Property(e => e.property)
                        .IsRequired()
                        .HasColumnName("property")
                        .HasMaxLength(30)
                        .HasDefaultValueSql("(N'')");


                    entity.Property(e => e.prop_key)
                        .IsRequired()
                        .HasColumnName("prop_key")
                        .HasMaxLength(30)
                        .HasDefaultValueSql("(N'')");


                    entity.Property(e => e.old_value)
                        .HasColumnName("old_value");


                    entity.Property(e => e.value)
                        .HasColumnName("value");


            }); 
            modelBuilder.Entity<T_journals>(entity =>
            {
                 
                    entity.HasKey(e => e.id)
                        .HasName("PK__journals__3213E83F15FD1E86");

                    entity.HasIndex(e => new {  e.journalized_id , e.journalized_type } )
                        .HasName("journals_journalized_id");

                    entity.HasIndex(e =>  e.user_id)
                        .HasName("index_journals_on_user_id");

                    entity.HasIndex(e =>  e.journalized_id)
                        .HasName("index_journals_on_journalized_id");

                    entity.HasIndex(e =>  e.created_on)
                        .HasName("index_journals_on_created_on");
;


                    entity.Property(e => e.id)
                        .IsRequired()
                        .HasColumnName("id");


                    entity.Property(e => e.journalized_id)
                        .IsRequired()
                        .HasColumnName("journalized_id")
                        .HasDefaultValueSql("((0))");


                    entity.Property(e => e.journalized_type)
                        .IsRequired()
                        .HasColumnName("journalized_type")
                        .HasMaxLength(30)
                        .HasDefaultValueSql("(N'')");


                    entity.Property(e => e.user_id)
                        .IsRequired()
                        .HasColumnName("user_id")
                        .HasDefaultValueSql("((0))");


                    entity.Property(e => e.notes)
                        .HasColumnName("notes");


                    entity.Property(e => e.created_on)
                        .IsRequired()
                        .HasColumnName("created_on")
                        .HasColumnType("datetime");


                    entity.Property(e => e.private_notes)
                        .IsRequired()
                        .HasColumnName("private_notes")
                        .HasDefaultValueSql("((0))");


            }); 
            modelBuilder.Entity<T_member_roles>(entity =>
            {
                 
                    entity.HasKey(e => e.id)
                        .HasName("PK__member_r__3213E83FD2290748");

                    entity.HasIndex(e =>  e.member_id)
                        .HasName("index_member_roles_on_member_id");

                    entity.HasIndex(e =>  e.role_id)
                        .HasName("index_member_roles_on_role_id");
;


                    entity.Property(e => e.id)
                        .IsRequired()
                        .HasColumnName("id");


                    entity.Property(e => e.member_id)
                        .IsRequired()
                        .HasColumnName("member_id");


                    entity.Property(e => e.role_id)
                        .IsRequired()
                        .HasColumnName("role_id");


                    entity.Property(e => e.inherited_from)
                        .HasColumnName("inherited_from");


            }); 
            modelBuilder.Entity<T_members>(entity =>
            {
                 
                    entity.HasKey(e => new { e.id } )
                        .HasName("PK__members__3213E83F5ECA7CDE");

                    entity.HasIndex(e =>  e.user_id)
                        .HasName("index_members_on_user_id");

                    entity.HasIndex(e =>  e.project_id)
                        .HasName("index_members_on_project_id");

                    entity.HasIndex(e => new {  e.user_id , e.project_id } )
                        .HasName("index_members_on_user_id_and_project_id");
;


                    entity.Property(e => e.id)
                        .IsRequired()
                        .HasColumnName("id");


                    entity.Property(e => e.user_id)
                        .IsRequired()
                        .HasColumnName("user_id")
                        .HasDefaultValueSql("((0))");


                    entity.Property(e => e.project_id)
                        .IsRequired()
                        .HasColumnName("project_id")
                        .HasDefaultValueSql("((0))");


                    entity.Property(e => e.created_on)
                        .HasColumnName("created_on")
                        .HasColumnType("datetime");


                    entity.Property(e => e.mail_notification)
                        .IsRequired()
                        .HasColumnName("mail_notification")
                        .HasDefaultValueSql("((0))");


            }); 
            modelBuilder.Entity<T_messages>(entity =>
            {
                 
                    entity.HasKey(e => e.id)
                        .HasName("PK__messages__3213E83F638B7F03");

                    entity.HasIndex(e =>  e.board_id)
                        .HasName("messages_board_id");

                    entity.HasIndex(e =>  e.parent_id)
                        .HasName("messages_parent_id");

                    entity.HasIndex(e =>  e.last_reply_id)
                        .HasName("index_messages_on_last_reply_id");

                    entity.HasIndex(e =>  e.author_id)
                        .HasName("index_messages_on_author_id");

                    entity.HasIndex(e =>  e.created_on)
                        .HasName("index_messages_on_created_on");
;


                    entity.Property(e => e.id)
                        .IsRequired()
                        .HasColumnName("id");


                    entity.Property(e => e.board_id)
                        .IsRequired()
                        .HasColumnName("board_id");


                    entity.Property(e => e.parent_id)
                        .HasColumnName("parent_id");


                    entity.Property(e => e.subject)
                        .IsRequired()
                        .HasColumnName("subject")
                        .HasMaxLength(4000)
                        .HasDefaultValueSql("(N'')");


                    entity.Property(e => e.content)
                        .HasColumnName("content");


                    entity.Property(e => e.author_id)
                        .HasColumnName("author_id");


                    entity.Property(e => e.replies_count)
                        .IsRequired()
                        .HasColumnName("replies_count")
                        .HasDefaultValueSql("((0))");


                    entity.Property(e => e.last_reply_id)
                        .HasColumnName("last_reply_id");


                    entity.Property(e => e.created_on)
                        .IsRequired()
                        .HasColumnName("created_on")
                        .HasColumnType("datetime");


                    entity.Property(e => e.updated_on)
                        .IsRequired()
                        .HasColumnName("updated_on")
                        .HasColumnType("datetime");


                    entity.Property(e => e.locked)
                        .HasColumnName("locked")
                        .HasDefaultValueSql("((0))");


                    entity.Property(e => e.sticky)
                        .HasColumnName("sticky")
                        .HasDefaultValueSql("((0))");


            }); 
            modelBuilder.Entity<T_news>(entity =>
            {
                 
                    entity.HasKey(e => e.id)
                        .HasName("PK__news__3213E83F4548DE55");

                    entity.HasIndex(e =>  e.project_id)
                        .HasName("news_project_id");

                    entity.HasIndex(e =>  e.author_id)
                        .HasName("index_news_on_author_id");

                    entity.HasIndex(e =>  e.created_on)
                        .HasName("index_news_on_created_on");
;


                    entity.Property(e => e.id)
                        .IsRequired()
                        .HasColumnName("id");


                    entity.Property(e => e.project_id)
                        .HasColumnName("project_id");


                    entity.Property(e => e.title)
                        .IsRequired()
                        .HasColumnName("title")
                        .HasMaxLength(60)
                        .HasDefaultValueSql("(N'')");


                    entity.Property(e => e.summary)
                        .HasColumnName("summary")
                        .HasMaxLength(255)
                        .HasDefaultValueSql("(N'')");


                    entity.Property(e => e.description)
                        .HasColumnName("description");


                    entity.Property(e => e.author_id)
                        .IsRequired()
                        .HasColumnName("author_id")
                        .HasDefaultValueSql("((0))");


                    entity.Property(e => e.created_on)
                        .HasColumnName("created_on")
                        .HasColumnType("datetime");


                    entity.Property(e => e.comments_count)
                        .IsRequired()
                        .HasColumnName("comments_count")
                        .HasDefaultValueSql("((0))");


            }); 
            modelBuilder.Entity<T_open_id_authentication_associations>(entity =>
            {
                 
                    entity.HasKey(e => e.id)
                        .HasName("PK__open_id___3213E83FFB9F98B8");
;


                    entity.Property(e => e.id)
                        .IsRequired()
                        .HasColumnName("id");


                    entity.Property(e => e.issued)
                        .HasColumnName("issued");


                    entity.Property(e => e.lifetime)
                        .HasColumnName("lifetime");


                    entity.Property(e => e.handle)
                        .HasColumnName("handle")
                        .HasMaxLength(4000);


                    entity.Property(e => e.assoc_type)
                        .HasColumnName("assoc_type")
                        .HasMaxLength(4000);


                    entity.Property(e => e.server_url)
                        .HasColumnName("server_url");


                    entity.Property(e => e.secret)
                        .HasColumnName("secret");


            }); 
            modelBuilder.Entity<T_open_id_authentication_nonces>(entity =>
            {
                 
                    entity.HasKey(e => e.id)
                        .HasName("PK__open_id___3213E83FAC4484AB");
;


                    entity.Property(e => e.id)
                        .IsRequired()
                        .HasColumnName("id");


                    entity.Property(e => e.timestamp)
                        .IsRequired()
                        .HasColumnName("timestamp");


                    entity.Property(e => e.server_url)
                        .HasColumnName("server_url")
                        .HasMaxLength(4000);


                    entity.Property(e => e.salt)
                        .IsRequired()
                        .HasColumnName("salt")
                        .HasMaxLength(4000);


            }); 
            modelBuilder.Entity<T_projects>(entity =>
            {
                 
                    entity.HasKey(e => e.id)
                        .HasName("PK__projects__3213E83F51C7747A");

                    entity.HasIndex(e =>  e.lft)
                        .HasName("index_projects_on_lft");

                    entity.HasIndex(e =>  e.rgt)
                        .HasName("index_projects_on_rgt");
;


                    entity.Property(e => e.id)
                        .IsRequired()
                        .HasColumnName("id");


                    entity.Property(e => e.name)
                        .IsRequired()
                        .HasColumnName("name")
                        .HasMaxLength(4000)
                        .HasDefaultValueSql("(N'')");


                    entity.Property(e => e.description)
                        .HasColumnName("description");


                    entity.Property(e => e.homepage)
                        .HasColumnName("homepage")
                        .HasMaxLength(4000)
                        .HasDefaultValueSql("(N'')");


                    entity.Property(e => e.is_public)
                        .IsRequired()
                        .HasColumnName("is_public")
                        .HasDefaultValueSql("((1))");


                    entity.Property(e => e.parent_id)
                        .HasColumnName("parent_id");


                    entity.Property(e => e.created_on)
                        .HasColumnName("created_on")
                        .HasColumnType("datetime");


                    entity.Property(e => e.updated_on)
                        .HasColumnName("updated_on")
                        .HasColumnType("datetime");


                    entity.Property(e => e.identifier)
                        .HasColumnName("identifier")
                        .HasMaxLength(4000);


                    entity.Property(e => e.status)
                        .IsRequired()
                        .HasColumnName("status")
                        .HasDefaultValueSql("((1))");


                    entity.Property(e => e.lft)
                        .HasColumnName("lft");


                    entity.Property(e => e.rgt)
                        .HasColumnName("rgt");


                    entity.Property(e => e.inherit_members)
                        .IsRequired()
                        .HasColumnName("inherit_members")
                        .HasDefaultValueSql("((0))");


                    entity.Property(e => e.default_version_id)
                        .HasColumnName("default_version_id");


            }); 
            modelBuilder.Entity<T_projects_trackers>(entity =>
            {
                 
                    entity.HasKey(e => new {  e.project_id , e.tracker_id } )
                        .HasName("projects_trackers_unique");

                    entity.HasIndex(e =>  e.project_id)
                        .HasName("projects_trackers_project_id");

                    entity.HasIndex(e => new {  e.project_id , e.tracker_id } )
                        .HasName("projects_trackers_unique");
;


                    entity.Property(e => e.project_id)
                        .IsRequired()
                        .HasColumnName("project_id")
                        .HasDefaultValueSql("((0))");


                    entity.Property(e => e.tracker_id)
                        .IsRequired()
                        .HasColumnName("tracker_id")
                        .HasDefaultValueSql("((0))");


            }); 
            modelBuilder.Entity<T_queries>(entity =>
            {
                 
                    entity.HasKey(e => e.id)
                        .HasName("PK__queries__3213E83F6FABB92E");

                    entity.HasIndex(e =>  e.project_id)
                        .HasName("index_queries_on_project_id");

                    entity.HasIndex(e =>  e.user_id)
                        .HasName("index_queries_on_user_id");
;


                    entity.Property(e => e.id)
                        .IsRequired()
                        .HasColumnName("id");


                    entity.Property(e => e.project_id)
                        .HasColumnName("project_id");


                    entity.Property(e => e.name)
                        .IsRequired()
                        .HasColumnName("name")
                        .HasMaxLength(4000)
                        .HasDefaultValueSql("(N'')");


                    entity.Property(e => e.filters)
                        .HasColumnName("filters");


                    entity.Property(e => e.user_id)
                        .IsRequired()
                        .HasColumnName("user_id")
                        .HasDefaultValueSql("((0))");


                    entity.Property(e => e.column_names)
                        .HasColumnName("column_names");


                    entity.Property(e => e.sort_criteria)
                        .HasColumnName("sort_criteria");


                    entity.Property(e => e.group_by)
                        .HasColumnName("group_by")
                        .HasMaxLength(4000);


                    entity.Property(e => e.type)
                        .HasColumnName("type")
                        .HasMaxLength(4000);


                    entity.Property(e => e.visibility)
                        .HasColumnName("visibility")
                        .HasDefaultValueSql("((0))");


                    entity.Property(e => e.options)
                        .HasColumnName("options");


            }); 
            modelBuilder.Entity<T_queries_roles>(entity =>
            {
                 
                    entity.HasKey(e => new {  e.query_id , e.role_id } )
                        .HasName("queries_roles_ids");

                    entity.HasIndex(e => new {  e.query_id , e.role_id } )
                        .HasName("queries_roles_ids");
;


                    entity.Property(e => e.query_id)
                        .IsRequired()
                        .HasColumnName("query_id");


                    entity.Property(e => e.role_id)
                        .IsRequired()
                        .HasColumnName("role_id");


            }); 
            modelBuilder.Entity<T_repositories>(entity =>
            {
                 
                    entity.HasKey(e => e.id)
                        .HasName("PK__reposito__3213E83FB65175CE");

                    entity.HasIndex(e =>  e.project_id)
                        .HasName("index_repositories_on_project_id");
;


                    entity.Property(e => e.id)
                        .IsRequired()
                        .HasColumnName("id");


                    entity.Property(e => e.project_id)
                        .IsRequired()
                        .HasColumnName("project_id")
                        .HasDefaultValueSql("((0))");


                    entity.Property(e => e.url)
                        .IsRequired()
                        .HasColumnName("url")
                        .HasMaxLength(4000)
                        .HasDefaultValueSql("(N'')");


                    entity.Property(e => e.login)
                        .HasColumnName("login")
                        .HasMaxLength(60)
                        .HasDefaultValueSql("(N'')");


                    entity.Property(e => e.password)
                        .HasColumnName("password")
                        .HasMaxLength(4000)
                        .HasDefaultValueSql("(N'')");


                    entity.Property(e => e.root_url)
                        .HasColumnName("root_url")
                        .HasMaxLength(255)
                        .HasDefaultValueSql("(N'')");


                    entity.Property(e => e.type)
                        .HasColumnName("type")
                        .HasMaxLength(4000);


                    entity.Property(e => e.path_encoding)
                        .HasColumnName("path_encoding")
                        .HasMaxLength(64);


                    entity.Property(e => e.log_encoding)
                        .HasColumnName("log_encoding")
                        .HasMaxLength(64);


                    entity.Property(e => e.extra_info)
                        .HasColumnName("extra_info");


                    entity.Property(e => e.identifier)
                        .HasColumnName("identifier")
                        .HasMaxLength(4000);


                    entity.Property(e => e.is_default)
                        .HasColumnName("is_default")
                        .HasDefaultValueSql("((0))");


                    entity.Property(e => e.created_on)
                        .HasColumnName("created_on")
                        .HasColumnType("datetime");


            }); 
            modelBuilder.Entity<T_roles>(entity =>
            {
                 
                    entity.HasKey(e => e.id)
                        .HasName("PK__roles__3213E83FC5FCEA16");
;


                    entity.Property(e => e.id)
                        .IsRequired()
                        .HasColumnName("id");


                    entity.Property(e => e.name)
                        .IsRequired()
                        .HasColumnName("name")
                        .HasMaxLength(30)
                        .HasDefaultValueSql("(N'')");


                    entity.Property(e => e.position)
                        .HasColumnName("position")
                        .HasDefaultValueSql("((1))");


                    entity.Property(e => e.assignable)
                        .HasColumnName("assignable")
                        .HasDefaultValueSql("((1))");


                    entity.Property(e => e.builtin)
                        .IsRequired()
                        .HasColumnName("builtin")
                        .HasDefaultValueSql("((0))");


                    entity.Property(e => e.permissions)
                        .HasColumnName("permissions");


                    entity.Property(e => e.issues_visibility)
                        .IsRequired()
                        .HasColumnName("issues_visibility")
                        .HasMaxLength(30)
                        .HasDefaultValueSql("(N'default')");


                    entity.Property(e => e.users_visibility)
                        .IsRequired()
                        .HasColumnName("users_visibility")
                        .HasMaxLength(30)
                        .HasDefaultValueSql("(N'all')");


                    entity.Property(e => e.time_entries_visibility)
                        .IsRequired()
                        .HasColumnName("time_entries_visibility")
                        .HasMaxLength(30)
                        .HasDefaultValueSql("(N'all')");


                    entity.Property(e => e.all_roles_managed)
                        .IsRequired()
                        .HasColumnName("all_roles_managed")
                        .HasDefaultValueSql("((1))");


            }); 
            modelBuilder.Entity<T_roles_managed_roles>(entity =>
            {
                 
                    entity.HasKey(e => new {  e.role_id , e.managed_role_id } )
                        .HasName("index_roles_managed_roles_on_role_id_and_managed_role_id");

                    entity.HasIndex(e => new {  e.role_id , e.managed_role_id } )
                        .HasName("index_roles_managed_roles_on_role_id_and_managed_role_id");
;


                    entity.Property(e => e.role_id)
                        .IsRequired()
                        .HasColumnName("role_id");


                    entity.Property(e => e.managed_role_id)
                        .IsRequired()
                        .HasColumnName("managed_role_id");


            }); 
            modelBuilder.Entity<T_schema_migrations>(entity =>
            {
                 
                    entity.HasKey(e =>  e.version)
                        .HasName("unique_schema_migrations");

                    entity.HasIndex(e =>  e.version)
                        .HasName("unique_schema_migrations");
;


                    entity.Property(e => e.version)
                        .IsRequired()
                        .HasColumnName("version")
                        .HasMaxLength(4000);


            }); 
            modelBuilder.Entity<T_settings>(entity =>
            {
                 
                    entity.HasKey(e => e.id)
                        .HasName("PK__settings__3213E83FEC37B7C3");

                    entity.HasIndex(e =>  e.name)
                        .HasName("index_settings_on_name");
;


                    entity.Property(e => e.id)
                        .IsRequired()
                        .HasColumnName("id");


                    entity.Property(e => e.name)
                        .IsRequired()
                        .HasColumnName("name")
                        .HasMaxLength(255)
                        .HasDefaultValueSql("(N'')");


                    entity.Property(e => e.value)
                        .HasColumnName("value");


                    entity.Property(e => e.updated_on)
                        .HasColumnName("updated_on")
                        .HasColumnType("datetime");


            }); 
            modelBuilder.Entity<T_time_entries>(entity =>
            {
                 
                    entity.HasKey(e => e.id)
                        .HasName("PK__time_ent__3213E83F62AE94BC");

                    entity.HasIndex(e =>  e.project_id)
                        .HasName("time_entries_project_id");

                    entity.HasIndex(e =>  e.issue_id)
                        .HasName("time_entries_issue_id");

                    entity.HasIndex(e =>  e.activity_id)
                        .HasName("index_time_entries_on_activity_id");

                    entity.HasIndex(e =>  e.user_id)
                        .HasName("index_time_entries_on_user_id");

                    entity.HasIndex(e =>  e.created_on)
                        .HasName("index_time_entries_on_created_on");
;


                    entity.Property(e => e.id)
                        .IsRequired()
                        .HasColumnName("id");


                    entity.Property(e => e.project_id)
                        .IsRequired()
                        .HasColumnName("project_id");


                    entity.Property(e => e.user_id)
                        .IsRequired()
                        .HasColumnName("user_id");


                    entity.Property(e => e.issue_id)
                        .HasColumnName("issue_id");


                    entity.Property(e => e.hours)
                        .IsRequired()
                        .HasColumnName("hours");


                    entity.Property(e => e.comments)
                        .HasColumnName("comments")
                        .HasMaxLength(1024);


                    entity.Property(e => e.activity_id)
                        .IsRequired()
                        .HasColumnName("activity_id");


                    entity.Property(e => e.spent_on)
                        .IsRequired()
                        .HasColumnName("spent_on")
                        .HasColumnType("date");


                    entity.Property(e => e.tyear)
                        .IsRequired()
                        .HasColumnName("tyear");


                    entity.Property(e => e.tmonth)
                        .IsRequired()
                        .HasColumnName("tmonth");


                    entity.Property(e => e.tweek)
                        .IsRequired()
                        .HasColumnName("tweek");


                    entity.Property(e => e.created_on)
                        .IsRequired()
                        .HasColumnName("created_on")
                        .HasColumnType("datetime");


                    entity.Property(e => e.updated_on)
                        .IsRequired()
                        .HasColumnName("updated_on")
                        .HasColumnType("datetime");


            }); 
            modelBuilder.Entity<T_tokens>(entity =>
            {
                 
                    entity.HasKey(e => e.id)
                        .HasName("PK__tokens__3213E83F370DD788");

                    entity.HasIndex(e =>  e.user_id)
                        .HasName("index_tokens_on_user_id");

                    entity.HasIndex(e =>  e.value)
                        .HasName("tokens_value");
;


                    entity.Property(e => e.id)
                        .IsRequired()
                        .HasColumnName("id");


                    entity.Property(e => e.user_id)
                        .IsRequired()
                        .HasColumnName("user_id")
                        .HasDefaultValueSql("((0))");


                    entity.Property(e => e.action)
                        .IsRequired()
                        .HasColumnName("action")
                        .HasMaxLength(30)
                        .HasDefaultValueSql("(N'')");


                    entity.Property(e => e.value)
                        .IsRequired()
                        .HasColumnName("value")
                        .HasMaxLength(40)
                        .HasDefaultValueSql("(N'')");


                    entity.Property(e => e.created_on)
                        .IsRequired()
                        .HasColumnName("created_on")
                        .HasColumnType("datetime");


                    entity.Property(e => e.updated_on)
                        .HasColumnName("updated_on")
                        .HasColumnType("datetime");


            }); 
            modelBuilder.Entity<T_trackers>(entity =>
            {
                 
                    entity.HasKey(e => e.id)
                        .HasName("PK__trackers__3213E83F49AA360F");
;


                    entity.Property(e => e.id)
                        .IsRequired()
                        .HasColumnName("id");


                    entity.Property(e => e.name)
                        .IsRequired()
                        .HasColumnName("name")
                        .HasMaxLength(30)
                        .HasDefaultValueSql("(N'')");


                    entity.Property(e => e.is_in_chlog)
                        .IsRequired()
                        .HasColumnName("is_in_chlog")
                        .HasDefaultValueSql("((0))");


                    entity.Property(e => e.position)
                        .HasColumnName("position")
                        .HasDefaultValueSql("((1))");


                    entity.Property(e => e.is_in_roadmap)
                        .IsRequired()
                        .HasColumnName("is_in_roadmap")
                        .HasDefaultValueSql("((1))");


                    entity.Property(e => e.fields_bits)
                        .HasColumnName("fields_bits")
                        .HasDefaultValueSql("((0))");


                    entity.Property(e => e.default_status_id)
                        .HasColumnName("default_status_id");


            }); 
            modelBuilder.Entity<T_user_preferences>(entity =>
            {
                 
                    entity.HasKey(e => e.id)
                        .HasName("PK__user_pre__3213E83F71EB9260");

                    entity.HasIndex(e =>  e.user_id)
                        .HasName("index_user_preferences_on_user_id");
;


                    entity.Property(e => e.id)
                        .IsRequired()
                        .HasColumnName("id");


                    entity.Property(e => e.user_id)
                        .IsRequired()
                        .HasColumnName("user_id")
                        .HasDefaultValueSql("((0))");


                    entity.Property(e => e.others)
                        .HasColumnName("others");


                    entity.Property(e => e.hide_mail)
                        .HasColumnName("hide_mail")
                        .HasDefaultValueSql("((1))");


                    entity.Property(e => e.time_zone)
                        .HasColumnName("time_zone")
                        .HasMaxLength(4000);


            }); 
            modelBuilder.Entity<T_users>(entity =>
            {
                 
                    entity.HasKey(e => e.id)
                        .HasName("PK__users__3213E83F7B8BB651");

                    entity.HasIndex(e => new {  e.id , e.type } )
                        .HasName("index_users_on_id_and_type");

                    entity.HasIndex(e =>  e.auth_source_id)
                        .HasName("index_users_on_auth_source_id");

                    entity.HasIndex(e =>  e.type)
                        .HasName("index_users_on_type");
;


                    entity.Property(e => e.id)
                        .IsRequired()
                        .HasColumnName("id");


                    entity.Property(e => e.login)
                        .IsRequired()
                        .HasColumnName("login")
                        .HasMaxLength(4000)
                        .HasDefaultValueSql("(N'')");


                    entity.Property(e => e.hashed_password)
                        .IsRequired()
                        .HasColumnName("hashed_password")
                        .HasMaxLength(40)
                        .HasDefaultValueSql("(N'')");


                    entity.Property(e => e.firstname)
                        .IsRequired()
                        .HasColumnName("firstname")
                        .HasMaxLength(30)
                        .HasDefaultValueSql("(N'')");


                    entity.Property(e => e.lastname)
                        .IsRequired()
                        .HasColumnName("lastname")
                        .HasMaxLength(255)
                        .HasDefaultValueSql("(N'')");


                    entity.Property(e => e.admin)
                        .IsRequired()
                        .HasColumnName("admin")
                        .HasDefaultValueSql("((0))");


                    entity.Property(e => e.status)
                        .IsRequired()
                        .HasColumnName("status")
                        .HasDefaultValueSql("((1))");


                    entity.Property(e => e.last_login_on)
                        .HasColumnName("last_login_on")
                        .HasColumnType("datetime");


                    entity.Property(e => e.language)
                        .HasColumnName("language")
                        .HasMaxLength(5)
                        .HasDefaultValueSql("(N'')");


                    entity.Property(e => e.auth_source_id)
                        .HasColumnName("auth_source_id");


                    entity.Property(e => e.created_on)
                        .HasColumnName("created_on")
                        .HasColumnType("datetime");


                    entity.Property(e => e.updated_on)
                        .HasColumnName("updated_on")
                        .HasColumnType("datetime");


                    entity.Property(e => e.type)
                        .HasColumnName("type")
                        .HasMaxLength(4000);


                    entity.Property(e => e.identity_url)
                        .HasColumnName("identity_url")
                        .HasMaxLength(4000);


                    entity.Property(e => e.mail_notification)
                        .IsRequired()
                        .HasColumnName("mail_notification")
                        .HasMaxLength(4000)
                        .HasDefaultValueSql("(N'')");


                    entity.Property(e => e.salt)
                        .HasColumnName("salt")
                        .HasMaxLength(64);


                    entity.Property(e => e.must_change_passwd)
                        .IsRequired()
                        .HasColumnName("must_change_passwd")
                        .HasDefaultValueSql("((0))");


                    entity.Property(e => e.passwd_changed_on)
                        .HasColumnName("passwd_changed_on")
                        .HasColumnType("datetime");


            }); 
            modelBuilder.Entity<T_versions>(entity =>
            {
                 
                    entity.HasKey(e => e.id)
                        .HasName("PK__versions__3213E83F76446B86");

                    entity.HasIndex(e =>  e.project_id)
                        .HasName("versions_project_id");

                    entity.HasIndex(e =>  e.sharing)
                        .HasName("index_versions_on_sharing");
;


                    entity.Property(e => e.id)
                        .IsRequired()
                        .HasColumnName("id");


                    entity.Property(e => e.project_id)
                        .IsRequired()
                        .HasColumnName("project_id")
                        .HasDefaultValueSql("((0))");


                    entity.Property(e => e.name)
                        .HasColumnName("name")
                        .HasMaxLength(4000)
                        .HasDefaultValueSql("(N'')");


                    entity.Property(e => e.description)
                        .HasColumnName("description")
                        .HasMaxLength(4000)
                        .HasDefaultValueSql("(N'')");


                    entity.Property(e => e.effective_date)
                        .HasColumnName("effective_date")
                        .HasColumnType("date");


                    entity.Property(e => e.created_on)
                        .HasColumnName("created_on")
                        .HasColumnType("datetime");


                    entity.Property(e => e.updated_on)
                        .HasColumnName("updated_on")
                        .HasColumnType("datetime");


                    entity.Property(e => e.wiki_page_title)
                        .HasColumnName("wiki_page_title")
                        .HasMaxLength(4000);


                    entity.Property(e => e.status)
                        .HasColumnName("status")
                        .HasMaxLength(4000)
                        .HasDefaultValueSql("(N'open')");


                    entity.Property(e => e.sharing)
                        .IsRequired()
                        .HasColumnName("sharing")
                        .HasMaxLength(4000)
                        .HasDefaultValueSql("(N'none')");


            }); 
            modelBuilder.Entity<T_watchers>(entity =>
            {
                 
                    entity.HasKey(e => e.id)
                        .HasName("PK__watchers__3213E83F5826DD60");

                    entity.HasIndex(e => new {  e.user_id , e.watchable_type } )
                        .HasName("watchers_user_id_type");

                    entity.HasIndex(e =>  e.user_id)
                        .HasName("index_watchers_on_user_id");

                    entity.HasIndex(e => new {  e.watchable_id , e.watchable_type } )
                        .HasName("index_watchers_on_watchable_id_and_watchable_type");
;


                    entity.Property(e => e.id)
                        .IsRequired()
                        .HasColumnName("id");


                    entity.Property(e => e.watchable_type)
                        .IsRequired()
                        .HasColumnName("watchable_type")
                        .HasMaxLength(4000)
                        .HasDefaultValueSql("(N'')");


                    entity.Property(e => e.watchable_id)
                        .IsRequired()
                        .HasColumnName("watchable_id")
                        .HasDefaultValueSql("((0))");


                    entity.Property(e => e.user_id)
                        .HasColumnName("user_id");


            }); 
            modelBuilder.Entity<T_wiki_content_versions>(entity =>
            {
                 
                    entity.HasKey(e => e.id)
                        .HasName("PK__wiki_con__3213E83FC48DFA0C");

                    entity.HasIndex(e =>  e.wiki_content_id)
                        .HasName("wiki_content_versions_wcid");

                    entity.HasIndex(e =>  e.updated_on)
                        .HasName("index_wiki_content_versions_on_updated_on");
;


                    entity.Property(e => e.id)
                        .IsRequired()
                        .HasColumnName("id");


                    entity.Property(e => e.wiki_content_id)
                        .IsRequired()
                        .HasColumnName("wiki_content_id");


                    entity.Property(e => e.page_id)
                        .IsRequired()
                        .HasColumnName("page_id");


                    entity.Property(e => e.author_id)
                        .HasColumnName("author_id");


                    entity.Property(e => e.data)
                        .HasColumnName("data");


                    entity.Property(e => e.compression)
                        .HasColumnName("compression")
                        .HasMaxLength(6)
                        .HasDefaultValueSql("(N'')");


                    entity.Property(e => e.comments)
                        .HasColumnName("comments")
                        .HasMaxLength(1024)
                        .HasDefaultValueSql("(N'')");


                    entity.Property(e => e.updated_on)
                        .IsRequired()
                        .HasColumnName("updated_on")
                        .HasColumnType("datetime");


                    entity.Property(e => e.version)
                        .IsRequired()
                        .HasColumnName("version");


            }); 
            modelBuilder.Entity<T_wiki_contents>(entity =>
            {
                 
                    entity.HasKey(e => e.id)
                        .HasName("PK__wiki_con__3213E83F093926D5");

                    entity.HasIndex(e =>  e.page_id)
                        .HasName("wiki_contents_page_id");

                    entity.HasIndex(e =>  e.author_id)
                        .HasName("index_wiki_contents_on_author_id");
;


                    entity.Property(e => e.id)
                        .IsRequired()
                        .HasColumnName("id");


                    entity.Property(e => e.page_id)
                        .IsRequired()
                        .HasColumnName("page_id");


                    entity.Property(e => e.author_id)
                        .HasColumnName("author_id");


                    entity.Property(e => e.text)
                        .HasColumnName("text");


                    entity.Property(e => e.comments)
                        .HasColumnName("comments")
                        .HasMaxLength(1024)
                        .HasDefaultValueSql("(N'')");


                    entity.Property(e => e.updated_on)
                        .IsRequired()
                        .HasColumnName("updated_on")
                        .HasColumnType("datetime");


                    entity.Property(e => e.version)
                        .IsRequired()
                        .HasColumnName("version");


            }); 
            modelBuilder.Entity<T_wiki_pages>(entity =>
            {
                 
                    entity.HasKey(e => e.id)
                        .HasName("PK__wiki_pag__3213E83FD2C900EF");

                    entity.HasIndex(e => new {  e.wiki_id , e.title } )
                        .HasName("wiki_pages_wiki_id_title");

                    entity.HasIndex(e =>  e.wiki_id)
                        .HasName("index_wiki_pages_on_wiki_id");

                    entity.HasIndex(e =>  e.parent_id)
                        .HasName("index_wiki_pages_on_parent_id");
;


                    entity.Property(e => e.id)
                        .IsRequired()
                        .HasColumnName("id");


                    entity.Property(e => e.wiki_id)
                        .IsRequired()
                        .HasColumnName("wiki_id");


                    entity.Property(e => e.title)
                        .IsRequired()
                        .HasColumnName("title")
                        .HasMaxLength(255);


                    entity.Property(e => e.created_on)
                        .IsRequired()
                        .HasColumnName("created_on")
                        .HasColumnType("datetime");


                    entity.Property(e => e.@protected)
                        .IsRequired()
                        .HasColumnName("protected")
                        .HasDefaultValueSql("((0))");


                    entity.Property(e => e.parent_id)
                        .HasColumnName("parent_id");


            }); 
            modelBuilder.Entity<T_wiki_redirects>(entity =>
            {
                 
                    entity.HasKey(e => e.id)
                        .HasName("PK__wiki_red__3213E83F4606176D");

                    entity.HasIndex(e => new {  e.wiki_id , e.title } )
                        .HasName("wiki_redirects_wiki_id_title");

                    entity.HasIndex(e =>  e.wiki_id)
                        .HasName("index_wiki_redirects_on_wiki_id");
;


                    entity.Property(e => e.id)
                        .IsRequired()
                        .HasColumnName("id");


                    entity.Property(e => e.wiki_id)
                        .IsRequired()
                        .HasColumnName("wiki_id");


                    entity.Property(e => e.title)
                        .HasColumnName("title")
                        .HasMaxLength(4000);


                    entity.Property(e => e.redirects_to)
                        .HasColumnName("redirects_to")
                        .HasMaxLength(4000);


                    entity.Property(e => e.created_on)
                        .IsRequired()
                        .HasColumnName("created_on")
                        .HasColumnType("datetime");


                    entity.Property(e => e.redirects_to_wiki_id)
                        .IsRequired()
                        .HasColumnName("redirects_to_wiki_id");


            }); 
            modelBuilder.Entity<T_wikis>(entity =>
            {
                 
                    entity.HasKey(e => e.id)
                        .HasName("PK__wikis__3213E83F9EBC76F0");

                    entity.HasIndex(e =>  e.project_id)
                        .HasName("wikis_project_id");
;


                    entity.Property(e => e.id)
                        .IsRequired()
                        .HasColumnName("id");


                    entity.Property(e => e.project_id)
                        .IsRequired()
                        .HasColumnName("project_id");


                    entity.Property(e => e.start_page)
                        .IsRequired()
                        .HasColumnName("start_page")
                        .HasMaxLength(255);


                    entity.Property(e => e.status)
                        .IsRequired()
                        .HasColumnName("status")
                        .HasDefaultValueSql("((1))");


            }); 
            modelBuilder.Entity<T_workflows>(entity =>
            {
                 
                    entity.HasKey(e => e.id)
                        .HasName("PK__workflow__3213E83F1B3C6A8A");

                    entity.HasIndex(e => new {  e.role_id , e.tracker_id , e.old_status_id } )
                        .HasName("wkfs_role_tracker_old_status");

                    entity.HasIndex(e =>  e.old_status_id)
                        .HasName("index_workflows_on_old_status_id");

                    entity.HasIndex(e =>  e.role_id)
                        .HasName("index_workflows_on_role_id");

                    entity.HasIndex(e =>  e.new_status_id)
                        .HasName("index_workflows_on_new_status_id");
;


                    entity.Property(e => e.id)
                        .IsRequired()
                        .HasColumnName("id");


                    entity.Property(e => e.tracker_id)
                        .IsRequired()
                        .HasColumnName("tracker_id")
                        .HasDefaultValueSql("((0))");


                    entity.Property(e => e.old_status_id)
                        .IsRequired()
                        .HasColumnName("old_status_id")
                        .HasDefaultValueSql("((0))");


                    entity.Property(e => e.new_status_id)
                        .IsRequired()
                        .HasColumnName("new_status_id")
                        .HasDefaultValueSql("((0))");


                    entity.Property(e => e.role_id)
                        .IsRequired()
                        .HasColumnName("role_id")
                        .HasDefaultValueSql("((0))");


                    entity.Property(e => e.assignee)
                        .IsRequired()
                        .HasColumnName("assignee")
                        .HasDefaultValueSql("((0))");


                    entity.Property(e => e.author)
                        .IsRequired()
                        .HasColumnName("author")
                        .HasDefaultValueSql("((0))");


                    entity.Property(e => e.type)
                        .HasColumnName("type")
                        .HasMaxLength(30);


                    entity.Property(e => e.field_name)
                        .HasColumnName("field_name")
                        .HasMaxLength(30);


                    entity.Property(e => e.rule)
                        .HasColumnName("rule")
                        .HasMaxLength(30);


            }); 


        } // End Sub OnModelCreating 


    } // End partial class RedmineContext : DbContext


} // End namespace BlueMine.Db
