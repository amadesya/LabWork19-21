import mysql.connector
from mysql.connector import Error

def connect_to_database():
    """Функция для подключения к MySQL и выполнения простого запроса."""
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

            print("Таблицы в БД:")
            for table in tables:
                print(f" - {table[0]}")

            cursor.close()

    except Error as e:
        print(f"Ошибка подключения к базе: {e}")

    finally:
        if 'conn' in locals() and conn.is_connected():
            conn.close()
            print("Соединение с базой закрыто")

if __name__ == "__main__":
    connect_to_database()
