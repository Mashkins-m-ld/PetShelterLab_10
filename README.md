---

## **Интерфейс взаимодействия с формой**

### **Описание**
Проект **PetShelter** предоставляет удобный интерфейс для управления данными о питомцах в приютах. Основная форма позволяет фильтровать и отображать информацию о животных, а также добавлять, удалять и редактировать записи. Взаимодействие с данными осуществляется через интуитивно понятные элементы управления, такие как выпадающие списки, чекбоксы и кнопки.

---

### **Основные элементы интерфейса**

1. **Выпадающие списки (`ComboBox`)**:
   - **Тип животного**: Позволяет выбрать категорию питомца (Все питомцы, Кошки, Собаки, Кролики).
   - **Приют**: Фильтрация по названию приюта (Все приюты, Братья наши меньшие, Поставьте фулл балл пожалуйста, Доброе сердце).
   - **Формат данных**: Выбор формата сохранения данных (JSON, XML).

2. **Чекбокс (`CheckBox`)**:
   - **Только приюты с открытым участком**: Фильтрация приютов по наличию открытой территории.

3. **Кнопка (`Button`)**:
   - **Поиск**: Запуск фильтрации данных на основе выбранных параметров.
   - **Добавить**: Открывает форму для добавления нового питомца.
   - **Удалить**: Удаляет выбранные записи о питомцах.

4. **Таблицы (`DataGridView`)**:
   - Отображают информацию о питомцах в удобном табличном формате.
   - Позволяют выбирать записи для удаления с помощью чекбоксов.

5. **Форма добавления питомца**:
   - Поля для ввода данных: Имя, Возраст, Вес, Порода, Окрас, История, Клаустрофобия.
   - Дополнительные поля в зависимости от типа животного:
     - **Кошки**: Приучен к лотку.
     - **Собаки**: Команды.
     - **Кролики**: Без дополнительных полей.
   - Кнопка **Добавить**: Сохранение данных о новом питомце.

---

### **Особенности взаимодействия**

- **Фильтрация данных**:
  - При выборе типа питомца и приюта данные в таблице автоматически обновляются.
  - Чекбокс "Только приюты с открытым участком" учитывает ограничения по клаустрофобии питомцев.

- **Сохранение данных**:
  - Данные о питомцах сохраняются в выбранном формате (JSON или XML).
  - При смене формата все существующие данные автоматически конвертируются.

- **Управление питомцами**:
  - Добавление нового питомца через форму с валидацией данных.
  - Удаление выбранных питомцев с подтверждением действия.

---

### **Пользовательский опыт**

- Простота и интуитивность: Все действия выполняются через понятные элементы управления.
- Гибкость: Возможность фильтрации по разным критериям и выбора формата данных.
- Надежность: Валидация данных при добавлении и удалении записей.

---

📂 **Примечание**: Проект использует классы для хранения данных о питомцах (`Cat`, `Dog`, `Rabbit`) и приютах (`Shelter`), а также сериализацию для сохранения данных в файлы (JSON/XML). 

Наслаждайтесь работой с **PetShelter**! 🐱🐶🐰
