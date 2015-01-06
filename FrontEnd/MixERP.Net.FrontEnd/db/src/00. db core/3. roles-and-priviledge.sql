DO
$$
BEGIN
    IF NOT EXISTS (SELECT * FROM pg_catalog.pg_user WHERE  usename = 'mix_erp') THEN
        CREATE ROLE mix_erp WITH LOGIN PASSWORD 'change-on-deployment';
    ELSE
        ALTER ROLE mix_erp WITH PASSWORD 'change-on-deployment';    
    END IF;

    COMMENT ON ROLE mix_erp IS 'The default user for MixERP databases.';

    REVOKE ALL ON SCHEMA audit FROM public;
    REVOKE ALL ON SCHEMA core FROM public;
    REVOKE ALL ON SCHEMA office FROM public;
    REVOKE ALL ON SCHEMA policy FROM public;
    REVOKE ALL ON SCHEMA transactions FROM public;
    REVOKE ALL ON SCHEMA crm FROM public;
    REVOKE ALL ON SCHEMA mrp FROM public;
    
    GRANT USAGE ON SCHEMA public TO mix_erp;
    GRANT USAGE ON SCHEMA information_schema TO mix_erp;
    GRANT USAGE ON SCHEMA audit TO mix_erp;
    GRANT USAGE ON SCHEMA core TO mix_erp;
    GRANT USAGE ON SCHEMA office TO mix_erp;
    GRANT USAGE ON SCHEMA policy TO mix_erp;
    GRANT USAGE ON SCHEMA transactions TO mix_erp;
    GRANT USAGE ON SCHEMA crm TO mix_erp;
    GRANT USAGE ON SCHEMA mrp TO mix_erp;

    ALTER DEFAULT PRIVILEGES IN SCHEMA public GRANT SELECT, INSERT, UPDATE, DELETE ON TABLES TO mix_erp;
    ALTER DEFAULT PRIVILEGES IN SCHEMA information_schema GRANT SELECT ON TABLES TO mix_erp;
    ALTER DEFAULT PRIVILEGES IN SCHEMA audit GRANT SELECT, INSERT, UPDATE, DELETE ON TABLES TO mix_erp;
    ALTER DEFAULT PRIVILEGES IN SCHEMA core GRANT SELECT, INSERT, UPDATE, DELETE ON TABLES TO mix_erp;
    ALTER DEFAULT PRIVILEGES IN SCHEMA office GRANT SELECT, INSERT, UPDATE, DELETE ON TABLES TO mix_erp;
    ALTER DEFAULT PRIVILEGES IN SCHEMA policy GRANT SELECT, INSERT, UPDATE, DELETE ON TABLES TO mix_erp;
    ALTER DEFAULT PRIVILEGES IN SCHEMA transactions GRANT SELECT, INSERT, UPDATE, DELETE ON TABLES TO mix_erp;
    ALTER DEFAULT PRIVILEGES IN SCHEMA crm GRANT SELECT, INSERT, UPDATE, DELETE ON TABLES TO mix_erp;
    ALTER DEFAULT PRIVILEGES IN SCHEMA mrp GRANT SELECT, INSERT, UPDATE, DELETE ON TABLES TO mix_erp;

    ALTER DEFAULT PRIVILEGES IN SCHEMA public GRANT ALL ON SEQUENCES TO mix_erp;
    ALTER DEFAULT PRIVILEGES IN SCHEMA audit GRANT ALL ON SEQUENCES TO mix_erp;
    ALTER DEFAULT PRIVILEGES IN SCHEMA core GRANT ALL ON SEQUENCES TO mix_erp;
    ALTER DEFAULT PRIVILEGES IN SCHEMA office GRANT ALL ON SEQUENCES TO mix_erp;
    ALTER DEFAULT PRIVILEGES IN SCHEMA policy GRANT ALL ON SEQUENCES TO mix_erp;
    ALTER DEFAULT PRIVILEGES IN SCHEMA transactions GRANT ALL ON SEQUENCES TO mix_erp;
    ALTER DEFAULT PRIVILEGES IN SCHEMA crm GRANT ALL ON SEQUENCES TO mix_erp;
    ALTER DEFAULT PRIVILEGES IN SCHEMA mrp GRANT ALL ON SEQUENCES TO mix_erp;




    ALTER DEFAULT PRIVILEGES IN SCHEMA public GRANT EXECUTE ON FUNCTIONS TO mix_erp;
    ALTER DEFAULT PRIVILEGES IN SCHEMA information_schema GRANT EXECUTE ON FUNCTIONS TO mix_erp;
    ALTER DEFAULT PRIVILEGES IN SCHEMA audit GRANT EXECUTE ON FUNCTIONS TO mix_erp;
    ALTER DEFAULT PRIVILEGES IN SCHEMA core GRANT EXECUTE ON FUNCTIONS TO mix_erp;
    ALTER DEFAULT PRIVILEGES IN SCHEMA office GRANT EXECUTE ON FUNCTIONS TO mix_erp;
    ALTER DEFAULT PRIVILEGES IN SCHEMA policy GRANT EXECUTE ON FUNCTIONS TO mix_erp;
    ALTER DEFAULT PRIVILEGES IN SCHEMA transactions GRANT EXECUTE ON FUNCTIONS TO mix_erp;
    ALTER DEFAULT PRIVILEGES IN SCHEMA crm GRANT EXECUTE ON FUNCTIONS TO mix_erp;
    ALTER DEFAULT PRIVILEGES IN SCHEMA mrp GRANT EXECUTE ON FUNCTIONS TO mix_erp;
   
END
$$
LANGUAGE plpgsql;




DO
$$
BEGIN
    IF NOT EXISTS (SELECT * FROM pg_catalog.pg_user WHERE  usename = 'report_user') THEN
        CREATE ROLE report_user WITH LOGIN PASSWORD 'change-on-deployment';
    ELSE
        ALTER ROLE report_user WITH PASSWORD 'change-on-deployment';    
    END IF;

    COMMENT ON ROLE report_user IS 'This user account should be used by the Reporting Engine to run ad-hoc queries.
    It is strictly advised for this user to have a read-only access to the database.';

    GRANT USAGE ON SCHEMA public TO report_user;
    GRANT USAGE ON SCHEMA information_schema TO report_user;
    GRANT USAGE ON SCHEMA audit TO report_user;
    GRANT USAGE ON SCHEMA core TO report_user;
    GRANT USAGE ON SCHEMA office TO report_user;
    GRANT USAGE ON SCHEMA policy TO report_user;
    GRANT USAGE ON SCHEMA transactions TO report_user;
    GRANT USAGE ON SCHEMA crm TO report_user;
    GRANT USAGE ON SCHEMA mrp TO report_user;

    ALTER DEFAULT PRIVILEGES IN SCHEMA public GRANT SELECT ON TABLES TO report_user;
    ALTER DEFAULT PRIVILEGES IN SCHEMA information_schema GRANT SELECT ON TABLES TO report_user;
    ALTER DEFAULT PRIVILEGES IN SCHEMA audit GRANT SELECT ON TABLES TO report_user;
    ALTER DEFAULT PRIVILEGES IN SCHEMA core GRANT SELECT ON TABLES TO report_user;
    ALTER DEFAULT PRIVILEGES IN SCHEMA office GRANT SELECT ON TABLES TO report_user;
    ALTER DEFAULT PRIVILEGES IN SCHEMA policy GRANT SELECT ON TABLES TO report_user;
    ALTER DEFAULT PRIVILEGES IN SCHEMA transactions GRANT SELECT ON TABLES TO report_user;
    ALTER DEFAULT PRIVILEGES IN SCHEMA crm GRANT SELECT ON TABLES TO report_user;
    ALTER DEFAULT PRIVILEGES IN SCHEMA mrp GRANT SELECT ON TABLES TO report_user;


    ALTER DEFAULT PRIVILEGES IN SCHEMA public GRANT EXECUTE ON FUNCTIONS TO report_user;
    ALTER DEFAULT PRIVILEGES IN SCHEMA information_schema GRANT EXECUTE ON FUNCTIONS TO report_user;
    ALTER DEFAULT PRIVILEGES IN SCHEMA audit GRANT EXECUTE ON FUNCTIONS TO report_user;
    ALTER DEFAULT PRIVILEGES IN SCHEMA core GRANT EXECUTE ON FUNCTIONS TO report_user;
    ALTER DEFAULT PRIVILEGES IN SCHEMA office GRANT EXECUTE ON FUNCTIONS TO report_user;
    ALTER DEFAULT PRIVILEGES IN SCHEMA policy GRANT EXECUTE ON FUNCTIONS TO report_user;
    ALTER DEFAULT PRIVILEGES IN SCHEMA transactions GRANT EXECUTE ON FUNCTIONS TO report_user;
    ALTER DEFAULT PRIVILEGES IN SCHEMA crm GRANT EXECUTE ON FUNCTIONS TO report_user;
    ALTER DEFAULT PRIVILEGES IN SCHEMA mrp GRANT EXECUTE ON FUNCTIONS TO report_user;
END
$$
LANGUAGE plpgsql;

