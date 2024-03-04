
DO $$ 
    BEGIN

    -- create scheme
    CREATE SCHEMA IF NOT EXISTS classifierprototypeservice;
    COMMENT ON SCHEMA classifierprototypeservice IS 'Schema for classifier prototype';

    END;
$$