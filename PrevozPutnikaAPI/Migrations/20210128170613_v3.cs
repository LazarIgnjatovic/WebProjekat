using Microsoft.EntityFrameworkCore.Migrations;

namespace PrevozPutnikaAPI.Migrations
{
    public partial class v3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rezervacija_Korisnik_KorisnikID",
                table: "Rezervacija");

            migrationBuilder.DropForeignKey(
                name: "FK_Rezervacija_Rezervacija_rezervacijaID",
                table: "Rezervacija");

            migrationBuilder.DropIndex(
                name: "IX_Rezervacija_rezervacijaID",
                table: "Rezervacija");

            migrationBuilder.DropColumn(
                name: "rezervacijaID",
                table: "Rezervacija");

            migrationBuilder.RenameColumn(
                name: "KorisnikID",
                table: "Rezervacija",
                newName: "korisnikID");

            migrationBuilder.RenameIndex(
                name: "IX_Rezervacija_KorisnikID",
                table: "Rezervacija",
                newName: "IX_Rezervacija_korisnikID");

            migrationBuilder.AddForeignKey(
                name: "FK_Rezervacija_Korisnik_korisnikID",
                table: "Rezervacija",
                column: "korisnikID",
                principalTable: "Korisnik",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rezervacija_Korisnik_korisnikID",
                table: "Rezervacija");

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
        }
    }
}
