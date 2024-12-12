import json
import random

# Загрузка данных из вашего JSON
with open("DatabaseTest.json", "r", encoding="utf-8") as file:
    data = json.load(file)

# Функция для добавления isBusy
def add_is_busy(data):
    # Добавляем isBusy для пользователей
    for user in data.get("Users", []):
        user["isBusy"] = random.randint(False, True)
    
    # Добавляем isBusy для курортов
    for resort in data.get("Resorts", []):
        for room in resort.get("rooms", []):
            room["isBusy"] = random.randint(0, 1)
    
    return data

# Применяем изменения
data = add_is_busy(data)

# Сохраняем изменённый JSON в новый файл
with open("updated_db.json", "w", encoding="utf-8") as file:
    json.dump(data, file, indent=4, ensure_ascii=False)

print("Свойство 'isBusy' добавлено и сохранено в updated_db.json")
