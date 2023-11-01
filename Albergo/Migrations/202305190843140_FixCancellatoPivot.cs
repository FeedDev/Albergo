namespace Albergo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FixCancellatoPivot : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PivotPrenotazioneServizio", "Cancellato", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.PivotPrenotazioneServizio", "Cancellato");
        }
    }
}
