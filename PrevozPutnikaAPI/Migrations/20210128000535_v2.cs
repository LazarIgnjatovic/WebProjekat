using Microsoft.EntityFrameworkCore.Migrations;

namespace PrevozPutnikaAPI.Migrations
{
    public partial class v2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rezervacija_Korisnik_korisnikID",
                table: "Rezervacija");

            migrationBuilder.DropForeignKey(
                name: "FK_Sediste_Korisnik_korisnikID",
                table: "Sediste");

            migrationBuilder.DropForeignKey(
                name: "FK_Sediste_Rezervacija_RezervacijaID",
                table: "Sediste");

            migrationBuilder.DropIndex(
                name: "IX_Sediste_korisnikID",
                table: "Sediste");

            migrationBuilder.DropColumn(
                name: "korisnikID",
                table: "Sediste");

            migrationBuilder.RenameColumn(
                name: "RezervacijaID",
                table: "Sediste",
                newName: "rezervacijaID");

            migrationBuilder.RenameIndex(
                name: "IX_Sediste_RezervacijaID",
                table: "Sediste",
                newName: "IX_Sediste_rezervacijaID");

            migrationBuilder.RenameColumn(
                name: "korisnikID",
                table: "Rezervacija",
                newName: "KorisnikID");

            migrationBuilder.RenameIndex(
                name: "IX_Rezervacija_korisnikID",
                table: "Rezervacija",
                newName: "IX_Rezervacija_KorisnikID");

            migrationBuilder.AddColumn<int>(
                name: "rezervacijaID",
                table: "Rezervacija",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Rezervacija_rezervacijaID",
                table: "Rezervacija",
                column: "rezervacijaID");

            migrationBuilder.AddForeignKey(
                name: "FK_Rezervacija_Korisnik_KorisnikID",
                table: "Rezervacija",
                column: "KorisnikID",
                principalTable: "Korisnik",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Rezervacija_Rezervacija_rezervacijaID",
                table: "Rezervacija",
                column: "rezervacijaID",
                principalTable: "Rezervacija",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Sediste_Rezervacija_rezervacijaID",
                table: "Sediste",
                column: "rezervacijaID",
                principalTable: "Rezervacija",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rezervacija_Korisnik_KorisnikID",
                table: "Rezervacija");

            migrationBuilder.DropForeignKey(
                name: "FK_Rezervacija_Rezervacija_rezervacijaID",
                table: "Rezervacija");

            migrationBuilder.DropForeignKey(
                name: "FK_Sediste_Rezervacija_rezervacijaID",
                table: "Sediste");

            migrationBuilder.DropIndex(
                name: "IX_Rezervacija_rezervacijaID",
                table: "Rezervacija");

            migrationBuilder.DropColumn(
                name: "rezervacijaID",
                table: "Rezervacija");

            migrationBuilder.RenameColumn(
                name: "rezervacijaID",
                table: "Sediste",
                newName: "RezervacijaID");

            migrationBuilder.RenameIndex(
                name: "IX_Sediste_rezervacijaID",
                table: "Sediste",
                newName: "IX_Sediste_RezervacijaID");

            migrationBuilder.RenameColumn(
                name: "KorisnikID",
                table: "Rezervacija",
                newName: "korisnikID");

            migrationBuilder.RenameIndex(
                name: "IX_Rezervacija_KorisnikID",
                table: "Rezervacija",
                newName: "IX_Rezervacija_korisnikID");

            migrationBuilder.AddColumn<int>(
                name: "korisnikID",
                table: "Sediste",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Sediste_korisnikID",
                table: "Sediste",
                column: "korisnikID");

            migrationBuilder.AddForeignKey(
                name: "FK_Rezervacija_Korisnik_korisnikID",
                table: "Rezervacija",
                column: "korisnikID",
                principalTable: "Korisnik",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Sediste_Korisnik_korisnikID",
                table: "Sediste",
                column: "korisnikID",
                principalTable: "Korisnik",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Sediste_Rezervacija_RezervacijaID",
                table: "Sediste",
                column: "RezervacijaID",
                principalTable: "Rezervacija",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
