import mysql.connector
from mysql.connector import Error

def connect_to_database():
    """Функция для подключения к MySQL, вывода таблиц, данных и поиска книг по цене."""
    try:
        conn = mysql.connector.connect(
            host="localhost",
            user="root",
            passwd="root",
            database="market" 
        )

        if conn.is_connected():
            print("Успешное подключение к базе")

            cursor = conn.cursor()

            cursor.execute("SHOW TABLES;")
            tables = cursor.fetchall()

            print("\nТаблицы в БД:")
            for table in tables:
                print(f" - {table[0]}")


            table_name = "book"  # Указать таблицу

            cursor.execute(f"SELECT id, author_id, title, genre, price, weight, year_publication, pages FROM {table_name}")
            results = cursor.fetchall()

            print("\nСписок книг:")
            for row in results:
                print(f"Название: {row[0]}, Цена: {row[1]} руб.")

            max_price = float(input("\nВведите максимальную цену книги: "))

            cursor.execute(f"SELECT title, price FROM {table_name} WHERE price < %s", (max_price,))
            filtered_books = cursor.fetchall()

            print("\nКниги дешевле указанной цены:")
            if filtered_books:
                for book in filtered_books:
                    print(f"Название: {book[0]}, Цена: {book[1]} руб.")
            else:
                print("Нет книг с ценой ниже указанной.")

            cursor.close()

    except Error as e:
        print(f"Ошибка подключения к базе: {e}")

    finally:
        if 'conn' in locals() and conn.is_connected():
            conn.close()
            print("\nСоединение с базой закрыто.")

if __name__ == "__main__":
    connect_to_database()
