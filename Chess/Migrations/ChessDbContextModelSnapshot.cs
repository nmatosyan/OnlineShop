﻿// <auto-generated />
using System;
using Chess.CHessGame;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Chess.Migrations
{
    [DbContext(typeof(ChessDbContext))]
    partial class ChessDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Chess.CHessGame.ChessGameDb", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("BlackPlayerId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("Date")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Moves")
                        .HasColumnType("text");

                    b.Property<int>("WhitePlayerId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("BlackPlayerId");

                    b.HasIndex("WhitePlayerId");

                    b.ToTable("ChessGames");
                });

            modelBuilder.Entity("Chess.CHessGame.MoveDb", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("GameId")
                        .HasColumnType("integer");

                    b.Property<string>("Move")
                        .HasColumnType("text");

                    b.Property<int>("MoveNumber")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("GameId");

                    b.ToTable("Moves");
                });

            modelBuilder.Entity("Chess.CHessGame.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("UserId"));

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.HasKey("UserId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Chess.CHessGame.ChessGameDb", b =>
                {
                    b.HasOne("Chess.CHessGame.User", "BlackPlayer")
                        .WithMany("GamesAsBlack")
                        .HasForeignKey("BlackPlayerId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Chess.CHessGame.User", "WhitePlayer")
                        .WithMany("GamesAsWhite")
                        .HasForeignKey("WhitePlayerId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("BlackPlayer");

                    b.Navigation("WhitePlayer");
                });

            modelBuilder.Entity("Chess.CHessGame.MoveDb", b =>
                {
                    b.HasOne("Chess.CHessGame.ChessGameDb", "Game")
                        .WithMany("MoveList")
                        .HasForeignKey("GameId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Game");
                });

            modelBuilder.Entity("Chess.CHessGame.ChessGameDb", b =>
                {
                    b.Navigation("MoveList");
                });

            modelBuilder.Entity("Chess.CHessGame.User", b =>
                {
                    b.Navigation("GamesAsBlack");

                    b.Navigation("GamesAsWhite");
                });
#pragma warning restore 612, 618
        }
    }
}
