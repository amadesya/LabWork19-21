import mysql.connector
from mysql.connector import Error

def connect_to_database():
    """Подключение к MySQL, выборка, фильтрация и обновление данных."""
    try:
        conn = mysql.connector.connect(
            host="localhost",
            user="root",
            passwd="root",
            database="market"
        )

        if conn.is_connected():
            print("Успешное подключение к базе!")

            cursor = conn.cursor()

            cursor.execute("SHOW TABLES;")
            tables = cursor.fetchall()

            print("\n Таблицы в БД:")
            for table in tables:
                print(f" - {table[0]}")

            # Выбор таблицы
            table_name = "book"

            # Ввести столбцы для вывода
            
            cursor.execute(f"SELECT id, author_id, title, genre, price, weight, year_publication, pages FROM {table_name}")
            results = cursor.fetchall()

            print("\n Список книг:")
            for row in results:
                print(f"ID: {row[0]}, Автор ID: {row[1]}, Название: {row[2]}, Жанр: {row[3]}, Цена: {row[4]} руб., Вес: {row[5]} г., Год: {row[6]}, Страниц: {row[7]}")

            max_price = float(input("\nВведите максимальную цену книги: "))

            cursor.execute(f"SELECT title, price FROM {table_name} WHERE price < %s", (max_price,))
            filtered_books = cursor.fetchall()

            print("\nКниги дешевле указанной цены:")
            if filtered_books:
                for book in filtered_books:
                    print(f"Название: {book[0]}, Цена: {book[1]} руб.")
            else:
                print("Нет книг с ценой ниже указанной.")

            # Изменение цены
            book_id = int(input("\nВведите ID книги, цену которой хотите изменить: "))
            new_price = float(input("Введите новую цену: "))

            cursor.execute(f"UPDATE {table_name} SET price = %s WHERE id = %s", (new_price, book_id))
            conn.commit()  

            print("\n Цена книги успешно обновлена!")

            # Вывод новых данных
            cursor.execute(f"SELECT id, title, price FROM {table_name} WHERE id = %s", (book_id,))
            updated_book = cursor.fetchone()

            if updated_book:
                print(f"\n Обновлённые данные книги: ID: {updated_book[0]}, Название: {updated_book[1]}, Новая цена: {updated_book[2]} руб.")
            else:
                print(" Книга с указанным ID не найдена.")

            cursor.close()

    except Error as e:
        print(f" Ошибка подключения к базе: {e}")

    finally:
        if 'conn' in locals() and conn.is_connected():
            conn.close()
            print("\n Соединение с базой закрыто.")

if __name__ == "__main__":
    connect_to_database()
