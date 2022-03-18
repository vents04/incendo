using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ServerAPI.Migrations
{
    public partial class campaignModelChanges : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Campaigns_CampaignConfiguration_Configurationid",
                table: "Campaigns");

            migrationBuilder.RenameColumn(
                name: "Configurationid",
                table: "Campaigns",
                newName: "ConfigurationId");

            migrationBuilder.RenameIndex(
                name: "IX_Campaigns_Configurationid",
                table: "Campaigns",
                newName: "IX_Campaigns_ConfigurationId");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "CampaignConfiguration",
                newName: "Id");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Campaigns",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Campaigns",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "OrganisationId",
                table: "Campaigns",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<int>(
                name: "Type",
                table: "Campaigns",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_Campaigns_CampaignConfiguration_ConfigurationId",
                table: "Campaigns",
                column: "ConfigurationId",
                principalTable: "CampaignConfiguration",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Campaigns_CampaignConfiguration_ConfigurationId",
                table: "Campaigns");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Campaigns");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Campaigns");

            migrationBuilder.DropColumn(
                name: "OrganisationId",
                table: "Campaigns");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "Campaigns");

            migrationBuilder.RenameColumn(
                name: "ConfigurationId",
                table: "Campaigns",
                newName: "Configurationid");

            migrationBuilder.RenameIndex(
                name: "IX_Campaigns_ConfigurationId",
                table: "Campaigns",
                newName: "IX_Campaigns_Configurationid");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "CampaignConfiguration",
                newName: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_Campaigns_CampaignConfiguration_Configurationid",
                table: "Campaigns",
                column: "Configurationid",
                principalTable: "CampaignConfiguration",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
