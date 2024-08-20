CREATE OR REPLACE PROCEDURE GetMedian(
    INOUT V_Median FLOAT)
LANGUAGE plpgsql
AS $$
DECLARE
    total_count INT;
BEGIN
    -- Сначала вычисляем количество строк
    SELECT COUNT(*) INTO total_count FROM "TextLines";
    
    -- Теперь вычисляем медиану
    SELECT
        CASE
            WHEN total_count % 2 = 0 THEN
                -- Если четное количество значений, берем среднее двух средних элементов
                (SELECT AVG("DoubleNum") 
                 FROM (SELECT "DoubleNum"
                       FROM "TextLines"
                       ORDER BY "DoubleNum"
                       LIMIT 2 OFFSET (total_count / 2) - 1) AS subquery)
            ELSE
                -- Если нечетное количество значений, берем средний элемент
                (SELECT "DoubleNum"
                 FROM "TextLines"
                 ORDER BY "DoubleNum"
                 OFFSET total_count / 2 LIMIT 1)
        END INTO V_Median;
END;
$$;