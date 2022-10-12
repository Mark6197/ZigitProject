using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Projects",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<long>(type: "bigint", nullable: false),
                    ProjectGuid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Score = table.Column<int>(type: "int", nullable: false),
                    DurationInDays = table.Column<int>(type: "int", nullable: false),
                    BugsCount = table.Column<int>(type: "int", nullable: false),
                    MadeDeadline = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Projects", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PersonalDetails",
                columns: table => new
                {
                    UserId = table.Column<long>(type: "bigint", nullable: false),
                    Team = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Avatar = table.Column<string>(type: "nvarchar(1024)", maxLength: 1024, nullable: false),
                    JoinedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonalDetails", x => x.UserId);
                    table.ForeignKey(
                        name: "FK_PersonalDetails_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Projects",
                columns: new[] { "Id", "BugsCount", "DurationInDays", "MadeDeadline", "Name", "ProjectGuid", "Score", "UserId" },
                values: new object[,]
                {
                    { 1L, 74, 35, false, "Backend Project", new Guid("9c163d66-2453-4e99-96fe-47752bbad4d3"), 88, 1L },
                    { 2L, 52, 55, false, "Design Project", new Guid("faa77f1d-be19-40e3-b496-bda6a0833157"), 68, 1L },
                    { 3L, 32, 51, true, "Frontend Project", new Guid("62e0bbe1-f23e-470d-bf65-9756b1d481e8"), 99, 1L },
                    { 4L, 42, 68, true, "Design Project", new Guid("3e9f6f85-efcc-4b70-b994-ff5f625734f1"), 97, 1L },
                    { 5L, 64, 66, false, "Backend Project", new Guid("b1046bc7-b8a7-471f-a0b0-cc483d97de6c"), 97, 1L },
                    { 6L, 63, 61, true, "Fullstack Project", new Guid("8b75298f-4a60-47cc-ba61-9ac7f39a2e15"), 79, 1L },
                    { 7L, 50, 62, true, "Backend Project", new Guid("1e8ad54a-4995-4be8-8242-2bcec1123bf1"), 66, 1L },
                    { 8L, 72, 44, false, "Backend Project", new Guid("feb9ddc1-1487-4160-9942-b1b682148dc9"), 79, 1L },
                    { 9L, 72, 66, true, "Backend Project", new Guid("17111b01-66e4-4bf5-b08e-d517a8068044"), 93, 1L },
                    { 10L, 47, 39, true, "Design Project", new Guid("33eb1907-637c-4d90-92f8-306b7bbb8318"), 100, 1L },
                    { 11L, 56, 68, false, "Design Project", new Guid("84824669-f771-443a-84ee-a427aa735b5b"), 87, 1L },
                    { 12L, 73, 36, true, "Fullstack Project", new Guid("7e691d32-4df1-4f39-a4b9-a964f923b071"), 76, 1L },
                    { 13L, 34, 36, true, "Backend Project", new Guid("bd373f61-4171-4e63-85fe-87ff6cab55da"), 90, 1L },
                    { 14L, 65, 59, false, "Design Project", new Guid("19cd75b7-131f-4bb7-a698-d2e8e15505a9"), 95, 1L }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "Password" },
                values: new object[] { 1L, "admin@gmail.com", "Aa123456" });

            migrationBuilder.InsertData(
                table: "PersonalDetails",
                columns: new[] { "UserId", "Avatar", "JoinedAt", "Name", "Team" },
                values: new object[] { 1L, "https://avatarfiles.alphacoders.com/164/thumb-164632.jpg", new DateTime(2018, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Test Test", "Developers" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PersonalDetails");

            migrationBuilder.DropTable(
                name: "Projects");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
