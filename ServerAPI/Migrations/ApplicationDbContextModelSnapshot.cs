﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ServerAPI.Common;

namespace ServerAPI.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.14")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Data.Models.Campaign", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("ActivationTime")
                        .HasColumnType("datetime2");

                    b.Property<Guid?>("ConfigurationId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("FinishTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("Hash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("KeyId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("LastStateChange")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("OrganisationId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("OrganisationPublicKey")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("OutcomeId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("State")
                        .HasColumnType("int");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ConfigurationId");

                    b.HasIndex("KeyId");

                    b.HasIndex("OutcomeId");

                    b.ToTable("Campaigns");
                });

            modelBuilder.Entity("Data.Models.CampaignConfiguration", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<long>("DecryptionPhaseDuration")
                        .HasColumnType("bigint");

                    b.Property<long>("ModificationsPhaseDuration")
                        .HasColumnType("bigint");

                    b.Property<int>("PermutationLength")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("CampaignConfiguration");
                });

            modelBuilder.Entity("Data.Models.CampaignEvent", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("CampaignId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("TimeStamp")
                        .HasColumnType("datetime2");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.Property<string>("data")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CampaignId");

                    b.ToTable("CampaignEvent");
                });

            modelBuilder.Entity("Data.Models.CampaignOutcome", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.ToTable("CampaignOutcome");
                });

            modelBuilder.Entity("Data.Models.CampaignPhase", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("CampaignId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("InitialPermutationId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("CampaignId");

                    b.HasIndex("InitialPermutationId");

                    b.ToTable("CampaignPhase");
                });

            modelBuilder.Entity("Data.Models.CampaignPhaseOutcome", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("CampaignOutcomeId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("NumberOfSubmissions")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CampaignOutcomeId");

                    b.ToTable("CampaignPhaseOutcome");
                });

            modelBuilder.Entity("Data.Models.ModificationOutcome", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("CampaignPhaseOutcomeId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ParticipantPublicKey")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("PermutationOutcomeId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("CampaignPhaseOutcomeId");

                    b.HasIndex("PermutationOutcomeId");

                    b.ToTable("ModificationOutcome");
                });

            modelBuilder.Entity("Data.Models.OrganisationUser", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Token")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("OrganisationUsers");
                });

            modelBuilder.Entity("Data.Models.Permutation", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("EncryptedSequence")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("HasKey")
                        .HasColumnType("bit");

                    b.Property<int>("Length")
                        .HasColumnType("int");

                    b.Property<string>("SequenceHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("SequenceId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("SequenceId");

                    b.ToTable("Permutation");
                });

            modelBuilder.Entity("Data.Models.PermutationOutcome", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("SequenceId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("SequenceId");

                    b.ToTable("PermutationOutcome");
                });

            modelBuilder.Entity("Data.Models.RSAKeyPair", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.ToTable("RSAKeyPair");
                });

            modelBuilder.Entity("Data.Models.Sequence", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.ToTable("Sequence");
                });

            modelBuilder.Entity("Data.Models.Campaign", b =>
                {
                    b.HasOne("Data.Models.CampaignConfiguration", "Configuration")
                        .WithMany()
                        .HasForeignKey("ConfigurationId");

                    b.HasOne("Data.Models.RSAKeyPair", "Key")
                        .WithMany()
                        .HasForeignKey("KeyId");

                    b.HasOne("Data.Models.CampaignOutcome", "Outcome")
                        .WithMany()
                        .HasForeignKey("OutcomeId");

                    b.Navigation("Configuration");

                    b.Navigation("Key");

                    b.Navigation("Outcome");
                });

            modelBuilder.Entity("Data.Models.CampaignEvent", b =>
                {
                    b.HasOne("Data.Models.Campaign", null)
                        .WithMany("Events")
                        .HasForeignKey("CampaignId");
                });

            modelBuilder.Entity("Data.Models.CampaignPhase", b =>
                {
                    b.HasOne("Data.Models.Campaign", null)
                        .WithMany("Phases")
                        .HasForeignKey("CampaignId");

                    b.HasOne("Data.Models.Permutation", "InitialPermutation")
                        .WithMany()
                        .HasForeignKey("InitialPermutationId");

                    b.Navigation("InitialPermutation");
                });

            modelBuilder.Entity("Data.Models.CampaignPhaseOutcome", b =>
                {
                    b.HasOne("Data.Models.CampaignOutcome", null)
                        .WithMany("PhaseOutcomes")
                        .HasForeignKey("CampaignOutcomeId");
                });

            modelBuilder.Entity("Data.Models.ModificationOutcome", b =>
                {
                    b.HasOne("Data.Models.CampaignPhaseOutcome", null)
                        .WithMany("ModificationOutcomes")
                        .HasForeignKey("CampaignPhaseOutcomeId");

                    b.HasOne("Data.Models.PermutationOutcome", "PermutationOutcome")
                        .WithMany()
                        .HasForeignKey("PermutationOutcomeId");

                    b.Navigation("PermutationOutcome");
                });

            modelBuilder.Entity("Data.Models.Permutation", b =>
                {
                    b.HasOne("Data.Models.Sequence", "Sequence")
                        .WithMany()
                        .HasForeignKey("SequenceId");

                    b.Navigation("Sequence");
                });

            modelBuilder.Entity("Data.Models.PermutationOutcome", b =>
                {
                    b.HasOne("Data.Models.Sequence", "Sequence")
                        .WithMany()
                        .HasForeignKey("SequenceId");

                    b.Navigation("Sequence");
                });

            modelBuilder.Entity("Data.Models.Campaign", b =>
                {
                    b.Navigation("Events");

                    b.Navigation("Phases");
                });

            modelBuilder.Entity("Data.Models.CampaignOutcome", b =>
                {
                    b.Navigation("PhaseOutcomes");
                });

            modelBuilder.Entity("Data.Models.CampaignPhaseOutcome", b =>
                {
                    b.Navigation("ModificationOutcomes");
                });
#pragma warning restore 612, 618
        }
    }
}
