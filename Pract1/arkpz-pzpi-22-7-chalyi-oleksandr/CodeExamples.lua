------------------------------------------------------------------------------------------------------------
-- Перший приклад - загальний
 
--[[
    Функція для обчислення факторіалу числа
    Використовує рекурсивний підхід.
    Параметри:
        n (number): Число, для якого обчислюється факторіал.
    Повертає:
        number: Факторіал числа n.
]]

local function calculateFactorial(n)
    -- Перевірка вхідного значення
    if type(n) ~= "number" or n < 0 then
        error("Input must be a non-negative number.")
    end

    -- Базовий випадок
    if n == 0 or n == 1 then
        return 1
    end

    -- Рекурсивний виклик
    return n * calculateFactorial(n - 1)
end

--[[
    Приклад використання функції
]]
local number = 5 -- Змінна для зберігання числа
local result = calculateFactorial(number) -- Виклик функції
print("Factorial of " .. number .. " is " .. result) -- Виведення результату

------------------------------------------------------------------------------------------------------------
-- Другий приклад - іменування, коментарі, відступи

-- Перевіряє, чи є сьогодні вихідний день
-- Пояснення: Функція перевіряє, чи поточний день субота (7) або неділя (1)
local function is_weekend(day)
    return day == 7 or day == 1 -- Якщо день = 7 (субота) або 1 (неділя)
end

-- Виводить привітання залежно від часу доби
-- Пояснення: якщо час менший за 12, то це ранок, інакше — день
local function greet_by_time(hour)
    if hour < 12 then
        print("Доброго ранку!")
    else
        print("Гарного дня!")
    end
end

-- Основна функція
-- Пояснення: отримує поточну дату, перевіряє, чи є сьогодні вихідний
local function main()
    local current_date = os.date("*t") -- Отримуємо поточну дату і час

    -- Перевіряє, чи є сьогодні вихідний день
    if is_weekend(current_date.wday) then
        print("Сьогодні вихідний день")
    else
        print("Сьогодні робочий день")
    end

    -- Виводить привітання залежно від часу доби
    greet_by_time(current_date.hour)
end

main()

------------------------------------------------------------------------------------------------------------
-- Третій приклад - документування

--[[ 
  Функція для обчислення площі кола
  @param radius Розмір радіуса
  @return Площа кола
]]
function calculateArea(radius)
    return math.pi * radius * radius
end

------------------------------------------------------------------------------------------------------------
-- Четвертий приклад - приклад гарного коду 

--[[
    Функція для обчислення площі кола
    @param radius Розмір радіуса кола
    @return Площа кола
]]

function calculateCircleArea(radius)
    -- Перевіряємо, чи є радіус додатнім числом
    if radius <= 0 then
        return "Невірний радіус"
    end
    -- Обчислюємо площу за формулою
    return math.pi * radius * radius
end

-- Приклад використання функції:
local area = calculateCircleArea(5)
print("Площа кола:", area)

------------------------------------------------------------------------------------------------------------
-- П'ятий приклад - приклад поганого коду 

function a(r)
    if r <= 0 then
        return "Invalid"
    end
    return 3.14 * r * r
end

local res = a(5)
print(res)
