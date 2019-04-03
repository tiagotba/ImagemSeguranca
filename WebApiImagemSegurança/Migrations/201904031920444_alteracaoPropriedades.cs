namespace WebApiImagemSegurança.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class alteracaoPropriedades : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TB_CAMERA", "cameraLigada", c => c.Boolean(nullable: false));
            AddColumn("dbo.TB_PORTAO", "portaoLigado", c => c.Boolean(nullable: false));
            AddColumn("dbo.TB_PORTAO", "portaoAberto", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.TB_PORTAO", "portaoAberto");
            DropColumn("dbo.TB_PORTAO", "portaoLigado");
            DropColumn("dbo.TB_CAMERA", "cameraLigada");
        }
    }
}
