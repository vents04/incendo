using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace ServerAPI.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CampaignConfiguration",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ModificationsPhaseDuration = table.Column<long>(type: "bigint", nullable: false),
                    DecryptionPhaseDuration = table.Column<long>(type: "bigint", nullable: false),
                    PermutationLength = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CampaignConfiguration", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "CampaignOutcome",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CampaignOutcome", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OrganisationUsers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Token = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrganisationUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RSAKeyPair",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RSAKeyPair", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Sequence",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sequence", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CampaignPhaseOutcome",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NumberOfSubmissions = table.Column<int>(type: "int", nullable: false),
                    CampaignOutcomeId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CampaignPhaseOutcome", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CampaignPhaseOutcome_CampaignOutcome_CampaignOutcomeId",
                        column: x => x.CampaignOutcomeId,
                        principalTable: "CampaignOutcome",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Campaigns",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OrganisationPublicKey = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    KeyId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Configurationid = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    State = table.Column<int>(type: "int", nullable: false),
                    LastStateChange = table.Column<DateTime>(type: "datetime2", nullable: false),
                    OutcomeId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    FinishTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ActivationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Hash = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Campaigns", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Campaigns_CampaignConfiguration_Configurationid",
                        column: x => x.Configurationid,
                        principalTable: "CampaignConfiguration",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Campaigns_CampaignOutcome_OutcomeId",
                        column: x => x.OutcomeId,
                        principalTable: "CampaignOutcome",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Campaigns_RSAKeyPair_KeyId",
                        column: x => x.KeyId,
                        principalTable: "RSAKeyPair",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Permutation",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Length = table.Column<int>(type: "int", nullable: false),
                    EncryptedSequence = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SequenceHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HasKey = table.Column<bool>(type: "bit", nullable: false),
                    SequenceId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Permutation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Permutation_Sequence_SequenceId",
                        column: x => x.SequenceId,
                        principalTable: "Sequence",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PermutationOutcome",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SequenceId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PermutationOutcome", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PermutationOutcome_Sequence_SequenceId",
                        column: x => x.SequenceId,
                        principalTable: "Sequence",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CampaignEvent",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TimeStamp = table.Column<DateTime>(type: "datetime2", nullable: false),
                    data = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Type = table.Column<int>(type: "int", nullable: false),
                    CampaignId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CampaignEvent", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CampaignEvent_Campaigns_CampaignId",
                        column: x => x.CampaignId,
                        principalTable: "Campaigns",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CampaignPhase",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    InitialPermutationId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CampaignId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CampaignPhase", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CampaignPhase_Campaigns_CampaignId",
                        column: x => x.CampaignId,
                        principalTable: "Campaigns",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CampaignPhase_Permutation_InitialPermutationId",
                        column: x => x.InitialPermutationId,
                        principalTable: "Permutation",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ModificationOutcome",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ParticipantPublicKey = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PermutationOutcomeId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CampaignPhaseOutcomeId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ModificationOutcome", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ModificationOutcome_CampaignPhaseOutcome_CampaignPhaseOutcomeId",
                        column: x => x.CampaignPhaseOutcomeId,
                        principalTable: "CampaignPhaseOutcome",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ModificationOutcome_PermutationOutcome_PermutationOutcomeId",
                        column: x => x.PermutationOutcomeId,
                        principalTable: "PermutationOutcome",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CampaignEvent_CampaignId",
                table: "CampaignEvent",
                column: "CampaignId");

            migrationBuilder.CreateIndex(
                name: "IX_CampaignPhase_CampaignId",
                table: "CampaignPhase",
                column: "CampaignId");

            migrationBuilder.CreateIndex(
                name: "IX_CampaignPhase_InitialPermutationId",
                table: "CampaignPhase",
                column: "InitialPermutationId");

            migrationBuilder.CreateIndex(
                name: "IX_CampaignPhaseOutcome_CampaignOutcomeId",
                table: "CampaignPhaseOutcome",
                column: "CampaignOutcomeId");

            migrationBuilder.CreateIndex(
                name: "IX_Campaigns_Configurationid",
                table: "Campaigns",
                column: "Configurationid");

            migrationBuilder.CreateIndex(
                name: "IX_Campaigns_KeyId",
                table: "Campaigns",
                column: "KeyId");

            migrationBuilder.CreateIndex(
                name: "IX_Campaigns_OutcomeId",
                table: "Campaigns",
                column: "OutcomeId");

            migrationBuilder.CreateIndex(
                name: "IX_ModificationOutcome_CampaignPhaseOutcomeId",
                table: "ModificationOutcome",
                column: "CampaignPhaseOutcomeId");

            migrationBuilder.CreateIndex(
                name: "IX_ModificationOutcome_PermutationOutcomeId",
                table: "ModificationOutcome",
                column: "PermutationOutcomeId");

            migrationBuilder.CreateIndex(
                name: "IX_Permutation_SequenceId",
                table: "Permutation",
                column: "SequenceId");

            migrationBuilder.CreateIndex(
                name: "IX_PermutationOutcome_SequenceId",
                table: "PermutationOutcome",
                column: "SequenceId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CampaignEvent");

            migrationBuilder.DropTable(
                name: "CampaignPhase");

            migrationBuilder.DropTable(
                name: "ModificationOutcome");

            migrationBuilder.DropTable(
                name: "OrganisationUsers");

            migrationBuilder.DropTable(
                name: "Campaigns");

            migrationBuilder.DropTable(
                name: "Permutation");

            migrationBuilder.DropTable(
                name: "CampaignPhaseOutcome");

            migrationBuilder.DropTable(
                name: "PermutationOutcome");

            migrationBuilder.DropTable(
                name: "CampaignConfiguration");

            migrationBuilder.DropTable(
                name: "RSAKeyPair");

            migrationBuilder.DropTable(
                name: "CampaignOutcome");

            migrationBuilder.DropTable(
                name: "Sequence");
        }
    }
}