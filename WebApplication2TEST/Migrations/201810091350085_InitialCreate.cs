namespace WebApplication2TEST.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AgencesVoyages",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Nom = c.String(nullable: false, maxLength: 60),
                    })
                .PrimaryKey(t => t.ID)
                .Index(t => t.Nom, unique: true);
            
            CreateTable(
                "dbo.Voyages",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        DateAller = c.DateTime(nullable: false),
                        DateRetour = c.DateTime(nullable: false),
                        PlacesDisponibles = c.Int(nullable: false),
                        PrixParPersonne = c.Decimal(nullable: false, storeType: "money"),
                        AgenceVoyageID = c.Int(nullable: false),
                        DestinationID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.AgencesVoyages", t => t.AgenceVoyageID, cascadeDelete: true)
                .ForeignKey("dbo.Destinations", t => t.DestinationID, cascadeDelete: true)
                .Index(t => new { t.DateAller, t.DateRetour, t.AgenceVoyageID, t.DestinationID }, unique: true, name: "IX_DatesAgenceDestination");
            
            CreateTable(
                "dbo.Destinations",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Continent = c.String(nullable: false, maxLength: 40),
                        Pays = c.String(nullable: false, maxLength: 40),
                        Region = c.String(nullable: false, maxLength: 40),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .Index(t => new { t.Continent, t.Pays, t.Region }, unique: true, name: "IX_ContinentPaysRegion");
            
            CreateTable(
                "dbo.DossiersReservations",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        NumeroCarteBancaire = c.String(nullable: false, maxLength: 20),
                        Etat = c.Byte(nullable: false),
                        ClientID = c.Int(nullable: false),
                        VoyageID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Clients", t => t.ClientID, cascadeDelete: true)
                .ForeignKey("dbo.Voyages", t => t.VoyageID, cascadeDelete: true)
                .Index(t => t.ClientID)
                .Index(t => t.VoyageID);
            
            CreateTable(
                "dbo.Assurances",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Montant = c.Decimal(nullable: false, storeType: "money"),
                        Type = c.Byte(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .Index(t => new { t.Montant, t.Type }, unique: true, name: "IX_MontantType");
            
            CreateTable(
                "dbo.Clients",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Email = c.String(nullable: false, maxLength: 60),
                        Civilite = c.String(nullable: false, maxLength: 20),
                        Nom = c.String(nullable: false, maxLength: 30),
                        Prenom = c.String(nullable: false, maxLength: 30),
                        Adresse = c.String(nullable: false, maxLength: 60),
                        Telephone = c.String(nullable: false, maxLength: 20),
                        DateNaissance = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .Index(t => t.Email, unique: true)
                .Index(t => new { t.Civilite, t.Nom, t.Prenom, t.Adresse }, unique: true, name: "IX_PersonneUnique");
            
            CreateTable(
                "dbo.Participants",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        DossierReservationID = c.Int(nullable: false),
                        Civilite = c.String(nullable: false, maxLength: 20),
                        Nom = c.String(nullable: false, maxLength: 30),
                        Prenom = c.String(nullable: false, maxLength: 30),
                        Adresse = c.String(nullable: false, maxLength: 60),
                        Telephone = c.String(nullable: false, maxLength: 20),
                        DateNaissance = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.DossiersReservations", t => t.DossierReservationID, cascadeDelete: true)
                .Index(t => t.DossierReservationID)
                .Index(t => new { t.Civilite, t.Nom, t.Prenom, t.Adresse }, unique: true, name: "IX_PersonneUnique");
            
            CreateTable(
                "dbo.Roles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.UserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.Roles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.UserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.UserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AssuranceDossierReservations",
                c => new
                    {
                        Assurance_ID = c.Int(nullable: false),
                        DossierReservation_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Assurance_ID, t.DossierReservation_ID })
                .ForeignKey("dbo.Assurances", t => t.Assurance_ID, cascadeDelete: true)
                .ForeignKey("dbo.DossiersReservations", t => t.DossierReservation_ID, cascadeDelete: true)
                .Index(t => t.Assurance_ID)
                .Index(t => t.DossierReservation_ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserRoles", "UserId", "dbo.Users");
            DropForeignKey("dbo.UserLogins", "UserId", "dbo.Users");
            DropForeignKey("dbo.UserClaims", "UserId", "dbo.Users");
            DropForeignKey("dbo.UserRoles", "RoleId", "dbo.Roles");
            DropForeignKey("dbo.DossiersReservations", "VoyageID", "dbo.Voyages");
            DropForeignKey("dbo.Participants", "DossierReservationID", "dbo.DossiersReservations");
            DropForeignKey("dbo.DossiersReservations", "ClientID", "dbo.Clients");
            DropForeignKey("dbo.AssuranceDossierReservations", "DossierReservation_ID", "dbo.DossiersReservations");
            DropForeignKey("dbo.AssuranceDossierReservations", "Assurance_ID", "dbo.Assurances");
            DropForeignKey("dbo.Voyages", "DestinationID", "dbo.Destinations");
            DropForeignKey("dbo.Voyages", "AgenceVoyageID", "dbo.AgencesVoyages");
            DropIndex("dbo.AssuranceDossierReservations", new[] { "DossierReservation_ID" });
            DropIndex("dbo.AssuranceDossierReservations", new[] { "Assurance_ID" });
            DropIndex("dbo.UserLogins", new[] { "UserId" });
            DropIndex("dbo.UserClaims", new[] { "UserId" });
            DropIndex("dbo.Users", "UserNameIndex");
            DropIndex("dbo.UserRoles", new[] { "RoleId" });
            DropIndex("dbo.UserRoles", new[] { "UserId" });
            DropIndex("dbo.Roles", "RoleNameIndex");
            DropIndex("dbo.Participants", "IX_PersonneUnique");
            DropIndex("dbo.Participants", new[] { "DossierReservationID" });
            DropIndex("dbo.Clients", "IX_PersonneUnique");
            DropIndex("dbo.Clients", new[] { "Email" });
            DropIndex("dbo.Assurances", "IX_MontantType");
            DropIndex("dbo.DossiersReservations", new[] { "VoyageID" });
            DropIndex("dbo.DossiersReservations", new[] { "ClientID" });
            DropIndex("dbo.Destinations", "IX_ContinentPaysRegion");
            DropIndex("dbo.Voyages", "IX_DatesAgenceDestination");
            DropIndex("dbo.AgencesVoyages", new[] { "Nom" });
            DropTable("dbo.AssuranceDossierReservations");
            DropTable("dbo.UserLogins");
            DropTable("dbo.UserClaims");
            DropTable("dbo.Users");
            DropTable("dbo.UserRoles");
            DropTable("dbo.Roles");
            DropTable("dbo.Participants");
            DropTable("dbo.Clients");
            DropTable("dbo.Assurances");
            DropTable("dbo.DossiersReservations");
            DropTable("dbo.Destinations");
            DropTable("dbo.Voyages");
            DropTable("dbo.AgencesVoyages");
        }
    }
}
