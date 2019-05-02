using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Intranet.Migrations
{
    public partial class firstcommt : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Artigos",
                columns: table => new
                {
                    artigo = table.Column<string>(maxLength: 48, nullable: false),
                    descricao = table.Column<string>(maxLength: 50, nullable: true),
                    codbarrasartigo = table.Column<string>(maxLength: 30, nullable: true),
                    armazem = table.Column<string>(maxLength: 10, nullable: true),
                    stkActual = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Artigos", x => x.artigo);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Name = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    UserName = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(maxLength: 256, nullable: true),
                    Email = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(nullable: false),
                    PasswordHash = table.Column<string>(nullable: true),
                    SecurityStamp = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(nullable: false),
                    TwoFactorEnabled = table.Column<bool>(nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
                    LockoutEnabled = table.Column<bool>(nullable: false),
                    AccessFailedCount = table.Column<int>(nullable: false),
                    Discriminator = table.Column<string>(nullable: false),
                    profilePictureUrl = table.Column<string>(nullable: true),
                    isSuperAdmin = table.Column<bool>(nullable: true),
                    ApplicationUserRole = table.Column<bool>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Filial",
                columns: table => new
                {
                    filialId = table.Column<string>(maxLength: 38, nullable: false),
                    dataCriacao = table.Column<DateTime>(nullable: false),
                    nome = table.Column<string>(maxLength: 50, nullable: false),
                    descricao = table.Column<string>(maxLength: 50, nullable: true),
                    porDefeito = table.Column<bool>(nullable: false),
                    morada1 = table.Column<string>(maxLength: 50, nullable: false),
                    morada2 = table.Column<string>(maxLength: 50, nullable: true),
                    cidade = table.Column<string>(maxLength: 30, nullable: true),
                    provincia = table.Column<string>(maxLength: 30, nullable: true),
                    pais = table.Column<string>(maxLength: 30, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Filial", x => x.filialId);
                });

            migrationBuilder.CreateTable(
                name: "Funcionarios",
                columns: table => new
                {
                    codigo = table.Column<string>(nullable: false),
                    nome = table.Column<string>(nullable: true),
                    cdu_CodigoBarras = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Funcionarios", x => x.codigo);
                });

            migrationBuilder.CreateTable(
                name: "Projeto",
                columns: table => new
                {
                    codigo = table.Column<string>(nullable: false),
                    descricao = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Projeto", x => x.codigo);
                });

            migrationBuilder.CreateTable(
                name: "TipoDocumentoStock",
                columns: table => new
                {
                    documento = table.Column<string>(maxLength: 5, nullable: false),
                    tipo = table.Column<string>(maxLength: 5, nullable: true),
                    descricao = table.Column<string>(maxLength: 30, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoDocumentoStock", x => x.documento);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    RoleId = table.Column<string>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<string>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(nullable: false),
                    ProviderKey = table.Column<string>(nullable: false),
                    ProviderDisplayName = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    RoleId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    LoginProvider = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CabecStock",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    numDoc = table.Column<int>(nullable: false),
                    serie = table.Column<string>(maxLength: 10, nullable: true),
                    tipodoc = table.Column<string>(maxLength: 5, nullable: true),
                    entradaSaida = table.Column<string>(maxLength: 5, nullable: true),
                    funcionario = table.Column<string>(maxLength: 20, nullable: true),
                    nome = table.Column<string>(maxLength: 50, nullable: true),
                    armazem = table.Column<string>(maxLength: 10, nullable: true),
                    localizacao = table.Column<string>(maxLength: 10, nullable: true),
                    nrDocExterno = table.Column<string>(maxLength: 10, nullable: true),
                    areaNegocio = table.Column<string>(maxLength: 15, nullable: true),
                    projecto = table.Column<string>(maxLength: 15, nullable: true),
                    notas = table.Column<string>(nullable: true),
                    data = table.Column<DateTime>(nullable: false),
                    integradoErp = table.Column<bool>(nullable: false),
                    status = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CabecStock", x => x.id);
                    table.ForeignKey(
                        name: "FK_CabecStock_TipoDocumentoStock_tipodoc",
                        column: x => x.tipodoc,
                        principalTable: "TipoDocumentoStock",
                        principalColumn: "documento",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "LinhasStock",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    artigo = table.Column<string>(maxLength: 48, nullable: true),
                    descricao = table.Column<string>(maxLength: 50, nullable: true),
                    areaNegocio = table.Column<string>(maxLength: 15, nullable: true),
                    projecto = table.Column<string>(maxLength: 15, nullable: true),
                    notas = table.Column<string>(nullable: true),
                    codbarrasCabec = table.Column<string>(maxLength: 30, nullable: true),
                    quantidade = table.Column<double>(nullable: false),
                    quantTrans = table.Column<double>(nullable: false),
                    quantPendente = table.Column<double>(nullable: false),
                    idLinhaOrigem = table.Column<int>(nullable: false),
                    idDocumentoOrigem = table.Column<int>(nullable: false),
                    status = table.Column<int>(nullable: false),
                    CabecStockId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LinhasStock", x => x.id);
                    table.ForeignKey(
                        name: "FK_LinhasStock_CabecStock_CabecStockId",
                        column: x => x.CabecStockId,
                        principalTable: "CabecStock",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "TipoDocumentoStock",
                columns: new[] { "documento", "descricao", "tipo" },
                values: new object[,]
                {
                    { "DF", "Devolução de Ferramentas", "DF" },
                    { "SF", "Saida de Ferramentas", "SF" },
                    { "DC", "Devolução de Consumiveis", "DC" },
                    { "SC", "Saida de Consumiveis", "SC" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_CabecStock_tipodoc",
                table: "CabecStock",
                column: "tipodoc");

            migrationBuilder.CreateIndex(
                name: "IX_LinhasStock_CabecStockId",
                table: "LinhasStock",
                column: "CabecStockId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Artigos");

            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "Filial");

            migrationBuilder.DropTable(
                name: "Funcionarios");

            migrationBuilder.DropTable(
                name: "LinhasStock");

            migrationBuilder.DropTable(
                name: "Projeto");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "CabecStock");

            migrationBuilder.DropTable(
                name: "TipoDocumentoStock");
        }
    }
}
