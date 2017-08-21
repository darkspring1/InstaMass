DROP SCHEMA IF EXISTS migrations CASCADE;

CREATE SCHEMA migrations AUTHORIZATION postgres;
CREATE TABLE migrations."history" (number int NOT NULL);
INSERT INTO migrations."history"(number) VALUES (0);

CREATE OR REPLACE FUNCTION migrations.set_migration (migration_num int) RETURNS int AS
$$
DECLARE current_migration int;
BEGIN
	SELECT max(number) INTO current_migration FROM migrations."history";
	--RAISE NOTICE 'current_migration: %', current_migration;
	IF migration_num != current_migration + 1 THEN
		RAISE EXCEPTION 'current migration number: %', current_migration;
	END IF;

	INSERT INTO migrations."history"(number) VALUES (migration_num);
	
	RETURN migration_num; 
END;
$$
LANGUAGE plpgsql;