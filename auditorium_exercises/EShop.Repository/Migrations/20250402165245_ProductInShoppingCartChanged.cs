using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EShop.Repository.Migrations
{
    /// <inheritdoc />
    public partial class ProductInShoppingCartChanged : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductsInShoppingCarts_Products_ProductId1",
                table: "ProductsInShoppingCarts");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductsInShoppingCarts_ShoppingCarts_ShoppingCartId1",
                table: "ProductsInShoppingCarts");

            migrationBuilder.DropIndex(
                name: "IX_ProductsInShoppingCarts_ProductId1",
                table: "ProductsInShoppingCarts");

            migrationBuilder.DropIndex(
                name: "IX_ProductsInShoppingCarts_ShoppingCartId1",
                table: "ProductsInShoppingCarts");

            migrationBuilder.DropColumn(
                name: "ProductId1",
                table: "ProductsInShoppingCarts");

            migrationBuilder.DropColumn(
                name: "ShoppingCartId1",
                table: "ProductsInShoppingCarts");

            migrationBuilder.AlterColumn<Guid>(
                name: "ShoppingCartId",
                table: "ProductsInShoppingCarts",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "ProductId",
                table: "ProductsInShoppingCarts",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "AspNetUserTokens",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(128)",
                oldMaxLength: 128);

            migrationBuilder.AlterColumn<string>(
                name: "LoginProvider",
                table: "AspNetUserTokens",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(128)",
                oldMaxLength: 128);

            migrationBuilder.AlterColumn<string>(
                name: "ProviderKey",
                table: "AspNetUserLogins",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(128)",
                oldMaxLength: 128);

            migrationBuilder.AlterColumn<string>(
                name: "LoginProvider",
                table: "AspNetUserLogins",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(128)",
                oldMaxLength: 128);

            migrationBuilder.CreateIndex(
                name: "IX_ProductsInShoppingCarts_ProductId",
                table: "ProductsInShoppingCarts",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductsInShoppingCarts_ShoppingCartId",
                table: "ProductsInShoppingCarts",
                column: "ShoppingCartId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductsInShoppingCarts_Products_ProductId",
                table: "ProductsInShoppingCarts",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductsInShoppingCarts_ShoppingCarts_ShoppingCartId",
                table: "ProductsInShoppingCarts",
                column: "ShoppingCartId",
                principalTable: "ShoppingCarts",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductsInShoppingCarts_Products_ProductId",
                table: "ProductsInShoppingCarts");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductsInShoppingCarts_ShoppingCarts_ShoppingCartId",
                table: "ProductsInShoppingCarts");

            migrationBuilder.DropIndex(
                name: "IX_ProductsInShoppingCarts_ProductId",
                table: "ProductsInShoppingCarts");

            migrationBuilder.DropIndex(
                name: "IX_ProductsInShoppingCarts_ShoppingCartId",
                table: "ProductsInShoppingCarts");

            migrationBuilder.AlterColumn<string>(
                name: "ShoppingCartId",
                table: "ProductsInShoppingCarts",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ProductId",
                table: "ProductsInShoppingCarts",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ProductId1",
                table: "ProductsInShoppingCarts",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ShoppingCartId1",
                table: "ProductsInShoppingCarts",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "AspNetUserTokens",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "LoginProvider",
                table: "AspNetUserTokens",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "ProviderKey",
                table: "AspNetUserLogins",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "LoginProvider",
                table: "AspNetUserLogins",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.CreateIndex(
                name: "IX_ProductsInShoppingCarts_ProductId1",
                table: "ProductsInShoppingCarts",
                column: "ProductId1");

            migrationBuilder.CreateIndex(
                name: "IX_ProductsInShoppingCarts_ShoppingCartId1",
                table: "ProductsInShoppingCarts",
                column: "ShoppingCartId1");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductsInShoppingCarts_Products_ProductId1",
                table: "ProductsInShoppingCarts",
                column: "ProductId1",
                principalTable: "Products",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductsInShoppingCarts_ShoppingCarts_ShoppingCartId1",
                table: "ProductsInShoppingCarts",
                column: "ShoppingCartId1",
                principalTable: "ShoppingCarts",
                principalColumn: "Id");
        }
    }
}
