CREATE OR REPLACE PROCEDURE GetSum(INOUT sumOfIntegers BIGINT)
LANGUAGE plpgsql
AS $$
BEGIN
    SELECT 
        SUM(CAST("IntegerNum" as BIGINT))
    INTO 
        sumOfIntegers
    FROM 
        "TextLines";
END;
$$;