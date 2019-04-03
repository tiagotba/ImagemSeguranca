namespace WebApiImagemSegurança.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class criacaoEstrutura : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TB_CAMERA",
                c => new
                    {
                        idCamera = c.Int(nullable: false, identity: true),
                        nome = c.String(),
                        sensorLigado = c.Boolean(nullable: false),
                        ip = c.String(),
                        mac = c.String(),
                        status = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.idCamera);
            
            CreateTable(
                "dbo.TB_EVENTOS_DISPOSITIVOS",
                c => new
                    {
                        id = c.Long(nullable: false, identity: true),
                        dataEvento = c.DateTime(nullable: false),
                        statusSucesso = c.Boolean(nullable: false),
                        statusFalha = c.Boolean(nullable: false),
                        idPortao = c.Int(nullable: false),
                        idCamera = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.TB_CAMERA", t => t.idCamera, cascadeDelete: true)
                .ForeignKey("dbo.TB_PORTAO", t => t.idPortao, cascadeDelete: true)
                .Index(t => t.idPortao)
                .Index(t => t.idCamera);
            
            CreateTable(
                "dbo.TB_PORTAO",
                c => new
                    {
                        idPortao = c.Int(nullable: false, identity: true),
                        nome = c.String(),
                        ip = c.String(),
                        mac = c.String(),
                        status = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.idPortao);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TB_EVENTOS_DISPOSITIVOS", "idPortao", "dbo.TB_PORTAO");
            DropForeignKey("dbo.TB_EVENTOS_DISPOSITIVOS", "idCamera", "dbo.TB_CAMERA");
            DropIndex("dbo.TB_EVENTOS_DISPOSITIVOS", new[] { "idCamera" });
            DropIndex("dbo.TB_EVENTOS_DISPOSITIVOS", new[] { "idPortao" });
            DropTable("dbo.TB_PORTAO");
            DropTable("dbo.TB_EVENTOS_DISPOSITIVOS");
            DropTable("dbo.TB_CAMERA");
        }
    }
}
