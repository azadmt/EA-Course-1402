using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Catalog.Persistence.EF.Migrations
{
    public partial class createoutbox : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
            
                        CREATE TABLE [dbo].[Outbox](
	                        [Id] [bigint] IDENTITY(1,1) NOT NULL,
	                        [EventType] [nvarchar](500) NOT NULL,
	                        [EventBody] [nvarchar](max) NOT NULL,
	                        [PublishedAt] [datetime] NULL,
	                        [Created] [datetime] NOT NULL,
	                        [EventId] [uniqueidentifier] NOT NULL
                        ) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
                        
                       
");

            migrationBuilder.Sql("ALTER TABLE [dbo].[Outbox] ADD  CONSTRAINT [DF_Outbox_Created]  DEFAULT (getdate()) FOR [Created]");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
