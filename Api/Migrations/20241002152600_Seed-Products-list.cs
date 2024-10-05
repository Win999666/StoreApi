using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Api.Migrations
{
    /// <inheritdoc />
    public partial class SeedProductslist : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                table: "Products",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Category", "Description", "Image", "Name", "Price", "SpetialTag" },
                values: new object[,]
                {
                    { 1, "Категория 2", "Соответствующих консультация последовательного существующий настолько.", "https://placehold.co/200", "Практичный Хлопковый Носки", 6022793.00m, "Новинки" },
                    { 2, "Категория 1", "Работы создание занимаемых соответствующих уточнения влечёт целесообразности.", "https://placehold.co/200", "Маленький Резиновый Носки", 5812602.70m, "Рекомендуемый" },
                    { 3, "Категория 1", "Поэтапного проблем настолько роль изменений же играет.", "https://placehold.co/200", "Интеллектуальный Натуральный Ножницы", 7535386.60m, "Рекомендуемый" },
                    { 4, "Категория 3", "Инновационный направлений от прогресса отметить социально-экономическое дальнейших другой.", "https://placehold.co/200", "Маленький Пластиковый Стул", 2707002.47m, "Рекомендуемый" },
                    { 5, "Категория 3", "Высшего занимаемых порядка не целесообразности структура высшего условий за соображения.", "https://placehold.co/200", "Грубый Резиновый Компьютер", 7785961.46m, "Рекомендуемый" },
                    { 6, "Категория 3", "Общества нами также определения позиции принимаемых инновационный.", "https://placehold.co/200", "Невероятный Хлопковый Кошелек", 9317681.31m, "Рекомендуемый" },
                    { 7, "Категория 1", "Вызывает систему высшего повышение формированию предпосылки предпосылки массового соображения от.", "https://placehold.co/200", "Невероятный Стальной Ботинок", 1665923.47m, "Рекомендуемый" },
                    { 8, "Категория 3", "Мира обуславливает обеспечивает опыт постоянное воздействия активом следует место экономической.", "https://placehold.co/200", "Большой Натуральный Берет", 3908655.42m, "Рекомендуемый" },
                    { 9, "Категория 3", "Сфера опыт нами отношении.", "https://placehold.co/200", "Невероятный Меховой Ремень", 2272373.01m, "Рекомендуемый" },
                    { 10, "Категория 1", "Играет формировании процесс забывать технологий высокотехнологичная сложившаяся формирования.", "https://placehold.co/200", "Фантастический Меховой Сабо", 7431981.04m, "Рекомендуемый" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DropColumn(
                name: "Price",
                table: "Products");
        }
    }
}
