using Microsoft.EntityFrameworkCore.Migrations;

namespace DomingoRoofWorks.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Customer",
                columns: table => new
                {
                    CustomerID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "varchar(25)", unicode: false, maxLength: 25, nullable: false),
                    Surname = table.Column<string>(type: "varchar(25)", unicode: false, maxLength: 25, nullable: false),
                    Address = table.Column<string>(type: "varchar(60)", unicode: false, maxLength: 60, nullable: false),
                    City = table.Column<string>(type: "varchar(25)", unicode: false, maxLength: 25, nullable: false),
                    PostalCode = table.Column<string>(type: "char(5)", unicode: false, fixedLength: true, maxLength: 5, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customer", x => x.CustomerID);
                });

            migrationBuilder.CreateTable(
                name: "Employee",
                columns: table => new
                {
                    EmployeeID = table.Column<string>(type: "varchar(6)", unicode: false, maxLength: 6, nullable: false),
                    Name = table.Column<string>(type: "varchar(25)", unicode: false, maxLength: 25, nullable: false),
                    Surname = table.Column<string>(type: "varchar(25)", unicode: false, maxLength: 25, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employee", x => x.EmployeeID);
                });

            migrationBuilder.CreateTable(
                name: "JobType",
                columns: table => new
                {
                    JobTypeID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    JobType = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    Rate = table.Column<decimal>(type: "decimal(18,0)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobType", x => x.JobTypeID);
                });

            migrationBuilder.CreateTable(
                name: "Material",
                columns: table => new
                {
                    MaterialID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "varchar(250)", unicode: false, maxLength: 250, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Material", x => x.MaterialID);
                });

            migrationBuilder.CreateTable(
                name: "Job",
                columns: table => new
                {
                    JobCardID = table.Column<int>(type: "int", nullable: false),
                    CustomerID = table.Column<int>(type: "int", nullable: false),
                    JobTypeID = table.Column<int>(type: "int", nullable: false),
                    Days = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Job__98F41DDF3D847537", x => x.JobCardID);
                    table.ForeignKey(
                        name: "FK__Job__CustomerID__3E52440B",
                        column: x => x.CustomerID,
                        principalTable: "Customer",
                        principalColumn: "CustomerID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK__Job__JobTypeID__3F466844",
                        column: x => x.JobTypeID,
                        principalTable: "JobType",
                        principalColumn: "JobTypeID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Job_Employee",
                columns: table => new
                {
                    JobEmployeeID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    JobCardID = table.Column<int>(type: "int", nullable: false),
                    EmployeeID = table.Column<string>(type: "varchar(6)", unicode: false, maxLength: 6, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Job_Employee", x => x.JobEmployeeID);
                    table.ForeignKey(
                        name: "FK__Job_Emplo__Emplo__4316F928",
                        column: x => x.EmployeeID,
                        principalTable: "Employee",
                        principalColumn: "EmployeeID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK__Job_Emplo__JobCa__4222D4EF",
                        column: x => x.JobCardID,
                        principalTable: "Job",
                        principalColumn: "JobCardID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Job_Material",
                columns: table => new
                {
                    JobMaterialID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    JobCardID = table.Column<int>(type: "int", nullable: false),
                    MaterialID = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Job_Material", x => x.JobMaterialID);
                    table.ForeignKey(
                        name: "FK__Job_Mater__JobCa__46E78A0C",
                        column: x => x.JobCardID,
                        principalTable: "Job",
                        principalColumn: "JobCardID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK__Job_Mater__Mater__47DBAE45",
                        column: x => x.MaterialID,
                        principalTable: "Material",
                        principalColumn: "MaterialID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Job_CustomerID",
                table: "Job",
                column: "CustomerID");

            migrationBuilder.CreateIndex(
                name: "IX_Job_JobTypeID",
                table: "Job",
                column: "JobTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_Job_Employee_EmployeeID",
                table: "Job_Employee",
                column: "EmployeeID");

            migrationBuilder.CreateIndex(
                name: "IX_Job_Employee_JobCardID",
                table: "Job_Employee",
                column: "JobCardID");

            migrationBuilder.CreateIndex(
                name: "IX_Job_Material_JobCardID",
                table: "Job_Material",
                column: "JobCardID");

            migrationBuilder.CreateIndex(
                name: "IX_Job_Material_MaterialID",
                table: "Job_Material",
                column: "MaterialID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Job_Employee");

            migrationBuilder.DropTable(
                name: "Job_Material");

            migrationBuilder.DropTable(
                name: "Employee");

            migrationBuilder.DropTable(
                name: "Job");

            migrationBuilder.DropTable(
                name: "Material");

            migrationBuilder.DropTable(
                name: "Customer");

            migrationBuilder.DropTable(
                name: "JobType");
        }
    }
}
