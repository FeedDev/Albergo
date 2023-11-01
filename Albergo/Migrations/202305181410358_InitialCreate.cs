namespace Albergo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Camera",
                c => new
                    {
                        IdCamera = c.Int(nullable: false, identity: true),
                        CodiceCamera = c.String(nullable: false, maxLength: 10),
                        Descrizione = c.String(),
                        Tipologia = c.String(nullable: false, maxLength: 30),
                        Piano = c.Int(),
                        Cancellato = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.IdCamera);
            
            CreateTable(
                "dbo.Prenotazione",
                c => new
                    {
                        IdPrenotazione = c.Int(nullable: false, identity: true),
                        DataPrenotazione = c.DateTime(nullable: false, storeType: "date"),
                        CodicePrenotazione = c.Int(nullable: false),
                        Anno = c.String(nullable: false, maxLength: 10, fixedLength: true),
                        InizioSoggiorno = c.DateTime(nullable: false, storeType: "date"),
                        FineSoggiorno = c.DateTime(nullable: false, storeType: "date"),
                        Caparra = c.Decimal(nullable: false, storeType: "money"),
                        Tariffa = c.Decimal(nullable: false, storeType: "money"),
                        IdTipologiaFk = c.Int(nullable: false),
                        IdClienteFk = c.Int(nullable: false),
                        Cancellato = c.Boolean(nullable: false),
                        IdCameraFk = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.IdPrenotazione)
                .ForeignKey("dbo.Cliente", t => t.IdClienteFk)
                .ForeignKey("dbo.Tipologia", t => t.IdTipologiaFk)
                .ForeignKey("dbo.Camera", t => t.IdCameraFk)
                .Index(t => t.IdTipologiaFk)
                .Index(t => t.IdClienteFk)
                .Index(t => t.IdCameraFk);
            
            CreateTable(
                "dbo.Cliente",
                c => new
                    {
                        IdCliente = c.Int(nullable: false, identity: true),
                        CodiceFiscale = c.String(nullable: false, maxLength: 20),
                        Nome = c.String(nullable: false, maxLength: 20),
                        Cognome = c.String(nullable: false, maxLength: 20),
                        Città = c.String(nullable: false, maxLength: 50),
                        Provincia = c.String(nullable: false, maxLength: 40),
                        Email = c.String(nullable: false, maxLength: 30),
                        Telefono = c.String(maxLength: 30),
                        Cellulare = c.String(maxLength: 30),
                    })
                .PrimaryKey(t => t.IdCliente);
            
            CreateTable(
                "dbo.PivotPrenotazioneServizio",
                c => new
                    {
                        IdPivotPrenotazioneServizio = c.Int(nullable: false, identity: true),
                        IdPrenotazione = c.Int(nullable: false),
                        IdServizio = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.IdPivotPrenotazioneServizio)
                .ForeignKey("dbo.Servizio", t => t.IdServizio)
                .ForeignKey("dbo.Prenotazione", t => t.IdPrenotazione)
                .Index(t => t.IdPrenotazione)
                .Index(t => t.IdServizio);
            
            CreateTable(
                "dbo.Servizio",
                c => new
                    {
                        IdServizio = c.Int(nullable: false, identity: true),
                        NomeServizio = c.String(nullable: false, maxLength: 50, fixedLength: true),
                        Prezzo = c.Decimal(nullable: false, storeType: "money"),
                        Quantità = c.Int(nullable: false),
                        DataServizio = c.DateTime(nullable: false, storeType: "date"),
                        Cancellato = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.IdServizio);
            
            CreateTable(
                "dbo.Tipologia",
                c => new
                    {
                        IdTipologia = c.Int(nullable: false, identity: true),
                        NomeTipologia = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.IdTipologia);
            
            CreateTable(
                "dbo.User",
                c => new
                    {
                        IdUser = c.Int(nullable: false, identity: true),
                        Username = c.String(nullable: false, maxLength: 50),
                        Password = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.IdUser);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Prenotazione", "IdCameraFk", "dbo.Camera");
            DropForeignKey("dbo.Prenotazione", "IdTipologiaFk", "dbo.Tipologia");
            DropForeignKey("dbo.PivotPrenotazioneServizio", "IdPrenotazione", "dbo.Prenotazione");
            DropForeignKey("dbo.PivotPrenotazioneServizio", "IdServizio", "dbo.Servizio");
            DropForeignKey("dbo.Prenotazione", "IdClienteFk", "dbo.Cliente");
            DropIndex("dbo.PivotPrenotazioneServizio", new[] { "IdServizio" });
            DropIndex("dbo.PivotPrenotazioneServizio", new[] { "IdPrenotazione" });
            DropIndex("dbo.Prenotazione", new[] { "IdCameraFk" });
            DropIndex("dbo.Prenotazione", new[] { "IdClienteFk" });
            DropIndex("dbo.Prenotazione", new[] { "IdTipologiaFk" });
            DropTable("dbo.User");
            DropTable("dbo.Tipologia");
            DropTable("dbo.Servizio");
            DropTable("dbo.PivotPrenotazioneServizio");
            DropTable("dbo.Cliente");
            DropTable("dbo.Prenotazione");
            DropTable("dbo.Camera");
        }
    }
}
