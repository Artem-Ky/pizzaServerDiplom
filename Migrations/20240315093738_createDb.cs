using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace pizzaServerApp.Migrations
{
    /// <inheritdoc />
    public partial class createDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "public");

            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    UserName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "boolean", nullable: false),
                    PasswordHash = table.Column<string>(type: "text", nullable: true),
                    SecurityStamp = table.Column<string>(type: "text", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: true),
                    PhoneNumber = table.Column<string>(type: "text", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "boolean", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "bannerType",
                schema: "public",
                columns: table => new
                {
                    id = table.Column<short>(type: "smallint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    type = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_bannerType", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "coupon",
                schema: "public",
                columns: table => new
                {
                    id = table.Column<short>(type: "smallint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "text", nullable: false),
                    startDate = table.Column<DateOnly>(type: "date", nullable: false),
                    endDate = table.Column<DateOnly>(type: "date", nullable: false),
                    discont = table.Column<short>(type: "smallint", nullable: false),
                    minCost = table.Column<short>(type: "smallint", nullable: false),
                    isPersent = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_coupon", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "courier",
                schema: "public",
                columns: table => new
                {
                    id = table.Column<short>(type: "smallint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "text", nullable: false),
                    phone = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_courier", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "ingredientType",
                schema: "public",
                columns: table => new
                {
                    id = table.Column<short>(type: "smallint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    type = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ingredientType", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "orderStatus",
                schema: "public",
                columns: table => new
                {
                    id = table.Column<short>(type: "smallint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    status = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_orderStatus", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "productSize",
                schema: "public",
                columns: table => new
                {
                    id = table.Column<short>(type: "smallint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    size = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_productSize", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "productType",
                schema: "public",
                columns: table => new
                {
                    id = table.Column<short>(type: "smallint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    type = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_productType", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    RoleId = table.Column<string>(type: "text", nullable: false),
                    ClaimType = table.Column<string>(type: "text", nullable: true),
                    ClaimValue = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalSchema: "public",
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<string>(type: "text", nullable: false),
                    ClaimType = table.Column<string>(type: "text", nullable: true),
                    ClaimValue = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalSchema: "public",
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                schema: "public",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "text", nullable: false),
                    ProviderKey = table.Column<string>(type: "text", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "text", nullable: true),
                    UserId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalSchema: "public",
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                schema: "public",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "text", nullable: false),
                    RoleId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalSchema: "public",
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalSchema: "public",
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                schema: "public",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "text", nullable: false),
                    LoginProvider = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Value = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalSchema: "public",
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "user_coupon",
                schema: "public",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    couponId = table.Column<short>(type: "smallint", nullable: false),
                    userId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user_coupon", x => x.id);
                    table.ForeignKey(
                        name: "FK_user_coupon_AspNetUsers_userId",
                        column: x => x.userId,
                        principalSchema: "public",
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_user_coupon_coupon_couponId",
                        column: x => x.couponId,
                        principalSchema: "public",
                        principalTable: "coupon",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ingredient",
                schema: "public",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "text", nullable: false),
                    photo = table.Column<string>(type: "text", nullable: false),
                    costForOne = table.Column<int>(type: "integer", nullable: false),
                    weightForOne = table.Column<int>(type: "integer", nullable: false),
                    count = table.Column<int>(type: "integer", nullable: false),
                    isAvailible = table.Column<bool>(type: "boolean", nullable: false),
                    ingredientTypeId = table.Column<short>(type: "smallint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ingredient", x => x.id);
                    table.ForeignKey(
                        name: "FK_ingredient_ingredientType_ingredientTypeId",
                        column: x => x.ingredientTypeId,
                        principalSchema: "public",
                        principalTable: "ingredientType",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "userOrder",
                schema: "public",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    address = table.Column<string>(type: "text", nullable: false),
                    totalCost = table.Column<int>(type: "integer", nullable: false),
                    orderStartTime = table.Column<TimeOnly>(type: "time without time zone", nullable: false),
                    orderDeliveryTime = table.Column<TimeOnly>(type: "time without time zone", nullable: false),
                    orderEndTime = table.Column<TimeOnly>(type: "time without time zone", nullable: false),
                    orderTotalTime = table.Column<TimeOnly>(type: "time without time zone", nullable: false),
                    couponId = table.Column<short>(type: "smallint", nullable: false),
                    userId = table.Column<string>(type: "text", nullable: false),
                    courierId = table.Column<short>(type: "smallint", nullable: false),
                    statusId = table.Column<short>(type: "smallint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_userOrder", x => x.id);
                    table.ForeignKey(
                        name: "FK_userOrder_AspNetUsers_userId",
                        column: x => x.userId,
                        principalSchema: "public",
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_userOrder_coupon_couponId",
                        column: x => x.couponId,
                        principalSchema: "public",
                        principalTable: "coupon",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_userOrder_courier_courierId",
                        column: x => x.courierId,
                        principalSchema: "public",
                        principalTable: "courier",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_userOrder_orderStatus_statusId",
                        column: x => x.statusId,
                        principalSchema: "public",
                        principalTable: "orderStatus",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "product",
                schema: "public",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "text", nullable: false),
                    cost = table.Column<int>(type: "integer", nullable: false),
                    weight = table.Column<int>(type: "integer", nullable: false),
                    about = table.Column<string>(type: "text", nullable: true),
                    descr = table.Column<string>(type: "text", nullable: false),
                    productTypeId = table.Column<short>(type: "smallint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_product", x => x.id);
                    table.ForeignKey(
                        name: "FK_product_productType_productTypeId",
                        column: x => x.productTypeId,
                        principalSchema: "public",
                        principalTable: "productType",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "toping",
                schema: "public",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ingredientId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_toping", x => x.id);
                    table.ForeignKey(
                        name: "FK_toping_ingredient_ingredientId",
                        column: x => x.ingredientId,
                        principalSchema: "public",
                        principalTable: "ingredient",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "comboProduct",
                schema: "public",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false),
                    photo = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_comboProduct", x => x.id);
                    table.ForeignKey(
                        name: "FK_comboProduct_product_id",
                        column: x => x.id,
                        principalSchema: "public",
                        principalTable: "product",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "customProduct",
                schema: "public",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false),
                    ingredientHash = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_customProduct", x => x.id);
                    table.ForeignKey(
                        name: "FK_customProduct_product_id",
                        column: x => x.id,
                        principalSchema: "public",
                        principalTable: "product",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "defaultProduct",
                schema: "public",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false),
                    photo = table.Column<string>(type: "text", nullable: false),
                    bannerTypeId = table.Column<short>(type: "smallint", nullable: false),
                    sizeProductId = table.Column<short>(type: "smallint", nullable: false),
                    isAvailible = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_defaultProduct", x => x.id);
                    table.ForeignKey(
                        name: "FK_defaultProduct_bannerType_bannerTypeId",
                        column: x => x.bannerTypeId,
                        principalSchema: "public",
                        principalTable: "bannerType",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_defaultProduct_productSize_sizeProductId",
                        column: x => x.sizeProductId,
                        principalSchema: "public",
                        principalTable: "productSize",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_defaultProduct_product_id",
                        column: x => x.id,
                        principalSchema: "public",
                        principalTable: "product",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "order_product",
                schema: "public",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    orderId = table.Column<int>(type: "integer", nullable: false),
                    productId = table.Column<int>(type: "integer", nullable: false),
                    count = table.Column<int>(type: "integer", nullable: false),
                    cost = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_order_product", x => x.id);
                    table.ForeignKey(
                        name: "FK_order_product_product_productId",
                        column: x => x.productId,
                        principalSchema: "public",
                        principalTable: "product",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_order_product_userOrder_orderId",
                        column: x => x.orderId,
                        principalSchema: "public",
                        principalTable: "userOrder",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "twoHalfPizza",
                schema: "public",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false),
                    firstPizzaId = table.Column<int>(type: "integer", nullable: false),
                    secondPizzaId = table.Column<int>(type: "integer", nullable: false),
                    twoHalfPizzaHash = table.Column<string>(type: "text", nullable: false),
                    sizeProductId = table.Column<short>(type: "smallint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_twoHalfPizza", x => x.id);
                    table.ForeignKey(
                        name: "FK_twoHalfPizza_productSize_sizeProductId",
                        column: x => x.sizeProductId,
                        principalSchema: "public",
                        principalTable: "productSize",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_twoHalfPizza_product_id",
                        column: x => x.id,
                        principalSchema: "public",
                        principalTable: "product",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ingredient_customProduct",
                schema: "public",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ingredientId = table.Column<int>(type: "integer", nullable: false),
                    customProductId = table.Column<int>(type: "integer", nullable: false),
                    cost = table.Column<int>(type: "integer", nullable: false),
                    count = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ingredient_customProduct", x => x.id);
                    table.ForeignKey(
                        name: "FK_ingredient_customProduct_customProduct_customProductId",
                        column: x => x.customProductId,
                        principalSchema: "public",
                        principalTable: "customProduct",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ingredient_customProduct_ingredient_ingredientId",
                        column: x => x.ingredientId,
                        principalSchema: "public",
                        principalTable: "ingredient",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "defaultProduct_comboProduct",
                schema: "public",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    defaultProductId = table.Column<int>(type: "integer", nullable: false),
                    comboProductId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_defaultProduct_comboProduct", x => x.id);
                    table.ForeignKey(
                        name: "FK_defaultProduct_comboProduct_comboProduct_comboProductId",
                        column: x => x.comboProductId,
                        principalSchema: "public",
                        principalTable: "comboProduct",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_defaultProduct_comboProduct_defaultProduct_defaultProductId",
                        column: x => x.defaultProductId,
                        principalSchema: "public",
                        principalTable: "defaultProduct",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "product_toping",
                schema: "public",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    order_productId = table.Column<int>(type: "integer", nullable: false),
                    topingId = table.Column<int>(type: "integer", nullable: false),
                    count = table.Column<int>(type: "integer", nullable: false),
                    cost = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_product_toping", x => x.id);
                    table.ForeignKey(
                        name: "FK_product_toping_order_product_order_productId",
                        column: x => x.order_productId,
                        principalSchema: "public",
                        principalTable: "order_product",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_product_toping_toping_topingId",
                        column: x => x.topingId,
                        principalSchema: "public",
                        principalTable: "toping",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                schema: "public",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                schema: "public",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                schema: "public",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                schema: "public",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                schema: "public",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                schema: "public",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                schema: "public",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_defaultProduct_bannerTypeId",
                schema: "public",
                table: "defaultProduct",
                column: "bannerTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_defaultProduct_sizeProductId",
                schema: "public",
                table: "defaultProduct",
                column: "sizeProductId");

            migrationBuilder.CreateIndex(
                name: "IX_defaultProduct_comboProduct_comboProductId",
                schema: "public",
                table: "defaultProduct_comboProduct",
                column: "comboProductId");

            migrationBuilder.CreateIndex(
                name: "IX_defaultProduct_comboProduct_defaultProductId",
                schema: "public",
                table: "defaultProduct_comboProduct",
                column: "defaultProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ingredient_ingredientTypeId",
                schema: "public",
                table: "ingredient",
                column: "ingredientTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_ingredient_customProduct_customProductId",
                schema: "public",
                table: "ingredient_customProduct",
                column: "customProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ingredient_customProduct_ingredientId",
                schema: "public",
                table: "ingredient_customProduct",
                column: "ingredientId");

            migrationBuilder.CreateIndex(
                name: "IX_order_product_orderId",
                schema: "public",
                table: "order_product",
                column: "orderId");

            migrationBuilder.CreateIndex(
                name: "IX_order_product_productId",
                schema: "public",
                table: "order_product",
                column: "productId");

            migrationBuilder.CreateIndex(
                name: "IX_product_productTypeId",
                schema: "public",
                table: "product",
                column: "productTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_product_toping_order_productId",
                schema: "public",
                table: "product_toping",
                column: "order_productId");

            migrationBuilder.CreateIndex(
                name: "IX_product_toping_topingId",
                schema: "public",
                table: "product_toping",
                column: "topingId");

            migrationBuilder.CreateIndex(
                name: "IX_toping_ingredientId",
                schema: "public",
                table: "toping",
                column: "ingredientId");

            migrationBuilder.CreateIndex(
                name: "IX_twoHalfPizza_sizeProductId",
                schema: "public",
                table: "twoHalfPizza",
                column: "sizeProductId");

            migrationBuilder.CreateIndex(
                name: "IX_user_coupon_couponId",
                schema: "public",
                table: "user_coupon",
                column: "couponId");

            migrationBuilder.CreateIndex(
                name: "IX_user_coupon_userId",
                schema: "public",
                table: "user_coupon",
                column: "userId");

            migrationBuilder.CreateIndex(
                name: "IX_userOrder_couponId",
                schema: "public",
                table: "userOrder",
                column: "couponId");

            migrationBuilder.CreateIndex(
                name: "IX_userOrder_courierId",
                schema: "public",
                table: "userOrder",
                column: "courierId");

            migrationBuilder.CreateIndex(
                name: "IX_userOrder_statusId",
                schema: "public",
                table: "userOrder",
                column: "statusId");

            migrationBuilder.CreateIndex(
                name: "IX_userOrder_userId",
                schema: "public",
                table: "userOrder",
                column: "userId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims",
                schema: "public");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims",
                schema: "public");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins",
                schema: "public");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles",
                schema: "public");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens",
                schema: "public");

            migrationBuilder.DropTable(
                name: "defaultProduct_comboProduct",
                schema: "public");

            migrationBuilder.DropTable(
                name: "ingredient_customProduct",
                schema: "public");

            migrationBuilder.DropTable(
                name: "product_toping",
                schema: "public");

            migrationBuilder.DropTable(
                name: "twoHalfPizza",
                schema: "public");

            migrationBuilder.DropTable(
                name: "user_coupon",
                schema: "public");

            migrationBuilder.DropTable(
                name: "AspNetRoles",
                schema: "public");

            migrationBuilder.DropTable(
                name: "comboProduct",
                schema: "public");

            migrationBuilder.DropTable(
                name: "defaultProduct",
                schema: "public");

            migrationBuilder.DropTable(
                name: "customProduct",
                schema: "public");

            migrationBuilder.DropTable(
                name: "order_product",
                schema: "public");

            migrationBuilder.DropTable(
                name: "toping",
                schema: "public");

            migrationBuilder.DropTable(
                name: "bannerType",
                schema: "public");

            migrationBuilder.DropTable(
                name: "productSize",
                schema: "public");

            migrationBuilder.DropTable(
                name: "product",
                schema: "public");

            migrationBuilder.DropTable(
                name: "userOrder",
                schema: "public");

            migrationBuilder.DropTable(
                name: "ingredient",
                schema: "public");

            migrationBuilder.DropTable(
                name: "productType",
                schema: "public");

            migrationBuilder.DropTable(
                name: "AspNetUsers",
                schema: "public");

            migrationBuilder.DropTable(
                name: "coupon",
                schema: "public");

            migrationBuilder.DropTable(
                name: "courier",
                schema: "public");

            migrationBuilder.DropTable(
                name: "orderStatus",
                schema: "public");

            migrationBuilder.DropTable(
                name: "ingredientType",
                schema: "public");
        }
    }
}
