

-- 2. Заполнение таблицы manufacturer (производители)
INSERT INTO manufacturer (name, country, founded_year) VALUES 
    ('Toyota', 'Япония', 1937),
    ('Volkswagen', 'Германия', 1937),
    ('Ford', 'США', 1903),
    ('BMW', 'Германия', 1916),
    ('Mercedes-Benz', 'Германия', 1926)
ON CONFLICT DO NOTHING;

-- 3. Заполнение таблицы vehicle_category (категории транспорта)
INSERT INTO vehicle_category (name) VALUES 
    ('Легковой'),
    ('Грузовой'),
    ('Автобус'),
    ('Мотоцикл')
ON CONFLICT (name) DO NOTHING;

-- 4. Заполнение таблицы fuel_type (типы топлива)
INSERT INTO fuel_type (name) VALUES 
    ('Бензин'),
    ('Дизель'),
    ('Электричество'),
    ('Гибрид'),
    ('Газ')
ON CONFLICT (name) DO NOTHING;

-- 5. Заполнение таблицы color (цвета)
INSERT INTO color (name, hex_code) VALUES 
    ('Белый', '#FFFFFF'),
    ('Черный', '#000000'),
    ('Серый', '#808080'),
    ('Красный', '#FF0000'),
    ('Синий', '#0000FF'),
    ('Зеленый', '#008000'),
    ('Желтый', '#FFFF00'),
    ('Серебристый', '#C0C0C0')
ON CONFLICT (name) DO NOTHING;

-- 6. Заполнение таблицы vehicle_model (модели транспорта)
-- ВАЖНО: Сначала убедитесь, что есть записи в manufacturer, vehicle_category и fuel_type
INSERT INTO vehicle_model (name, manufacturer_id, vehicle_category_id, power, fuel_type_id, load_capacity, co2_emissions) VALUES 
    ('Camry', 1, 1, 180, 1, 500, 145.50),
    ('Corolla', 1, 1, 140, 1, 450, 125.30),
    ('Golf', 2, 1, 150, 1, 400, 130.00),
    ('Focus', 3, 1, 125, 1, 420, 128.50),
    ('X5', 4, 1, 340, 2, 650, 210.00),
    ('C-Class', 5, 1, 250, 2, 520, 165.00)
ON CONFLICT DO NOTHING;



-- Проверка заполнения (можно выполнить для проверки)
-- SELECT COUNT(*) FROM role;
-- SELECT COUNT(*) FROM manufacturer;
-- SELECT COUNT(*) FROM vehicle_category;
-- SELECT COUNT(*) FROM fuel_type;
-- SELECT COUNT(*) FROM color;
-- SELECT COUNT(*) FROM vehicle_model;
-- SELECT COUNT(*) FROM maintenance_type;

