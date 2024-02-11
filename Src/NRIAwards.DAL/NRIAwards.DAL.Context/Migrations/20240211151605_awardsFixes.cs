using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NRIAwards.DAL.Context.Migrations
{
    /// <inheritdoc />
    public partial class awardsFixes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Awards_VisualContents_VisualContentId",
                table: "Awards");

            migrationBuilder.DropForeignKey(
                name: "FK_Nominations_Reader_ReaderId",
                table: "Nominations");

            migrationBuilder.DropForeignKey(
                name: "FK_Nominations_VisualContents_VisualContentId",
                table: "Nominations");

            migrationBuilder.DropForeignKey(
                name: "FK_Nominee_Nominations_NominationId",
                table: "Nominee");

            migrationBuilder.DropForeignKey(
                name: "FK_Nominee_VisualContents_VisualContentId",
                table: "Nominee");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_VisualContents_VisualContentId",
                table: "Users");

            migrationBuilder.DropForeignKey(
                name: "FK_Votes_Nominee_NomineeId",
                table: "Votes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Reader",
                table: "Reader");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Nominee",
                table: "Nominee");

            migrationBuilder.RenameTable(
                name: "Reader",
                newName: "Readers");

            migrationBuilder.RenameTable(
                name: "Nominee",
                newName: "Nominees");

            migrationBuilder.RenameColumn(
                name: "TelegramUserName",
                table: "Votes",
                newName: "TelegramUsername");

            migrationBuilder.RenameColumn(
                name: "IsCanseld",
                table: "Votes",
                newName: "IsCanceled");

            migrationBuilder.RenameColumn(
                name: "NominationPassed",
                table: "AwardSessions",
                newName: "PassedStagesCount");

            migrationBuilder.RenameIndex(
                name: "IX_Nominee_VisualContentId",
                table: "Nominees",
                newName: "IX_Nominees_VisualContentId");

            migrationBuilder.RenameIndex(
                name: "IX_Nominee_NominationId",
                table: "Nominees",
                newName: "IX_Nominees_NominationId");

            migrationBuilder.AlterColumn<int>(
                name: "VisualContentId",
                table: "Users",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "VisualContentId",
                table: "Nominations",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "VisualContentId",
                table: "Awards",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "VisualContentId",
                table: "AwardEvents",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "VisualContentId",
                table: "Nominees",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Readers",
                table: "Readers",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Nominees",
                table: "Nominees",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Awards_VisualContents_VisualContentId",
                table: "Awards",
                column: "VisualContentId",
                principalTable: "VisualContents",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Nominations_Readers_ReaderId",
                table: "Nominations",
                column: "ReaderId",
                principalTable: "Readers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Nominations_VisualContents_VisualContentId",
                table: "Nominations",
                column: "VisualContentId",
                principalTable: "VisualContents",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Nominees_Nominations_NominationId",
                table: "Nominees",
                column: "NominationId",
                principalTable: "Nominations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Nominees_VisualContents_VisualContentId",
                table: "Nominees",
                column: "VisualContentId",
                principalTable: "VisualContents",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_VisualContents_VisualContentId",
                table: "Users",
                column: "VisualContentId",
                principalTable: "VisualContents",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Votes_Nominees_NomineeId",
                table: "Votes",
                column: "NomineeId",
                principalTable: "Nominees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Awards_VisualContents_VisualContentId",
                table: "Awards");

            migrationBuilder.DropForeignKey(
                name: "FK_Nominations_Readers_ReaderId",
                table: "Nominations");

            migrationBuilder.DropForeignKey(
                name: "FK_Nominations_VisualContents_VisualContentId",
                table: "Nominations");

            migrationBuilder.DropForeignKey(
                name: "FK_Nominees_Nominations_NominationId",
                table: "Nominees");

            migrationBuilder.DropForeignKey(
                name: "FK_Nominees_VisualContents_VisualContentId",
                table: "Nominees");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_VisualContents_VisualContentId",
                table: "Users");

            migrationBuilder.DropForeignKey(
                name: "FK_Votes_Nominees_NomineeId",
                table: "Votes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Readers",
                table: "Readers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Nominees",
                table: "Nominees");

            migrationBuilder.RenameTable(
                name: "Readers",
                newName: "Reader");

            migrationBuilder.RenameTable(
                name: "Nominees",
                newName: "Nominee");

            migrationBuilder.RenameColumn(
                name: "TelegramUsername",
                table: "Votes",
                newName: "TelegramUserName");

            migrationBuilder.RenameColumn(
                name: "IsCanceled",
                table: "Votes",
                newName: "IsCanseld");

            migrationBuilder.RenameColumn(
                name: "PassedStagesCount",
                table: "AwardSessions",
                newName: "NominationPassed");

            migrationBuilder.RenameIndex(
                name: "IX_Nominees_VisualContentId",
                table: "Nominee",
                newName: "IX_Nominee_VisualContentId");

            migrationBuilder.RenameIndex(
                name: "IX_Nominees_NominationId",
                table: "Nominee",
                newName: "IX_Nominee_NominationId");

            migrationBuilder.AlterColumn<int>(
                name: "VisualContentId",
                table: "Users",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<int>(
                name: "VisualContentId",
                table: "Nominations",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<int>(
                name: "VisualContentId",
                table: "Awards",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<int>(
                name: "VisualContentId",
                table: "AwardEvents",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<int>(
                name: "VisualContentId",
                table: "Nominee",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Reader",
                table: "Reader",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Nominee",
                table: "Nominee",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Awards_VisualContents_VisualContentId",
                table: "Awards",
                column: "VisualContentId",
                principalTable: "VisualContents",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Nominations_Reader_ReaderId",
                table: "Nominations",
                column: "ReaderId",
                principalTable: "Reader",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Nominations_VisualContents_VisualContentId",
                table: "Nominations",
                column: "VisualContentId",
                principalTable: "VisualContents",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Nominee_Nominations_NominationId",
                table: "Nominee",
                column: "NominationId",
                principalTable: "Nominations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Nominee_VisualContents_VisualContentId",
                table: "Nominee",
                column: "VisualContentId",
                principalTable: "VisualContents",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_VisualContents_VisualContentId",
                table: "Users",
                column: "VisualContentId",
                principalTable: "VisualContents",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Votes_Nominee_NomineeId",
                table: "Votes",
                column: "NomineeId",
                principalTable: "Nominee",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
